using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorChecker : MonoBehaviour
{
    [SerializeField] private LivingParticleArrayController arrayControllers;

    private void Start()
    {
        // if (arrayControllers == null)
        // {
        //     arrayControllers = GetComponent<LivingParticleArrayController>();
        // }
    }

    private void OnTriggerEnter(Collider other)
    {     
        if (CheckLayerMask(other.gameObject, LayerMask.NameToLayer("Player")))
        {
            Debug.Log("Player entered");
        }
        arrayControllers.AddAffector(other.transform);
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (CheckLayerMask(other.gameObject, LayerMask.NameToLayer("Player")))
        {
            Debug.Log("Player exited");
        }

        arrayControllers.RemoveAffector(other.transform);
        
    }


    public bool CheckLayerMask(GameObject obj, LayerMask layers)
    {
        if (((1 << obj.layer) & layers) != 0)
        {
            return true;
        }

        return false;
    }
}