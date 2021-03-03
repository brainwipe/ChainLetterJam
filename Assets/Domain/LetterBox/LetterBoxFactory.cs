using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lang.ChainLetterJam
{
    public class LetterBoxFactory : MonoBehaviour
    {
        public Yellowy yellowy;
        public LetterBox prefab;
        public CompletedWord completedWord;
        public float spawnTimeSeconds = 3f;
        float spawnTimeRemaining;
        public float distanceToStartFrom = 9;
        bool halt { 
            get => halt1; 
            set => halt1 = value; 
        }

        public Transform W;
        public Transform I;
        public Transform N;

        List<LetterBox> LetterBoxes = new List<LetterBox>();
        private bool halt1;

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
            letterBox.SetRandomLetter(yellowy.CurrentLetter);
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

    }
}