using UnityEngine;
using UnityEngine.UIElements;

namespace TeaGames.Asteroids.UI
{
    public class Widget : MonoBehaviour
    {
        protected VisualElement root;

        protected virtual void Awake()
        {
            root = GetComponent<UIDocument>().rootVisualElement;
        }
    }
}