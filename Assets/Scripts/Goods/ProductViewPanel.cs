using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Goods
{
    public class ProductViewPanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI title;
        [SerializeField] private TextMeshProUGUI description;
        [SerializeField] private Image image;
        [SerializeField] private OtherImagesView otherImagesContainer;
        [SerializeField] private TextMeshProUGUI size;
        [SerializeField] private TextMeshProUGUI amount;
        [SerializeField] private TextMeshProUGUI price;
        [SerializeField] private Button prevImage;
        [SerializeField] private Button nextImage;
        [SerializeField] private Button increaseSize;
        [SerializeField] private Button decreaseSize;
        [SerializeField] private Button increaseAmount;
        [SerializeField] private Button decreaseAmount;
        [SerializeField] private Button buy;
        [SerializeField] private Button close;


        private List<Sprite> _sprites;
        private int _currentImage = 0;
        private int _price;

        private List<GameObject> _otherImages = new List<GameObject>();

        public List<Sprite> Sprites
        {
            get => _sprites;
            set
            {
                _sprites = value;
                otherImagesContainer.Sprites = _sprites;
            }
        }

        public Sprite Image
        {
            set => image.sprite = value;
        }

        public string Title
        {
            get => title.text;
            set => title.text = value;
        }


        public string Description
        {
            get => description.text;
            set => description.text = value;
        }

        public int Size
        {
            get => Convert.ToInt32(size.text);
            set => size.text = Math.Max(value, 0).ToString();
        }

        public int Amount
        {
            get => Convert.ToInt32(amount.text);
            set
            {
                amount.text = Math.Max(value, 1).ToString();
                UpdatePrice();
            }
        }

        private void UpdatePrice()
        {
            price.text = $"Купить за {Price * Amount} тг";
        }


        public string URL { get; set; }

        public int Price
        {
            get => _price;
            set { _price = value; }
        }

        public int CurrentImage
        {
            get => _currentImage;
            set
            {
                _currentImage = value;
                image.sprite = Sprites[_currentImage];
            }
        }


        private void OnEnable()
        {
            close.onClick.AddListener(() => gameObject.SetActive(false));
            buy.onClick.AddListener(OpenUrl);
            nextImage.onClick.AddListener(() => SelectImage(CurrentImage + 1));
            prevImage.onClick.AddListener(() => SelectImage(CurrentImage - 1));
            increaseAmount.onClick.AddListener(() => Amount += 1);
            decreaseAmount.onClick.AddListener(() => Amount -= 1);
            increaseSize.onClick.AddListener(() => Size += 1);
            decreaseSize.onClick.AddListener(() => Size -= 1);
        }


        private void OpenUrl()
        {
            Application.OpenURL(URL);
        }


        public void SelectImage(int id)
        {
            // to make id in valid range 0 <= id < Sprites.Count
            CurrentImage = (id + Sprites.Count) % Sprites.Count;

            otherImagesContainer.SelectImage(CurrentImage);
        }


        private void OnDisable()
        {
            buy.onClick.RemoveListener(OpenUrl);
            close.onClick.RemoveAllListeners();
            nextImage.onClick.RemoveAllListeners();
            prevImage.onClick.RemoveAllListeners();
            increaseAmount.onClick.RemoveAllListeners();
            decreaseAmount.onClick.RemoveAllListeners();
            increaseSize.onClick.RemoveAllListeners();
            decreaseSize.onClick.RemoveAllListeners();
        }
    }
}