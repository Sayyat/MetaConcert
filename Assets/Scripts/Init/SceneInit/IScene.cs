using Photon.Realtime;

namespace Init.SceneInit
{
    public interface IScene
    {
        public Player OwnerPlayer { get; set; }
        public void StartScene();
    }
}