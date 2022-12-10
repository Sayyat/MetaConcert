using System;
using System.Collections.Generic;
using UnityEngine;

namespace Goods
{
    public class ProductViewController: MonoBehaviour
    {
        public List<ProductView> products;
        public ProductViewPanelController ProductViewPanelController { get; set; }
        private void Start()
        {   
            foreach (var good in products)
            {
                good.ShareMyId += ProductViewPanelController.GoodClicked;
            }
        }
        
    }
}