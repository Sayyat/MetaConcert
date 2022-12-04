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
    public AgoraController Controller { get; private set; }
    public event Action OnJoinedRoom;

    // PLEASE KEEP THIS App ID IN SAFE PLACE
    // Get your own App ID at https://dashboard.agora.io/
    [SerializeField] private string appID = "your_appid";
    [SerializeField] private string channelName = "your_appid";
    [SerializeField] private string appToken = "your_token";


    private bool IsJoinedRoom { get; set; } = false;
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
        // DontDestroyOnLoad(this.gameObject);
    }
    
    
    
    private void Start()
    {
        CheckAppId();
    }
    
//Temporary button on Scene
    public void JoinRoom()
    {
        // create app if nonexistent
        if (ReferenceEquals(Controller, null))
        {
            Controller = new AgoraController(); // create app
            // _controller.tmp = debugField;
            Controller.LoadEngine(appID); // load engine
        }

        ChannelName = channelName;
        Controller.Join(ChannelName, appToken, true, true);

        IsJoinedRoom = true;

        // SceneManager.sceneLoaded += OnLevelFinishedLoading; // configure GameObject after scene is loaded
        // SceneManager.LoadScene(PlaySceneName, LoadSceneMode.Single);
        OnJoinedRoom?.Invoke();
    }


    public void ToggleVideo()
    {
        if (!IsJoinedRoom)
        {
            JoinRoom();
            return;
        }
        
        Controller.ToggleVideo();
    }

    public void ToggleAudio()
    {
        if (!IsJoinedRoom)
        {
            JoinRoom();
            return;
        }
        Controller.ToggleAudio();
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


    public void Quit()
    {
        if (!ReferenceEquals(Controller, null))
        {
            Controller.Leave(); // leave channel
            Controller.UnloadEngine(); // delete engine
            Controller = null; // delete app
        }
        Destroy(gameObject);
    }
    
    private void OnApplicationPause(bool paused)
    {
        if (!ReferenceEquals(Controller, null))
        {
            Controller.EnableVideo(paused);
        }
    }

    private void OnApplicationQuit()
    {
        Quit();
    }
}
