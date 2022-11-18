﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingParticleArrayController : MonoBehaviour
{
    public List<Transform> affectors;

    private Vector4[] positions;
    private ParticleSystemRenderer psr;

    void Start()
    {
        psr = GetComponent<ParticleSystemRenderer>();
        Vector4[] maxArray = new Vector4[20];
        psr.material.SetVectorArray("_Affectors", maxArray);
    }

    // Sending an array of positions to particle shader
    void Update()
    {
        if (affectors.Count < 1)
            return;

        positions = new Vector4[affectors.Count];
        for (int i = 0; i < positions.Length; i++)
        {
            positions[i] = affectors[i].position;
        }

        psr.material.SetVectorArray("_Affectors", positions);
        psr.material.SetInt("_AffectorCount", affectors.Count);
    }

    public void AddAffector(Transform otherTransform)
    {
       affectors.Add(otherTransform);
    }

    public void RemoveAffector(Transform otherTransform)
    {
        affectors.Remove(otherTransform);
    }
}