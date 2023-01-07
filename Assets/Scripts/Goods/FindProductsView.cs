using UnityEngine;

namespace Goods
{
    public class FindProductsView: MonoBehaviour
    {
        private ProductsView[] _productsViews;
        private ProductViewPanelController _productViewPanelController;

        public void UpdateProductsViewPanel(ProductViewPanelController productViewPanelController)
        {
            _productViewPanelController = productViewPanelController;

            _productsViews = transform.GetComponentsInChildren<ProductsView>();
            foreach (var productsView in _productsViews)
            {
                productsView.ProductViewPanelController = _productViewPanelController;
            }
        }
    }
}