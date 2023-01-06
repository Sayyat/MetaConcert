using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffsetMaterial : MonoBehaviour
{
    private Renderer _rend;
    private Material _material;
    private Vector2 _offsetBaseMap;
    public float _speedX = -0.6f;
    public float _speedY = -0.1f;
    private void Start()
    {
        _rend = GetComponent<Renderer> ();
        _material = _rend.material;
        _material.SetTextureOffset("_BaseMap", new Vector2(float.MaxValue,float.MaxValue));
    }
    
    private void Update()
    {
        _offsetBaseMap = new Vector2(_offsetBaseMap.x + _speedX*Time.deltaTime, _offsetBaseMap.y + _speedY*_speedX*Time.deltaTime);
        _material.SetTextureOffset("_BaseMap", _offsetBaseMap);
    }
}
