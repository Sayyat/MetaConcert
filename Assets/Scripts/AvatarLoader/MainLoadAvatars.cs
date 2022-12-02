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

        public MainLoadAvatars(AvatarCashes avatarCashes, PanelControl panelController, AvatarRenderController avatarRenderController)
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
                LoadAvatarFromResource(url);

                var components = _avatarCashes.gameObject.GetComponentsInChildren<Transform>();
                
                if (_avatarCashes.HasAvatar2d(url))
                {
                    continue;
                }

                _loading = true;
                
//todo Make async Load method for changed _loading in this method
                LoadOfUrl(url);

                yield return new WaitUntil(() => !_loading);
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

        private GameObject LoadAvatarFromResource(string url)
        {
            var go = Resources.Load($"{url}/{url}.prefab") as GameObject;
            
            return go;
        }
    }
}