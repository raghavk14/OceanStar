using System.Collections.Generic;
using System.Text;

namespace Promotion.Business.Objects
{
    public class Promotion
    {
        public IPromotion PriceRule { get; set; }

        private Promotion() { }

        public Promotion(IPromotion priceRule)
        {
            PriceRule = priceRule;
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();

            foreach (var thisRule in PriceRule.SKURules)
            {
                var discount = string.Empty;
                if (thisRule.Discount != 1)
                {
                    stringBuilder.Append(thisRule.Count + " " + thisRule.SkuType + " @" + thisRule.Discount + "% Off");
                }
                else
                {
                    stringBuilder.Append(thisRule.Count + " " + thisRule.SkuType + " @" + thisRule.PromotionPrice);
                }

            }
            return stringBuilder.ToString();
        }

        public double Apply(Dictionary<Product, int> items)
        {
            //Iterate through cart items and apply the promotion rule.
            return PriceRule.Apply(items);
        }
    }
}