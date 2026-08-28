using System;
using UnityEngine;

namespace WiesnKrisn.Roles
{
    public class Timmy : MonoBehaviour
    {
        public static Timmy Instance {get; private set;}

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            gameObject.SetActive(GameManager.Instance.GetTimmyUnlocked());
        }
        
        public void Unlock() => gameObject.SetActive(true);
    }
}