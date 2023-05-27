using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GoodsAccountingSystem.Models
{
    public class GoodModel
    {
        public int Id { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime CreationDate { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        public string Description { get; set; }
        public bool InStock { get; set; }
        public string Attachment { get; set; }
    }
}
