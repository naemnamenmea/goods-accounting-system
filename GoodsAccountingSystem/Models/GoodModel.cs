using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace GoodsAccountingSystem.Models
{
    public class GoodModel
    {
        [Required]
        public DateTime CreationDate { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public float Price { get; set; }
        public string Description { get; set; }
        bool InStock { get; set; }
        public string Attachment { get; set; }
    }
}
