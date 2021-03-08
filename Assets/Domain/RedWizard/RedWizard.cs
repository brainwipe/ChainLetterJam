using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lang.ChainLetterJam
{
    public class RedWizard : MonoBehaviour
    {
        bool isLose = false;
        float growthRate = 0.0025f;

        
        void Update()
        {
            if (isLose)
            {
                transform.localScale = transform.localScale + (transform.localScale * growthRate);
            }
        }

        public void PlayerLost()
        {
            isLose = true;
        }
    }

}