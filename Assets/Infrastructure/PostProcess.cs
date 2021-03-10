using Lang.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Lang.ChainLetterJam
{
    public class PostProcess : MonoBehaviour
    {
        Volume volume;
        Vignette vignette;
        public float nearDeath;
        public float vigIntensity;

        void Awake()
        {
            volume = GetComponent<Volume>();
            volume.sharedProfile.TryGet(out vignette);
        }

        void Update()
        {
            if (GameManager.Instance == null)
            {
                Reset();
            }
            else
            {
                nearDeath = GameManager.Instance.HowNearToDeath;
                vignette.intensity.SetValue(new NoInterpFloatParameter(nearDeath.Remap(0, 1f, 0.25f, 0.522f)));
                vignette.smoothness.SetValue(new NoInterpFloatParameter(nearDeath.Remap(0, 1f, 0.4f, 0.856f)));
            }
        }

        public void Reset()
        {
            vignette.intensity.SetValue(new NoInterpFloatParameter(0.25f));
            vignette.smoothness.SetValue(new NoInterpFloatParameter(0.4f));

        }

    }
}