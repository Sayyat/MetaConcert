using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AvatarLoader
{
    public class AvatarRenderView : MonoBehaviour, IAvatarRenderView
    {
        public event Action<string> OnSelected; 
    
        private Dictionary<GameObject,string > _icons = new Dictionary<GameObject, string>();

        public void SetupTexture(Texture2D texture, string url)
        {
            var go = new GameObject(url);
            go.transform.parent = transform;
            var sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(.5f, .5f));
        
            go.AddComponent<Image>().sprite = sprite;
            _icons.Add(go, url);
        
            go.AddComponent<Button>().onClick.AddListener(() => OnSelected.Invoke(_icons[go]));
        }
    }
}
