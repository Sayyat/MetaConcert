using System;
using System.Collections.Generic;
using System.Linq;
using AvatarLoader;
using ReadyPlayerMe;
using UnityEngine;

public class AvatarCashes : MonoBehaviour
{
    public string SelectedAvatarUrl { get; set; }

    public Dictionary<string, AvatarRenderModel> PlayerAvatars2d { get; set; } 

    private void Awake()
    {
        PlayerAvatars2d = new Dictionary<string, AvatarRenderModel>();
        DontDestroyOnLoad(this);
    }

    public void SaveAvatarInCash(GameObject avatar, string url)
    {
        avatar.name = ShortenUrl(url);
        avatar.transform.parent = transform;
        avatar.SetActive(false);
    }

    public string ShortenUrl(string url)
    {
        return url.Substring(38, 24);
    }

    public bool HasAvatar2d(string url)
    {
        return PlayerAvatars2d.ContainsKey(url) && PlayerAvatars2d[url].Url == url;
    }
    
    public bool HasAvatar3d(string url)
    {
        var children = Utils.GetChildren(gameObject);
        
        return children.Exists(_ => _.gameObject.name == ShortenUrl(url));
    }

    public GameObject GetAvatar(string url)
    {
        
        // todo add load from ReadyPlayer if avatar not found in cashes
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


    public static class Utils
    {
        public static List<GameObject> GetChildren(GameObject go)
        {
            List<GameObject> list = new List<GameObject>();
            return GetChildrenHelper(go, list);
        }

        private static List<GameObject> GetChildrenHelper(GameObject go, List<GameObject> list)
        {
            if (go == null || go.transform.childCount == 0)
            {
                return list;
            }

            foreach (Transform t in go.transform)
            {
                list.Add(t.gameObject);
            }

            return list;
        }
    }
}