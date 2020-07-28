using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Presentation.Costify.ViewModels.Costify
{
    public class CostifyViewModel
    {
        public Guid Id { get; set;}
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public double Price { get; set; }
        
        public Guid CategoryId { get; set; }
        [DisplayName("Category")]
        public string CategoryName { get; set; }
    }
}