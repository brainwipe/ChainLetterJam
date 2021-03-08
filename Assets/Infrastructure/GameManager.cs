using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Lang.ChainLetterJam
{
    public class GameManager : MonoBehaviour
    {
        public struct Level
        {
            public string Word;
            public int CriticalMassLetterCount;
        }

        int currentLevelIndex = 0;
        int currentLetterIndex = 0;
        
        public static GameManager Instance { get; private set; }
        LetterBoxFactory letterBoxFactory;
        [SerializeField]
        RedWizard redWizard;
        [SerializeField]
        Fader Fader;

        public Level CurrentLevel => Levels[currentLevelIndex];
        public string CurrentLetter => CurrentLevel.Word.Substring(currentLetterIndex, 1);

        public Level[] Levels = new[]
        {
            new Level
            {
                Word = "miziziziz",
                CriticalMassLetterCount = 10
            },
            new Level
            {
                Word = "vimlark",
                CriticalMassLetterCount = 10
            },
            new Level
            {
                Word = "zyger",
                CriticalMassLetterCount = 10
            },
            new Level
            {
                Word = "yannick",
                CriticalMassLetterCount = 10
            }
        };

        void Awake()
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
            Invoke(nameof(BackToStart), 4);
        }

        internal void Lose()
        {
            letterBoxFactory.Lose();
            redWizard.PlayerLost();
            Invoke(nameof(BackToStart), 4);
        }

        public void BackToStart()
        {
            Fader.FadeToLevel(0);
        }

        internal void NextLetter()
        {
            currentLetterIndex++;
            if (currentLetterIndex == CurrentLevel.Word.Length)
            {
                currentLevelIndex++;
                if (currentLevelIndex == Levels.Length)
                {
                    Win();
                    return;
                }
                currentLetterIndex = 0;
                WordComplete();
            }
        }
    }
}