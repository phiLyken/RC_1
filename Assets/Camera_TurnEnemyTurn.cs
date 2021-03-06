﻿using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;
public class Camera_TurnEnemyTurn : MonoBehaviour {

    public Grayscale m_GrayScale;
    public NoiseAndGrain Noise;

    public void Start()
    {
        if(TurnSystem.Instance != null)
        {
            TurnSystem.Instance.OnStartTurn += OnStartTurn;
        }
    }

    void OnStartTurn(ITurn turn)
    {
        float start = m_GrayScale.GetGray();

        if( turn.GetType() == typeof(Unit) && (turn as Unit).OwnerID == 1 && !(turn as Unit).IsIdentified)
        {
            StopAllCoroutines();
            Noise.enabled = true;
            StartCoroutine(M_Extensions.YieldT(f =>  m_GrayScale.SetGray(start + (1 - start) * f ), 0.25f));
        } else
        {
            StopAllCoroutines();
            Noise.enabled = false;
            StartCoroutine(M_Extensions.YieldT(f => m_GrayScale.SetGray( start * ( 1- f)) , 0.25f));
        }
    }

    void OnDestroy()
    {
        if (TurnSystem.Instance != null)
        {
            TurnSystem.Instance.OnStartTurn -= OnStartTurn;
        }
    }
}
