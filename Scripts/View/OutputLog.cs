using CcgCore.Controller;
using TMPro;
using UnityEngine;

namespace BumpySellotape.CcgCore.View
{
    public class OutputLog : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textLog;

        public void Initialise(CardGameControllerBase cardGameController)
        {
            cardGameController.OnOutputMessageSent += AddMessage;
        }

        public void AddMessage(string message)
        {
            if (textLog.text.Length > 0)
                textLog.text += "/r/n/r/n";
            textLog.text += message;
        }
    }
}
