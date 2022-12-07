using System;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Goods
{
    public class GoodsView : MonoBehaviour
    {
        public TextMeshProUGUI title;
        public Image image;
        public TextMeshProUGUI size;
        public TextMeshProUGUI price;
        public Button buy;
        public Button close;


        public List<GoodsModel> GoodsModels = new List<GoodsModel>();
      
        private int _id;

        private void Awake()
        {
           
            buy.onClick.AddListener(OpenUrl);
            close.onClick.AddListener(() => gameObject.SetActive(false));
           
        }

        public void Init()
        {
            GoodsModels = new List<GoodsModel>();
            GoodsModels.Add(new GoodsModel()
            {
                ID = 0,
                Image = null,
                Price = 100d,
                Size = 45,
                Title = "Shoe1",
                URL = "https://google.com/"
            });

            GoodsModels.Add(new GoodsModel()
            {
                ID = 1,
                Image = null,
                Price = 120d,
                Size = 40,
                Title = "Shoe2",
                URL = "https://google.com/"
            });
            GoodsModels.Add(new GoodsModel()
            {
                ID = 2,
                Image = null,
                Price = 120d,
                Size = 40,
                Title = "Shoe3",
                URL = "https://google.com/"
            });

        }
        private void Start()
        {
            
        }

        public void GoodClicked(int id)
        {
            Debug.Log($"Clicked: {id}");
            _id = id;
            title.text = GoodsModels[id].Title;
            size.text = GoodsModels[id].Size.ToString();
            price.text = GoodsModels[id].Price.ToString(CultureInfo.CurrentCulture);
            title.text = GoodsModels[id].Title;

            gameObject.SetActive(true);
        }

        private void OpenUrl()
        {
            Application.OpenURL(GoodsModels[_id].URL);
        }

        private void OnDestroy()
        {
            buy.onClick.RemoveListener(OpenUrl);
            close.onClick.RemoveAllListeners();
        }
    }
}