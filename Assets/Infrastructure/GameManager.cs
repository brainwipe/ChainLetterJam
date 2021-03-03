using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Lang.ChainLetterJam
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        LetterBoxFactory letterBoxFactory;

        private void Awake()
        {
            if(Instance != null && Instance != this)
            {
                Destroy(gameObject);
            } 
            else
            {
                Instance = this;
            }

            letterBoxFactory = GetComponent<LetterBoxFactory>();
        }

        public void WordComplete()
        {
            letterBoxFactory.Reset();
        }

        internal void Win()
        {
            letterBoxFactory.Win();
            Invoke(nameof(GoBackToMainMenu), 4);
        }

        void GoBackToMainMenu()
        {
            SceneManager.LoadScene(0);
        }
    }
}