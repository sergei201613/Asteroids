using UnityEngine;
using TeaGames.Utils;

namespace TeaGames.Asteroids
{
    public class Boot : MonoBehaviour
    {
        [SerializeField] private string _startScene;

        private void Awake()
        {
            SceneHelper.AddSceneAsync(_startScene, () =>
            {
                Destroy(gameObject);
            });
        }
    }
}
