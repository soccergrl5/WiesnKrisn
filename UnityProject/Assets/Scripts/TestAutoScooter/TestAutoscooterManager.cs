using UnityEngine;

namespace WiesnKrisn.TestAutoScooter
{
    public class TestAutoscooterManager : MonoBehaviour
    {
        public static TestAutoscooterManager Instance {get; private set;}

        [SerializeField] private TestScoreDisplay testScoreDisplay1;
        [SerializeField] private TestScoreDisplay testScoreDisplay2;

        private int _score1;
        private int _score2;

        private float _timer = 60f;
        
        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            Cursor.visible = false;
            
            testScoreDisplay1.UpdateScore(0);
            testScoreDisplay2.UpdateScore(0);
                
            TestTimeDisplay.Instance.UpdateTimer(_timer);
        }

        public void StartTimer()
        {
            Invoke(nameof(DecreaseTimer), 1f);
        }

        public void HitOtherCar(int car)
        {
            if (car == 1)
            {
                _score1++;
                testScoreDisplay1.UpdateScore(_score1);
            }
            else
            {
                _score2++;
                testScoreDisplay2.UpdateScore(_score2);
            }
        }

        private void DecreaseTimer()
        {
            _timer -= 1f;
            TestTimeDisplay.Instance.UpdateTimer(_timer);

            if (_timer <= 0f)
            {
                TestPlayerController.Instance.Stop();
                TestOpponentCar.Instance.Stop();
                
                GameOverScript.Instance.GameOver(_score1 > _score2);
                
                Cursor.visible = true;
                
                return;
            }
            
            Invoke(nameof(DecreaseTimer), 1f);
        }
    }
}