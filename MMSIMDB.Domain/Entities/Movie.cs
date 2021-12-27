using MMSIMDB.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMSIMDB.Domain.Entities
{
    public class Movie : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int MovieTypeID { get; set; }
        public int Year { get; set; }
        public string ImageName { get; set; }
        public int RatingSum { get; set; }
        public int NumberOfRatings { get; set; }
        public int NumberOFStars { get; set; }
        public MovieType MovieType { get; set; }

        public ICollection<MovieUserRating> MovieUserRating { get; set; }
    }
}
