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


        void Spawn()
        {
            if (halt) return;
            var letterBox = Instantiate(prefab);
            var unitCircle = Random.insideUnitCircle.normalized;
            letterBox.transform.position = new Vector3(unitCircle.x, unitCircle.y, 0) * distanceToStartFrom;
            letterBox.SetRandomLetter(yellowy.CurrentLetter);
            LetterBoxes.Add(letterBox);
        }

        internal void Resume()
        {
            //halt = false;
            spawnTimeRemaining = spawnTimeSeconds;
        }

        internal void Reset()
        {
            Debug.Log("Factory reset");
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
            Invoke(nameof(KeepGoing), 5);
        }

        void KeepGoing()
        {
            completedWord.Clear();
            halt = false;
        }
    }
}