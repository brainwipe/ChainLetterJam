using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Lang.ChainLetterJam.UserInterface
{
    public class Menu : MonoBehaviour
    {

        public Transform[] title = null;
        List<LetterBox> letterBoxes = new List<LetterBox>();
        PostProcess postProcess;

        [SerializeField]
        Fader fader;

        Camera cam;
        public LetterBox letterBoxPrefab;

        public void Awake()
        {
            cam = Camera.main;
            postProcess = FindObjectOfType<PostProcess>();
        }

        public void Start()
        {
            postProcess.Reset();
            foreach(var letter in title)
            {
                letterBoxes.Add(CreateLetter(letter.name, letter.position));
            }
        }

        public void OnPlayGame()
        {
            foreach(var letterBox in letterBoxes)
            {
                letterBox.Boom();
            }
            fader.FadeToLevel(1);
        }

        public void OnQuit()
        {
            Application.Quit();
        }

        LetterBox CreateLetter(string letter, Vector3 position)
        {
            var letterBox = Instantiate(letterBoxPrefab);
            letterBox.transform.position = LetterBoxFactory.GetPosition(cam);
            letterBox.transform.rotation = Quaternion.Euler(0, 0, 180);
            letterBox.SetLetter(letter);
            letterBox.SetUi(position);
            return letterBox;
        }
    }

}