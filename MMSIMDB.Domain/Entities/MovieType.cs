using MMSIMDB.Domain.Common;

namespace MMSIMDB.Domain.Entities
{
    public class MovieType : BaseEntity
    { 
        public string Name { get; set; }
        public ICollection<Movie> Movie { get; set; }
    }
}
