using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Lang.ChainLetterJam.UserInterface
{

    public class Menu : MonoBehaviour
    {
        public void OnPlayGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        public void OnQuit()
        {
            Application.Quit();
        }
    }

}