using UnityEngine;
using UnityEngine.UI;
using WiesnKrisn.Roles;

namespace WiesnKrisn.UI
{
    public class SelectSuspectUI : MonoBehaviour
    {
        public static SelectSuspectUI Instance { get; private set; }

        [SerializeField] private Button[] suspects;

        private void Awake()
        {
            Instance = this;

            suspects[0].onClick.AddListener(() =>
            {
                Hide();
                DetectiveBookUI.Instance.DisplaySuspect(Suspects.Saufi1);
            });
            suspects[1].onClick.AddListener(() =>
            {
                Hide();
                DetectiveBookUI.Instance.DisplaySuspect(Suspects.Saufi2);
            });
            suspects[2].onClick.AddListener(() =>
            {
                Hide();
                DetectiveBookUI.Instance.DisplaySuspect(Suspects.Aperoli1);
            });
            suspects[3].onClick.AddListener(() =>
            {
                Hide();
                DetectiveBookUI.Instance.DisplaySuspect(Suspects.Aperoli2);
            });
            suspects[4].onClick.AddListener(() =>
            {
                Hide();
                DetectiveBookUI.Instance.DisplaySuspect(Suspects.Infoluenci);
            });
            suspects[5].onClick.AddListener(() =>
            {
                Hide();
                DetectiveBookUI.Instance.DisplaySuspect(Suspects.Suessigkeiti);
            });
            suspects[6].onClick.AddListener(() =>
            {
                Hide();
                DetectiveBookUI.Instance.DisplaySuspect(Suspects.GreifiTypi);
            });
            suspects[7].onClick.AddListener(() =>
            {
                Hide();
                DetectiveBookUI.Instance.DisplaySuspect(Suspects.Achterbahni);
            });
            suspects[8].onClick.AddListener(() =>
            {
                Hide();
                DetectiveBookUI.Instance.DisplaySuspect(Suspects.Geisterbahni);
            });
            suspects[9].onClick.AddListener(() =>
            {
                Hide();
                DetectiveBookUI.Instance.DisplaySuspect(Suspects.DosiWerfi);
            });
        }

        private void Start()
        {
            Hide();
        }

        private void Hide() => gameObject.SetActive(false);
        public void Show() => gameObject.SetActive(true);
    }
}