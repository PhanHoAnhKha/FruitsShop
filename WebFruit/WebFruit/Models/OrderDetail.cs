﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebFruit.Models
{
    public class OrderDetail
    {
        [Key]
        public int OrderDetailId { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product? Product { get; set; }
        [ForeignKey("Order")]
        public int OrderId { get; set; }
        public Order? Order { get; set; }
        public int? Quantity { get; set; }
        public decimal? Price { get; set; }
    }
}
