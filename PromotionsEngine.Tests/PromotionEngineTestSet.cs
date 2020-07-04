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

        //4.Define a discount on price for a promotion rule for one type of Sku
        [Fact]
        public void OneType_Sku_DiscountPrice_PromotionTest()
        {
            var cart = new PR.Cart();
            var productA = new PR.Product(PR.SKUType.A, 50);
            cart.AddItem(productA);
            var discountPriceRule1 = new PR.ProductPromoter(productA.SkuType, productA.Price, 2, 50, 0.1);
            var promoRuleList = new List<PR.ProductPromoter>();
            promoRuleList.Add(discountPriceRule1);

            PR.ProductPromotionStrategy promotion = new PR.DiscountedPricePromotion(promoRuleList);
            Assert.Equal(" 2 A(s) @ 10% OFF on Total Price", promotion.ToString());
        }

        //5.Define a discount on price for a promotion rule for different types of Sku
        [Fact]
        public void MultipleType_Sku_DiscountPrice_PromotionTest()
        {
            var cart = new PR.Cart();
            var productA = new PR.Product(PR.SKUType.A, 50);
            cart.AddItem(productA);
            var productB = new PR.Product(PR.SKUType.B, 30);
            cart.AddItem(productA);

            var discountPriceRule1 = new PR.ProductPromoter(productA.SkuType, productA.Price, 1, 50, 0.1);
            var discountPriceRule2 = new PR.ProductPromoter(productA.SkuType, productA.Price, 2, 50, 0.2);
            var promoRuleList = new List<PR.ProductPromoter>();
            promoRuleList.Add(discountPriceRule1);
            promoRuleList.Add(discountPriceRule2);

            PR.ProductPromotionStrategy promotion = new PR.DiscountedPricePromotion(promoRuleList);
            Assert.Equal(" 1 A @ 10% OFF on Total Price 2 A(s) @ 20% OFF on Total Price", promotion.ToString());
        }

    }

}
