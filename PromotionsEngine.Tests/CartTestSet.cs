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

        //get cart total with a promotion applied
        [Fact]
        public void Cart_With_OneTypeSkus_FlatPrice_PromotionTest()
        {
            var cart = new PR.Cart();
            var productA = new PR.Product(PR.SKUType.A, 50);

            //Add 5 As
            for (int i = 0; i < 5; i++)
            {
                cart.AddItem(productA);

            }

            var flatPriceRule1 = new PR.ProductPromoter(productA.SkuType, productA.Price, 3, 130, 1);

            var promoRuleList = new List<PR.ProductPromoter>();
            promoRuleList.Add(flatPriceRule1);


            PR.ProductPromotionStrategy promoRules = new PR.FlatPricePromotion(promoRuleList);
            cart.PromotionStrategy = promoRules;
            var totalPrice = cart.GetCartTotal();
            Assert.Equal(230, totalPrice);
        }

        [Fact]
        public void Cart_With_MulipleTypeSkus_OneItemEach_FlatPrice_PromotionTest()
        {
            var cart = new PR.Cart();
            var productA = new PR.Product(PR.SKUType.A, 50);
            var productB = new PR.Product(PR.SKUType.B, 30);
            var productC = new PR.Product(PR.SKUType.C, 20);
            //Add 1 A and 1 B and 1 C

            cart.AddItem(productA);
            cart.AddItem(productB);
            cart.AddItem(productC);


            var flatPriceRule1 = new PR.ProductPromoter(productA.SkuType, productA.Price, 3, 130, 1);
            var flatPriceRule2 = new PR.ProductPromoter(productB.SkuType, productB.Price, 2, 45, 1);
            var flatPriceRule3 = new PR.ProductPromoter(productC.SkuType, productC.Price, 1, 20, 1);

            var promoRuleList = new List<PR.ProductPromoter>();
            promoRuleList.Add(flatPriceRule1);
            promoRuleList.Add(flatPriceRule2);
            promoRuleList.Add(flatPriceRule3);


            PR.ProductPromotionStrategy promoRules = new PR.FlatPricePromotion(promoRuleList);
            cart.PromotionStrategy = promoRules;
            var totalPrice = cart.GetCartTotal();
            Assert.Equal(100, totalPrice);
        }

        [Fact] //Scenario B
        public void Cart_With_MulipleTypeSkus_FlatPrice_PromotionTest()
        {
            var cart = new PR.Cart();
            var productA = new PR.Product(PR.SKUType.A, 50);
            var productB = new PR.Product(PR.SKUType.B, 30);
            var productC = new PR.Product(PR.SKUType.C, 20);
            //Add 5 A and 5 B
            for (int i = 0; i < 5; i++)
            {
                cart.AddItem(productA);
                cart.AddItem(productB);
            }
            //Add 1 C
            cart.AddItem(productC);
            var flatPriceRule1 = new PR.ProductPromoter(productA.SkuType, productA.Price, 3, 130, 1);
            var flatPriceRule2 = new PR.ProductPromoter(productB.SkuType, productB.Price, 2, 45, 1);
            var flatPriceRule3 = new PR.ProductPromoter(productC.SkuType, productC.Price, 1, 20, 1);

            var promoRuleList = new List<PR.ProductPromoter>();
            promoRuleList.Add(flatPriceRule1);
            promoRuleList.Add(flatPriceRule2);
            promoRuleList.Add(flatPriceRule3);


            PR.ProductPromotionStrategy promoRules = new PR.FlatPricePromotion(promoRuleList);
            cart.PromotionStrategy = promoRules;
            var totalPrice = cart.GetCartTotal();
            Assert.Equal(370, totalPrice);
        }

        [Fact] //Scenario C
        public void Cart_With_MulipleTypeSkus_Combo_FlatPrice_PromotionTest()
        {
            var cart = new PR.Cart();
            var productA = new PR.Product(PR.SKUType.A, 50);
            var productB = new PR.Product(PR.SKUType.B, 30);
            var productC = new PR.Product(PR.SKUType.C, 20);
            var productD = new PR.Product(PR.SKUType.D, 15);

            //Add 3 A
            for (int i = 0; i < 3; i++)
            {
                cart.AddItem(productA);

            }
            //Add  5 B
            for (int i = 0; i < 5; i++)
            {

                cart.AddItem(productB);
            }
            //Add 1 C
            cart.AddItem(productC);
            //Add 1 D
            cart.AddItem(productD);


            var flatPriceRule1 = new PR.ProductPromoter(productA.SkuType, productA.Price, 3, 130, 1);
            var flatPriceRule2 = new PR.ProductPromoter(productB.SkuType, productB.Price, 2, 45, 1);
            var flatPriceRule3 = new PR.ProductPromoter(productC.SkuType, productC.Price, 1, 15, 1);
            var flatPriceRule4 = new PR.ProductPromoter(productD.SkuType, productD.Price, 1, 15, 1);

            var promoRuleList = new List<PR.ProductPromoter>();
            promoRuleList.Add(flatPriceRule1);
            promoRuleList.Add(flatPriceRule2);
            promoRuleList.Add(flatPriceRule3);
            promoRuleList.Add(flatPriceRule4);


            PR.ProductPromotionStrategy promoRules = new PR.FlatPricePromotion(promoRuleList);
            cart.PromotionStrategy = promoRules;
            var totalPrice = cart.GetCartTotal();
            Assert.Equal(280, totalPrice);
        }
    }
}
