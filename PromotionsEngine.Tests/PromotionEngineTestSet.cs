using System;
using Xunit;
using PR = Promotion.Business.Objects;

namespace PromotionsEngine.Tests
{
    public class PromotionEngineTestSet
    {
       
       //1.Create a promotion rule
       [Fact]
        public void Promotion_CreateTest()
        {
            var promotion = new PR.FlatPricePromotion(null);
            Assert.IsAssignableFrom<PR.ProductPromotionStrategy>(promotion);
        }

        //Invalid Product promoter, throws exception
        [Fact]
        public void ZeroProductCount_ThrowsException_Test()
        {
            Assert.Throws<Exception>(() => new PR.ProductPromoter(PR.SKUType.B, 40, 0, 25, 1));

        }

        //Invalid Product promoter, throws exception
        [Fact]
        public void InvalidPromotionPrice_ThrowsException_Test()
        {
            Assert.Throws<Exception>(() => new PR.ProductPromoter(PR.SKUType.B, 10, 30, 0, 1));

        }
    }
    
}
