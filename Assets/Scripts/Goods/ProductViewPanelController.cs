using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Goods
{
    public class ProductViewPanelController
    {
        private List<ProductModel> _productModels = new List<ProductModel>();
        private ProductViewPanel _productViewPanel;

        public ProductViewPanelController(ProductViewPanel productViewPanel)
        {
            InitModels();
            _productViewPanel = productViewPanel;
        }

        public void InitModels()
        {
            _productModels = new List<ProductModel>();
            _productModels.Add(new ProductModel()
            {
                ID = 0,
                Sprites = new List<Sprite>()
                {
                    Resources.Load<Sprite>("Shoes_0/1"),
                    Resources.Load<Sprite>("Shoes_0/2"),
                    Resources.Load<Sprite>("Shoes_0/3"),
                    Resources.Load<Sprite>("Shoes_0/4"),
                    Resources.Load<Sprite>("Shoes_0/5"),
                    Resources.Load<Sprite>("Shoes_0/6"),
                },
                Price = 50000,
                InStock = 1,
                Size = 40,
                Title = "SIYAH NATA",
                Description = "Бренд: Asyl Adam\n" +
                              "Вид товара: ботинки\n" +
                              "Материал верха: натуральная кожа\n" +
                              "Материал подкладки: натуральная шерсть\n" +
                              "Цвет: черный\n" +
                              "Страна производитель: Турция\n" +
                              "Коллекция: AW 2021-2022",
                URL = "https://asyladam.shop/product/siyah-nata-8/"
            });

            _productModels.Add(new ProductModel()
            {
                ID = 1,
                Sprites = new List<Sprite>()
                {
                    Resources.Load<Sprite>("Shoes_1/1"),
                    Resources.Load<Sprite>("Shoes_1/2"),
                    Resources.Load<Sprite>("Shoes_1/3"),
                },
                Price = 30000,
                InStock = 1000,
                Size = 40,
                Title = "Asyl Adam",
                Description = "Бренд: Asyl Adam\n" +
                              "Модель: W112-35-1",
                URL = "https://asyladam.shop/product/%d0%be%d0%b1%d1%83%d0%b2%d1%8c-asyl-adam/"
            });
            _productModels.Add(new ProductModel()
            {
                ID = 2,
                Sprites = new List<Sprite>()
                {
                    Resources.Load<Sprite>("Shoes_2/1"),
                    Resources.Load<Sprite>("Shoes_2/2"),
                    Resources.Load<Sprite>("Shoes_2/3"),
                    Resources.Load<Sprite>("Shoes_2/4"),
                    Resources.Load<Sprite>("Shoes_2/5"),
                    Resources.Load<Sprite>("Shoes_2/6"),
                },
                Price = 50000,
                Size = 40,
                Title = "LACIVERT NATA",
                Description = "Бренд: Asyl Adam\n" +
                              "Вид товара: ботинки\n" +
                              "Материал верха: натуральная кожа\n" +
                              "Материал подкладки: натуральная шерсть\n" +
                              "Цвет: темно-синий\n" +
                              "Страна производитель: Турция\n" +
                              "Коллекция: AW 2021-2022\n",
                URL = "https://asyladam.shop/product/lacivert-nata/"
            });
            _productModels.Add(new ProductModel()
            {
                ID = 3,
                Sprites = new List<Sprite>()
                {
                    Resources.Load<Sprite>("Suits_0/1"),
                    Resources.Load<Sprite>("Suits_0/2"),
                    Resources.Load<Sprite>("Suits_0/3"),
                    Resources.Load<Sprite>("Suits_0/4"),
                    Resources.Load<Sprite>("Suits_0/5"),
                },
                Price = 75000,
                Size = 48,
                Title = "Костюм Асыл Адам",
                Description = "Бренд: Asyl Adam\n" +
                              "Вид товара: Классика\n" +
                              "Ткань: Франция\n" +
                              "Лекал: Hugo Boss, Германия\n" +
                              "Пошив на заказ из Турции\n",
                URL = "https://asyladam.shop/product/%d0%ba%d0%be%d1%81%d1%82%d1%8e%d0%bc-%d0%b0%d1%81%d1%8b%d0%bb-%d0%b0%d0%b4%d0%b0%d0%bc-4/"
            });
        }


        public void GoodClicked(int id)
        {
            Debug.Log($"Clicked: {id}");
            Debug.Log($"Sprites: {_productModels[id].Sprites.Count}");

            _productViewPanel.Sprites = _productModels[id].Sprites;
            _productViewPanel.CurrentImage = 0;
            _productViewPanel.URL = _productModels[id].URL;
            _productViewPanel.Title = _productModels[id].Title;
            _productViewPanel.Description = _productModels[id].Description;
            _productViewPanel.Price = _productModels[id].Price;
            _productViewPanel.Size = _productModels[id].Size;
            _productViewPanel.Amount = 1;
            _productViewPanel.gameObject.SetActive(true);
        }
    }
}