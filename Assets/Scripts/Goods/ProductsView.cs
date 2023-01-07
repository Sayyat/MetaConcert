using System.Collections.Generic;
using UnityEngine;

namespace Goods
{
    public class ProductsView: MonoBehaviour
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

        private void OnDestroy()
        {
            foreach (var good in products)
            {
                good.ShareMyId -= ProductViewPanelController.GoodClicked;
            }
        }
    }
}