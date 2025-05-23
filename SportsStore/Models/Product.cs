﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportsStore.Models
{
  public class Product
  {
    [Key]
    public long ProductID { get; set; }
    public string Name { get; set; }
    public string Discription { get; set; }
    [Column(TypeName = "decimal(8, 2)")]
    public decimal Price { get; set; }
    public string Category { get; set; }
  }
}
