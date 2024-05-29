﻿using System.ComponentModel.DataAnnotations;

namespace FruitShopMVC.Models
{
    public class Categories
    {
        [Key]
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
    }
}
