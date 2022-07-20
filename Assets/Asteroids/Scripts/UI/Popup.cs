using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

namespace TeaGames.Asteroids.UI
{
    public class Popup : Widget
    {
        private const string hideStyle = "popup-hide";
        private const string showStyle = "popup-show";

        protected VisualElement popup;
        protected Label label;

        protected override void Awake()
        {
            base.Awake();

            popup = root.Q("popup");
            label = root.Q<Label>("text");
        }

        public void SetText(string text)
        {
            label.text = text;
        }

        public void Show(System.Action onComplete = null)
        {
            popup.RegisterCallback<TransitionEndEvent>(e => onComplete?.Invoke());
            StartCoroutine(ShowCoroutine());
        }

        public void Hide(System.Action onComplete = null)
        {
            popup.RegisterCallback<TransitionEndEvent>(e => onComplete?.Invoke());
            popup.RemoveFromClassList(showStyle);
            popup.AddToClassList(hideStyle);
        }

        private IEnumerator ShowCoroutine()
        {
            popup.AddToClassList(hideStyle);

            yield return new WaitForEndOfFrame();

            popup.RemoveFromClassList(hideStyle);
            popup.AddToClassList(showStyle);
        }
    }
}
