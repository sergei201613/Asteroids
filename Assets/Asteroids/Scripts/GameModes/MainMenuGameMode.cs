using TeaGames.UIFramework;
using TeaGames.ServiceLocator;
using UnityEngine;

namespace TeaGames.Asteroids
{
    public class MainMenuGameMode : MonoBehaviour
    {
        [SerializeField] private Panel _mainMenuPanelPrefab;

        private UIManager _uiManager;

        private void Awake()
        {
            _uiManager = GlobalServices.Get<UIManager>();
            _uiManager.OpenPanel(_mainMenuPanelPrefab);
        }
    }
}
