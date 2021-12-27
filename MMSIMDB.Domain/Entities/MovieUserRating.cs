using MMSIMDB.Domain.Common;

namespace MMSIMDB.Domain.Entities
{
    public class MovieUserRating : BaseEntity
    { 
        public string UserID { get; set; }
        public int MovieID { get; set; }
        public int Rating { get; set; }

        public Movie Movie { get; set; }
    }
}
