using TMPro;
using UnityEngine;

namespace TestTask.UI
{
    public class UIService : MonoBehaviour
    {
        [SerializeField] private TMP_Text _gameConditionText;

        public void ShowText(string text)
        {
            _gameConditionText.transform.parent.gameObject.SetActive(true);
            _gameConditionText.text = text;
        }

        public void HideText()
        {
            _gameConditionText.transform.parent.gameObject.SetActive(false);
        }
    }
}