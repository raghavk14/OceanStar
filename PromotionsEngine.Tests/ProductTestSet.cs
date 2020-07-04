using System;
using Xunit;
using PR = Promotion.Business.Objects;

namespace PromotionEngine.Tests
{
    public class ProductTestSet
    {
        //create a product
        [Fact]
        public void CreateProductTest()
        {
            var product = new PR.Product(PR.SKUType.A, 1);
            Assert.IsAssignableFrom<PR.Product>(product);
            Assert.NotNull(product);
        }
        //print the product string
        [Fact]
        public void PrintProductTest()
        {
            var product = new PR.Product(PR.SKUType.A, 50);
            Assert.IsAssignableFrom<PR.Product>(product);
            Assert.NotNull(product);
            Assert.Equal("ProductA Priced @ 50", product.ToString());
        }

        //Invalid Product price throw exception
        [Fact]
        public void InvalidProductPrice_Throws_Exception()
        {
            Assert.Throws<Exception>(() => new PR.Product(PR.SKUType.A, 0.0));

        }
        //compare two products for similarity

        //compare two products for dissimilarity
    }
}