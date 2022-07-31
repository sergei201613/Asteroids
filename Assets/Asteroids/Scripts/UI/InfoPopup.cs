using UnityEngine.UIElements;

namespace TeaGames.UIFramework
{
    public class InfoPopup : Popup
    {
        private Button _okButton;

        private const string OkButtonQuery = "ok";

        public override void Init(UIManager uiManager)
        {
            base.Init(uiManager);

            _okButton = root.Q<Button>(OkButtonQuery);
            _okButton.RegisterCallback<ClickEvent>(e => OnOk());
        }

        private void OnOk()
        {
            uiManager.ClosePopup(this);
        }
    }
}
