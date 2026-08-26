using System.Collections.Generic;
using UnityEngine;
using WiesnKrisn.Roles;
using WiesnKrisn.UI;

namespace WiesnKrisn.Interactable.Texts
{
    public class TextManager : MonoBehaviour
    {
        public static TextManager Instance {get; private set;}
        
        private Witnesses _currentWitness;
        private Attractions _currentAttraction;
        private TextFormatWitness _currentTextWitness;
        private TextFormatAttraction _currentTextAttraction;
        private string _currentProgress;
        private int _progressInPart;
        private bool _inProgress;
        private int _currentSelectedOption;
        private bool _waitForOption;
        private string _type;
        
        private int _hintsTheresSomethingWrong;

        private bool _waitForUI;
        private string[] _waitForUITexts;
        private float[] _waitForUITimes;

        private bool _gameOver;

        private void Awake()
        {
            Instance = this;
            
            ResetManager();
        }

        private void Update()
        {
            if (!InputBlock.Instance.IsTextbox() || InputBlock.Instance.IsPaused() || _waitForOption) return;
            
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (_inProgress)
                {
                    CancelInvoke();
                    _inProgress = false;
                    TextboxUI.Instance.SkipToEndOfTextbox();
                    Debug.Log("SKIP");
                    
                    CheckIfOptionsDisplay();
                }
                else
                {
                    ShowNextTextbox();
                }
            }
        }

        public void ShowTextboxFor(Witnesses witness)
        {
            InputBlock.Instance.TextboxShown();
            
            TextAsset savedJson    = Resources.Load<TextAsset>(witness.ToString());
            _currentTextWitness    = JsonUtility.FromJson<TextFormatWitness>(savedJson.text);
            _currentWitness        = witness;
            _currentProgress       = "Intro";
            _progressInPart        = 0;
            _currentSelectedOption = 0;
            _type                  = "Witness";
            
            DisplayText(_currentTextWitness.Intro, _currentTextWitness.IntroTime);
        }

        public void ShowTextboxFor(Attractions attraction)
        {
            InputBlock.Instance.TextboxShown();
            
            TextAsset savedJson    = Resources.Load<TextAsset>(attraction.ToString());
            _currentTextAttraction = JsonUtility.FromJson<TextFormatAttraction>(savedJson.text);
            _currentAttraction     = attraction;
            _currentProgress       = "Intro";
            _progressInPart        = 0;
            _currentSelectedOption = 0;
            _type                  = "Attraction";
            
            DisplayText(_currentTextAttraction.Intro, _currentTextAttraction.IntroTime);
        }

        private void ShowNextTextbox()
        {
            switch (_type)
            {
                case "Witness":
                    ShowNextTextboxWitness();
                    break;
                
                case "Attraction":
                    ShowNextTextboxAttraction();
                    break;
            }
        }
        
