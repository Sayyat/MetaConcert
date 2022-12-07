using System;
using System.Collections.Generic;
using UnityEngine;

namespace Goods
{
    public class GoodsContainer: MonoBehaviour
    {
        public List<Goods> goods;
        public GoodsView GoodsView { get; set; }
        private void Start()
        {   
            foreach (var good in goods)
            {
                good.ShareMyId += GoodsView.GoodClicked;
            }
        }
        
    }
}