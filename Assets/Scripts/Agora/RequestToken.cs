using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace Agora
{
    public class RequestToken:MonoBehaviour
    {
        private const string URL = "https://agora-token-generator-beryl.vercel.app/api/generate";

        public event Action<string> RequestSuccess; 
        private void Start()
        {
            StartCoroutine(GetRequest(URL));
        }

        private IEnumerator GetRequest(string uri)
        {
            using var webRequest = UnityWebRequest.Get(uri);
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError("Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError("HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    var json = JsonUtility.FromJson<GeneratorResponse>(webRequest.downloadHandler.text);
                    RequestSuccess?.Invoke(json.token);
                    Debug.Log("Received: " + json.token);
                    break;
                case UnityWebRequest.Result.InProgress:
                    break;
            }
        }

        [Serializable]
        public class GeneratorResponse
        {
            public string token;
        }
    }
}