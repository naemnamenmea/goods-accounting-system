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
        [DisplayName("Дата создания")]
        public DateTime CreationDate { get; set; }
        [Required]
        [DisplayName("Название товара")]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        [DisplayName("Цена")]
        public decimal Price { get; set; }
        [DisplayName("Описание")]
        public string Description { get; set; }
        [DisplayName("Есть в наличии")]
        public bool InStock { get; set; }
        [DisplayName("Файл приложение")]
        public string Attachment { get; set; }
    }
}
