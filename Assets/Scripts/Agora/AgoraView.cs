using System;
using System.Collections;
using System.Collections.Generic;
using Agora;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AgoraView : MonoBehaviour
{
    // Use this for initialization
#if (UNITY_2018_3_OR_NEWER && UNITY_ANDROID)
    private ArrayList permissionList = new ArrayList();
#endif
    public AgoraController _controller { get; set; }
    public event Action IsJoin;

    // PLEASE KEEP THIS App ID IN SAFE PLACE
    // Get your own App ID at https://dashboard.agora.io/
    [SerializeField] private string appID = "your_appid";
    [SerializeField] private string channelName = "your_appid";
    [SerializeField] private string appToken = "your_token";
    
    public GameObject Quad { get; set; }
    private string ChannelName
    {
        get
        {
            string cached = PlayerPrefs.GetString("ChannelName");
            if (string.IsNullOrEmpty(cached))
            {
                cached = "Alem";
            }

            return cached;
        }

        set
        {
            PlayerPrefs.SetString("ChannelName", value);
        }
    }


    void Awake()
    {
#if (UNITY_2018_3_OR_NEWER && UNITY_ANDROID)
		permissionList.Add(Permission.Microphone);         
		permissionList.Add(Permission.Camera);               
#endif
        // keep this alive across scenes
        DontDestroyOnLoad(this.gameObject);
    }
    
    
    
    private void Start()
    {
        CheckAppId();
    }
    
//Temporary button on Scene
    public void JoinVideo()
    {
        OnJoinButtonClicked(true, false);
    }
    
    private void Update()
    {
        CheckPermissions();
    }
    
    /// <summary>
    ///   Checks for platform dependent permissions.
    /// </summary>
    private void CheckPermissions()
    {
#if (UNITY_2018_3_OR_NEWER && UNITY_ANDROID)
        foreach(string permission in permissionList)
        {
            if (!Permission.HasUserAuthorizedPermission(permission))
            {                 
				Permission.RequestUserPermission(permission);
			}
        }
#endif
    }
    
    private void CheckAppId()
    {
        Debug.Assert(appID.Length > 10, "Please fill in your AppId first on Game Controller object.");
    }
    
    // public void OnJoinAudience()
    // {
    //     // create app if nonexistent
    //     if (ReferenceEquals(_controller, null))
    //     {
    //         // Quad = GameObject.CreatePrimitive(PrimitiveType.Quad);
    //         _controller = new AgoraController(); // create app
    //         // _controller.tmp = debugField;
    //         _controller.LoadEngine(appID); // load engine
    //     }
    //
    //     ChannelName = channelName;
    //     _controller.JoinAudience(ChannelName);
    //     
    //         OnLevelFinishedLoading();
    //     
    //     // SceneManager.sceneLoaded += OnLevelFinishedLoading; // configure GameObject after scene is loaded
    //     // SceneManager.LoadScene(_firstSceneName, LoadSceneMode.Single);
    // }

    public void OnJoinButtonClicked(bool enableVideo, bool muted = false)
    {
        // create app if nonexistent
        if (ReferenceEquals(_controller, null))
        {
            Quad = GameObject.CreatePrimitive(PrimitiveType.Quad);
            var rotationQuad = Quad.transform.rotation;
            rotationQuad =Quaternion.Euler(new Vector3(rotationQuad.x, rotationQuad.y, 180));
            Quad.transform.rotation = rotationQuad;
            
            _controller = new AgoraController(Quad); // create app
            // _controller.tmp = debugField;
            _controller.LoadEngine(appID); // load engine
        }

        ChannelName = channelName;
        _controller.Join(ChannelName, appToken, enableVideo, muted);
        
        var currentScene = SceneManager.GetActiveScene();
       
            OnLevelFinishedLoading();
        
        // SceneManager.sceneLoaded += OnLevelFinishedLoading; // configure GameObject after scene is loaded
        // SceneManager.LoadScene(PlaySceneName, LoadSceneMode.Single);
        IsJoin.Invoke();
    }

    public void onLeaveButtonClicked()
    {
        if (!ReferenceEquals(_controller, null))
        {
            _controller.Leave(); // leave channel
            _controller.UnloadEngine(); // delete engine
            _controller = null; // delete app
            // SceneManager.LoadScene(HomeSceneName, LoadSceneMode.Single);
        }
        Destroy(gameObject);
    }
    
    public void OnLevelFinishedLoading()
    {
      
            if (!ReferenceEquals(_controller, null))
            {
                _controller.OnSceneVideoLoaded(); // call this after scene is loaded
            }
            // SceneManager.sceneLoaded -= OnLevelFinishedLoading;
        
    }
    
    private void OnApplicationPause(bool paused)
    {
        if (!ReferenceEquals(_controller, null))
        {
            _controller.EnableVideo(paused);
        }
    }

    private void OnApplicationQuit()
    {
        if (!ReferenceEquals(_controller, null))
        {
            _controller.UnloadEngine();
        }
    }
}
