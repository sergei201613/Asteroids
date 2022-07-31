using UnityEngine.UIElements;

namespace TeaGames.UIFramework
{
    public class Panel : Widget
    {
        // TODO: use ui elements naming convention
        private const string CloseButton = "close-button";

        public override void Init(UIManager uiManager)
        {
            base.Init(uiManager);

            var closeBtn = root.Q<Button>(CloseButton);
            closeBtn?.RegisterCallback<ClickEvent>(e => Back());
        }

        private void Back()
        {
            uiManager.Back();
        }
    }
}
