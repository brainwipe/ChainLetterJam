using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.VFX;

namespace Lang.ChainLetterJam
{
    public class GameManager : MonoBehaviour
    {
        public struct Level
        {
            public string Word;
            public int CriticalMassLetterCount;
            public float SpawnIntervalSeconds;
            public float PercentageLetter;
        }

        int currentLevelIndex = 0;
        int currentLetterIndex = 0;
        
        public static GameManager Instance { get; private set; }
        LetterBoxFactory letterBoxFactory;
        [SerializeField]
        RedWizard redWizard;
        [SerializeField]
        Fader Fader;
        [SerializeField]
        VisualEffect visualEffect;

        public Level CurrentLevel => currentLevelIndex < Levels.Length ? Levels[currentLevelIndex] : Levels[Levels.Length - 1];
        public string CurrentLetter => currentLetterIndex < CurrentLevel.Word.Length ? CurrentLevel.Word.Substring(currentLetterIndex, 1) : "F";

        public float HowNearToDeath => (float)letterBoxFactory.boxCount / (float)CurrentLevel.CriticalMassLetterCount;

        public Level[] Levels = new[]
        {
            new Level
            {
                Word = "yannick",
                CriticalMassLetterCount = 50,
                SpawnIntervalSeconds = 0.9f,
                PercentageLetter = 45f,
            },
            new Level
            {
                Word = "zyger",
                CriticalMassLetterCount = 60,
                SpawnIntervalSeconds = 0.45f,
                PercentageLetter = 30f,
            },
            new Level
            {
                Word = "vimlark",
                CriticalMassLetterCount = 50,
                SpawnIntervalSeconds = 0.5f,
                PercentageLetter = 45f,
            },
            new Level
            {
                Word = "miziziziz",
                CriticalMassLetterCount = 60,
                SpawnIntervalSeconds = 0.5f,
                PercentageLetter = 55f,
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
            visualEffect.Stop();
        }

        internal void Lose()
        {
            letterBoxFactory.Lose();
            redWizard.PlayerLost();
            Invoke(nameof(BackToStart), 4);
            visualEffect.Stop();
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