using System;
using UnityEngine;

namespace AvatarLoader
{
    public interface IAvatarRenderView
    {
        public GameObject LoaderAvatars { get; }
        public event Action<string> OnSelected; 
        public void SetupTexture(Texture2D texture, string url);
        public void SelectButton(string url);
    }
}