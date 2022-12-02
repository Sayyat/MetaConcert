using System.Collections;
using System.Collections.Generic;
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
                if (_avatarCashes.HasAvatar2d(url))
                {
                    continue;
                }

                _loading = true;

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
                    Debug.LogError($"Avatar {url} Loaded 2D");
                };
                loaderRender.OnFailed += (j, i) =>
                {
                    _loading = false;
                    Debug.LogError($"Avatar {url}  NOT Loaded 2D with string: {i}");
                };

                _avatarCashes.ConstructOnSuccess(sender, args);
                Debug.LogError($"Avatar {url} Loaded 3D");
            };
            loader.OnFailed += (sender, args) =>
            {
                _loading = false;
                Debug.LogError($"Error loading avatar: {args.Message}");
            };
            loader.LoadAvatar(url);
        }
    }
}