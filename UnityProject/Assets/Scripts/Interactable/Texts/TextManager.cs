using System.Collections.Generic;
using UnityEngine;
using WiesnKrisn.Audio;
using WiesnKrisn.CandyShop;
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
        
        private List<Attractions> _hintsTheresSomethingWrong = new List<Attractions>();
        private List<Witnesses> _receivedAllHints = new List<Witnesses>();

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
            
            TextAsset savedJson    = Resources.Load<TextAsset>("Text/Witnesses/" + witness);
            _currentTextWitness    = JsonUtility.FromJson<TextFormatWitness>(savedJson.text);
            _currentWitness        = witness;
            _currentProgress       = "Intro";
            _progressInPart        = 0;
            _currentSelectedOption = 0;
            _type                  = "Witness";

            if (_receivedAllHints.Contains(witness))
            {
                _currentProgress = "Afterwards";
                DisplayText(_currentTextWitness.Afterwards, _currentTextWitness.AfterwardsTime);
            }
            else
                DisplayText(_currentTextWitness.Intro, _currentTextWitness.IntroTime);
        }

        public void ShowTextboxFor(Attractions attraction)
        {
            InputBlock.Instance.TextboxShown();
            
            TextAsset savedJson    = Resources.Load<TextAsset>("Text/Attractions/" + attraction);
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

                        if (_currentWitness == Witnesses.KarussellKid)
                        {
                            _currentProgress = "Success";
                            _progressInPart  = -1;
                            ShowNextTextbox();
                            return;
                        }
                        
                        if (_hintsTheresSomethingWrong.Count >= 2)
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
                            VoiceLineManager.Instance.SelectFirstTrait(category, info);

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
                            string info1  = RoleDistribution.Instance.GetTestimonyForWitness(_currentWitness, 1);
                            int category1 = RoleDistribution.Instance.GetTestimonyTypeForWitness(_currentWitness, 1);
                            
                            string info2  = RoleDistribution.Instance.GetTestimonyForWitness(_currentWitness, 2);
                            int category2 = RoleDistribution.Instance.GetTestimonyTypeForWitness(_currentWitness, 2);
                            
                            CluesManager.Instance.AddClue(category1, _currentWitness, info1);
                            CluesManager.Instance.AddClue(category2, _currentWitness, info2);
                            
                            VoiceLineManager.Instance.SelectOtherTraits(category1, info1, category2, info2);
                            
                            _receivedAllHints.Add(_currentWitness);
                            
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
                            if (_currentWitness == Witnesses.AutoscooterKid)
                                GameManager.Instance.BuyCandy(Candy.CottonCandy);
                            else
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
                
                case "Afterwards":
                    if (_currentTextWitness.Afterwards.Length == _progressInPart)
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
                        DisplayText(_currentTextWitness.Afterwards, _currentTextWitness.AfterwardsTime);
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
            
            CheckIfOptionsDisplay();
        }

        private void TextboxEnd()
        {
            InputBlock.Instance.TextboxHidden();
            TextboxUI.Instance.Hide();
            
            VoiceLineManager.Instance.Stop();
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
            
            string identifier = _type == "Witness" ? _currentWitness.ToString() : _currentAttraction.ToString();
            VoiceLineManager.Instance.PlayVoiceLine(identifier, _currentProgress, _progressInPart + 1);
            
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
                        TextboxUI.Instance.TextsForTwoOptions(_currentWitness);
                        TextboxUI.Instance.ShowTwoOptions();
                        _waitForOption = true;
                        Cursor.visible = true;
                    }
                    break;
                
                case "Fail":
                    if (_progressInPart == _currentTextWitness.Fail.Length - 1)
                    {
                        TextboxUI.Instance.TextsForTwoOptions(_currentWitness);
                        TextboxUI.Instance.TextsForThreeOptions(_currentWitness);
                        TextboxUI.Instance.ShowThreeOptions();
                        _waitForOption = true;
                        Cursor.visible = true;
                    }
                    break;
                
                case "Afterwards":
                    if (_progressInPart == _currentTextWitness.Afterwards.Length - 1)
                    {
                        TextboxUI.Instance.TextsForTwoOptions(_currentWitness);
                        TextboxUI.Instance.ShowTwoOptions();
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
                        TextboxUI.Instance.TextsForTwoOptions(_currentAttraction);
                        TextboxUI.Instance.ShowTwoOptions();
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
            if (_receivedAllHints.Contains(_currentWitness))
            {
                TextboxEnd();
                return;
            }
            
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

        public void AddHintForSituation(Attractions attraction)
        {
            if (_hintsTheresSomethingWrong.Contains(attraction)) return;
            
            _hintsTheresSomethingWrong.Add(attraction);
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

            _hintsTheresSomethingWrong.Clear();
            _receivedAllHints.Clear();
        }
        
        public bool WaitForOption() => _waitForOption;
        
        public void SetGameOver() => _gameOver = true;
    }
}