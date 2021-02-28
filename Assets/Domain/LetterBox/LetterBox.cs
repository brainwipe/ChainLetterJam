﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Lang.ChainLetterJam
{

    public class LetterBox : MonoBehaviour
    {
        public static string Tag = "LetterBox";
        static string AlbedoTextureMapName = "_BaseMap";

        Rigidbody rigidBody;
        public float gravity;
        public Texture2D[] letterTexturesInput;
        public Material material;

        public bool IsSnagged = false;
        CompletedWord completedWord;
        float moveToCompletedSpeed = 0.02f;
        int snaggedPosition;

        [SerializeField]
        Transform cameraPosition;

        void Awake()
        {
            rigidBody = GetComponent<Rigidbody>();
            material = GetComponent<MeshRenderer>().material;
            cameraPosition = GameObject.Find("Main Camera").transform;
        }

        void FixedUpdate()
        {
            if (IsSnagged)
            {
                transform.position = transform.position + ((completedWord.GetPositionFor(snaggedPosition) - transform.position) * moveToCompletedSpeed);
                transform.LookAt(cameraPosition.position);
                transform.localScale = transform.localScale + ((completedWord.transform.localScale - transform.localScale) * moveToCompletedSpeed);
            }
            else
            {
                rigidBody.AddForce(-transform.position.normalized * gravity);
            }
             
        }

        internal void SetLetter(string letter)
        {
            var texture2D = letterTexturesInput.Single(x => x.name == letter.ToUpper());
            name = texture2D.name;
            material.SetTexture(AlbedoTextureMapName, texture2D);
        }

        internal void SetRandomLetter(string bias)
        {
            if (UnityEngine.Random.Range(0, 2) == 0)
            {
                SetLetter(bias);
                return;
            }

            var texture2D = letterTexturesInput[UnityEngine.Random.Range(0, letterTexturesInput.Length - 1)];
            name = texture2D.name;
            material.SetTexture(AlbedoTextureMapName, texture2D);
        }

        internal void Snagged(CompletedWord completedWord)
        {
            this.completedWord = completedWord;
            IsSnagged = true;
            snaggedPosition = completedWord.Snag();
            rigidBody.isKinematic = true;
        }
    }
}