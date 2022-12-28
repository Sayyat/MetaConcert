using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace Requests
{
    public class PostRequest : MonoBehaviour
    {
        [SerializeField] private Button send;
        private void Start()
        {
            send.onClick.AddListener(() => StartCoroutine(Upload("Sayat")));
        }

        private IEnumerator Upload(string nick)
        {
            var form = new WWWForm();
            form.AddField("nick", nick);
            form.AddField("time", DateTime.Now.ToString());

            using var www = UnityWebRequest.Post("http://localhost:3000/api/upload", form);
            yield return www.SendWebRequest();

            Debug.Log(www.result != UnityWebRequest.Result.Success ? www.error : "Form upload complete!");
        }


        private void OnDestroy()
        {
            send.onClick.RemoveAllListeners();
        }
    }
}
