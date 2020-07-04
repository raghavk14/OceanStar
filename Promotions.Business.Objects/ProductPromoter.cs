using System;

namespace Promotion.Business.Objects
{
    public class ProductPromoter : Product
    {
        private int _count;
        private double _promoPrice;

        public double PromotionPrice
        {
            get { return _promoPrice; }

            set
            {
                if (value <= 0)
                {
                    throw new Exception("Invalid Promotion Price");
                }
                _promoPrice = value;

            }
        }

        public int Count
        {
            get
            {
                return _count;
            }

            set
            {
                if (value < 1)
                {
                    throw new Exception("Invalid Count for this Product Promotion");
                }
                _count = value;
            }
        }

        public double ProudctPrice
        {
            get { return base.Price; }
            set { base.Price = value; }
        }

        public double Discount { get; set; } = 1;

        public ProductPromoter(SKUType sKUType, double productPrice, int count, double promotionPrice, double discount) : base(sKUType, productPrice)
        {
            PromotionPrice = promotionPrice;
            Count = count;
            Discount = discount;
        }


    }
}

