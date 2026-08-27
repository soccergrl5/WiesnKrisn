using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace WiesnKrisn.RollerCoaster
{
    public class KeySelector : MonoBehaviour
    {
        public static KeySelector Instance{get; private set;}

        [SerializeField] private GameObject keyPrefab;
        [SerializeField] private GameObject ui;

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

        private int _keyAmount = 0;
        
        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            Cursor.visible = false;
            WrongKeyCounter.Instance.ResetCounter();
            
            SelectKey();
            
            Invoke(nameof(Finished), 30f);
        }

        private void SelectKey()
        {
            _keyAmount++;
            
            Random random = new Random();
            KeyCode selected = _availableKeys[random.Next(_availableKeys.Count)];
            
            _usedKeys.Add(selected);
            
            GameObject keyObject = Instantiate(keyPrefab, ui.transform);
            _usedElements.Add(keyObject.GetComponent<KeyElement>());
            keyObject.GetComponent<KeyElement>().StartTimer(selected);
            
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
            
            Destroy(keyElement.gameObject);
            Debug.Log(":-)");
        }

        public void FailedKey(KeyCode key)
        {
            int index = _usedKeys.IndexOf(key);
            
            KeyElement keyElement = _usedElements[index];
            
            _usedKeys.RemoveAt(index);
            _usedElements.RemoveAt(index);
            
            Destroy(keyElement.gameObject);
            
            WrongKeyCounter.Instance.IncreaseCounter();
            Debug.Log(":-(");
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