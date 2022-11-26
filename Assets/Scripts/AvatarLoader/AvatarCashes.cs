using System;
using System.Collections;
using System.Collections.Generic;
using AvatarLoader;
using ReadyPlayerMe;
using UnityEngine;

public class AvatarCashes : MonoBehaviour
{
    public string SelectedAvatarUrl { get; set; }

    public Dictionary<string, DataPlayerAvatar> DataPlayerAvatars { get; set; } =
        new Dictionary<string, DataPlayerAvatar>();


    private List<ReadyPlayerMe.AvatarLoader> _avatarLoaders = new List<ReadyPlayerMe.AvatarLoader>();

    public void PreloadAvatars(List<string> urls)
    {
        foreach (var url in urls)
        {
            PreloadAvatar(url);
        }
    }

    public void PreloadAvatar(string url)
    {
        var avatarLoader = new ReadyPlayerMe.AvatarLoader();

        avatarLoader.OnCompleted += ConstructOnSuccess;
        avatarLoader.LoadAvatar(url);
        _avatarLoaders.Add(avatarLoader);
    }

    private void ConstructOnSuccess(object sender, CompletionEventArgs e)
    {
        var ava = e.Avatar;
        ava.name = ShortenUrl(e.Url);
        ava.transform.parent = transform;
        ava.SetActive(false);
    }


    private string ShortenUrl(string url)
    {
        return url.Substring(38,24);
    }


    private void OnDestroy()
    {
        foreach (var avatarLoader in _avatarLoaders)
        {
            avatarLoader.OnCompleted -= ConstructOnSuccess;
        }
    }


    public bool HasAvatar2d(string url)
    {
        return DataPlayerAvatars.ContainsKey(url) && DataPlayerAvatars[url].Avatar2d.Url == url;
    }

    public GameObject GetAvatar(string url)
    {
        var sh = ShortenUrl(url);
        Debug.Log($"Try to find {sh}");
        var ch = transform.Find(sh);
        
        return ch.gameObject;
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