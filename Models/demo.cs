using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace sample.rpg.Models
{
    public class demo
    {
        [Key]
        public int BookId { get; set; }
        public string BookTitle { get; set; }=string.Empty;
        public string BookAuthor { get; set; }=string.Empty;
        public int DateofPublish { get; set; }
        public int PriceofBook { get; set; }
       
    }
    }
