using System;
using System.Collections;
using System.Collections.Generic;
using AvatarLoader;
using ReadyPlayerMe;
using UnityEngine;
using UnityEngine.UI;

public class AvatarRenderController
{
    private IAvatarRenderView _renderView;
    private List<string> _urls;

    private string _selectedUrl;
    private readonly List<AvatarRenderModel> _modelsRender = new List<AvatarRenderModel>();

    private const string _blendShapeMesh = "Wolf3D_Avatar";
    private const AvatarRenderScene _scene = AvatarRenderScene.PortraitTransparent;

    private Dictionary<string, float> blendShapes = new Dictionary<string, float>
    {
        {"mouthSmile", 0.7f},
        {"viseme_aa", 0.5f},
        {"jawOpen", 0.3f}
    };

    private int countLoading;

    public AvatarRenderController(IAvatarRenderView renderView,
        List<string> urls)
    {
        _renderView = renderView;
        _urls = urls;

        countLoading = _urls.Count;

        renderView.OnSelected += (url) =>
        {
            _selectedUrl = url;
            Debug.Log(_selectedUrl);
        };
        _selectedUrl = "defaultUrl";

        foreach (var url in urls)
        {
            LoadAvatarRender(url, _scene);
        }
    }

    private void LoadAvatarRender(string url, AvatarRenderScene scene)
    {
        var loader = new AvatarRenderLoader();
        var model = new AvatarRenderModel
        {
            Url = url
        };

        loader.LoadRender(url, scene, _blendShapeMesh, blendShapes);

        loader.OnCompleted = (texture2D =>
        {
            model.texture = texture2D;
            _modelsRender.Add(model);
            
            countLoading--;
            if (countLoading <= 0)
            {
                Debug.Log("SetupAllIcon");
                SetupAllIcons();
            }
        });
    }

    private void SetupAllIcons()
    {
        foreach (var renderModel in _modelsRender)
        {
            _renderView.SetupTexture(renderModel.texture, renderModel.Url);
        }
    }

    //
    // private void LoadAvatar(string avatarUrl)
    // {
    //     var avatarLoader = new ReadyPlayerMe.AvatarLoader();
    //     avatarLoader.OnCompleted += (_, args) =>
    //     {
    //         var avatar = args.Avatar;
    //         AvatarAnimatorHelper.SetupAnimator(args.Metadata.BodyType, avatar);
    //     };
    //     avatarLoader.LoadAvatar(avatarUrl);
    // }

    // private void OnDestroy()
    // {
    //     if (avatarsIcons != null)
    //     {
    //         foreach (var avatarsIcon in avatarsIcons)
    //         {
    //             Destroy(avatarsIcon);
    //         }
    //
    //         foreach (var avatarModel in avatarsModels)
    //         {
    //             avatarModel.onCompletedSaving -= InstantSprite;
    //         }
    //     }
    // }
}