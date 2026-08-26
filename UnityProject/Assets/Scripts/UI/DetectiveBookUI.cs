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
        
        private int _currentPage;

        private void Awake()
        {
            Instance = this;
            
            _currentPage = 0;
            
            turnPageButtons[0].onClick.AddListener(PreviousPage);
            turnPageButtons[1].onClick.AddListener(NextPage);
        }

        private void Start()
        {
            Hide();
            
            _currentPage = GameManager.Instance.GetBookPage();
            
            pages[_currentPage].SetActive(true);
            
            if (_currentPage == 0)
                turnPageButtons[0].interactable = false;
            else if (_currentPage == pages.Length - 1)
                turnPageButtons[1].interactable = false;
            
            CluesManager.Instance.FillUpDetectiveBook();
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
        }

        public void ToggleUI()
        {
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
    }
}