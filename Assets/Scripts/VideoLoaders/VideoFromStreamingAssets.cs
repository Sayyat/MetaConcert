using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace VideoLoaders
{
    public class VideoFromStreamingAssets : MonoBehaviour
    {
        [SerializeField] private List<VideoPlayer> videoPlayers;
        [SerializeField] private List<string> urls;

        private string _prefix = "file://";

        private void Start()
        {
#if UNITY_EDITOR && UNITY_STANDALONE
            _prefix = "file://";
#elif UNITY_WEBGL
            _prefix = "https://";
#elif UNITY_ANDROID || UNITY_IOS
            _prefix = "";
#else
            _prefix = "file://";
#endif

            for (var i = 0; i < videoPlayers.Count; i++)
            {
                var path = Path.Combine(_prefix, Application.streamingAssetsPath, $"{urls[i]}.mp4");
                videoPlayers[i].url = path;
            }
        }
    }
}