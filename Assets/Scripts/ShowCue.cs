namespace ARTreasureHunt
{
    using TMPro;
    using UnityEngine;

    public class ShowCue : MonoBehaviour
    {
        [SerializeField] GameObject ShowCueBox;
        [SerializeField] TMP_Text Hint;

        public void OnItemClicked()
        {
            if (gameObject.name == "Dauvat Icon")
            {
                Hint.text = "Đấu Vật";
                ShowCueBox.SetActive(true);
            }
            else if (gameObject.name == "Thuy Icon")
            {
                Hint.text = "Thúy";
                ShowCueBox.SetActive(true);
            }
            else if (gameObject.name == "Londan Icon")
            {
                Hint.text = "Lợn Đàn";
                ShowCueBox.SetActive(true);
            }
            else if (gameObject.name == "Quanam Icon")
            {
                Hint.text = "Quan Âm";
                ShowCueBox.SetActive(true);
            }
            else if (gameObject.name == "Hungdua Icon")
            {
                Hint.text = "Hứng Dừa";
                ShowCueBox.SetActive(true);
            }
            else
            {
                ShowCueBox.SetActive(false);
            }
        }
    }
}
