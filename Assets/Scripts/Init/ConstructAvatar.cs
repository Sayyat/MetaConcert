using System;
using AvatarLoader;
using Photon.Pun;
using ReadyPlayerMe;
using TMPro;
using UnityEngine;

namespace Init
{
    [RequireComponent(typeof(Animator))]
    public class ConstructAvatar : MonoBehaviour, IPunInstantiateMagicCallback
    {
        [SerializeField] private GameObject defaultAvatar;
        [SerializeField] private TextMeshPro progress;

        private Animator _animator;
        private Avatar _avatarScheme;
        private ReadyPlayerMe.AvatarLoader _avatarLoader;
        private string _currentAvatarUrl;
        private DataPlayerAvatar _dataPlayerAvatar;
        public string CurrentAvatarUrl
        {
            get => _currentAvatarUrl;
            set
            {
                _currentAvatarUrl = value;
                LoadAvatar();
            }
        }
        

        private void Awake()
        {
            _dataPlayerAvatar = GameObject.Find("DataPlayerAvatar").GetComponent<DataPlayerAvatar>();
            _currentAvatarUrl = _dataPlayerAvatar.Avatar2d.Url;
            _animator = GetComponent<Animator>();
            
        }

        public void OnPhotonInstantiate(PhotonMessageInfo info)
        {
            LoadAvatar();
        }

        private void LoadAvatar()
        {
            _avatarLoader = new ReadyPlayerMe.AvatarLoader();

            _avatarLoader.OnCompleted += ConstructOnSuccess;
            _avatarLoader.OnProgressChanged += ProgressChanged;
            _avatarLoader.OnFailed += ConstructOnFailed;

            _avatarLoader.LoadAvatar(_currentAvatarUrl);
        }

        private void ProgressChanged(object sender, ProgressChangeEventArgs e)
        {
            float p = e.Progress * 100;
            progress.text = p + " %";

            if (e.Progress == 1.0f)
            {
                progress.gameObject.SetActive(false);
            }
        }

        public void ConstructOnSuccess(object sender, CompletionEventArgs args)
        {
            var avatar3d = new AvatarModel()
            {
                Avatar = args.Avatar,
                Metadata = args.Metadata,
                Url = args.Url
            };
            _dataPlayerAvatar.Avatar3d = avatar3d;
            
            Construct(args.Avatar);
            Debug.Log("Avatar loaded successfully");
        }


        public void ConstructOnFailed(object sender, FailureEventArgs args)
        {
            var avatar3d = new AvatarModel()
            {
                Avatar = defaultAvatar
            };
            _dataPlayerAvatar.Avatar3d = avatar3d;

            var go =  Instantiate(defaultAvatar);
            
            Construct(go);
            Debug.Log("Failed to load avatar. Creating default avatar");
        }


        private void Construct(GameObject playerTemplate)
        {
            _avatarScheme = playerTemplate.GetComponent<Animator>().avatar;

            // todo add smart method to grab children
            var mesh = playerTemplate.transform.GetChild(0);
            var armature = playerTemplate.transform.GetChild(1);


            mesh.gameObject.layer = LayerMask.NameToLayer("Player");
            armature.gameObject.layer = LayerMask.NameToLayer("Player");

            mesh.transform.parent = transform;
            armature.transform.parent = transform;


            mesh.transform.position = Vector3.zero;
            armature.transform.position = Vector3.zero;

            Destroy(playerTemplate);

            _animator.avatar = _avatarScheme;
        }

        private void OnDestroy()
        {
            _avatarLoader.OnCompleted -= ConstructOnSuccess;
            _avatarLoader.OnProgressChanged -= ProgressChanged;
            _avatarLoader.OnFailed -= ConstructOnFailed;
        }
    }
}