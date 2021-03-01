using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Lang.ChainLetterJam
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        LetterBoxFactory letterBoxFactory;

        private void Awake()
        {
            if(Instance != null && Instance != this)
            {
                Destroy(gameObject);
            } 
            else
            {
                Instance = this;
            }

            letterBoxFactory = GetComponent<LetterBoxFactory>();
        }

        public void WordComplete()
        {
            letterBoxFactory.Reset();
        }

        
    }
}