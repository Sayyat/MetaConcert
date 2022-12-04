using Photon.Realtime;
using UnityEngine;

namespace Init.SceneInit
{
    public class ControllerScene:IScene
    {
        public Player OwnerPlayer { get; set; }

        public void StartScene()
        {
            Debug.Log("Test controller scene");
        }
    }
}