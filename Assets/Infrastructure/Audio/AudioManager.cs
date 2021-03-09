using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lang.ChainLetterJam
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance { get; private set; }
        void Awake()
        {
            DontDestroyOnLoad(gameObject);

            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }


        }
    }
}
