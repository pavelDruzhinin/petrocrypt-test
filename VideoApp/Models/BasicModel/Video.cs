using System;

namespace VideoApp.Models
{
    public class Video
    {
        public int VideoID { get; set; }
        public string Title { get; set; }
        public DateTime DatePremiere { get; set; }
        public int AuthorID { get; set; }
        public int GenreID { get; set; }

        public virtual Genre Genre { get; set; }
        public virtual Author Author { get; set; }
    }
}