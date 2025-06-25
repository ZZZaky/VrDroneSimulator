using TMPro;
using UnityEngine;

namespace FDS.UI.Views
{
    public class UIElementsHolder : MonoBehaviour
    {
        [SerializeField] private GameObject routeCompleteMessage;
        [SerializeField] private TextMeshProUGUI scenarioTimerText;
        [SerializeField] private HomeArrow arrow;

        public GameObject RouteCompleteMessage
        {
            get
            {
                return routeCompleteMessage;
            }
        }

        public TextMeshProUGUI ScenarioTimerText
        {
            get
            {
                return scenarioTimerText;
            }
        }

        public HomeArrow Arrow => arrow;
    }
}

