using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace VideoLoaders
{
    public class VideoFromStreamingAssets : MonoBehaviour
    {
        [SerializeField] private List<VideoPlayer> videoPlayers;
        [SerializeField] private List<string> urls;

        private void Start()
        {
            for (var i = 0; i < videoPlayers.Count; i++)
            {
                var path = $"{Application.streamingAssetsPath}/{urls[i]}.mp4";
                videoPlayers[i].url = path;
            }
        }
    }
}