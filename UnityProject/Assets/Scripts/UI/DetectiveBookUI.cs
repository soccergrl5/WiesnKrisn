using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WiesnKrisn.Roles;

namespace WiesnKrisn.UI
{
    public class DetectiveBookUI : MonoBehaviour
    {
        public static DetectiveBookUI Instance {get; private set;}

        [SerializeField] private GameObject[] pages;
        [SerializeField] private Button[] turnPageButtons;
        
        [SerializeField] private CluesUI[] clues;

        [SerializeField] private Button selectSuspect;
        [SerializeField] private Button interrogateSuspect;
        [SerializeField] private Sprite[] suspectImages;
        [SerializeField] private Image suspectDisplay;
        [SerializeField] private TMP_Text suspectDisplayName;
        
        private int _currentPage;
        private Suspects _currentSuspect;

        private int _cluesAmount;
        private bool _suspectSelected;

        private const int MinClueAmount = 10;

        private void Awake()
        {
            Instance = this;
            
            _currentPage = 0;
            
            turnPageButtons[0].onClick.AddListener(PreviousPage);
            turnPageButtons[1].onClick.AddListener(NextPage);
            
            selectSuspect.onClick.AddListener(() =>
            {
                SelectSuspectUI.Instance.Show();

                selectSuspect.GetComponentInChildren<TMP_Text>().text = "Change Suspect";

                if (!_suspectSelected)
                {
                    _suspectSelected = true;
                    
                    if (_cluesAmount >= MinClueAmount)
                        interrogateSuspect.interactable = true;
                }
            });
            
            interrogateSuspect.onClick.AddListener(() =>
            {
                if (_currentSuspect == RoleDistribution.Instance.GetMainSuspect())
                {
                    GameManager.Instance.GameWon();
                }
                else
                {
                    GameManager.Instance.GameOverWrongGuy();
                }
            });
        }

        private void Start()
        {
            Hide();

            foreach (GameObject page in pages)
            {
                page.SetActive(false);
            }
            
            _currentPage = GameManager.Instance.GetBookPage();
            
            pages[_currentPage].SetActive(true);
            
            if (_currentPage == 0)
                turnPageButtons[0].interactable = false;
            else if (_currentPage == pages.Length - 1)
                turnPageButtons[1].interactable = false;
            
            CluesManager.Instance.FillUpDetectiveBook();

            suspectDisplay.sprite           = null;
            interrogateSuspect.interactable = false;
        }

        private void NextPage()
        {
            pages[_currentPage].SetActive(false);
            _currentPage++;
            pages[_currentPage].SetActive(true);
            
            turnPageButtons[0].interactable = true;
            
            if (_currentPage == pages.Length - 1)
                turnPageButtons[1].interactable = false;
        }

        private void PreviousPage()
        {
            pages[_currentPage].SetActive(false);
            _currentPage--;
            pages[_currentPage].SetActive(true);
            
            turnPageButtons[1].interactable = true;
            
            if (_currentPage == 0)
                turnPageButtons[0].interactable = false;
        }

        public void AddClue(Witnesses witness, string text, int category, int number)
        {
            switch (number)
            {
                case 0:
                    clues[category].SetFirstText(witness, text);
                    break;
                
                case 1:
                    clues[category].SetSecondText(witness, text);
                    break;
                
                case 2:
                    clues[category].SetThirdText(witness, text);
                    break;
            }
            
            _cluesAmount++;
            
            if (_suspectSelected && _cluesAmount >= MinClueAmount)
                interrogateSuspect.interactable = true;
        }

        public void DisplaySuspect(Suspects suspect)
        {
            suspectDisplay.sprite   = suspectImages[(int) suspect];
            suspectDisplayName.text = WitnessNames.SuspectNames[suspect];
            _currentSuspect         = suspect;
        }

        public void ToggleUI()
        {
            if (SelectSuspectUI.Instance.gameObject.activeSelf) return;
            
            if (gameObject.activeSelf)
                Hide();
            else
                Show();
        }
        
        private void Hide()
        {
            gameObject.SetActive(false);
            InputBlock.Instance.UnBlockInput();

            Cursor.visible = false;
        }

        private void Show()
        {
            gameObject.SetActive(true);
            InputBlock.Instance.BlockInput();
            
            Cursor.visible = true;
        }

        public int GetCurrentPage() => _currentPage;

        public void ResetColors()
        {
            foreach (CluesUI clue in clues)
                clue.ResetColors();
        }
    }
}