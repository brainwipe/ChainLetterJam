using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Lang.ChainLetterJam.UserInterface
{

    public class Menu : MonoBehaviour
    {
        [SerializeField]
        Fader fader;

        public void OnPlayGame()
        {
            fader.FadeToLevel(1);
        }

        public void OnQuit()
        {
            Application.Quit();
        }
    }

}