using System.Collections.Generic;
using AvatarLoader;
using ReadyPlayerMe;
using UnityEngine;

public class AvatarCashes : MonoBehaviour
{
    public string SelectedAvatarUrl { get; set; }

    public Dictionary<string, AvatarRenderModel> PlayerAvatars2d { get; set; } =
        new Dictionary<string, AvatarRenderModel>();


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

        return ch.gameObject;
    }


    public void AddAvatar(string url, AvatarRenderModel dpa)
    {
        if (PlayerAvatars2d.ContainsKey(url))
            PlayerAvatars2d[url] = dpa;
        else
            PlayerAvatars2d.Add(url, dpa);
    }

    public void AddAvatar2d(string url, AvatarRenderModel avatar2d)
    {
        if (PlayerAvatars2d.ContainsKey(url))
        {
            PlayerAvatars2d[url] = avatar2d;
        }
        else
        {
            PlayerAvatars2d.Add(url, avatar2d);
        }
    }


    public void RemoveAvatar(string url)
    {
        PlayerAvatars2d.Remove(url);
    }

    public void Clear()
    {
        PlayerAvatars2d.Clear();
    }


    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
}