using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lang.ChainLetterJam
{
    public class LetterBoxFactory : MonoBehaviour
    {
        public LetterBox prefab;
        public CompletedWord completedWord;
        public float spawnTimeSeconds = 3f;
        float spawnTimeRemaining;
        public float distanceToStartFrom = 9;
        public int boxCount;
        bool halt;

        public Transform W;
        public Transform I;
        public Transform N;

        List<LetterBox> LetterBoxes = new List<LetterBox>();

        private void Awake()
        {
            spawnTimeRemaining = spawnTimeSeconds;
        }

        void Update()
        {
            spawnTimeRemaining -= Time.deltaTime;
            if (spawnTimeRemaining < 0)
            {
                Spawn();
                spawnTimeRemaining = spawnTimeSeconds;
            }
        }

        LetterBox CreateLetter()
        {
            var letterBox = Instantiate(prefab);
            var unitCircle = Random.insideUnitCircle.normalized;
            letterBox.transform.position = new Vector3(unitCircle.x, unitCircle.y, 0) * distanceToStartFrom;
            LetterBoxes.Add(letterBox);
            return letterBox;
        }

        void Spawn()
        {
            if (halt) return;
            var letterBox = CreateLetter();
            letterBox.SetRandomLetter(GameManager.Instance.CurrentLetter);
            boxCount++;
            if (boxCount >= GameManager.Instance.CurrentLevel.CriticalMassLetterCount)
            {
                halt = true;
                GameManager.Instance.Lose();
            }
        }

        internal void Resume()
        {
            //halt = false;
            spawnTimeRemaining = spawnTimeSeconds;
        }

        internal void Reset()
        {
            Debug.Log("Factory reset");
            BangTheLetters();
            boxCount = 0;
            Invoke(nameof(KeepGoing), 5);
        }

        void KeepGoing()
        {
            completedWord.Clear();
            halt = false;
        }

        void BangTheLetters()
        {
            halt = true;
            foreach (var letterBox in LetterBoxes)
            {
                letterBox.PrepareToDie();
            }
            foreach (var letterBox in LetterBoxes)
            {
                letterBox.Boom();
            }
            LetterBoxes.Clear();
        }

        internal void Win()
        {
            BangTheLetters();

            // TODO Red Wizard Go Boom
            // TODO Yellowy pick random spot in screen space and bounce around

            var w = CreateLetter();
            w.SetLetter("w");
            w.SetUi(W.position);

            var i = CreateLetter();
            i.SetLetter("i");
            i.SetUi(I.position);

            var n = CreateLetter();
            n.SetLetter("n");
            n.SetUi(N.position);
        }

        internal void Lose()
        {
            for (int i = 1; i < 100; i++)
            {
                var letterBox = CreateLetter();
                letterBox.SetLetter("F");
            }

            foreach(var letterBox in LetterBoxes)
            {
                letterBox.Fwoop();
            }
            
        }
    }
}