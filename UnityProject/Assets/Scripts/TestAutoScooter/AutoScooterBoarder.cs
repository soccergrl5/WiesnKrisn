using UnityEngine;

namespace WiesnKrisn.TestAutoScooter
{
    public class AutoScooterBoarder : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D other)
        {
            AutoScooterSounds.Instance.PlayCrash();
        }
    }
}