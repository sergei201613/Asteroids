using UnityEngine;

namespace TeaGames.Asteroids
{
    [CreateAssetMenu(fileName = "New Product", menuName = "ScriptableObjects/New Product")]
    public class Product : ScriptableObject
    {
        [field: SerializeField]
        public string Name { get; private set; }

        [field: SerializeField]
        public int Price { get; private set; }

        [field: SerializeField]
        public Sprite Icon { get; private set; }

        // TODO: Not every Product should have prefab
        [field: SerializeField]
        public Player Prefab { get; private set; }
    }
}
