using System.Collections.Generic;

namespace VideoApp.Models
{
    public class Author
    {
        public int AuthorID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual ICollection<Video> Video { get; set; }
    }
}