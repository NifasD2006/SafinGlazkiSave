//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SafinGlazkiSave
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Media;

    public partial class Agent
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Agent()
        {
            this.AgentPriorityHistory = new HashSet<AgentPriorityHistory>();
            this.ProductSale = new HashSet<ProductSale>();
            this.Shop = new HashSet<Shop>();
        }

        public int ID { get; set; }
        public int AgentTypeID { get; set; }
        public string Title { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Logo { get; set; }
        public string Address { get; set; }
        public int Priority { get; set; }
        public string DirectorName { get; set; }
        public string INN { get; set; }
        public string KPP { get; set; }
    

        public string AgentTypeText
        {
            get
            {
                return AgentType.Title;
            }
        }

        public decimal Prodaja
        {
            get 
            {
                decimal p = 0;
                foreach(ProductSale sales in ProductSale)
                {
                    p = p + sales.Stoimost;
                }
                return p;
            }
        }
        public decimal Discount
        {
            get
            {
                decimal ski = 0;
                decimal p = 0;
                foreach (ProductSale sales in ProductSale)
                {
                    p = p + sales.Stoimost;
                }
                if(p>=0 && p <= 10000)
                {
                    ski = 0;
                }
                if (p >= 10000 && p <= 50000)
                {
                    ski = 5;
                }
                if (p >= 50000 && p <= 150000)
                {
                    ski = 10;
                }
                if (p >= 150000 && p <= 500000)
                {
                    ski = 20;
                }
                if (p>=500000)
                {
                    ski = 25;
                }
                return ski;
            }
        }
        public SolidColorBrush FonStyle
        {
            get
            {
                if (Discount >= 25)
                {
                    return (SolidColorBrush)new BrushConverter().ConvertFromString("LightGreen");
                }
                else
                {
                    return (SolidColorBrush)new BrushConverter().ConvertFromString("White");
                }
            }
        }
        public virtual AgentType AgentType { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AgentPriorityHistory> AgentPriorityHistory { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductSale> ProductSale { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Shop> Shop { get; set; }
    }
}
