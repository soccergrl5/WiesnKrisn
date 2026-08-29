using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using WiesnKrisn.Audio;
using WiesnKrisn.CandyShop;
using WiesnKrisn.Interactable.Texts;
using WiesnKrisn.Movement;
using WiesnKrisn.Roles;
using WiesnKrisn.UI;

namespace WiesnKrisn
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance {get; private set;}

        private static readonly Dictionary<Witnesses, Games> WitnessGames = new Dictionary<Witnesses, Games>()
        {
            { Witnesses.AutoscooterKid , Games.Autoscooter},
            { Witnesses.KarussellParents , Games.WireGame},
            { Witnesses.KarussellKid , Games.WireGame},
            { Witnesses.Influenci , Games.FerrisWheel},
            { Witnesses.Achterbahni , Games.RollerCoaster},
            { Witnesses.Geisterbahni , Games.GhostTrain},
            { Witnesses.DosiWerfi , Games.Dosenwerfen},
            { Witnesses.GreifiTypi , Games.Greifautomat},
            { Witnesses.SuessigkeitenFan , Games.CandyShop}
        };

        private static readonly Dictionary<Attractions, Games> AttractionGames = new Dictionary<Attractions, Games>()
        {
            { Attractions.RollerCoaster , Games.RollerCoaster},
            { Attractions.GhostTrain , Games.GhostTrain},
            { Attractions.FerrisWheel , Games.FerrisWheel},
            { Attractions.Karussell , Games.WireGame},
            { Attractions.Autoscooter, Games.Autoscooter},
            { Attractions.Dosenwerfen , Games.Dosenwerfen},
            { Attractions.Greifautomat, Games.Greifautomat},
            { Attractions.CandyBar , Games.CandyShop}
        };
        
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

        private static readonly Dictionary<Candy, float> CandyPrizes = new Dictionary<Candy, float>()
        {
            { Candy.ChocolateStrawberry, 5.5f },
            { Candy.CandiedAlmonds, 7f },
            { Candy.CottonCandy, 8f }
        };

        private float _camPosition = -75f;
        private int _bookPage      = 0;
        private float _money       = 120f;
        private float _drunkOMeter = 0f;

        private bool _easyMode;
        private const float EasyDrunkTime = 3f;
        private const float HardDrunkTime = 9f;
        
        private bool _playWithWitness;

        private bool _playingWireGame = false;
        private bool _timmyUnlocked   = false;
        
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
            
            CluesManager.Instance.ResetBook();
        }

        public void RestartGame()
        {
            _camPosition = -75f;
            _bookPage    = 0;
            _money       = 120f;
            _drunkOMeter = 0f;
            
            TextManager.Instance.ResetManager();
            CluesManager.Instance.ResetManager();
            
            StartGame(_easyMode);
        }

        public void RestartGame(bool easyMode)
        {
            _easyMode = easyMode;
            RestartGame();
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

        public void PlayWithWitness(Witnesses witness)
        {
            _playWithWitness = true;
            
            StartMiniGame(WitnessGames[witness]);
        }

        public void PlayAttraction(Attractions attraction)
        {
            StartMiniGame(AttractionGames[attraction]);
        }

        private void StartMiniGame(Games game)
        {
            SaveCamPosition();
            SaveCurrentBookPage();

            Cursor.visible   = true;

            switch (game)
            {
                case Games.Dosenwerfen:
                    _money -= AttractionPrizes[Games.Dosenwerfen];
                    SceneManager.LoadScene("DosenwerfenScene");
                    break;
                
                case Games.Autoscooter:
                    _money -= AttractionPrizes[Games.Autoscooter];
                    SceneManager.LoadScene("AutoscooterScene");
                    break;
                
                case Games.RollerCoaster:
                    _money -= AttractionPrizes[Games.RollerCoaster];
                    SceneManager.LoadScene("RollerCoasterScene");
                    break;
                
                case Games.GhostTrain:
                    _money -= AttractionPrizes[Games.GhostTrain];
                    SceneManager.LoadScene("Geisterbahn");
                    break;
                
                case Games.WireGame:
                    _playingWireGame = true;
                    SceneManager.LoadScene("WireGameScene");
                    break;
                
                case Games.FerrisWheel:
                    _money -= AttractionPrizes[Games.FerrisWheel];
                    SceneManager.LoadScene("FerrisWheelScene");
                    break;
                
                case Games.Greifautomat:
                    _money -= AttractionPrizes[Games.Greifautomat];
                    SceneManager.LoadScene("GreifautomatScene");
                    break;
                
                case Games.CandyShop:
                    SceneManager.LoadScene("CandyShopScene");
                    break;
            }
        }

        public void ReplayMiniGame(Games game) => _money -= AttractionPrizes[game];

        public void ExitMiniGame(bool success)
        {
            ChangeLocation("OutdoorAreaScene");

            if (_playingWireGame)
            {
                _timmyUnlocked = true;
                if (Timmy.Instance != null)
                    Timmy.Instance.Unlock();
            }
            
            if (_playWithWitness)
                TextManager.Instance.MiniGamePlayed(success);
        }

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

        public void BuyCandy(Candy candy)
        {
            _money -= CandyPrizes[candy];
            
            UpdateMoney();
        }

        private void UpdateDrunkOMeter(float addition)
        {
            _drunkOMeter += addition;
            DrunkOMeterUI.Instance.UpdateValue(_drunkOMeter);

            if (_drunkOMeter >= 1f)
            {
                GameOver();
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

        public void GameOverWrongGuy()
        {
            GameOver();
            SceneManager.LoadScene("GameOverWrongGuy");
        }

        private void GameOver()
        {
            TextManager.Instance.SetGameOver();
            InputBlock.Instance.UnBlockInput();
            InputBlock.Instance.OnResume();
            InputBlock.Instance.TextboxHidden();
            Cursor.visible = true;
            
            VoiceLineManager.Instance.Stop();
        }

        public void GameWon()
        {
            SceneManager.LoadScene("GameWonDecision");
        }
        
        public float GetCamPosition() => _camPosition;
        
        public int GetBookPage() => _bookPage;
        
        public float GetMoney() => _money;
        
        public float GetPrizeOfGame(Games game) => AttractionPrizes[game];
        
        public float GetBeerPrize() => BeerPrize;
        
        public float GetAperolPrize() => AperolPrize;
        
        public float GetCandyPrize(Candy candy) => CandyPrizes[candy];

        public float GetDrunkOMeter() => _drunkOMeter;

        public Games GetGameForWitness(Witnesses witness) => WitnessGames[witness];
        
        public Games GetGameForAttraction(Attractions attraction) => AttractionGames[attraction];

        public bool GetTimmyUnlocked() => _timmyUnlocked;
        
        public void BackToMainMenu()
        {
            SceneManager.LoadScene("MainMenu");
            Destroy(gameObject);
        }

        public bool InEasyMode() => _easyMode;
    }
}