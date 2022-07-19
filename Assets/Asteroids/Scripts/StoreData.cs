using System.Collections.Generic;
using UnityEngine;

namespace TeaGames.Asteroids
{
    [CreateAssetMenu(fileName = "New Store", menuName = "ScriptableObjects/New Store")]
    public class StoreData : ScriptableObject
    {
        public IReadOnlyList<Product> Products => _products.AsReadOnly();

        [SerializeField]
        private List<Product> _products = new();
    }
}
