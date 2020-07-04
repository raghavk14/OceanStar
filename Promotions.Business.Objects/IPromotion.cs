using System.Collections.Generic;

namespace Promotion.Business.Objects
{
    public interface IPromotion
    {
        List<ProductPromoter> SKURules { get; set; }
        double Apply(Dictionary<Product, int> cartItems);
    }
}