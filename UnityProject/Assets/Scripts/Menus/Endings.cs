using TMPro;
using UnityEngine;

namespace WiesnKrisn.Menus
{
    public class Endings : MonoBehaviour
    {
        public const string DrunkKey = "EndingDrunk";
        public const string WrongGuyKey = "EndingWrongGuy";
        public const string ArrestKey = "EndingArrest";
        public const string LoveKey = "EndingLove";

        private void Start()
        {
            int counter = 0;

            if (PlayerPrefs.GetInt(DrunkKey, 0) == 1)
                counter++;
            if (PlayerPrefs.GetInt(WrongGuyKey, 0) == 1)
                counter++;
            if (PlayerPrefs.GetInt(ArrestKey, 0) == 1)
                counter++;
            if (PlayerPrefs.GetInt(LoveKey, 0) == 1)
                counter++;

            if (counter == 0)
                gameObject.SetActive(false);
            else
                GetComponentInChildren<TMP_Text>().text = counter + "/4 Endings";
        }
    }
}