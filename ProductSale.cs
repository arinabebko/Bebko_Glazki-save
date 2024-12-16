//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Бебко_Глазки_save
{
    using System;
    using System.Collections.Generic;
    
    public partial class ProductSale
    {
        public int ID { get; set; }
        public int ProductID { get; set; }
        public int AgentID { get; set; }
        public System.DateTime SaleDate { get; set; }
        public int ProductCount { get; set; }
    
        public virtual Agent Agent { get; set; }
        public virtual Product Product { get; set; }

         public decimal Stoimost
        {

            get
            {
                // Проверяем, что продукт не равен null и у него есть цена
                decimal d;
                if (Product != null && Product.MinCostForAgent > 0)
                {
                    return Product.MinCostForAgent * this.ProductCount;
                }
                return 0; // Возвращаем 0, если продукт null или цена не положительная
            }
        }
    }
}
