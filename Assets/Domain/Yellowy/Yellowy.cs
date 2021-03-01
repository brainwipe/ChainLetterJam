using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lang.ChainLetterJam
{
    public class Yellowy : MonoBehaviour
    {
        public float speed = 2;
        public float direction = 0;
        string[] words = new[]
        {
            "a",
            "b",
            "miziziziz",
            "vimlark",
            "zyger",
            "yannick",
        };
        int currentWord = 0;
        int currentPosition = 0;
        [SerializeField]
        CompletedWord completedWord;

        void Update()
        {
            direction = 0;
            if (Input.GetKey(KeyCode.A))
            {
                direction = 1;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                direction = -1;
            }

            transform.Rotate(new Vector3(0, 0, speed * Time.deltaTime * direction));
        }

        void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.CompareTag(LetterBox.Tag))
            {
                var letterBox = collision.transform.GetComponent<LetterBox>();
                if (!letterBox.IsSnagged && CurrentLetter == letterBox.name.ToLower())
                {
                    Debug.Log($"Snagged {CurrentLetter}");
                    letterBox.Snagged(completedWord);

                    currentPosition++;
                    if (currentPosition == words[currentWord].Length)
                    {
                        currentPosition = 0;
                        currentWord++;
                        GameManager.Instance.WordComplete();
                    }
                    
                }
            }
        }

        public string CurrentLetter => words[currentWord].Substring(currentPosition, 1);
    }
}