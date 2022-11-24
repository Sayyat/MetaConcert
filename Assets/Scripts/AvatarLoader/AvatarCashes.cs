using System;
using System.Collections;
using System.Collections.Generic;
using AvatarLoader;
using UnityEngine;

public class AvatarCashes : MonoBehaviour
{
    public string SelectedAvatarUrl { get; set; }

    public Dictionary<string, DataPlayerAvatar> DataPlayerAvatars { get; set; } =
        new Dictionary<string, DataPlayerAvatar>();

    public bool HasAvatar2d(string url)
    {
        return DataPlayerAvatars.ContainsKey(url) && DataPlayerAvatars[url].Avatar2d.Url == url;
    }

    public bool HasAvatar3d(string url)
    {
        return DataPlayerAvatars.ContainsKey(url) && DataPlayerAvatars[url].Avatar3d.Url == url;
    }


    public void AddAvatar(string url, DataPlayerAvatar dpa)
    {
        if (DataPlayerAvatars.ContainsKey(url))
            DataPlayerAvatars[url] = dpa;
        else
            DataPlayerAvatars.Add(url, dpa);
    }

    public void AddAvatar2d(string url, AvatarRenderModel avatar2d)
    {
        if (DataPlayerAvatars.ContainsKey(url))
        {
            DataPlayerAvatars[url].Avatar2d = avatar2d;
        }
        else
        {
            DataPlayerAvatars.Add(url, new DataPlayerAvatar()
            {
                Avatar2d = avatar2d
            });
        }
    }

    public void AddAvatar3d(string url, AvatarModel avatar3d)
    {
        if (DataPlayerAvatars.ContainsKey(url))
        {
            DataPlayerAvatars[url].Avatar3d = avatar3d;
        }
        else
        {
            DataPlayerAvatars.Add(url, new DataPlayerAvatar()
            {
                Avatar3d = avatar3d
            });
        }
    }


    public void RemoveAvatar(string url)
    {
        DataPlayerAvatars.Remove(url);
    }

    public void Clear()
    {
        DataPlayerAvatars.Clear();
    }


    private void Awake()
    {
        DontDestroyOnLoad(this);
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("Cashed avatars:");
            foreach (var avatars in DataPlayerAvatars)
            {
                Debug.Log(avatars.Key);
                Debug.Log(avatars.Value.Avatar2d.ToString());
                Debug.Log(avatars.Value.Avatar3d.ToString());
            }
        }
    }
}