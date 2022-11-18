using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorChecker : MonoBehaviour
{
    // [SerializeField] private LivingParticleArrayController[] arrayControllers;
    //
    // private void Start()
    // {
    //     if (arrayControllers == null)
    //     {
    //         arrayControllers = GetComponents<LivingParticleArrayController>();
    //     }
    // }
    //
    // private void OnTriggerEnter(Collider other)
    // {
    //     if (CheckLayerMask(other.gameObject, LayerMask.NameToLayer("Player")))
    //     {
    //         Debug.Log("Player entered");
    //     }
    //
    //     for (int i = 0; i < arrayControllers.Length; i++)
    //     {
    //         arrayControllers[i].AddAffector(other.transform);
    //     }
    // }
    //
    // private void OnTriggerExit(Collider other)
    // {
    //     if (CheckLayerMask(other.gameObject, LayerMask.NameToLayer("Player")))
    //     {
    //         Debug.Log("Player exited");
    //     }
    //
    //     for (int i = 0; i < arrayControllers.Length; i++)
    //     {
    //         arrayControllers[i].RemoveAffector(other.transform);
    //     }
    // }
    //
    //
    // public bool CheckLayerMask(GameObject obj, LayerMask layers)
    // {
    //     if (((1 << obj.layer) & layers) != 0)
    //     {
    //         return true;
    //     }
    //
    //     return false;
    // }
}