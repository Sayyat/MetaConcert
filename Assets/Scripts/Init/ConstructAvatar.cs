using System;
using Photon.Pun;
using ReadyPlayerMe;
using TMPro;
using UnityEngine;

namespace Init
{
    [RequireComponent(typeof(Animator))]
    public class ConstructAvatar : MonoBehaviour, IPunInstantiateMagicCallback
    {
        private Animator _animator;
        private InitPlayer _initPlayer;
        [SerializeField] private TextMeshPro progress;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _initPlayer = GameObject.Find("InitPlayer")?.GetComponent<InitPlayer>();
            if (_initPlayer == null)
            {
                _initPlayer = GameObject.Find("InitPlayer(Clone)").GetComponent<InitPlayer>();
            }
        }

        public void OnPhotonInstantiate(PhotonMessageInfo info)
        {
            var avatarLoader = new AvatarLoader();

            avatarLoader.OnCompleted += ConstructOnSuccess;
            avatarLoader.OnProgressChanged += ProgressChanged;
            avatarLoader.OnFailed += ConstructOnFailed;

            // avatarLoader.LoadAvatar("https://api.readyplayer.me/v1/avatars/634f798f7baf0e2c647eee56.glb"); // Sayat avatar
            avatarLoader.LoadAvatar(
                "https://api.readyplayer.me/v1/avatars/634f88797c0746d6b326eec7.glb"); // Konilbay avatar
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
            
            Construct(args.Avatar);
            Debug.Log("Avatar loaded successfully");
        }


        public void ConstructOnFailed(object sender, FailureEventArgs args)
        {
            Construct(_initPlayer.defaultAvatar);
            Debug.Log("Failed to load avatar. Creating default avatar");
        }


        private void Construct(GameObject playerTemplate)
        {
            var child1 = playerTemplate.transform.GetChild(0);
            var child2 = playerTemplate.transform.GetChild(1);


            child1.gameObject.layer = LayerMask.NameToLayer("Player");
            child2.gameObject.layer = LayerMask.NameToLayer("Player");

            child1.transform.parent = transform;
            child2.transform.parent = transform;


            child1.transform.position = Vector3.zero;
            child2.transform.position = Vector3.zero;

            Destroy(playerTemplate);

            _animator.avatar = _initPlayer.avatarSchemeMan;
        }
    }
}