using System;
using UnityEngine;

namespace UnityStandardAssets.ImageEffects
{
    [ExecuteInEditMode]
    [AddComponentMenu("Image Effects/Color Adjustments/Grayscale")]
    public class Grayscale : ImageEffectBase {
        public Texture  textureRamp;

        [Range(-1.0f,1.0f)]
        public float    rampOffset;

        [Range(0, 1.0f)]
        public float strength;
        // Called by camera to apply image effect
        void OnRenderImage (RenderTexture source, RenderTexture destination) {
            material.SetTexture("_RampTex", textureRamp);
            material.SetFloat("_RampOffset", rampOffset);
            material.SetFloat("_Strength", strength);
            Graphics.Blit (source, destination, material);
        }
        public float GetGray()
        {
            return strength;
        }
        public void SetGray(float F)
        {
            strength = F;
        }
    }
}
