using System;
using UnityEngine;

namespace AvatarLoader
{
    public interface IAvatarRenderView
    {
       
        public event Action<string> OnSelected; 
        public void SetupTexture(Texture2D texture, string url);
    }
}