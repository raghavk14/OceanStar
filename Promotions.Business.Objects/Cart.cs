


   
    using System;
    using System.Collections.Generic;
    using System.Linq;

    namespace Promotion.Business.Objects
    {
        public class Cart
        {
            protected Dictionary<Product, int> items = new Dictionary<Product, int>();

            public ProductPromotionStrategy PromotionStrategy { get; set; }

            public int Count
            {
                get
                {
                    return this.items.Count;
                }
            }

            public void AddItem(Product newItem)
            {
                if (!items.ContainsKey(newItem))
                    items.Add(newItem, 1);
                else
                    items[newItem]++;

            }
            public void RemoveItem(Product item)
            {
                items.Remove(item);
            }

            public double GetCartTotal()
            {
                //if(PromotionStrategy == null || PromotionStrategy.GetType() == typeof(NoPromotion))
                if (PromotionStrategy == null)
                {
                    PromotionStrategy = GetNoPromotionRuleSet();
                }
                //return PromotionRule.Apply(items);
                return PromotionStrategy.Apply(items);
            }

            public string Receipt() { return this.ToString(); }

            public override string ToString()
            {
                string reciept = "";

                foreach (var item in items)
                {
                    reciept += item.ToString() + "\n";
                }

                reciept += "\n\n Total: " + GetCartTotal();
                return reciept;
            }

            public ProductPromotionStrategy GetNoPromotionRuleSet()
            {
                var promoRuleList = new List<ProductPromoter>();
                //Get distinct sku items from the cart.
                foreach (var skutype in Enum.GetNames(typeof(SKUType)))
                {
                    var product = items.Where(sku => sku.Key.SkuType.ToString() == skutype).Select(sku => sku.Key).FirstOrDefault();
                    //For each sku type add "no product promotion rule".
                    if (product != null)
                        promoRuleList.Add(new ProductPromoter(product.SkuType, product.Price, 1, product.Price, 1));
                }

                return new NoPromotion(promoRuleList);
            }
        }
    }

