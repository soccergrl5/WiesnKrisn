using UnityEngine;

namespace WiesnKrisn.FerrisWheel
{
    public class Mistake : MonoBehaviour
    {
        [SerializeField] private Difference difference;

        private bool _noticed;
        
        private void OnMouseDown()
        {
            if (_noticed) return;
            
            difference.Notice();
        }

        public void Notice()
        {
            _noticed = true;
            GetComponentInChildren<SpriteRenderer>().enabled = true;
        }
    }
}