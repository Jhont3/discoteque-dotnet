using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Discoteque.Data.Models
{
    public class Tour : BaseEntity<int>
    {
      public string Name { get; set; } = "";
      public string City { get; set; } = "";

 
    //   [GreaterThan2021(ErrorMessage = "The date must be greater than the year 2021.")] 
      public DateOnly Date { get; set; }
      public bool IscompletelySold { get; set; }
      
      [ForeignKey("Id")]
      public int ArtistId { get; set; }
      public virtual Artist ? Artist { get; set; } 

    }
    //Just to know
    // public class GreaterThan2021Attribute : ValidationAttribute
    // {
    //     public override bool IsValid(object value)
    //     {
    //         if (value is DateTime date)
    //         {
    //             return date.Year > 2021;
    //         }
    //         return false;
    //     }
    // }

}
