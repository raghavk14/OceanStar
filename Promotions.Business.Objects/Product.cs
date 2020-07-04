using System;
namespace Promotion.Business.Objects
{
    public class Product
    {
        private double _price;
        public SKUType SkuType { get; set; }

        public double Price
        {
            get { return _price; }
            set
            {
                if (value <= 0.0)
                {
                    throw new Exception("Invalid product price");
                }
                _price = value;
            }
        }

        public Product(SKUType skuType, double price)
        {
            SkuType = skuType;
            Price = price;
        }


        public override bool Equals(object otherProduct)
        {
            return this.SkuType.Equals((otherProduct as Product).SkuType);

        }

        public override string ToString()
        {
            return this.GetType().Name + this.SkuType + " Priced @ " + this.Price;
        }
    }
}