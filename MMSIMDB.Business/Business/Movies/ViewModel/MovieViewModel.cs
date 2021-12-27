using System;

namespace MMSIMDB.Application.Business.Movies.ViewModel
{
    public class MovieViewModel
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int MovieTypeID { get; set; }
        public int Year { get; set; }
        public string ImageName { get; set; }
        public int RatingSum { get; set; }
        public int NumberOfRatings { get; set; }
        public int NumberOFStars { get; set; }
        public int MyRating { get; set; }

    }
}
