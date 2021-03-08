using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Lang.ChainLetterJam
{
    public class Fader : MonoBehaviour
    {
        Animator animator;
        int levelToLoad;

        void Awake()
        {
            animator = GetComponent<Animator>();
        }

        public void FadeToLevel(int levelIndex)
        {
            levelToLoad = levelIndex;
            animator.SetTrigger("FadeOut");
        }

        public void OnFadeComplete()
        {
            SceneManager.LoadScene(levelToLoad);
        }

    }
}