using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AvatarLoader;
using ReadyPlayerMe;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class AvatarRenderController : IDisposable
{
    private IAvatarRenderView _renderView;
    private List<string> _urls;
    private AvatarCashes _avatarCashes;

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
    private PanelControl _panelControl;

    public AvatarRenderController(IAvatarRenderView renderView, List<string> urls, AvatarCashes avatarCashes,
        PanelControl panelControl)
    {
        _renderView = renderView;
        _urls = urls;
        _avatarCashes = avatarCashes;
        _panelControl = panelControl;

        countLoading = _urls.Count;

        _renderView.OnSelected += SelectModel;

        _avatarCashes.SelectedAvatarUrl = "https://api.readyplayer.me/v1/avatars/6360d011fff3a4d4797b7cf1.glb";

        //
        // foreach (var url in urls)
        // {
        //     LoadAvatarRender(url);
        // }
    }

    private void SelectModel(string url)
    {
        var model2d = _modelsRender.Find(model => model.Url == url);
        
        // _avatarCashes.AddAvatar(url, model2d);

        _selectedUrl = url;
        _avatarCashes.SelectedAvatarUrl = url;

        _renderView.SelectButton(url);
        Debug.Log(_selectedUrl);
    }

    public AvatarRenderLoader LoadAvatarRender(string url)
    {
        var loader = new AvatarRenderLoader();
        // try to load from cash
        if (_avatarCashes.HasAvatar2d(url))
        {
            _modelsRender.Add(_avatarCashes.PlayerAvatars2d[url]);
            DecreaseCount();
            return loader;
        }

        // url not found from cash, downloading 


        _loaders.Add(loader);
        var model = new AvatarRenderModel
        {
            Url = url
        };

        loader.LoadRender(url, _scene, _blendShapeMesh, blendShapes);
        loader.OnCompleted += (texture2D =>
        {
            model.texture = texture2D;
            _modelsRender.Add(model);
            _avatarCashes.AddAvatar(url, model);

            DecreaseCount();
        });
        loader.OnFailed += (type, s) =>
        {
            Debug.LogError(s);
            DecreaseCount();
        };
        return loader;
    }

    private void DecreaseCount()
    {
        countLoading--;

        _panelControl.Progress = 1f * (_urls.Count - countLoading) / _urls.Count;
        if (countLoading <= 0)
        {
            Debug.Log("SetupAllIcon");
        }
    }

    public void SetupAllIcons()
    {
        // stop preloaderVideo and change ui
        _panelControl.StopPreloaderVideo();

        _renderView.LoaderAvatars.SetActive(false);

        for (int i = 0; i < _avatarCashes.PlayerAvatars2d.Count; i++)
        {
            _renderView.SetupTexture(_avatarCashes.PlayerAvatars2d[_urls[i]].texture, _urls[i]);
        }
        // foreach (var renderModel in _modelsRender)
        // {
        //     _renderView.SetupTexture(renderModel.texture, renderModel.Url);
        // }
    }

    public void Dispose()
    {
        Debug.Log("Dispose");
        foreach (var loader in _loaders)
        {
            loader.OnCompleted = null;
            loader.OnFailed = null;
        }

        _renderView.OnSelected -= SelectModel;
    }
}