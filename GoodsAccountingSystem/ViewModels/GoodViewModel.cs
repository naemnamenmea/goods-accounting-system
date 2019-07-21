using Microsoft.AspNetCore.Http;
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
        [Required(ErrorMessage = "Required")]
        [DataType(DataType.Date)]
        [DisplayName("Creation Date")]
        public DateTime CreationDate { get; set; }
        [Required(ErrorMessage = "Required")]
        [DisplayName("Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Required")]
        [DataType(DataType.Currency)]
        [DisplayName("Price")]
        public decimal Price { get; set; }
        [DisplayName("Description")]
        public string Description { get; set; }
        [DisplayName("In Stock")]
        public bool InStock { get; set; }
        [DisplayName("Attachment")]
        public string Attachment { get; set; }
    }

    public class CreateGoodViewModel
    {
        [Required(ErrorMessage = "Required")]
        [DisplayName("Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Required")]
        [DataType(DataType.Currency)]
        [DisplayName("Price")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Required")]
        [DisplayName("Description")]
        public string Description { get; set; }
        [DisplayName("In Stock")]
        public bool InStock { get; set; } = true;
        [Required(ErrorMessage = "Required")]
        [DisplayName("Attachment")]
        //[FileExtensions(Extensions = "jpg,jpeg,png,pdf")]
        [DataType(DataType.Upload)]
        public IFormFile AttachmentUpload { get; set; }
    }

    public class EditGoodViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Required")]
        [DisplayName("Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Required")]
        [DataType(DataType.Currency)]
        [DisplayName("Price")]
        public decimal Price { get; set; }
        [DisplayName("Description")]
        public string Description { get; set; }
        [DisplayName("In Stock")]
        public bool InStock { get; set; }
        [Display(Name = "Attachment")]
        [DataType(DataType.Upload)]
        public IFormFile AttachmentUpload { get; set; }
    }
}
