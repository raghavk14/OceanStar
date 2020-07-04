using System;
using System.Collections.Generic;
using System.Text;

namespace Promotion.Business.Objects
{
    public class DiscountedPricePromotion : ProductPromotionStrategy
    {

        public DiscountedPricePromotion(List<ProductPromoter> skuRules) : base(skuRules)
        {

        }

        public override double CalculatePrice(int itemCount, ProductPromoter currentRule)
        {
            double price = 0.0;
            var chunk = itemCount / currentRule.Count;

            //double = int * double + (int - (int * int)) * double

            price += chunk * currentRule.PromotionPrice + (itemCount - (currentRule.Count * chunk)) * currentRule.ProudctPrice;

            return price * currentRule.Discount;
        }

        public override string ToString()
        {
            var discountPriceRuleString = new StringBuilder();
            string plural = " ";
            foreach (var promoter in SKURules)
            {
                plural = promoter.Count > 1 ? "(s) " : plural;
                discountPriceRuleString.Append(" " + promoter.Count + " " + promoter.SkuType + plural + "@ " + promoter.Discount * 100 + "% OFF on Total Price");
            }

            return discountPriceRuleString.ToString();
        }
    }

}