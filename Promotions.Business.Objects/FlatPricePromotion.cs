using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Promotion.Business.Objects
{
    public class FlatPricePromotion : ProductPromotionStrategy
    {

        public FlatPricePromotion(List<ProductPromoter> skuRules) : base(skuRules)
        {

        }

        public override double CalculatePrice(int itemCount, ProductPromoter currentRule)
        {
            double price = 0.0;
            var chunk = itemCount / currentRule.Count;
            price += chunk * currentRule.PromotionPrice + (itemCount - (currentRule.Count * chunk)) * currentRule.ProudctPrice;

            return price;
        }

        public override string ToString()
        {
            var flatPriceRuleString = new StringBuilder();
            string plural = " ";
            foreach (var promoter in SKURules)
            {
                plural = promoter.Count > 1 ? "(s) " : plural;
                flatPriceRuleString.Append(" " + promoter.Count + " " + promoter.SkuType + plural + "@ " + promoter.PromotionPrice + "$");
            }

            return flatPriceRuleString.ToString();
        }
    }

}