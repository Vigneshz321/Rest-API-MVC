using System.ComponentModel.DataAnnotations;

namespace Rest_API_MVC.Models
{
    public class Book
    {
        [Key] 
        public long Id { get; set; }
        [MaxLength(10)]
        public string Publisher { get; set; }
        [MaxLength(10)]
        public string Title { get; set; }
        [MaxLength(10)]
        public string AuthorLastName { get; set; }
        [MaxLength(10)]
        public string AuthorFirstName { get; set; }
        public decimal Price { get; set; }

        public string MLACitation
        {
            get
            {
                return $"{AuthorLastName}, {AuthorFirstName}. \"{Title}\". {Publisher}.";
            }
        }

        public string ChicagoCitation
        {
            get
            {
                return $"{AuthorLastName}, {AuthorFirstName}. <i>{Title}</i>. {Publisher}.";
            }
        }
    }

}
