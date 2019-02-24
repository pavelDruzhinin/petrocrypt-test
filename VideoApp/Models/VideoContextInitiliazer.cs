using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace VideoApp.Models
{
    public class VideoContextInitiliazer : DropCreateDatabaseIfModelChanges<VideoContext> 
    {
        protected override void Seed(VideoContext videocontext)
        {
            var authors = new List<Author> { 
                new Author { AuthorID = 1, FirstName = "Роберт", LastName = "Земекис" }
            };

            foreach (var item in authors)
            {
                videocontext.Authors.Add(item);
            }

            var genres = new List<Genre> {
                new Genre { GenreID = 1, KindGenre = "Приключения" },
                new Genre { GenreID = 2, KindGenre = "Боевик" },
                new Genre { GenreID = 3, KindGenre = "Комедия" },
                new Genre { GenreID = 4, KindGenre = "Мелодрама" }
            };

            foreach (var item in genres)
            {
                videocontext.Genres.Add(item);
            }

            var video = new List<Video> {
                new Video { Title = "Назад в будущее 1", AuthorID = 1, GenreID = 1, DatePremiere = new DateTime(1985, 6, 3) },
                new Video { Title = "Назад в будущее 2", AuthorID = 1, GenreID = 1, DatePremiere = new DateTime(1989, 11, 22) },
                new Video { Title = "Назад в будущее 3", AuthorID = 1, GenreID = 1, DatePremiere = new DateTime(1990, 05, 25) }
            };

            foreach (var item in video)
            {
                videocontext.Video.Add(item);
            }

            videocontext.SaveChanges();
        }
    }
}