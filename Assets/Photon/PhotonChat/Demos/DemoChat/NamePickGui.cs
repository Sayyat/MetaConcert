// --------------------------------------------------------------------------------------------------------------------
// <copyright company="Exit Games GmbH"/>
// <summary>Demo code for Photon Chat in Unity.</summary>
// <author>developer@exitgames.com</author>
// --------------------------------------------------------------------------------------------------------------------


using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;


namespace Photon.Chat.Demo
{
    [RequireComponent(typeof(ChatGui))]
    public class NamePickGui : MonoBehaviour
    {
        private const string UserNamePlayerPref = "NamePickUserName";

        public ChatGui chatNewComponent;

        public InputField idInput;

        public bool autoStart { get; set; }
        public void Start()
        {
            this.chatNewComponent = FindObjectOfType<ChatGui>();


            string prefsName = PlayerPrefs.GetString(UserNamePlayerPref);
            if (!string.IsNullOrEmpty(prefsName))
            {
                this.idInput.text = prefsName;
            }

            if(autoStart)
                StartChat();
        }


        // new UI will fire "EndEdit" event also when loosing focus. So check "enter" key and only then StartChat.
        public void EndEditOnEnter()
        {
            if (Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.KeypadEnter))
            {
                this.StartChat();
            }
        }

        public void StartChat()
        {
            ChatGui chatNewComponent = FindObjectOfType<ChatGui>();
            chatNewComponent.UserName = PhotonNetwork.NickName.Trim();
            chatNewComponent.Connect();
            this.enabled = false;

            PlayerPrefs.SetString(UserNamePlayerPref, chatNewComponent.UserName);
        }
    }
}