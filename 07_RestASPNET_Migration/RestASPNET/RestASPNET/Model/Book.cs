using System.ComponentModel.DataAnnotations.Schema;

namespace RestASPNET.Model
{
    [Table("books")]
    public class Book
    {
        [Column("id")]
        public long Id { get; set; }

        [Column("author")]
        public string Author { get; set; }

        [Column("launch_date")]
        public System.DateTime LaunchDate { get; set; }

        [Column("price")]
        public decimal price { get; set; }

        [Column("title")]
        public string title { get; set; }
    }
}
