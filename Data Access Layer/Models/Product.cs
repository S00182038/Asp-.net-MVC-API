using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public int ReorderLevel { get; set; }
        public float Price { get; set; }
        public int? Supplier_ID { get; set; }
        
        [ForeignKey("Supplier_ID")]

        public virtual Supplier ProductSupplier { get; set; } 

    }
}
