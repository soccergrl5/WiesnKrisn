using UnityEngine;
using UnityEngine.UI;

namespace WiesnKrisn.UI
{
    public class DetectiveBookUI : MonoBehaviour
    {
        public static DetectiveBookUI Instance {get; private set;}

        [SerializeField] private GameObject[] pages;
        [SerializeField] private Button[] turnPageButtons;
        
        private int _currentPage;

        private void Awake()
        {
            Instance = this;
            Hide();
            
            _currentPage = 0;
            
            turnPageButtons[0].onClick.AddListener(PreviousPage);
            turnPageButtons[1].onClick.AddListener(NextPage);
        }

        private void Start()
        {
            _currentPage = GameManager.Instance.GetBookPage();
            
            pages[_currentPage].SetActive(true);
            
            if (_currentPage == 0)
                turnPageButtons[0].interactable = false;
            else if (_currentPage == pages.Length - 1)
                turnPageButtons[1].interactable = false;
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