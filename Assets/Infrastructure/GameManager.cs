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

        public Level CurrentLevel => currentLevelIndex < Levels.Length ? Levels[currentLevelIndex] : Levels[Levels.Length - 1];
        public string CurrentLetter => currentLetterIndex < CurrentLevel.Word.Length ? CurrentLevel.Word.Substring(currentLetterIndex, 1) : "F";

        public float HowNearToDeath => (float)letterBoxFactory.boxCount / (float)CurrentLevel.CriticalMassLetterCount;

        public Level[] Levels = new[]
        {
            /*
            new Level
            {
                Word = "a",
                CriticalMassLetterCount = 10
            },
            new Level
            {
                Word = "b",
                CriticalMassLetterCount = 10
            },
            */
            
            new Level
            {
                Word = "yannick",
                CriticalMassLetterCount = 40
            },
            new Level
            {
                Word = "zyger",
                CriticalMassLetterCount = 30
            },
            new Level
            {
                Word = "vimlark",
                CriticalMassLetterCount = 30
            },
            new Level
            {
                Word = "miziziziz",
                CriticalMassLetterCount = 30
            },
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