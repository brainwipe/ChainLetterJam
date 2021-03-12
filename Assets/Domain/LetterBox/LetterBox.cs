using Lang.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Lang.ChainLetterJam
{
    public partial class LetterBox : MonoBehaviour
    {
        public LetterBoxStates.State CurrentState { get; private set; } = LetterBoxStates.Default;

        public static string Tag = "LetterBox";
        static string AlbedoTextureMapName = "LetterTexture";
        static string LetterColorName = "LetterColor";
        static string GlowName = "Glow";
        

        Collider myCollider;
        Rigidbody rigidBody;
        public float gravity;
        public Texture2D[] letterTexturesInput;
        public Material material;

        CompletedWord completedWord;
        float moveToCompletedSpeed = 0.08f;
        float fwoopRate = 0.01f;
        float gravityPull = 1.8f;
        int snaggedPosition;

        Vector3 uiPosition;
        Transform cameraPosition;
        AudioSource audioSource;
        [SerializeField]
        AudioClip hit;
        [SerializeField]
        AudioClip snag;

        void Awake()
        {
            rigidBody = GetComponent<Rigidbody>();
            myCollider = GetComponent<Collider>();
            material = GetComponent<MeshRenderer>().material;
            cameraPosition = Camera.main.transform;
            audioSource = GetComponent<AudioSource>();
        }

        void FixedUpdate()
        {
            CheckState();

            if (CurrentState.IsDestroyed) return;
            if (CurrentState.IsUI)
            {
                MoveTo(uiPosition, Vector3.one);
            }
            else if (CurrentState.IsFwoop)
            {
                transform.localScale = transform.localScale + (transform.localScale * fwoopRate);
                rigidBody.AddForce(-transform.position.normalized * gravity * gravityPull);
            }
            else if (CurrentState.IsSnagged)
            {
                MoveTo(completedWord.GetPositionFor(snaggedPosition), completedWord.transform.localScale);
            }
            else
            {
                rigidBody.AddForce(-transform.position.normalized * gravity);
            }
        }

        void OnCollisionEnter(Collision collision)
        {
            var velocity = collision.relativeVelocity.sqrMagnitude;
            if (velocity > 9)
            {
                Debug.Log(velocity);
                audioSource.clip = hit;
                audioSource.pitch = UnityEngine.Random.Range(0.6f, 1.4f);

                var baseVolume = velocity.Remap(10, 80, 0.2f, 0.6f);

                audioSource.volume = baseVolume;
                audioSource.Play();
            }
            
        }

        void CheckState()
        {
            if (CurrentState.IsSnagged || CurrentState.IsWin || CurrentState.IsFwoop || CurrentState.IsUI || CurrentState.IsDestroyed)
            { }
            else if (GameManager.Instance.CurrentLetter.ToUpper() == name)
            {
                CurrentState = LetterBoxStates.Want;
            }
            else
            {
                CurrentState = LetterBoxStates.Default;
            }

            material.SetColor(LetterColorName, CurrentState.Color);
            material.SetFloat(GlowName, CurrentState.Glow);
        }

        void MoveTo(Vector3 place, Vector3 scale)
        {
            transform.position = transform.position + ((place - transform.position) * moveToCompletedSpeed);
            transform.LookAt(cameraPosition.position);
            transform.localScale = transform.localScale + ((scale - transform.localScale) * moveToCompletedSpeed);
        }

        internal void SetLetter(string letter)
        {
            var texture2D = letterTexturesInput.Single(x => x.name == letter.ToUpper());
            name = texture2D.name;
            material.SetTexture(AlbedoTextureMapName, texture2D);
        }
         
        internal void PrepareToDie()
        {
            myCollider.enabled = false;
            rigidBody.isKinematic = false;
        }

        internal void Boom()
        {
            if (CurrentState.IsSnagged)
            {
                Invoke(nameof(BangDestroy), 3);
            }
            else
            {
                BangDestroy();
            }
        }

        void BangDestroy()
        {
            CurrentState = LetterBoxStates.Destroyed;
            rigidBody.AddExplosionForce(1500f, Vector3.zero, 100);
            Destroy(gameObject, 3);
        }

        internal void SetUi(Vector3 position)
        {
            uiPosition = position;
            CurrentState = LetterBoxStates.Menu;
        }

        internal void SetRandomLetter(string wanted)
        {
            if (UnityEngine.Random.Range(0, 100) < GameManager.Instance.CurrentLevel.PercentageLetter)
            {
                SetLetter(wanted);
                return;
            }

            var texture2D = letterTexturesInput[UnityEngine.Random.Range(0, letterTexturesInput.Length - 1)];
            name = texture2D.name;
            material.SetTexture(AlbedoTextureMapName, texture2D);
        }

        internal void Win(Vector3 position)
        {
            uiPosition = position;
            CurrentState = LetterBoxStates.Win;
        }

        internal void Fwoop()
        {
            CurrentState = LetterBoxStates.Fwoop;
            Destroy(gameObject, 9);
        }

        internal void Snagged(CompletedWord completedWord)
        {
            if (CurrentState.IsWin || CurrentState.IsFwoop) return;

            this.completedWord = completedWord;
            CurrentState = LetterBoxStates.Snagged;
            snaggedPosition = completedWord.Snag();
            rigidBody.isKinematic = true;
            audioSource.clip = snag;
            audioSource.pitch = 1;
            audioSource.volume = 1;
            audioSource.Play();
        }
    }
}