using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lang.ChainLetterJam
{
    public class Yellowy : MonoBehaviour
    {
        public float speed = 2;
        public float direction = 0;

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
    }
}