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
    public class TVSeriesSeed
    {
        public static async Task SeedAsync(IMovieRepositoryAsync movieRepositoryAsync)
        {
            if (!movieRepositoryAsync.GetAll().Any(el => el.MovieTypeID == (int)MovieTypeEnum.TVSeries))
            {
                string fileLocation = System.IO.Path.Combine(AppContext.BaseDirectory, @"Seeds\json\TVSeries.json");
                string json = System.IO.File.ReadAllText(fileLocation);
                var jsonData = Newtonsoft.Json.JsonConvert.DeserializeObject<Data[]>(json);
                List<Movie> data = new List<Movie>();
                Random random = new Random();
                for (int i = 0; i < jsonData.Length; i++)
                {
                    int year = 0;
                    string? yearS = System.Text.RegularExpressions.Regex.Matches(jsonData[i].title, "[(][0-9]+[)]").LastOrDefault()?.Value;
                    if (!String.IsNullOrEmpty(yearS))
                    {
                        jsonData[i].title = jsonData[i].title.Replace(yearS, "").Trim();
                        Int32.TryParse(yearS.Replace("(", "").Replace(")", "").Trim(), out year);
                    }

                    string? orderNo = System.Text.RegularExpressions.Regex.Matches(jsonData[i].title, "[0-9]*[.]").LastOrDefault()?.Value;
                    if (!String.IsNullOrEmpty(orderNo))
                    {
                        jsonData[i].title = jsonData[i].title.Remove(0, orderNo.Length).Trim();
                    }
                    int number = random.Next(1000, 2000);
                    int ratingSum = (int)(number * decimal.Parse(jsonData[i].rating, System.Globalization.CultureInfo.InvariantCulture) / 2);
                    int numberOfStars = (int)Math.Round((decimal)ratingSum / number);

                    string name = (i + 1).ToString("1000");
                    await DownloadFile(jsonData[i].url, name);
                    data.Add(new Movie()
                    {
                        Title = jsonData[i].title,
                        Description = jsonData[i].title,
                        MovieTypeID = (int)MovieTypeEnum.TVSeries,
                        NumberOfRatings = number,
                        RatingSum = ratingSum,
                        NumberOFStars = numberOfStars,
                        Year = year,
                        ImageName = name
                    });
                }
                await movieRepositoryAsync.AddAsync(data.ToArray());
            }
        }

        public static async Task DownloadFile(string url, string name)
        {
            using (var client = new HttpClient())
            {

                using (var result = await client.GetAsync(url))
                {
                    if (result.IsSuccessStatusCode)
                    {
                        var file = await result.Content.ReadAsByteArrayAsync();
                        System.IO.File.WriteAllBytes(@$"Images\{name}.jpg", file);
                    }

                }
            }
        }
        public class Data
        {
            public string title { get; set; }
            public string rating { get; set; }
            public string url { get; set; }
        }
    }
}
