using UnityEngine.UIElements;

namespace TeaGames.Asteroids.UI
{
    public class InfoPopup : Popup
    {
        private Button _okButton;

        protected override void Awake()
        {
            base.Awake();

            _okButton = root.Q<Button>("ok");
            _okButton.RegisterCallback<ClickEvent>(e => OnOk());
        }

        private void OnOk()
        {
            UIHelper.ClosePopup(this);
        }
    }
}
