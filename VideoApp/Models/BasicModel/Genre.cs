using System.Collections.Generic;

namespace VideoApp.Models
{
    public class Genre
    {
        public int GenreID { get; set; }
        public string KindGenre { get; set; }

        public virtual ICollection<Video> Video { get; set; }
    }
}