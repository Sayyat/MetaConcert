using System;
using System.Collections;
using System.Collections.Generic;
using ReadyPlayerMe;
using UnityEngine;

public class LoadAvatar : MonoBehaviour
{
    private AvatarLoader _avatarLoader;
    private void Start()
    {
        _avatarLoader = new AvatarLoader();
        _avatarLoader.OnCompleted += (sender, args) =>
        {
            
        };

        _avatarLoader.OnFailed += (sender, args) =>
        {

        };

        _avatarLoader.OnProgressChanged += (sender, args) =>
        {

        };
        
    }

    public void Load(string url)
    {
        _avatarLoader.LoadAvatar(url);
    }

    private void Update()
    {
        
    }
}