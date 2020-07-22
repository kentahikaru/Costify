using System;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Presentation.Costify.ViewModels
{
    public class CreateViewModel
    {
        // In
        public SelectList ListOfCategories { get; set; }

        // Out
        public DateTime Date { get; set; }
        public double Price { get; set; }
        [DisplayName("Category")]
        public string CategoryName { get; set; }
    }
}