using UnityEngine;

namespace TeaGames.Asteroids.UI
{
    public class UIHelper : MonoBehaviour
    {
        public Panel CurrentPanel { get; private set; }

        [SerializeField]
        private Panel _startPanelPrefab;

        private Panel _panelPrefab;
        private Panel _prevPanelPrefab;

        private void Awake()
        {
            CurrentPanel = OpenPanel(_startPanelPrefab);
        }

        public T OpenPanel<T>(T panelPrefab) where T : Panel
        {
            if (CurrentPanel != null)
                ClosePanel(CurrentPanel);

            _prevPanelPrefab = _panelPrefab;
            _panelPrefab = panelPrefab;

            var pnl = Instantiate(panelPrefab);
            CurrentPanel = pnl;

            return pnl;
        }

        public void ClosePanel(Panel panel)
        {
            Destroy(panel.gameObject);
        }

        public T OpenPopup<T>(T popupPrefab) where T : Popup
        {
            var popup = Instantiate(popupPrefab);
            popup.Show();
            return popup;
        }

        public void ClosePopup(Popup popup)
        {
            popup.Hide(() =>
            {
                Destroy(popup.gameObject);
            });
        }

        public void Back()
        {
            if (_prevPanelPrefab == null)
                return;

            OpenPanel(_prevPanelPrefab);
        }
    }
}
