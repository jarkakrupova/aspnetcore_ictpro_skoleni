using Newtonsoft.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC.Models {
    public class NewSocks {
        public int Id { get; set; }
        [MaxLength(200)]
        public string Brand { get; set; }
        public Size Size { get; set; }
        public decimal Price { get; set; }
        [Range(0,100, ErrorMessage ="Rozsah je od 0 do 100")]
        public int OnStock { get; set; }
    }
}
