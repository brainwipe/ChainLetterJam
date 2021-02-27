using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lang.ChainLetterJam
{

    public class LetterBox : MonoBehaviour
    {
        Rigidbody rigidBody;
        public float gravity;

        void Awake()
        {
            rigidBody = GetComponent<Rigidbody>();
        }

        void FixedUpdate()
        {
            rigidBody.AddForce(-transform.position.normalized * gravity);
        }
    }

}