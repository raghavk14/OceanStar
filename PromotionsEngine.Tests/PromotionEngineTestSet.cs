using System;
using System.Collections.Generic;
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
        //2.Add a sku type to the flat price promotion rule
        [Fact]
        public void OneTypeSkuFlatPricePromotionTest()
        {
            var cart = new PR.Cart();
            var product = new PR.Product(PR.SKUType.A, 50);
            cart.AddItem(product);
            var flatPriceRule = new PR.ProductPromoter(product.SkuType, product.Price, 3, 130, 1);
            var promoRuleList = new List<PR.ProductPromoter>();
            promoRuleList.Add(flatPriceRule);

            //PR.IPromotion promotion = new PR.FlatPricePromotion(promoRuleList);
            PR.ProductPromotionStrategy promotion = new PR.FlatPricePromotion(promoRuleList);
            Assert.Equal(" 3 A(s) @ 130$", promotion.ToString());
        }
        //3.Add multiple type skus to same flat price promotion rule
        [Fact]
        public void Promotion_AddMultipleSkuTest()
        {
            var cart = new PR.Cart();
            var productA = new PR.Product(PR.SKUType.A, 50);
            var productB = new PR.Product(PR.SKUType.B, 30);
            cart.AddItem(productA);
            for (int i = 0; i < 2; i++)
                cart.AddItem(productB);

            var flatPriceRule1 = new PR.ProductPromoter(productA.SkuType, productA.Price, 1, 50, 1);
            var flatPriceRule2 = new PR.ProductPromoter(productB.SkuType, productB.Price, 2, 30, 1);
            var promoRuleList = new List<PR.ProductPromoter>();
            promoRuleList.Add(flatPriceRule1);
            promoRuleList.Add(flatPriceRule2);

            PR.ProductPromotionStrategy promotion = new PR.FlatPricePromotion(promoRuleList);
            Assert.Equal(" 1 A @ 50$ 2 B(s) @ 30$", promotion.ToString());
        }

    }

}
