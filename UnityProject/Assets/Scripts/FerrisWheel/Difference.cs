using UnityEngine;

namespace WiesnKrisn.FerrisWheel
{
    public class Difference : MonoBehaviour
    {
        [SerializeField] private Mistake image1;
        [SerializeField] private Mistake image2;

        public void Notice()
        {
            image1.Notice();
            image2.Notice();
            
            
            MistakeManager.Instance.MistakeNoticed();
        }
    }
}