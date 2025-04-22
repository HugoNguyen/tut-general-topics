using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingWiki_Model.Models
{
    public class Book
    {
        // EF will know field with ending of Id is primary key
        public int BookId {  get; set; }
        public string Title { get; set; }
        public string ISBN2 { get; set; }
        public decimal Price { get; set; }
    }
}