        private void ShowNextTextboxWitness()
        {
            _progressInPart++;
            
            switch (_currentProgress)
            {
                case "Intro":
                    if (_currentTextWitness.Intro.Length == _progressInPart)
                    {
                        _progressInPart = 0;
                        if (_hintsTheresSomethingWrong >= 2)
                        {
                            _currentProgress = "IntelGathered";
                            
                            DisplayText(_currentTextWitness.IntelGathered, _currentTextWitness.IntelGatheredTime);
                        }
                        else
                        {
                            _currentProgress = "NoIntel";
                            
                            DisplayText(_currentTextWitness.NoIntel, _currentTextWitness.NoIntelTime);
                        }
                    }
                    else
                    {
                        DisplayText(_currentTextWitness.Intro, _currentTextWitness.IntroTime);
                    }
                    break;
                
                case "NoIntel":
                    if (_currentTextWitness.NoIntel.Length == _progressInPart)
                    {
                        TextboxEnd();
                    }
                    else
                    {
                        DisplayText(_currentTextWitness.NoIntel, _currentTextWitness.NoIntelTime);
                    }
                    break;
                
                case "IntelGathered":
                    if (_currentTextWitness.IntelGathered.Length == _progressInPart)
                    {
                        _progressInPart = 0;
                        
                        if (_currentSelectedOption == 1)
                        {
                            DoGameOption();
                        }
                        else
                        {
                            _currentProgress = "Refuse";
                            
                            DisplayText(_currentTextWitness.Refuse, _currentTextWitness.RefuseTime);
                        }
                        
                        _currentSelectedOption = 0;
                    }
                    else
                    {
                        if (_progressInPart == _currentTextWitness.IndexFirstInfo)
                        {
                            string info  = RoleDistribution.Instance.GetTestimonyForWitness(_currentWitness, 0);
                            int category = RoleDistribution.Instance.GetTestimonyTypeForWitness(_currentWitness, 0);
                            
                            CluesManager.Instance.AddClue(category, _currentWitness, info);

                            _currentTextWitness.IntelGathered[_progressInPart] = _currentTextWitness.IntelGathered[_progressInPart].Replace("[]", info);
                        }
                        
                        DisplayText(_currentTextWitness.IntelGathered, _currentTextWitness.IntelGatheredTime);
                    }
                    break;
                
                case "Refuse":
                    if (_currentTextWitness.Refuse.Length == _progressInPart)
                    {
                        TextboxEnd();
                    }
                    else
                    {
                        DisplayText(_currentTextWitness.Refuse, _currentTextWitness.RefuseTime);
                    }
                    break;
                
                case "Success":
                    if (_currentTextWitness.Success.Length == _progressInPart)
                    {
                        TextboxEnd();
                    }
                    else
                    {
                        if (_progressInPart == _currentTextWitness.IndexOtherInfos)
                        {
                            Debug.Log("TEST");
                            
                            string info1  = RoleDistribution.Instance.GetTestimonyForWitness(_currentWitness, 1);
                            int category1 = RoleDistribution.Instance.GetTestimonyTypeForWitness(_currentWitness, 1);
                            
                            string info2  = RoleDistribution.Instance.GetTestimonyForWitness(_currentWitness, 2);
                            int category2 = RoleDistribution.Instance.GetTestimonyTypeForWitness(_currentWitness, 2);
                            
                            CluesManager.Instance.AddClue(category1, _currentWitness, info1);
                            CluesManager.Instance.AddClue(category2, _currentWitness, info2);
                            
                            string info = info1 + " and " + info2;
                            
                            _currentTextWitness.Success[_progressInPart] = _currentTextWitness.Success[_progressInPart].Replace("[]", info);
                        }
                        
                        DisplayText(_currentTextWitness.Success, _currentTextWitness.SuccessTime);
                    }
                    break;
                
                case "Fail":
                    if (_currentTextWitness.Fail.Length == _progressInPart)
                    {
                        _progressInPart = 0;
                        if (_currentSelectedOption == 1)
                        {
                            _currentProgress = "Retry";
                            
                            DisplayText(_currentTextWitness.Retry, _currentTextWitness.RetryTime);
                        }
                        else if (_currentSelectedOption == 2)
                        {
                            GameManager.Instance.BuyBeer();
                            
                            _currentProgress = "Mass";
                            
                            DisplayText(_currentTextWitness.Mass, _currentTextWitness.MassTime);
                        }
                        else
                        {
                            _currentProgress = "Refuse";
                            
                            DisplayText(_currentTextWitness.Refuse, _currentTextWitness.RefuseTime);
                        }
                    }
                    else
                    {
                        DisplayText(_currentTextWitness.Fail, _currentTextWitness.FailTime);
                    }
                    break;
                
                case "Retry":
                    if (_currentTextWitness.Retry.Length == _progressInPart)
                    {
                        GameManager.Instance.PlayWithWitness(_currentWitness);
                    }
                    else
                    {
                        DisplayText(_currentTextWitness.Retry, _currentTextWitness.RetryTime);
                    }
                    break;
                
                case "Mass":
                    if (_currentTextWitness.Mass.Length == _progressInPart)
                    {
                        _currentProgress = "Success";
                        _progressInPart  = _currentTextWitness.IndexOtherInfos - 1;
                        
                        // Don't Change this, I'm fucking stupid
                        ShowNextTextbox();
                    }
                    else
                    {
                        DisplayText(_currentTextWitness.Mass, _currentTextWitness.MassTime);
                    }
                    break;
            }
        }

