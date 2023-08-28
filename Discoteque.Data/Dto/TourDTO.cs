using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Discoteque.Data.Models
{
    public class TourDTO : BaseEntity<int>
    {
        public string Name { get; set; } = "";
        public string City { get; set; } = "";
        public string Date { get; set; } = DateTime.Now.ToString("yyyy-MM-dd");
        public bool IscompletelySold { get; set; } = false;
        
        // [ForeignKey("Id")] More explicit but less maintainable
        [ForeignKey("Artist")] 
        public int ArtistId { get; set; }
        public virtual Artist ? Artist { get; set; }

    }
}