using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lang.ChainLetterJam
{
    public class LetterBoxFactory : MonoBehaviour
    {
        public LetterBox prefab;
        public float spawnTimeSeconds = 3f;
        float spawnTimeRemaining;
        public float distanceToStartFrom = 9;

        private void Awake()
        {
            spawnTimeRemaining = spawnTimeSeconds;
        }

        void Update()
        {
            spawnTimeRemaining -= Time.deltaTime;
            if (spawnTimeRemaining < 0)
            {
                // spawn
                var letterBox = Instantiate(prefab);
                var unitCircle = Random.insideUnitCircle.normalized;
                letterBox.transform.position = new Vector3(unitCircle.x, unitCircle.y, 0) * distanceToStartFrom;

                spawnTimeRemaining = spawnTimeSeconds;
            }
        }
    }
}