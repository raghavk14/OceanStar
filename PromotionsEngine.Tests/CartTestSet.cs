using PR = Promotions.Business.Objects;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

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
    }
}
