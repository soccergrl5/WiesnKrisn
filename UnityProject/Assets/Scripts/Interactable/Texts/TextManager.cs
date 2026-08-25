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
        private TextFormat _currentText;
        private string _currentProgress;
        private int _progressInPart;
        private bool _inProgress;
        private int _currentSelectedOption;
        private bool _waitForOption;
        
        private int _hintsTheresSomethingWrong = 2;

        private bool _waitForUI;
        private string[] _waitForUITexts;
        private float[] _waitForUITimes;

        private void Awake()
        {
            Instance = this;
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
            _currentText           = JsonUtility.FromJson<TextFormat>(savedJson.text);
            _currentWitness        = witness;
            _currentProgress       = "Intro";
            _progressInPart        = 0;
            _currentSelectedOption = 0;
            
            DisplayText(_currentText.Intro, _currentText.IntroTime);
        }

        private void ShowNextTextbox()
        {
            _progressInPart++;
            
            switch (_currentProgress)
            {
                case "Intro":
                    if (_currentText.Intro.Length == _progressInPart)
                    {
                        _progressInPart = 0;
                        if (_hintsTheresSomethingWrong >= 2)
                        {
                            _currentProgress = "IntelGathered";
                            
                            DisplayText(_currentText.IntelGathered, _currentText.IntelGatheredTime);
                        }
                        else
                        {
                            _currentProgress = "NoIntel";
                            
                            DisplayText(_currentText.NoIntel, _currentText.NoIntelTime);
                        }
                    }
                    else
                    {
                        DisplayText(_currentText.Intro, _currentText.IntroTime);
                    }
                    break;
                
                case "NoIntel":
                    if (_currentText.NoIntel.Length == _progressInPart)
                    {
                        TextboxEnd();
                    }
                    else
                    {
                        DisplayText(_currentText.NoIntel, _currentText.NoIntelTime);
                    }
                    break;
                
                case "IntelGathered":
                    if (_currentText.IntelGathered.Length == _progressInPart)
                    {
                        _progressInPart = 0;
                        
                        if (_currentSelectedOption == 1)
                        {
                            DoGameOption();
                        }
                        else
                        {
                            _currentProgress = "Refuse";
                            
                            DisplayText(_currentText.Refuse, _currentText.RefuseTime);
                        }
                        
                        _currentSelectedOption = 0;
                    }
                    else
                    {
                        if (_progressInPart == _currentText.IndexFirstInfo)
                        {
                            string info  = RoleDistribution.Instance.GetTestimonyForWitness(_currentWitness, 0);
                            int category = RoleDistribution.Instance.GetTestimonyTypeForWitness(_currentWitness, 0);
                            
                            CluesManager.Instance.AddClue(category, _currentWitness, info);

                            _currentText.IntelGathered[_progressInPart] = _currentText.IntelGathered[_progressInPart].Replace("[]", info);
                        }
                        
                        DisplayText(_currentText.IntelGathered, _currentText.IntelGatheredTime);
                    }
                    break;
                
                case "Refuse":
                    if (_currentText.Refuse.Length == _progressInPart)
                    {
                        TextboxEnd();
                    }
                    else
                    {
                        DisplayText(_currentText.Refuse, _currentText.RefuseTime);
                    }
                    break;
                
                case "Success":
                    if (_currentText.Success.Length == _progressInPart)
                    {
                        TextboxEnd();
                    }
                    else
                    {
                        if (_progressInPart == _currentText.IndexOtherInfos)
                        {
                            string info1  = RoleDistribution.Instance.GetTestimonyForWitness(_currentWitness, 1);
                            int category1 = RoleDistribution.Instance.GetTestimonyTypeForWitness(_currentWitness, 1);
                            
                            string info2  = RoleDistribution.Instance.GetTestimonyForWitness(_currentWitness, 2);
                            int category2 = RoleDistribution.Instance.GetTestimonyTypeForWitness(_currentWitness, 2);
                            
                            CluesManager.Instance.AddClue(category1, _currentWitness, info1);
                            CluesManager.Instance.AddClue(category2, _currentWitness, info2);
                            
                            string info = info1 + " and " + info2;
                            
                            _currentText.Success[_progressInPart] = _currentText.Success[_progressInPart].Replace("[]", info);
                        }
                        
                        DisplayText(_currentText.Success, _currentText.SuccessTime);
                    }
                    break;
                
                case "Fail":
                    if (_currentText.Fail.Length == _progressInPart)
                    {
                        _progressInPart = 0;
                        if (_currentSelectedOption == 1)
                        {
                            _currentProgress = "Retry";
                            
                            DisplayText(_currentText.Retry, _currentText.RetryTime);
                        }
                        else if (_currentSelectedOption == 2)
                        {
                            GameManager.Instance.BuyBeer();
                            
                            _currentProgress = "Mass";
                            
                            DisplayText(_currentText.Mass, _currentText.MassTime);
                        }
                        else
                        {
                            _currentProgress = "Refuse";
                            
                            DisplayText(_currentText.Refuse, _currentText.RefuseTime);
                        }
                    }
                    else
                    {
                        DisplayText(_currentText.Fail, _currentText.FailTime);
                    }
                    break;
                
                case "Retry":
                    if (_currentText.Retry.Length == _progressInPart)
                    {
                        GameManager.Instance.PlayWithWitness(_currentWitness);
                    }
                    else
                    {
                        DisplayText(_currentText.Retry, _currentText.RetryTime);
                    }
                    break;
                
                case "Mass":
                    if (_currentText.Mass.Length == _progressInPart)
                    {
                        _currentProgress = "Success";
                        _progressInPart  = _currentText.IndexOtherInfos - 1;
                        
                        ShowNextTextbox();
                    }
                    else
                    {
                        DisplayText(_currentText.Mass, _currentText.MassTime);
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
            switch (_currentProgress)
            {
                case "IntelGathered":
                    if (_progressInPart == _currentText.IntelGathered.Length - 1)
                    {
                        TextboxUI.Instance.ShowTwoOptions();
                        _waitForOption = true;
                        Cursor.visible = true;
                    }
                    break;
                
                case "Fail":
                    if (_progressInPart == _currentText.Fail.Length - 1)
                    {
                        TextboxUI.Instance.ShowThreeOptions();
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
            List<Witnesses> specials =  new List<Witnesses>() { Witnesses.SaufiGroup , Witnesses.SaufiGroup2};
            if (specials.Contains(_currentWitness))
            {
                GameManager.Instance.BuyBeer();

                _currentProgress = "Mass";
                _progressInPart  = 0;
                
                DisplayText(_currentText.Mass, _currentText.MassTime);
                return;
            }

            specials = new List<Witnesses>() { Witnesses.AperoliGroup, Witnesses.AperoliGroup2 };
            if (specials.Contains(_currentWitness))
            {
                GameManager.Instance.BuyAperol();

                _currentProgress = "Mass";
                _progressInPart  = 0;
                
                DisplayText(_currentText.Mass, _currentText.MassTime);
                return;
            }
            
            GameManager.Instance.PlayWithWitness(_currentWitness);
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
            
            _waitForOption  = false;
            _waitForUITexts = null;
            _waitForUITimes = null;
        }
        
        public bool WaitForOption() => _waitForOption;
    }
}