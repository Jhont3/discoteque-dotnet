using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Discoteque.Data.Models
{
    public class SongDTO : BaseEntity<int>
    {
        public string Name { get; set; } = "";
        public string Duration { get; set; } = "";

        // [ForeignKey("Id")]
        [ForeignKey("Album")]  
        [Required]
        public int AlbumId { get; set; }

        public virtual Album ? Album { get; set; } 
    }
}
