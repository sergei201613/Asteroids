using UnityEngine;
using TeaGames.ServiceLocator;
using UnityEngine.SceneManagement;

namespace TeaGames.Asteroids
{
    public class Boot : MonoBehaviour
    {
        [SerializeField] private string _startScene;

        private GlobalServiceLocator _globalServices;

        private void Awake()
        {
            _globalServices = GlobalServiceLocator.Instance;

            if (_globalServices.IsInitialized)
                LoadStartSceneThenDestroy();
            else
                _globalServices.Initialized += OnInitialized;
        }

        private void OnInitialized()
        {
            _globalServices.Initialized -= OnInitialized;
            LoadStartSceneThenDestroy();
        }

        private void LoadStartSceneThenDestroy()
        {
            var operation = SceneManager.LoadSceneAsync(_startScene, LoadSceneMode.Additive);
            operation.completed += _ =>
            {
                SceneManager.SetActiveScene(SceneManager.GetSceneByName(_startScene));
                Destroy(gameObject);
            };
        }
    }
}
