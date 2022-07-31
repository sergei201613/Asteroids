using UnityEngine;

namespace TeaGames.UIFramework
{
    public class UIManager : MonoBehaviour
    {
        public Panel CurrentPanel { get; private set; }

        private Panel _currentPanelPrefab;
        private Panel _previousPanelPrefab;

        public T OpenPanel<T>(T panelPrefab) where T : Panel
        {
            if (CurrentPanel != null)
                ClosePanel(CurrentPanel);

            _previousPanelPrefab = _currentPanelPrefab;
            _currentPanelPrefab = panelPrefab;

            var pnl = Instantiate(panelPrefab);
            pnl.Init(this);

            CurrentPanel = pnl;

            return pnl;
        }

        public void ClosePanel(Panel panel)
        {
            Destroy(panel.gameObject);
        }

        public void CloseCurrentPanel()
        {
            Destroy(CurrentPanel.gameObject);
        }

        public T OpenPopup<T>(T popupPrefab) where T : Popup
        {
            var popup = Instantiate(popupPrefab);
            popup.Init(this);
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
            if (_previousPanelPrefab == null)
                return;

            OpenPanel(_previousPanelPrefab);
        }
    }
}
