using MMSIMDB.Application.Interfaces.Repositories;
using MMSIMDB.Domain.Entities;
using MMSIMDB.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMSIMDB.Persistence.Seeds
{
    public class MovieTypeSeed
    {
        public static async Task SeedAsync(IMovieTypeRepositoryAsync movieTypeRepositoryAsync)
        {
            if (!movieTypeRepositoryAsync.GetAll().Any())
            {
                var data = new List<MovieType>();
                var values = Enum.GetValues(typeof(MovieTypeEnum));
                foreach (int item in values)
                {
                    data.Add(new MovieType()
                    {
                        ID = item,
                        Name = Enum.GetName(typeof(MovieTypeEnum), item)
                    });
                }
                await movieTypeRepositoryAsync.AddAsync(data.ToArray());
            }
        }
    }
}
