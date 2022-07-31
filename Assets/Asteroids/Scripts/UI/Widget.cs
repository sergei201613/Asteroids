using UnityEngine;
using UnityEngine.UIElements;
using TeaGames.MonoBehaviourExtensions;

namespace TeaGames.UIFramework
{
    [RequireComponent(typeof(UIDocument))]
    public class Widget : MonoBehaviour
    {
        protected VisualElement root;
        protected UIManager uiManager;

        public virtual void Init(UIManager uiManager)
        {
            this.uiManager = uiManager;
            root = this.RequireComponent<UIDocument>().rootVisualElement;
        }
    }
}