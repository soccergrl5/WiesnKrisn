using UnityEngine;
using UnityEngine.SceneManagement;
using WiesnKrisn.Roles;

namespace WiesnKrisn
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance {get; private set;}

        private float _camPosition = 0f;

        private void Awake()
        {
            Instance = this;
            
            DontDestroyOnLoad(gameObject);
        }

        public void StartGame(bool easyMode)
        {
            GoToOutdoorArea();
            
            RoleDistribution.Instance.SelectMainSuspectAndLover();
            
            if (easyMode)
                RoleDistribution.Instance.DistributionEasyMode();
            else
                RoleDistribution.Instance.DistributionHardMode();
        }
        
        public void GoToOutdoorArea()
        {
            SceneManager.LoadScene("OutdoorAreaScene");
        }

        public void StartMiniGame(Games game)
        {
            SaveCamPosition();

            switch (game)
            {
                case Games.Dosenwerfen:
                    SceneManager.LoadScene("SampleScene");
                    break;
                
                case Games.Autoscooter:
                    SceneManager.LoadScene("SampleScene");
                    break;
                
                case Games.RollerCoaster:
                    SceneManager.LoadScene("SampleScene");
                    break;
                
                case Games.GhostTrain:
                    SceneManager.LoadScene("SampleScene");
                    break;
                
                case Games.WireGame:
                    SceneManager.LoadScene("SampleScene");
                    break;
                
                case Games.FerrisWheel:
                    SceneManager.LoadScene("SampleScene");
                    break;
                
                case Games.Greifautomat:
                    SceneManager.LoadScene("SampleScene");
                    break;
            }
        }

        private void SaveCamPosition()
        {
            _camPosition = CameraMovement.Instance.GetPos();
        }

        public float GetCamPosition() => _camPosition;
    }
}