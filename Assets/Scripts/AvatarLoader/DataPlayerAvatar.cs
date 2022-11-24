using System;
using UnityEngine;

namespace AvatarLoader
{
    public class DataPlayerAvatar
    {
        private AvatarRenderModel _avatar2d = new AvatarRenderModel();
        private AvatarModel _avatar3d = new AvatarModel();
        
        public AvatarRenderModel Avatar2d
        {
            get => _avatar2d;
            set => _avatar2d = value;
        }

        public AvatarModel Avatar3d
        {
            get => _avatar3d;
            set => _avatar3d = value;
        }
    }
}