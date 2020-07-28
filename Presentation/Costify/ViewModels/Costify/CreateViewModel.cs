using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Presentation.Costify.ViewModels.Costify
{
    public class CreateViewModel
    {
        // In
        public SelectList ListOfCategories { get; set; }

        // Out
        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        [Required]
        public double Price { get; set; }
        [DisplayName("Category")]
        [Required]
        public string CategoryId { get; set; }
    }
}