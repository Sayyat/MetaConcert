using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickOnSite : MonoBehaviour
{
    private void OnMouseDown()
    {
        Debug.Log("clockOnSite");
        Application.OpenURL("https://asyladam.shop/");
    }
}
