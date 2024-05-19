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
                Hint.text = "Dauvat";
                ShowCueBox.SetActive(true);
            }
            else if (gameObject.name == "Thuy Icon")
            {
                Hint.text = "Thuy";
                ShowCueBox.SetActive(true);
            }
            else if (gameObject.name == "Londan Icon")
            {
                Hint.text = "Londan";
                ShowCueBox.SetActive(true);
            }
            else
            {
                ShowCueBox.SetActive(false);
            }
        }
    }
}
