using UnityEngine;

namespace WiesnKrisn.FerrisWheel
{
    public class Instructions : MonoBehaviour
    {
        private void Start()
        {
            Invoke(nameof(Hide), 3f);
        }
        
        private void Hide() => gameObject.SetActive(false);
    }
}