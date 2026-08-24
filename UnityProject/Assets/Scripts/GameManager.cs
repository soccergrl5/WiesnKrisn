using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using WiesnKrisn.Movement;
using WiesnKrisn.Roles;
using WiesnKrisn.UI;

namespace WiesnKrisn
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance {get; private set;}

        private static readonly Dictionary<Games, float> AttractionPrizes = new Dictionary<Games, float>()
        {
            { Games.Dosenwerfen , 6f},
            { Games.Autoscooter , 4f},
            { Games.RollerCoaster , 13f},
            { Games.GhostTrain , 10f},
            { Games.WireGame, 0f},
            { Games.FerrisWheel , 12f},
            { Games.Greifautomat , 2f}
        };
        private const float BeerPrize   = 18f;
        private const float AperolPrize = 12f;

        private float _camPosition = -75f;
        private int _bookPage      = 0;
        private float _money       = 120f;
        private float _drunkOMeter = 0f;

        private bool _easyMode;
        private const float EasyDrunkTime = 3f;
        private const float HardDrunkTime = 9f;
        
        private void Awake()
        {
            Instance = this;
            
            DontDestroyOnLoad(gameObject);
        }

        public void StartGame(bool easyMode)
        {
            _easyMode = easyMode;
            
            SceneManager.LoadScene("OutdoorAreaScene");
            
            RoleDistribution.Instance.SelectMainSuspectAndLover();
            
            if (easyMode)
                RoleDistribution.Instance.DistributionEasyMode();
            else
                RoleDistribution.Instance.DistributionHardMode();
        }

        public void RestartGame()
        {
            _camPosition = -75f;
            _bookPage    = 0;
            _money       = 120f;
            _drunkOMeter = 0f;
            
            StartGame(_easyMode);
        }

        public void ChangeLocation(string scene)
        {
            SaveCurrentBookPage();
            
            if (scene == "OutdoorAreaScene")
            {
                SceneManager.LoadScene("OutdoorAreaScene");
                return;
            }
            
            SaveCamPosition();
            SceneManager.LoadScene(scene);
        }

        public void StartMiniGame(Games game)
        {
            SaveCamPosition();
            SaveCurrentBookPage();

            switch (game)
            {
                case Games.Dosenwerfen:
                    _money -= AttractionPrizes[Games.Dosenwerfen];
                    SceneManager.LoadScene("DosenwerfenScene");
                    break;
                
                case Games.Autoscooter:
                    _money -= AttractionPrizes[Games.Autoscooter];
                    SceneManager.LoadScene("SampleScene");
                    break;
                
                case Games.RollerCoaster:
                    _money -= AttractionPrizes[Games.RollerCoaster];
                    SceneManager.LoadScene("SampleScene");
                    break;
                
                case Games.GhostTrain:
                    _money -= AttractionPrizes[Games.GhostTrain];
                    SceneManager.LoadScene("SampleScene");
                    break;
                
                case Games.WireGame:
                    SceneManager.LoadScene("SampleScene");
                    break;
                
                case Games.FerrisWheel:
                    _money -= AttractionPrizes[Games.FerrisWheel];
                    SceneManager.LoadScene("SampleScene");
                    break;
                
                case Games.Greifautomat:
                    _money -= AttractionPrizes[Games.Greifautomat];
                    SceneManager.LoadScene("SampleScene");
                    break;
            }
        }

        public void ReplayMiniGame(Games game) => _money -= AttractionPrizes[game];

        private void SaveCamPosition()
        {
            _camPosition = CameraMovement.Instance.GetPos();
        }

        private void SaveCurrentBookPage()
        {
            _bookPage = DetectiveBookUI.Instance.GetCurrentPage();
        }

        public void BuyBeer()
        {
            _money -= BeerPrize;
            
            UpdateMoney();
            UpdateDrunkOMeter(0.25f);
        }

        public void BuyAperol()
        {
            _money -= AperolPrize;
            
            UpdateMoney();
            UpdateDrunkOMeter(0.2f);
        }

        private void UpdateDrunkOMeter(float addition)
        {
            _drunkOMeter += addition;
            DrunkOMeterUI.Instance.UpdateValue(_drunkOMeter);

            if (_drunkOMeter >= 1f)
            {
                SceneManager.LoadScene("GameOverDrunk");
                return;
            }

            if (_drunkOMeter > 0f)
            {
                CancelInvoke();
                Invoke(nameof(DecreaseDrunkOMeter), _easyMode ? EasyDrunkTime : HardDrunkTime);
            }
        }

        private void DecreaseDrunkOMeter()
        {
            _drunkOMeter -= 0.01f;

            if (_drunkOMeter <= 0f)
                _drunkOMeter = 0f;
            
            if (DrunkOMeterUI.Instance != null)
            {
                DrunkOMeterUI.Instance.UpdateValue(_drunkOMeter);
            }

            if (_drunkOMeter > 0f)
                Invoke(nameof(DecreaseDrunkOMeter), _easyMode ? EasyDrunkTime : HardDrunkTime);
        }

        private void UpdateMoney()
        {
            if (MoneyUI.Instance != null)
                MoneyUI.Instance.UpdateAmount(_money);
        }
        
        public float GetCamPosition() => _camPosition;
        
        public int GetBookPage() => _bookPage;
        
        public float GetMoney() => _money;
        
        public float GetPrizeOfGame(Games game) => AttractionPrizes[game];
        
        public float GetBeerPrize() => BeerPrize;
        
        public float GetAperolPrize() => AperolPrize;

        public float GetDrunkOMeter() => _drunkOMeter;
        
        public void BackToMainMenu()
        {
            SceneManager.LoadScene("MainMenu");
            Destroy(gameObject);
        }
    }
}