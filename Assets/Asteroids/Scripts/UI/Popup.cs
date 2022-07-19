using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

namespace TeaGames.Asteroids.UI
{
    public class Popup : Widget
    {
        private const string hideStyle = "popup-hide";
        private const string showStyle = "popup-show";

        private VisualElement _root;
        private VisualElement _popup;

        private void OnEnable()
        {
            _root = GetComponent<UIDocument>().rootVisualElement;
            _popup = _root.Q("popup");
        }

        public void Show(System.Action onComplete = null)
        {
            _popup.RegisterCallback<TransitionEndEvent>(e => onComplete?.Invoke());
            StartCoroutine(ShowCoroutine());
        }

        public void Hide(System.Action onComplete = null)
        {
            _popup.RegisterCallback<TransitionEndEvent>(e => onComplete?.Invoke());
            _popup.RemoveFromClassList(showStyle);
            _popup.AddToClassList(hideStyle);
        }

        private IEnumerator ShowCoroutine()
        {
            _popup.AddToClassList(hideStyle);

            yield return new WaitForEndOfFrame();

            _popup.RemoveFromClassList(hideStyle);
            _popup.AddToClassList(showStyle);
        }
    }
}
