using System.Collections.Generic;
using Xunit;
using PR = Promotion.Business.Objects;

namespace PromotionsEngine.Tests
{
    public class CartTestSet
    {
        [Fact]
        public void EmptyCart_Test()
        {
            var cart = new PR.Cart();
            Assert.IsAssignableFrom<PR.Cart>(cart);
            Assert.True(cart.Count == 0);
        }

        //create a cart with products added
        [Fact]
        public void CartWithItems_Test()
        {
            var cart = new PR.Cart();
            cart.AddItem(new PR.Product(PR.SKUType.A, 50));
            cart.AddItem(new PR.Product(PR.SKUType.B, 30));
            cart.AddItem(new PR.Product(PR.SKUType.B, 30));
            cart.AddItem(new PR.Product(PR.SKUType.C, 20));
            cart.AddItem(new PR.Product(PR.SKUType.D, 15));
            Assert.IsAssignableFrom<PR.Cart>(cart);
            Assert.True(cart.Count == 5);
        }
        //get cart total
        [Fact] //Scenario A
        public void GetCartTotal_With_No_Promotion_Test()
        {
            //Arrange
            var cart = new PR.Cart();
            var productA = new PR.Product(PR.SKUType.A, 50);
            var productB = new PR.Product(PR.SKUType.B, 30);
            var productC = new PR.Product(PR.SKUType.C, 20);
            cart.AddItem(productA);
            cart.AddItem(productB);
            cart.AddItem(productC);

            var noPromoRuleA = new PR.ProductPromoter(productA.SkuType, productA.Price, 1, productA.Price, 1);
            var noPromoRuleA1 = new PR.ProductPromoter(productA.SkuType, productA.Price, 1, productA.Price, 1);
            var noPromoRuleB = new PR.ProductPromoter(productB.SkuType, productB.Price, 1, productB.Price, 1);
            var noPromoRuleC = new PR.ProductPromoter(productC.SkuType, productC.Price, 1, productC.Price, 1);

            var promoRuleList = new List<PR.ProductPromoter>();
            promoRuleList.Add(noPromoRuleA);
            promoRuleList.Add(noPromoRuleA1);
            promoRuleList.Add(noPromoRuleB);
            promoRuleList.Add(noPromoRuleC);


            //PR.ProductPromotionStrategy promoRules = new PR.NoPromotion(promoRuleList);
            //cart.PromotionRules = promoRules;
            //Act
            double totalPrice = cart.GetCartTotal();

            //Assert
            Assert.IsAssignableFrom<PR.Cart>(cart);
            Assert.True(cart.Count == 3);
            Assert.Equal(100.0, totalPrice, 1);
        }
    }
}
