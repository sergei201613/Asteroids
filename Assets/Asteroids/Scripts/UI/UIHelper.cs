using UnityEngine;

namespace TeaGames.Asteroids.UI
{
    public class UIHelper : MonoBehaviour
    {
        public static Panel OpenPanel(Panel panelPrefab)
        {
            return Instantiate(panelPrefab);
        }

        public static void ClosePanel(Panel panel)
        {
            Destroy(panel);
        }

        public static Popup OpenPopup(Popup popupPrefab)
        {
            return Instantiate(popupPrefab);
        }

        public static void ClosePopup(Popup popup)
        {
            Destroy(popup);
        }
    }
}