        private void ShowNextTextboxAttraction()
        {
            _progressInPart++;

            switch (_currentProgress)
            {
                case "Intro":
                    if (_currentTextAttraction.Intro.Length == _progressInPart)
                    {
                        _progressInPart = 0;
                        
                        if (_currentSelectedOption == 1)
                        {
                            _currentProgress = "Accept";
                            
                            DisplayText(_currentTextAttraction.Accept, _currentTextAttraction.AcceptTime);
                        }
                        else
                        {
                            _currentProgress = "Refuse";
                            
                            DisplayText(_currentTextAttraction.Refuse, _currentTextAttraction.RefuseTime);
                        }
                        
                        _currentSelectedOption = 0;
                    }
                    else
                    {
                        DisplayText(_currentTextAttraction.Intro, _currentTextAttraction.IntroTime);
                    }
                    break;
                
                case "Accept":
                    if (_currentTextAttraction.AcceptTime.Length == _progressInPart)
                    {
                        TextboxEnd();
                        DoGameOption();
                    }
                    else
                    {
                        DisplayText(_currentTextAttraction.Accept, _currentTextAttraction.AcceptTime);
                    }
                    break;
                
                case "Refuse":
                    if (_currentTextAttraction.RefuseTime.Length == _progressInPart)
                    {
                        TextboxEnd();
                    }
                    else
                    {
                        DisplayText(_currentTextAttraction.Refuse, _currentTextAttraction.RefuseTime);
                    }
                    break;
            }
        }

        private void ProgressOver()
        {
            _inProgress = false;
            Debug.Log("DONE");
            
            CheckIfOptionsDisplay();
        }

        private void TextboxEnd()
        {
            InputBlock.Instance.TextboxHidden();
            TextboxUI.Instance.Hide();
        }

        private void DisplayText(string[] texts, float[] times)
        {
            if (TextboxUI.Instance == null)
            {
                _waitForUI      = true;
                _waitForUITexts = texts;
                _waitForUITimes = times;

                return;
            }
            
            Debug.Log(texts[_progressInPart]);
            
            TextboxUI.Instance.DisplayText(texts[_progressInPart], times[_progressInPart]);
            
            _inProgress = true;
            Invoke(nameof(ProgressOver), times[_progressInPart]);
        }

        private void CheckIfOptionsDisplay()
        {
            switch (_type)
            {
                case "Witness":
                    CheckIfOptionsDisplayWitness();
                    break;
                
                case "Attraction":
                    CheckIfOptionsDisplayAttraction();
                    break;
            }
        }
        
        private void CheckIfOptionsDisplayWitness()
        {
            switch (_currentProgress)
            {
                case "IntelGathered":
                    if (_progressInPart == _currentTextWitness.IntelGathered.Length - 1)
                    {
                        string option1;
                        bool option1Available;
                        if (_currentWitness == Witnesses.SaufiGroup || _currentWitness == Witnesses.SaufiGroup2)
                        {
                            float prize = GameManager.Instance.GetBeerPrize();
                            option1 = "Drink a Beer - " + prize + "€";
                            option1Available = prize <= GameManager.Instance.GetMoney();
                        }
                        else if (_currentWitness == Witnesses.AperoliGroup ||
                                 _currentWitness == Witnesses.AperoliGroup2)
                        {
                            float prize = GameManager.Instance.GetAperolPrize();
                            option1 = "Drink an Aperol - " + prize + "€";
                            option1Available = prize <= GameManager.Instance.GetMoney();
                        }
                        else
                        {
                            float prize =
                                GameManager.Instance.GetPrizeOfGame(
                                    GameManager.Instance.GetGameForWitness(_currentWitness));
                            option1 = "Play the Game - " + prize + "€";
                            option1Available = prize <= GameManager.Instance.GetMoney();
                        }
                        
                        TextboxUI.Instance.ShowTwoOptions(option1, option1Available, "No Thanks!");
                        _waitForOption = true;
                        Cursor.visible = true;
                    }
                    break;
                
                case "Fail":
                    if (_progressInPart == _currentTextWitness.Fail.Length - 1)
                    {
                        string option1 = "Replay the Game - " + GameManager.Instance.GetPrizeOfGame(GameManager.Instance.GetGameForWitness(_currentWitness)) + "€";
                        string option2 = "Drink a Beer - " + GameManager.Instance.GetBeerPrize() + "€";
                        
                        bool option1Available = GameManager.Instance.GetPrizeOfGame(GameManager.Instance.GetGameForWitness(_currentWitness)) <= GameManager.Instance.GetMoney();
                        bool option2Available = GameManager.Instance.GetBeerPrize() <= GameManager.Instance.GetMoney();
                        
                        TextboxUI.Instance.ShowThreeOptions(option1, option1Available, option2, option2Available, "No Thanks!");
                        _waitForOption = true;
                        Cursor.visible = true;
                    }
                    break;
            }
        }

