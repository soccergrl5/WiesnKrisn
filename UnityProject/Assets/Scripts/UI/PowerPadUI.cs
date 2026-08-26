using UnityEngine;
using UnityEngine.UI;

namespace WiesnKrisn.UI
{
    public class PowerPadUI : MonoBehaviour
    {
        public static PowerPadUI Instance {get; private set;}

        [SerializeField] private Sprite ferrisWheel;
        [SerializeField] private Sprite autoscooter;
        [SerializeField] private Sprite ghostTrain;
        [SerializeField] private Sprite rollerCoaster;

        [SerializeField] private Image image;
        [SerializeField] private Button close;

        private void Awake()
        {
            Instance = this;
            
            close.onClick.AddListener(Hide);
        }

        private void Start()
        {
            gameObject.SetActive(false);
        }

        public void ShowPad(Attractions attractions)
        {
            switch (attractions)
            {
                case Attractions.FerrisWheel:
                    image.sprite = ferrisWheel;
                    break;
                
                case Attractions.Autoscooter:
                    image.sprite = autoscooter;
                    break;
                
                case Attractions.GhostTrain:
                    image.sprite = ghostTrain;
                    break;
                
                case Attractions.RollerCoaster:
                    image.sprite = rollerCoaster;
                    break;
                
                default:
                    return;
            }
            
            Show();
        }
        
        private void Hide()
        {
            gameObject.SetActive(false);
            
            InputBlock.Instance.TextboxHidden();
            Cursor.visible = false;
        }

        private void Show()
        {
            gameObject.SetActive(true);
            
            InputBlock.Instance.TextboxShown();
            Cursor.visible = true;
        }
    }
}