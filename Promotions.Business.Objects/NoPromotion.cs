using System.Collections.Generic;

namespace Promotion.Business.Objects
{
    //Null object pattern
    public class NoPromotion : FlatPricePromotion
    {

        public NoPromotion(List<ProductPromoter> skuRules) : base(skuRules)
        {

        }

        public override double CalculatePrice(int itemCount, ProductPromoter currentRule) => base.CalculatePrice(itemCount, currentRule);


        public override string ToString() => base.ToString();

    }

}
