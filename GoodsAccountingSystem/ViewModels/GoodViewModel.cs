using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GoodsAccountingSystem.ViewModels
{
    public class GoodViewModel
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

    public class CreateGoodViewModel
    {
        [Required(ErrorMessage = "Укажите название товара")]
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

    public class EditGoodViewModel
    {
        public int Id { get; set; }
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
