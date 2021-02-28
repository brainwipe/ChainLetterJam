using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lang.ChainLetterJam
{
    public class CompletedWord : MonoBehaviour
    {
        int letterCount;
        float spacing = 0.6f;

        public Vector3 GetPositionFor(int letterPosition) =>
            transform.position +
            ((letterPosition - 1) * new Vector3(spacing, 0, 0)) +
            new Vector3(-spacing * ((letterCount - 1f) / 2f), 0, 0);
        

        internal int Snag()
        {
            letterCount++;
            return letterCount;
        }
    }
}