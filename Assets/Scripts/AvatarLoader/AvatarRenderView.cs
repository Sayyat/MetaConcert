using System;
using System.Collections.Generic;
using Assets.Scripts.UI;
using ExitGames.Client.Photon.StructWrapping;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace AvatarLoader
{
    public class AvatarRenderView : MonoBehaviour, IAvatarRenderView
    {
        public event Action<string> OnSelected;
        public GameObject LoaderAvatars { get; private set; }

        private Dictionary<GameObject, string> _iconOfUrl = new Dictionary<GameObject, string>();
        private Dictionary<string, GameObject> _urlOfIcon = new Dictionary<string, GameObject>();
        private Dictionary<string, BtnViewController> _urlOfBtn = new Dictionary<string, BtnViewController>();

        private string _lastUrl;

        private void Awake()
        {
            //Init loader
            var loader = new GameObject("LoaderAvatars");
            loader.transform.parent = transform;
            var loaderTMP = loader.AddComponent<TextMeshProUGUI>();
            var rectPanel = gameObject.GetComponent<RectTransform>();
            Canvas.ForceUpdateCanvases();
            loaderTMP.rectTransform.sizeDelta = rectPanel.rect.size;
            loaderTMP.alignment = TextAlignmentOptions.Center;
            loaderTMP.alignment = TextAlignmentOptions.Midline;
            loaderTMP.text = "Loading avatars...";
            LoaderAvatars = loader;
        }

        public void SetupTexture(Texture2D texture, string url)
        {
            var go = new GameObject(url);
            go.transform.parent = transform;
            var sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(.5f, .5f));

            go.AddComponent<Image>().sprite = sprite;
            _iconOfUrl.Add(go, url);
            _urlOfIcon.Add(url, go);

            AddButtonOnTexture(go);
            _urlOfIcon[url].transform.localScale = Vector3.one;
        }

        public void SelectButton(string url)
        {
            //Enable current effector
            _urlOfIcon[url].transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            _urlOfBtn[url].enabled = false;

            if (_lastUrl != null && url != _lastUrl)
            {
                //Disable last effector
                _urlOfIcon[_lastUrl].transform.localScale = Vector3.one;
                _urlOfBtn[_lastUrl].enabled = true;
            }

            //Save url with effector
            _lastUrl = url;
        }

        private void AddButtonOnTexture(GameObject textureObject)
        {
            var button = textureObject.AddComponent<Button>();
            button.onClick.AddListener(() => OnSelected.Invoke(_iconOfUrl[textureObject]));
            //init effectors for button
            var btnController = textureObject.AddComponent<BtnViewController>();
            btnController.percentScale = 30f;
            btnController.CurrentButton = button;

            //save url effector
            _urlOfBtn.Add(_iconOfUrl[textureObject], btnController);
        }
    }
}