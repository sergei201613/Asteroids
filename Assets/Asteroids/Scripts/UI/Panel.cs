using UnityEngine.UIElements;

namespace TeaGames.Asteroids.UI
{
    public class Panel : Widget
    {
        protected override void Awake()
        {
            base.Awake();

            var closeBtn = root.Q<Button>("close-button");
            closeBtn?.RegisterCallback<ClickEvent>(e => Close());
        }

        private void Close()
        {
            UIHelper.Instance.Back();
        }
    }
}
