using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Promotion.Business.Objects
{
    public abstract class ProductPromotionStrategy
    {
        //Instantiate necessary members thro' abstract constructor
        protected ProductPromotionStrategy(List<ProductPromoter> skuRules)
        {
            SKURules = skuRules;
        }

        protected List<ProductPromoter> SKURules { get; set; }

        public abstract double CalculatePrice(int itemCount, ProductPromoter currentRule);

        public virtual double Apply(Dictionary<Product, int> cartItems)
        {
            var totalPrice = 0.0;
            //First create groups based on sku type of the product
            var productGroups = GetProductsGroupedBySkuType(cartItems);

            //Then for each group apply the promotion rule of that SKUType
            foreach (var currentGroup in productGroups)
            {
                var itemCount = currentGroup.First().Value;
                var currentRule = SKURules.Where(p => p.SkuType == currentGroup.Key).FirstOrDefault();
                totalPrice += CalculatePrice(itemCount, currentRule);
            }

            //Return total price of this group

            return totalPrice;
        }

        protected IOrderedEnumerable<IGrouping<SKUType, KeyValuePair<Product, int>>> GetProductsGroupedBySkuType(Dictionary<Product, int> cartItems)
        {
            var productGroups = from item in cartItems
                                group item by item.Key.SkuType into prodGroup
                                orderby prodGroup.Key
                                select prodGroup;
            return productGroups;
        }
    }


}