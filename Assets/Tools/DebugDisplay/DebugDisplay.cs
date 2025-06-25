using TMPro;
using UnityEngine;

namespace DebugLogVR
{
    public class DebugDisplay : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI logText;

        [SerializeField]
        LogType logType = LogType.Log;

        private void OnEnable()
        {
            Application.logMessageReceived += Application_logMessageReceived;
        }

        private void OnDisable()
        {
            Application.logMessageReceived -= Application_logMessageReceived;
        }

        private void Application_logMessageReceived(string condition, string stackTrace, LogType type)
        {
            if (type == logType)
            {
                logText.text += condition + "\n";
            }
        }
    }
}