        private void CheckIfOptionsDisplayAttraction()
        {
            switch (_currentProgress)
            {
                case "Intro":
                    if (_progressInPart == _currentTextAttraction.Intro.Length - 1)
                    {
                        string option1 = "Accept";
                        bool option1Available = true;
                        
                        TextboxUI.Instance.ShowTwoOptions(option1, option1Available, "No Thanks!");
                        _waitForOption = true;
                        Cursor.visible = true;
                    }
                    break;
            }
        }

        public void OptionSelected(int option)
        {
            _currentSelectedOption = option;
            _waitForOption         = false;
            Cursor.visible         = false;
            
            ShowNextTextbox();
        }

        private void DoGameOption()
        {
            switch (_type)
            {
                case "Witness":
                    DoGameOptionWitness();
                    break;
                
                case "Attraction":
                    DoGameOptionAttraction();
                    break;
            }
        }
        
        private void DoGameOptionWitness()
        {
            List<Witnesses> specials =  new List<Witnesses>() { Witnesses.SaufiGroup , Witnesses.SaufiGroup2};
            if (specials.Contains(_currentWitness))
            {
                GameManager.Instance.BuyBeer();
                
                if (_gameOver)
                    return;

                _currentProgress = "Mass";
                _progressInPart  = 0;
                
                DisplayText(_currentTextWitness.Mass, _currentTextWitness.MassTime);
                return;
            }

            specials = new List<Witnesses>() { Witnesses.AperoliGroup, Witnesses.AperoliGroup2 };
            if (specials.Contains(_currentWitness))
            {
                GameManager.Instance.BuyAperol();
                
                if (_gameOver)
                    return;

                _currentProgress = "Mass";
                _progressInPart  = 0;
                
                DisplayText(_currentTextWitness.Mass, _currentTextWitness.MassTime);
                return;
            }
            
            GameManager.Instance.PlayWithWitness(_currentWitness);
        }

        private void DoGameOptionAttraction()
        {
            GameManager.Instance.PlayAttraction(_currentAttraction);
        }

        public void MiniGamePlayed(bool success)
        {
            _currentProgress = success ? "Success" : "Fail";
            _progressInPart  = -1;
            
            InputBlock.Instance.TextboxShown();
            ShowNextTextbox();
        }

        public void UIReady()
        {
            if (!_waitForUI) return;
            
            DisplayText(_waitForUITexts, _waitForUITimes);
            
            _waitForUI      = false;
            _waitForUITexts = null;
            _waitForUITimes = null;
        }

        public void ResetManager()
        {
            _currentProgress       = "Intro";
            _progressInPart        = 0;
            _currentSelectedOption = 0;
            _inProgress            = false;
            _waitForUI             = false;
            _waitForUITexts        = null;
            _waitForUITimes        = null;
            _waitForOption         = false;

            _gameOver = false;

            _hintsTheresSomethingWrong = 2;
        }
        
        public bool WaitForOption() => _waitForOption;
        
        public void SetGameOver() => _gameOver = true;
    }
}