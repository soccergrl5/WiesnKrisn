using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace WiesnKrisn.RollerCoaster
{
    public class KeySelector : MonoBehaviour
    {
        public static KeySelector Instance{get; private set;}

        [SerializeField] private GameObject keyPrefab;
        
        [SerializeField] private GameObject[] spawnPoints;

        private readonly List<KeyCode> _availableKeys = new List<KeyCode>()
        {
            KeyCode.W,
            KeyCode.A,
            KeyCode.S,
            KeyCode.D,
            KeyCode.I,
            KeyCode.J,
            KeyCode.K,
            KeyCode.L,
            
        };
        private List<KeyCode> _usedKeys = new List<KeyCode>();
        private List<KeyElement> _usedElements = new List<KeyElement>();
        
        private readonly List<int> _positionIndexes = new List<int>() {0, 1, 2, 3, 4, 5, 6, 7, 8 };

        private int _keyAmount = 0;
        
        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            Cursor.visible = false;
            WrongKeyCounter.Instance.ResetCounter();
        }

        public void StartCoaster()
        {
            Invoke(nameof(SelectKey), 1f);
            
            Invoke(nameof(Finished), 31f);
        }

        private void SelectKey()
        {
            _keyAmount++;
            
            Random random = new Random();
            KeyCode selected = _availableKeys[random.Next(_availableKeys.Count)];
            
            _usedKeys.Add(selected);
            
            int position = random.Next(_positionIndexes.Count);
            int positionIndex = _positionIndexes[position];
            _positionIndexes.RemoveAt(position);
            
            GameObject keyObject = Instantiate(keyPrefab, spawnPoints[positionIndex].transform);
            _usedElements.Add(keyObject.GetComponent<KeyElement>());
            keyObject.GetComponent<KeyElement>().StartTimer(selected, positionIndex);
            
            Debug.Log(selected);

            if (_keyAmount == 5)
            {
                _availableKeys.Add(KeyCode.E);
                _availableKeys.Add(KeyCode.U);
                _availableKeys.Add(KeyCode.F);
                _availableKeys.Add(KeyCode.H);
            }

            if (_keyAmount == 10)
            {
                _availableKeys.Add(KeyCode.O);
                _availableKeys.Add(KeyCode.Q);
                _availableKeys.Add(KeyCode.N);
                _availableKeys.Add(KeyCode.C);
            }

            if (_keyAmount == 20)
            {
                _availableKeys.Add(KeyCode.M);
                _availableKeys.Add(KeyCode.X);
                _availableKeys.Add(KeyCode.G);
                _availableKeys.Add(KeyCode.T);
            }
            
            if (_keyAmount <= 7)
                Invoke(nameof(SelectKey), 2f);
            else if (_keyAmount <= 14)
                Invoke(nameof(SelectKey), 1f);
            else if (_keyAmount <= 24)
                Invoke(nameof(SelectKey), 0.5f);
        }

        public void PressedKey(KeyCode key)
        {
            int index = _usedKeys.IndexOf(key);
            
            KeyElement keyElement = _usedElements[index];
            
            _usedKeys.RemoveAt(index);
            _usedElements.RemoveAt(index);
            _positionIndexes.Add(keyElement.GetPositionIndex());
            
            Destroy(keyElement.gameObject);
        }

        public void FailedKey(KeyCode key)
        {
            PressedKey(key);
            
            WrongKeyCounter.Instance.IncreaseCounter();
        }

        private void Finished()
        {
            CancelInvoke();
            
            Cursor.visible = true;
            GameOverScript.Instance.GameOver(WrongKeyCounter.Instance.GetCounter() <= 3);
            
            Debug.Log("DONE");
        }
        
        public List<KeyCode> GetAvailableKeys() => _availableKeys;
        public List<KeyCode> GetUsedKeys() => _usedKeys;
    }
}