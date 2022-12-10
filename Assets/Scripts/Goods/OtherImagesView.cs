using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Goods
{
    public class OtherImagesView : MonoBehaviour
    {

        [SerializeField] private List<Image> images;
        private List<Sprite> _sprites;
        
        private int _selectedSprite;

        public List<Image> Images;
        public List<Sprite> Sprites
        {
            get => _sprites;
            set
            {
                _sprites = value;
                UpdateImages();
            }
        }

        public void SelectImage(int number)
        {
            _selectedSprite = number;
            UpdateImages();
        }
        public void UpdateImages()
        {
            if(Sprites.Count == 0)
                return;

            var prevIndex = (_selectedSprite - 1 + Sprites.Count) % _sprites.Count;
            var nextIndex = (_selectedSprite + 1) % _sprites.Count;
            
            images[0].sprite = Sprites[prevIndex];
            images[1].sprite = Sprites[_selectedSprite];
            images[2].sprite = Sprites[nextIndex];
        }
    
    }
}
