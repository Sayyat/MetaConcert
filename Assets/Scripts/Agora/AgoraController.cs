using System;
using System.Collections.Generic;
using agora_gaming_rtc;
using agora_utilities;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Agora
{
    public class AgoraController
    {
        public event Action<uint, int, VideoSurface> OtherUserJoined;

        public event Action<string, uint, int, VideoSurface> SelfUserJoined;
        // instance of agora engine

        
        private IRtcEngine mRtcEngine { get; set; }
        private string ChannelName { get; set; }

        private CLIENT_ROLE_TYPE ClientRole { get; set; }

        // private Text ChannelNameLabel { get; set; }

        private const float Offset = 100;

        private bool _testVolumeIndication = false;

        private List<GameObject> remoteUserDisplays = new List<GameObject>();
        protected Dictionary<uint, VideoSurface> UserVideoDict = new Dictionary<uint, VideoSurface>();

        private bool _pubVideo;
        private bool _subVideo;
        private bool _pubAudio;
        private bool _subAudio;

        public void LoadEngine(string appId)
        {
            // start sdk
            Debug.Log("initializeEngine");

            if (mRtcEngine != null)
            {
                Debug.Log("Engine exists. Please unload it first!");
                return;
            }

            // init engine
            mRtcEngine = IRtcEngine.GetEngine(appId);

            // enable log
            mRtcEngine.SetLogFilter(LOG_FILTER.DEBUG | LOG_FILTER.INFO | LOG_FILTER.WARNING | LOG_FILTER.ERROR |
                                    LOG_FILTER.CRITICAL);
        }

        public void JoinAudience(string channelName)
        {
            Debug.Log("calling join (channel = " + channelName + ")");

            // TMPText = mRtcEngine.ToString();

            if (mRtcEngine == null)
                return;
            // set callbacks (optional)
            mRtcEngine.OnJoinChannelSuccess += onJoinChannelSuccess;
            mRtcEngine.OnUserJoined += onUserJoined;
            mRtcEngine.OnUserOffline += onUserOffline;
            mRtcEngine.OnLeaveChannel += OnLeaveChannelHandler;
            mRtcEngine.OnWarning = (int warn, string msg) =>
            {
                Debug.LogWarningFormat("Warning code:{0} msg:{1}", warn, IRtcEngine.GetErrorDescription(warn));
            };
            mRtcEngine.OnError += HandleError;
            mRtcEngine.OnClientRoleChanged += handleOnClientRoleChanged;
            mRtcEngine.OnClientRoleChangeFailed += OnClientRoleChangeFailedHandler;
            mRtcEngine.SetChannelProfile(CHANNEL_PROFILE.CHANNEL_PROFILE_LIVE_BROADCASTING);
            mRtcEngine.SetClientRole(CLIENT_ROLE_TYPE.CLIENT_ROLE_AUDIENCE);
            mRtcEngine.EnableVideo();
            mRtcEngine.EnableVideoObserver();
            mRtcEngine.JoinChannelByKey("", channelName);
            ChannelName = channelName;
            ClientRole = CLIENT_ROLE_TYPE.CLIENT_ROLE_AUDIENCE;
        }


        private void onJoinChannelSuccess(string channelName, uint uid, int elapsed)
        {
            Debug.Log("JoinChannel " + channelName + " Success: uid = " + uid);

            var textVersionGameObject = GameObject.Find("VersionText");
            if (textVersionGameObject)
            {
                textVersionGameObject.GetComponent<Text>().text = "SDK Version : " + IRtcEngine.GetSdkVersion();
            }
            
            var videoObject = MakeQuadSurface(uid.ToString());
            
            // ChannelNameLabel.text = channelName;
            SelfUserJoined.Invoke(channelName, uid, elapsed, videoObject);
        }

        private void onUserJoined(uint uid, int elapsed)
        {
            Debug.Log("onUserJoined: uid = " + uid + " elapsed = " + elapsed);
            // this is called in main thread

            // TMPText = mRtcEngine.ToString();
            // find a game object to render video stream from 'uid'
            var go = GameObject.Find(uid.ToString());
            if (!ReferenceEquals(go, null))
            {
                // TMPText = "TWO UID";
                return; // reuse
            }

            // create a GameObject and assign to this new user
            var videoSurface = MakeQuadSurface(uid.ToString());
            if (!ReferenceEquals(videoSurface, null))
            {
                // configure videoSurface
                videoSurface.SetForUser(uid);
                videoSurface.SetEnable(true);
                videoSurface.SetVideoSurfaceType(AgoraVideoSurfaceType.Renderer);

                remoteUserDisplays.Add(videoSurface.gameObject);
                UserVideoDict[uid] = videoSurface;
            }

            if (videoSurface != null)
            {
                // TMPText = "Exist VS";
                // TMPText = videoSurface.transform.position.ToString();
                // TMPText = videoSurface.transform.localPosition.ToString();
            }
            
            OtherUserJoined.Invoke(uid, elapsed, videoSurface);
        }

        private void onUserOffline(uint uid, USER_OFFLINE_REASON reason)
        {
            // remove video stream
            Debug.Log("onUserOffline: uid = " + uid + " reason = " + reason);
            // this is called in main thread
            GameObject go = GameObject.Find(uid.ToString());
            if (!ReferenceEquals(go, null))
            {
                Object.Destroy(go);
            }
        }

        private void OnLeaveChannelHandler(RtcStats stats)
        {
            Debug.LogFormat("OnLeaveChannelSuccess ---- duration = {0} txVideoBytes:{1} ", stats.duration,
                stats.txVideoBytes);
            // Clean up the displays
            foreach (var go in remoteUserDisplays)
            {
                GameObject.Destroy(go);
            }

            remoteUserDisplays.Clear();
        }

        public void Join(string channelName, string token, bool enableVideo, bool muted = false)
        {
            Debug.Log("calling join (channel = " + channelName + ")");

            // TMPText = mRtcEngine.ToString();

            if (mRtcEngine == null)
                return;

            // SetupInitState();

            // set callbacks (optional)
            mRtcEngine.OnJoinChannelSuccess += onJoinChannelSuccess;
            mRtcEngine.OnUserJoined = onUserJoined;
            mRtcEngine.OnUserOffline += onUserOffline;
            mRtcEngine.OnLeaveChannel += OnLeaveChannelHandler;
            mRtcEngine.OnWarning += (int warn, string msg) =>
            {
                Debug.LogWarningFormat("Warning code:{0} msg:{1}", warn, IRtcEngine.GetErrorDescription(warn));
            };
            mRtcEngine.OnError += HandleError;

            mRtcEngine.OnUserMutedAudio += OnUserMutedAudio;
            mRtcEngine.OnUserMuteVideo += OnUserMutedVideo;
            //   mRtcEngine.OnVolumeIndication = OnVolumeIndicationHandler;
            mRtcEngine.OnClientRoleChanged += handleOnClientRoleChanged;
            mRtcEngine.OnClientRoleChangeFailed += OnClientRoleChangeFailedHandler;
            mRtcEngine.OnVideoSizeChanged += OnVideoSizeChangedHandler;

            mRtcEngine.SetChannelProfile(CHANNEL_PROFILE.CHANNEL_PROFILE_LIVE_BROADCASTING);
            mRtcEngine.SetClientRole(CLIENT_ROLE_TYPE.CLIENT_ROLE_BROADCASTER);

            // Turn this on to receive volumenIndication
            if (_testVolumeIndication)
            {
                mRtcEngine.EnableAudioVolumeIndication(500, 8, report_vad: true);
            }

            var _orientationMode = ORIENTATION_MODE.ORIENTATION_MODE_FIXED_LANDSCAPE;

            VideoEncoderConfiguration config = new VideoEncoderConfiguration
            {
                orientationMode = _orientationMode,
                degradationPreference = DEGRADATION_PREFERENCE.MAINTAIN_FRAMERATE,
                mirrorMode = VIDEO_MIRROR_MODE_TYPE.VIDEO_MIRROR_MODE_DISABLED
                // note: mirrorMode is not effective for WebGL
            };
            mRtcEngine.SetVideoEncoderConfiguration(config);

            // enable video
            if (enableVideo)
            {
                mRtcEngine.EnableVideo();
                // allow camera output callback
                mRtcEngine.EnableVideoObserver();
            }
            else
            {
                // AudioVideoState.subVideo = false;
                // AudioVideoState.pubVideo = false;
            }

            // NOTE, we use the third button to invoke JoinChannelByKey
            // otherwise, it joins using JoinChannelWithUserAccount
            if (muted)
            {
                // mute locally only. still subscribing
                mRtcEngine.EnableLocalAudio(false);
                mRtcEngine.MuteLocalAudioStream(true);
                mRtcEngine.JoinChannelByKey(channelKey: null, channelName: channelName, info: "", uid: 0);
            }
            else
            {
                // join channel with string user name
                // ************************************************************************************* 
                // !!!  There is incompatibiity with string Native UID and Web string UIDs !!!
                // !!!  We strongly recommend to use uint uid only !!!!
                // mRtcEngine.JoinChannelWithUserAccount(null, channel, "user" + Random.Range(1, 99999),
                // ************************************************************************************* 
                mRtcEngine.JoinChannel(
                    token: token,
                    channelId: channelName,
                    info: "",
                    uid: 0,
                    options: new ChannelMediaOptions(true, true, true, true)
                );
            }

            ChannelName = channelName;
            ClientRole = CLIENT_ROLE_TYPE.CLIENT_ROLE_BROADCASTER;
            // TMPText = "END Joined";
        }

        public void Leave()
        {
            Debug.Log("calling leave");

            if (mRtcEngine == null)
                return;

            // leave channel
            mRtcEngine.LeaveChannel();
            // deregister video frame observers in native-c code
            mRtcEngine.DisableVideoObserver();
        }

        public void UnloadEngine()
        {
            Debug.Log("calling unloadEngine");

            // delete
            if (mRtcEngine != null)
            {
                IRtcEngine.Destroy(); // Place this call in ApplicationQuit
                mRtcEngine = null;
            }
        }
        

        public void EnableVideo(bool paused)
        {
            if (mRtcEngine != null)
            {
                if (!paused)
                {
                    mRtcEngine.EnableVideo();
                }
                else
                {
                    mRtcEngine.DisableVideo();
                }
            }
        }

        public VideoSurface MakeQuadSurface(string goName)
        {
            var go = Object.Instantiate(Resources.Load("Quad") as GameObject);

            if (go == null)
            {
                return null;
            }

            go.name = goName;

            go.transform.Rotate(0f, 0.0f, 180.0f);
            // var xPos = Random.Range(Offset - Screen.width / 2f, Screen.width / 2f - Offset);
            // var yPos = Random.Range(Offset, Screen.height / 2f - Offset);
            go.transform.localPosition = new Vector3(-1, 1.3f, -3.26f);


            var videoSurface = go.AddComponent<VideoSurface>();
            return videoSurface;
        }

        public VideoSurface MakeImageSurface(string goName)
        {
            var go = new GameObject();

            if (go == null)
            {
                // TMPText = "NO GO";
                return null;
            }

            go.name = goName;

            // to be renderered onto
            go.AddComponent<RawImage>();

            // make the object draggable
            go.AddComponent<UIElementDragger>();


            GameObject canvas = GameObject.Find("Canvas");
            // TMPText = canvas.GetComponent<RectTransform>().rect.ToString();

            if (canvas == null)
                // TMPText = "NO CANVAS";

                if (canvas != null)
                {
                    go.transform.SetParent(canvas.transform);
                }

            // set up transform
            go.transform.Rotate(0f, 0.0f, 180.0f);
            var xPos = Random.Range(Offset - Screen.width / 2f, Screen.width / 2f - Offset);
            var yPos = Random.Range(Offset, Screen.height / 2f - Offset);
            go.transform.localPosition = new Vector3(xPos, yPos, 0f);

            // configure videoSurface
            var videoSurface = go.AddComponent<VideoSurface>();
            return videoSurface;
        }


        #region ChangesStats

        private void OnUserMutedAudio(uint uid, bool muted)
        {
            Debug.LogFormat("user {0} muted audio:{1}", uid, muted);
        }

        private void OnUserMutedVideo(uint uid, bool muted)
        {
            Debug.LogFormat("user {0} muted video:{1}", uid, muted);
        }

        private void OnClientRoleChangeFailedHandler(CLIENT_ROLE_CHANGE_FAILED_REASON reason,
            CLIENT_ROLE_TYPE currentRole)
        {
            Debug.Log("Engine OnClientRoleChangeFaile: " + reason + " c-> " + currentRole);
        }

        void handleOnClientRoleChanged(CLIENT_ROLE_TYPE oldRole, CLIENT_ROLE_TYPE newRole)
        {
            Debug.Log("Engine OnClientRoleChanged: " + oldRole + " -> " + newRole);
        }

        private float EnforcingViewLength = 360f;

        void OnVideoSizeChangedHandler(uint uid, int width, int height, int rotation)
        {
            Debug.LogWarning(string.Format("OnVideoSizeChangedHandler, uid:{0}, width:{1}, height:{2}, rotation:{3}",
                uid,
                width, height, rotation));
            if (UserVideoDict.ContainsKey(uid))
            {
                GameObject go = UserVideoDict[uid].gameObject;
                Vector2 v2 = new Vector2(width, height);
                RawImage image = go.GetComponent<RawImage>();
                v2 = AgoraUIUtils.GetScaledDimension(width, height, EnforcingViewLength);

                if (rotation == 90 || rotation == 270)
                {
                    v2 = new Vector2(v2.y, v2.x);
                }

                image.rectTransform.sizeDelta = v2;
            }
        }

        #endregion


        #region Error Handling

        private Text MessageText { get; set; }
        private int LastError { get; set; }

        private void HandleError(int error, string msg)
        {
            if (error == LastError)
            {
                return;
            }

            msg = string.Format("Error code:{0} msg:{1}", error, IRtcEngine.GetErrorDescription(error));

            switch (error)
            {
                case 101:
                    msg +=
                        "\nPlease make sure your AppId is valid and it does not require a certificate for this demo.";
                    break;
            }

            Debug.LogError(msg);
            if (MessageText != null)
            {
                if (MessageText.text.Length > 0)
                {
                    msg = "\n" + msg;
                }

                MessageText.text += msg;
            }

            LastError = error;
        }

        #endregion
    }
}