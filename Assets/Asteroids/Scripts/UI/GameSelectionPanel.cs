using UnityEngine.UIElements;
using TeaGames.UIFramework;
using TeaGames.Utils;

namespace TeaGames.Asteroids.UI
{
    public class GameSelectionPanel : Panel
    {
        public override void Init(UIManager uiManager)
        {
            base.Init(uiManager);

            var itemFreeFlight = root.Q("item-free-flight");
            itemFreeFlight.AddToClassList("game-item-selected");

            var playBtn = root.Q<Button>("play");
            playBtn.RegisterCallback<ClickEvent>(OnPlay);
        }

        private void OnPlay(ClickEvent evt)
        {
            SceneHelper.ChangeSceneAsync("Game");
        }
    }
}
