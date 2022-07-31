using static UnityEngine.SceneManagement.SceneManager;
using UnityEngine.SceneManagement;
using System;

namespace TeaGames.Utils
{
    public static class SceneHelper
    {
        public static void AddSceneAsync(string sceneName, Action completed = null)
        {
            LoadSceneAsync(sceneName, LoadSceneMode.Additive).completed += _ =>
            {
                SetActiveScene(GetSceneByName(sceneName));
                completed?.Invoke();
            };
        }

        public static void ChangeSceneAsync(string sceneName, Action completed = null)
        {
            UnloadSceneAsync(GetActiveScene(), UnloadSceneOptions.None);
            LoadSceneAsync(sceneName, LoadSceneMode.Additive).completed += _ =>
            {
                SetActiveScene(GetSceneByName(sceneName));
                completed?.Invoke();
            };
        }
    }
}
