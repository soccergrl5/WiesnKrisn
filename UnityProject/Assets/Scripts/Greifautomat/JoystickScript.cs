using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;
public class JoystickScript: MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{

	private Image _outerCircle;//Outer boundary of joystick.
	private float _bgImageSizeX, _bgImageSizey;
	private Image _innerCircle;//Inner circle of joystick.
	public JoystickDirection joyStickDirection;
	/// <summary>
	/// This defines how far joystick inner circle can move with respect to outer circle.
	/// Since inner circle needs to move only half size distance of outer circle default value is its half size i.e 0.5
	/// </summary>
	float _offsetFactorWithBgSize = 0.5f;
	public static event Action<Vector2> OnJoyStickMoved;

	public Vector2 InputDirection { set; get; }


	private void Start()
	{

		_outerCircle = transform.GetChild(0).GetChild(0).GetComponent<Image>();
		_innerCircle = transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>();
		_bgImageSizeX = _outerCircle.rectTransform.sizeDelta.x;
		_bgImageSizey = _outerCircle.rectTransform.sizeDelta.y;
	}


	/// <summary>
	/// Unity function called when we are tapping and moving finger in screen.
	/// </summary>
	/// <param name="ped"></param>
	public void OnDrag(PointerEventData ped)
	{
		Vector2 tappedpOint;
		//This if statment gives local position of the pointer at "out touchPoint"
		//if we press or touched inside the area of outerCircle
		if (RectTransformUtility.ScreenPointToLocalPointInRectangle
			(_outerCircle.rectTransform, ped.position, ped.pressEventCamera, out tappedpOint))
		{

			//Getting tappedPoint position in fraction where  maxmimum value would be in denominator of below fraction.
			tappedpOint.x = (tappedpOint.x / (_bgImageSizeX * _offsetFactorWithBgSize));
			tappedpOint.y = (tappedpOint.y / (_bgImageSizey * _offsetFactorWithBgSize));

			SetJoyStickDirection(tappedpOint.x, tappedpOint.y);//Updates InputDirection value.
															   //Limit value of InputDirection between 0 and 1.
			InputDirection = InputDirection.magnitude > 1 ? InputDirection.normalized : InputDirection;
			//Updating position of inner circle of joystick.
			_innerCircle.rectTransform.anchoredPosition =
				new Vector3(InputDirection.x * (_outerCircle.rectTransform.sizeDelta.x * _offsetFactorWithBgSize),
					InputDirection.y * (_outerCircle.rectTransform.sizeDelta.y * _offsetFactorWithBgSize));


			OnJoyStickMoved?.Invoke(InputDirection);

		}
	}
	public GameObject joyStickparent;
	/// <summary>
	/// Unity function called when we tapped on the screen.
	/// Here we enable joystick at initial press point.
	/// </summary>
	public virtual void OnPointerDown(PointerEventData ped)
	{

		Vector2 initMousePos =ped.pressEventCamera.ScreenToWorldPoint(Input.mousePosition);
		OnDrag(ped);

	}

	/// <summary>
	/// Unity function called when mouse button is not pressed or no touch detected in touch screen device.
	///Disabling joystick and resetting joystick innerCircle to zero position.
	/// </summary>
	public virtual void OnPointerUp(PointerEventData ped)
	{
		_innerCircle.transform.position = transform.position;
	}

	/// <summary>
	/// Changes input direction value based on joy stick direction we have set.
	/// </summary>
	/// <param name="x"></param>
	/// <param name="y"></param>
	public void SetJoyStickDirection(float x, float y)
	{
		if (joyStickDirection == JoystickDirection.Both)
		{
			// For both horizonatal and vertical directional joystick
			InputDirection = new Vector3(x, y);
		}
		else if (joyStickDirection == JoystickDirection.Vertical)
		{
			//for y directional joystick
			InputDirection = new Vector3(0, y);
		}
		else if (joyStickDirection == JoystickDirection.Horizontal)
		{
			//for x dirctional joystick
			InputDirection = new Vector3(x, 0);
		}
	}

}
public enum JoystickDirection
{
	Horizontal,
	Vertical,
	Both
}
