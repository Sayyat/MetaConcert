using ReadyPlayerMe;
using UnityEngine;

namespace AvatarLoader
{
    public class AvatarModel
    {
        public GameObject Avatar { get; set; }
        public AvatarMetadata Metadata { get; set; }
        public string Url { get; set; }
    }
}