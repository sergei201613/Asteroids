using UnityEngine.UIElements;

namespace TeaGames.UIFramework
{
    public class Panel : Widget
    {
        // TODO: rename in visual tree assets
        private const string CloseButtonQuery = "panel__close-button";

        public override void Init(UIManager uiManager)
        {
            base.Init(uiManager);

            var closeBtn = root.Q<Button>(CloseButtonQuery);
            closeBtn?.RegisterCallback<ClickEvent>(e => OnCloseButtonClicked());
        }

        protected virtual void OnCloseButtonClicked()
        {
            uiManager.Back();
        }
    }
}
