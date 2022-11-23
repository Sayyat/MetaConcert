using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AvatarLoader;
using ReadyPlayerMe;
using UnityEngine;
using UnityEngine.UI;

public class AvatarRenderController : IDisposable
{
    private IAvatarRenderView _renderView;
    private List<string> _urls;
    private DataPlayerAvatar _dataPlayerAvatar;

    private List<AvatarRenderLoader> _loaders = new List<AvatarRenderLoader>();

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

    public AvatarRenderController(IAvatarRenderView renderView, List<string> urls, DataPlayerAvatar dataPlayerAvatar)
    {
        _renderView = renderView;
        _urls = urls;
        _dataPlayerAvatar = dataPlayerAvatar;

        countLoading = _urls.Count;

        _renderView.OnSelected += SelectModel;
        
        //Default loading Avatar without selected
        _selectedUrl = "https://api.readyplayer.me/v1/avatars/635e103a1260644e7e39a393.glb";
        _dataPlayerAvatar.Avatar2d = new AvatarRenderModel()
        {
            texture = Texture2D.grayTexture,
            Url = _selectedUrl
        };


        foreach (var url in urls)
        {
            LoadAvatarRender(url);
        }
    }

    private void SelectModel(string url)
    {
        _dataPlayerAvatar.Avatar2d = _modelsRender.Find(model => model.Url == url);

        _selectedUrl = url;

        _renderView.SelectButton(url);
        Debug.Log(_selectedUrl);
    }

    private void LoadAvatarRender(string url)
    {
        var loader = new AvatarRenderLoader();
        _loaders.Add(loader);
        var model = new AvatarRenderModel
        {
            Url = url
        };

        loader.LoadRender(url, _scene, _blendShapeMesh, blendShapes);

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
        _renderView.LoaderAvatars.SetActive(false);
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

    public void Dispose()
    {
        Debug.Log("Dispose");
        foreach (var loader in _loaders)
        {
            loader.OnCompleted = null;
        }

        _renderView.OnSelected -= SelectModel;
    }
}