using System;
using System.Collections.Generic;
using AvatarLoader;
using ReadyPlayerMe;
using UnityEngine;

public class AvatarCashes : MonoBehaviour
{
    public string SelectedAvatarUrl { get; set; }

    public Dictionary<string, AvatarRenderModel> PlayerAvatars2d { get; set; } 
    
    private List<ReadyPlayerMe.AvatarLoader> _avatarLoaders = new List<ReadyPlayerMe.AvatarLoader>();

    private void Awake()
    {
        PlayerAvatars2d = new Dictionary<string, AvatarRenderModel>();
        DontDestroyOnLoad(this);
    }
    
    public void ConstructOnSuccess(object sender, CompletionEventArgs e)
    {
        var ava = e.Avatar;
        ava.name = ShortenUrl(e.Url);
        ava.transform.parent = transform;
        ava.SetActive(false);
    }

    private string ShortenUrl(string url)
    {
        return url.Substring(38, 24);
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
        return PlayerAvatars2d.ContainsKey(url) && PlayerAvatars2d[url].Url == url;
    }

    public GameObject GetAvatar(string url)
    {
        var sh = ShortenUrl(url);
        Debug.Log($"Try to find {sh}");
        var ch = transform.Find(sh);

        Debug.Log($"Is find {ch.gameObject}");
        return ch.gameObject;
    }


    public void AddAvatar(string url, AvatarRenderModel model)
    {
        if (PlayerAvatars2d.ContainsKey(url))
            PlayerAvatars2d[url] = model;
        else
            PlayerAvatars2d.Add(url, model);
    }

    public void RemoveAvatar(string url)
    {
        PlayerAvatars2d.Remove(url);
    }

    public void Clear()
    {
        PlayerAvatars2d.Clear();
    }


}