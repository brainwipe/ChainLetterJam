using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lang.ChainLetterJam
{
    public class LetterBoxFactory : MonoBehaviour
    {
        public LetterBox prefab;
        public CompletedWord completedWord;
        float spawnTimeRemaining;
        public int boxCount;
        bool halt;
        bool isWin = false;
        bool isLose = false;
        Camera cam;

        public Transform W;
        public Transform I;
        public Transform N;

        List<LetterBox> LetterBoxes = new List<LetterBox>();

        private void Awake()
        {
            cam = Camera.main;
        }

        private void Start()
        {
            spawnTimeRemaining = GameManager.Instance.CurrentLevel.SpawnIntervalSeconds;
        }

        void Update()
        {
            spawnTimeRemaining -= Time.deltaTime;
            if (spawnTimeRemaining < 0)
            {
                Spawn();
                spawnTimeRemaining = GameManager.Instance.CurrentLevel.SpawnIntervalSeconds;
            }
        }

        LetterBox CreateLetter()
        {
            var letterBox = Instantiate(prefab);
            letterBox.transform.position = GetPosition(cam);
            letterBox.transform.rotation = Quaternion.Euler(0,0,180);
            LetterBoxes.Add(letterBox);
            return letterBox;
        }

        public static Vector3 GetPosition(Camera cam)
        {
            var side = Random.Range(0, 4);
            var x = 0;
            var y = 0;
            switch(side)
            {
                case 0: 
                    x = 0;
                    y = Random.Range(0, cam.pixelHeight);
                    break;
                case 1:
                    x = cam.pixelWidth;
                    y = Random.Range(0, cam.pixelHeight);
                    break;
                case 2:
                    x = Random.Range(0, cam.pixelWidth);
                    y = 0;
                    break;
                case 3:
                    x = Random.Range(0, cam.pixelWidth);
                    y = cam.pixelHeight;
                    break;
            }

            return cam.ScreenToWorldPoint(new Vector3(x, y, -cam.transform.position.z));
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
            spawnTimeRemaining = GameManager.Instance.CurrentLevel.SpawnIntervalSeconds;
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

        void CreateWin(string letter, Vector3 position)
        {
            var letterBox = CreateLetter();
            letterBox.SetLetter(letter);
            letterBox.Win(position);
        }

        internal void Win()
        {
            if (isLose) return;
            isWin = true;
            BangTheLetters();

            // TODO Red Wizard Go Boom
            // TODO Yellowy pick random spot in screen space and bounce around

            CreateWin("w", W.position);
            CreateWin("i", I.position);
            CreateWin("n", N.position);
        }

        internal void Lose()
        {
            if (isWin) return;
            isLose = true;
            for (int i = 1; i < 30; i++)
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