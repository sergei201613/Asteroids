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
            Destroy(panel.gameObject);
        }

        public static Popup OpenPopup(Popup popupPrefab)
        {
            var popup = Instantiate(popupPrefab);
            popup.Show();
            return popup;
        }

        public static void ClosePopup(Popup popup)
        {
            popup.Hide(() =>
            {
                Destroy(popup.gameObject);
            });
        }
    }
}
