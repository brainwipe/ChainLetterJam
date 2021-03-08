using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Lang.ChainLetterJam.LetterBox;

namespace Lang.ChainLetterJam
{
    public class Yellowy : MonoBehaviour
    {
        public float speed = 2;
        public float direction = 0;
        [SerializeField]
        CompletedWord completedWord;

        void Update()
        {
            direction = 0;
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                direction = 1;
            }
            else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
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
                if (!letterBox.CurrentState.IsSnagged && GameManager.Instance.CurrentLetter == letterBox.name.ToLower())
                {
                    letterBox.Snagged(completedWord);
                    GameManager.Instance.NextLetter();
                }
            }
        }
    }
}