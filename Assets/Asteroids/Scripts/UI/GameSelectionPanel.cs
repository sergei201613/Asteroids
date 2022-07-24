using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace TeaGames.Asteroids.UI
{
    public class GameSelectionPanel : Panel
    {
        protected override void Awake()
        {
            base.Awake();

            root.Q("item-soon").SetEnabled(false);

            var itemFreeFlight = root.Q("item-free-flight");
            itemFreeFlight.AddToClassList("game-item-selected");

            var playBtn = root.Q<Button>("play");
            playBtn.RegisterCallback<ClickEvent>(OnPlay);
        }

        private void OnPlay(ClickEvent evt)
        {
            SceneManager.LoadScene("Game");
        }
    }
}
