using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevBooks.Models
{
    public class Book : IAuditInfo
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        [MaxLength(50)]
        public string Author { get; set; }

        [Required]
        [MaxLength(14)]
        public string Isbn { get; set; }

        [Required]
        public decimal? Price { get; set; }

        [Required]
        public int? Pages { get; set; }

        [MaxLength(500)]
        public string  Description { get; set; }

        [Required]
        [MaxLength(100)]
        public string Publisher { get; set; }

        public string ImageName { get; set; }

        
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
