using System;
using Photon.Pun;
using UnityEngine;

namespace Init
{
    [RequireComponent(typeof(Animator))]
    public class ConstructAvatar : MonoBehaviour, IPunInstantiateMagicCallback
    {
      
        private InitPlayer _initPlayer;
        private Animator _animator;

        private void Awake()
        {
          _animator = this.GetComponent<Animator>();
            var initPlayer = GameObject.Find("InitPlayer");
            _initPlayer = initPlayer.GetComponent<InitPlayer>();
        }

        public void OnPhotonInstantiate(PhotonMessageInfo info)
        {
            _initPlayer.Avatar = this;
        }
        
        public void Construct(GameObject playerOnScene)
        {
            var child1 = playerOnScene.transform.GetChild(0);
            var child2 = playerOnScene.transform.GetChild(1);
            
//todo Add player layer
            child1.transform.parent = transform;
            child2.transform.parent = transform;
            
            child1.transform.position = Vector3.zero;
            child2.transform.position = Vector3.zero;
        }

        public void SetupAvatarOnAnimator(Avatar avatarScheme)
        {
            _animator.avatar = avatarScheme;
        }
    }
}