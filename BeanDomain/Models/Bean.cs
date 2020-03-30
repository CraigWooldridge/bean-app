using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BeanApp.Domain.Models
{
    public class Bean
    {
        public int Id { get; set; }

        [Display(Name = "Bean Name")]
        [Required]
        public string BeanName { get; set; }
        public string Aroma { get; set; }
        public string Colour { get; set; }

        [Display(Name = "Cost Per 100g (£)")]
        [DataType(DataType.Currency)]
        public decimal CostPer100g { get; set; }

        [Display(Name = "Date To Be Shown")]
        [DataType(DataType.Date)]
        public DateTime DateToBeShownOn { get; set; }
        public BeanImage Image { get; set; }

        public Bean() { }

        public Bean(string beanName, string aroma, string colour, decimal costPer100g, DateTime dateShownOn, BeanImage image)
        {
            BeanName = beanName;
            Aroma = aroma;
            Colour = colour;
            CostPer100g = costPer100g;
            DateToBeShownOn = dateShownOn;
            Image = image;
        }
    }
}
