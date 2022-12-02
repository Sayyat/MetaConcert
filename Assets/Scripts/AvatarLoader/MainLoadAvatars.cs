using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace AvatarLoader
{
    public class MainLoadAvatars
    {
        private readonly AvatarCashes _avatarCashes;
        private readonly PanelControl _panelController;
        private readonly AvatarRenderController _avatarRenderController;

        private bool _loading;
        private bool _loadFromUrl;
        private GameObject _avatarFromResources;
        private Texture2D _avatarRendererFromResources;

        public MainLoadAvatars(AvatarCashes avatarCashes, PanelControl panelController,
            AvatarRenderController avatarRenderController)
        {
            _avatarCashes = avatarCashes;
            _panelController = panelController;
            _avatarRenderController = avatarRenderController;
        }

        public IEnumerator LoadAvatars(HashSet<string> urlSet)
        {
            _loading = false;

            foreach (var url in urlSet)
            {
                var shortUrl = _avatarCashes.ShortenUrl(url);

                var has3dCash = false;
                var has2dCash = false;

                has3dCash = _avatarCashes.HasAvatar3d(url);
                has2dCash = _avatarCashes.HasAvatar2d(url);

                if (has2dCash && has3dCash)
                {
                    continue;
                }

                if (!_loadFromUrl && TryLoadAvatarFromResource(shortUrl))
                {
                    if (!has3dCash)
                    {
                        _avatarCashes.SaveAvatarInCash(_avatarFromResources, url);
                        _avatarFromResources = null;
                    }

                    if (has2dCash) continue;
                    TryLoadAvatarRendererFromResource(shortUrl);
                    
                    _avatarCashes.PlayerAvatars2d.Add(url, new AvatarRenderModel(){Url = url, texture = _avatarRendererFromResources});
                    // _loading = true;
                    //
                    // //todo Make async Load method for changed _loading in this method
                    // LoadAvatarTexture(url);
                    //
                    // yield return new WaitUntil(() => !_loading);
                }
                else
                {
                    _loading = true;
                    //todo Make async Load method for changed _loading in this method
                    LoadOfUrl(url);

                    yield return new WaitUntil(() => !_loading);
                }
            }

            _avatarRenderController.SetupAllIcons();
            _panelController.StopPreloaderVideo();
            Debug.LogError("All Avatar Loaded");
        }


        private void LoadOfUrl(string url)
        {
            var loader = new ReadyPlayerMe.AvatarLoader();

            loader.Timeout = 30;
#if UNITY_WEBGL && !UNITY_EDITOR
                loader.Timeout = 100;
#endif

            loader.OnCompleted += (sender, args) =>
            {
                LoadAvatarTexture(url);

                _avatarCashes.SaveAvatarInCash(args.Avatar, args.Url);
                Debug.Log($"Avatar {url} Loaded 3D");
            };
            loader.OnFailed += (sender, args) =>
            {
                _loading = false;
                Debug.LogError($"Error loading avatar: {args.Message}");
            };
            loader.LoadAvatar(url);
        }

        private void LoadAvatarTexture(string url)
        {
            var loaderRender = _avatarRenderController.LoadAvatarRender(url);
            loaderRender.OnCompleted += (_) =>
            {
                _loading = false;
                Debug.Log($"Avatar {url} Loaded 2D");
            };
            loaderRender.OnFailed += (j, i) =>
            {
                _loading = false;
                Debug.Log($"Avatar {url}  NOT Loaded 2D with string: {i}");
            };
        }

        private bool TryLoadAvatarFromResource(string url)
        {
            var line = $"{url}";
            var go = Resources.Load<GameObject>(line);

            if (go == null)
            {
                return false;
            }

            _avatarFromResources = GameObject.Instantiate(go);
            return true;
        }
        
        private bool TryLoadAvatarRendererFromResource(string url)
        {
           var line = $"{url}";
           var texture = Resources.Load<Texture2D>(line);
           
           if (texture == null)
           {
               return false;
           }

           _avatarRendererFromResources = texture;
           return true;
        }
    }
}