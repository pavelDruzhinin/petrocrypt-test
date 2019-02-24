using System.Collections.Generic;
using System.Linq;

namespace VideoApp.Models.ViewModel
{
    public class VideoViewModel
    {
        private List<Video> _videos;
        public List<Video> Videos
        {
            get { return _videos ?? (_videos = new List<Video>()); }
            set { _videos = value; }
        }

        private List<Author> _authors;
        public List<Author> Authors
        {
            get { return _authors ?? (_authors = new List<Author>()); }
            set { _authors = value; }
        }

        private List<Genre> _genres;
        public List<Genre> Genres
        {
            get { return _genres ?? (_genres = new List<Genre>()); }
            set { _genres = value; }
        }

        public void LoadData()
        {
            using (var videocontext = new VideoContext())
            {
                videocontext.Configuration.ProxyCreationEnabled = false;
                _videos = videocontext.Video.ToList();

                _authors = videocontext.Authors.ToList();
                _genres = videocontext.Genres.ToList();
            }
        }

        public void DeleteVideo(int id)
        {
            using (var videocontext = new VideoContext())
            {
                var entity = videocontext.Video.First(x => x.VideoID == id);
                videocontext.Video.Remove(entity);
                videocontext.SaveChanges();
            }
        }

        public void AddVideo(Video video)
        {
            using (var videocontext = new VideoContext())
            {
                videocontext.Configuration.ValidateOnSaveEnabled = false;
                videocontext.Video.Add(video);
                videocontext.SaveChanges();
            }
        }

        public void UpdateVideo(Video video)
        {
            using (var videocontext = new VideoContext())
            {

                videocontext.Configuration.ValidateOnSaveEnabled = false;
                foreach (var entity in videocontext.Video.Where(x => x.VideoID == video.VideoID))
                {
                    entity.DatePremiere = video.DatePremiere;
                    entity.GenreID = video.GenreID;
                    entity.Title = video.Title;
                    entity.GenreID = video.GenreID;
                }
                videocontext.SaveChanges();
            }
        }
    }
}