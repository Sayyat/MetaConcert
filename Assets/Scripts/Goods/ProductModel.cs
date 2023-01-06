using System.Collections.Generic;
using UnityEngine;

namespace Goods
{
    public class ProductModel
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<Sprite> Sprites { get; set; }
        public int Size { get; set; }
        public int InStock { get; set; }
        public int Price { get; set; }
        public string URL { get; set; }
        
        
    }
}