using System;
using NUnit.Framework.Constraints;
using UnityEngine;

public class BallScript : MonoBehaviour
{ 
      [SerializeField] private GameObject ball;
      [SerializeField] private float velocityMultiplier = 0.1f;

      [Header("Trajectory Prediction")]
      [SerializeField] private int trajectoryPoints = 30;
      [SerializeField] private float trajectoryTimeStep = 0.05f;
      
      private Vector3 _mouseDistance = Vector3.zero;
      private Vector3 _startingPos = Vector3.zero;
      private Vector3 _velocity = Vector3.zero;

      private bool _ballThrown = false;
      private bool _isAiming = false;
    
      private LineRenderer _lineRenderer;
      private Rigidbody _rigidbody;
      

      public void Awake()
      {
            trajectoryPoints = 30;
            trajectoryTimeStep = 0.05f;
            _mouseDistance = Vector3.zero;
            _startingPos = Vector3.zero;
            _velocity = Vector3.zero;

            _ballThrown = false;
            _isAiming = false;

      }

      void Start()
    {
          _lineRenderer = GetComponent<LineRenderer>();
          _lineRenderer.positionCount = 0;
          _rigidbody = GetComponent<Rigidbody>();
    }
    void Update()
    {
          CalculateMouseDistance();

          if (_isAiming)
          {
                var previewVelocity = GetVelocityFromMouse(Input.mousePosition);
                DrawTrajectory(ball.transform.position, previewVelocity);
          }
          if (_ballThrown)
          {
                ThrowBall();
                _lineRenderer.enabled = false;
                
          }

          if (IsOutOfBounds())
          {
                gameObject.SetActive(false);
                BowlScript.Instance().SetIsBallAlreadyThere(false);
                CountingScript.Instance().AddToAmountOfBallsThrown(1);
          }
    }
    
     /// <summary>
     /// Calculates how far the player drags the mouse when aiming at the target.
     /// This length of the drag path is added to the velocity of the ball
     /// </summary>
    private void CalculateMouseDistance()
    {
          if (Input.GetMouseButtonDown(0))
          {
                _startingPos = Input.mousePosition;
                _isAiming = true;
          }
    
          if (Input.GetMouseButtonUp(0))
          {
                var endPos = Input.mousePosition;
                _mouseDistance = _startingPos - endPos;
                _velocity = _mouseDistance;
                _mouseDistance = Vector3.zero;
                // ballThrown is set to true, because that makes life easier
                _ballThrown = true;
                _isAiming = false;
          }
    }

    private void ThrowBall()
    {
          //Since the mouse moves in 2d but we need 3d coordinates, the y coordinate of the mouse movement is used for z
          var ballrb = ball.GetComponent<Rigidbody>();
          ballrb.linearVelocity = new Vector3(0.1f * _velocity.x, 0.1f *_velocity.y, 0.1f * _velocity.y);
          _ballThrown = false;
    }
    private Vector3 GetVelocityFromMouse(Vector3 currentMousePos)
    {
          var distance = _startingPos - currentMousePos;
          return new Vector3(velocityMultiplier * distance.x, velocityMultiplier * distance.y, velocityMultiplier * distance.y);
    }
    
    /// <summary>
    /// Simulates the ball's trajectory step by step (mirroring Rigidbody physics)
    /// and draws it using the LineRenderer.
    /// </summary>
    private void DrawTrajectory(Vector3 startPos, Vector3 startVelocity)
    {
          _lineRenderer.positionCount = trajectoryPoints;

          Vector3 simPos = startPos;
          Vector3 simVelocity = startVelocity;
          float drag = _rigidbody.linearDamping;

          for (int i = 0; i < trajectoryPoints; i++)
          {
                _lineRenderer.SetPosition(i, simPos);
                
                simVelocity += Physics.gravity * trajectoryTimeStep;
                simVelocity *= (1f - drag * trajectoryTimeStep); 
                simPos += simVelocity * trajectoryTimeStep;
          }
    }

    private bool IsOutOfBounds()
    {
          if (gameObject.transform.position.y < -10)
          {
                return true;
          } 
          return false;
    }
}
