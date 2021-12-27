using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MMSIMDB.Application.Business.Movies.Commands.AddRating;
using MMSIMDB.Application.Business.Movies.Queries.GetMoviePage;
using System.Threading.Tasks;

namespace MMSIMDB.WebAPI.Controllers.v1
{
    [ApiVersion("1.0")]
    [Authorize]
    public class MovieController : BaseApiController
    {
        [HttpPost]
        public async Task<IActionResult> GetMoviePage(GetMoviePageQueryRequest request)
        {
            return Ok(await Mediator.Send(request));
        }

        [HttpPost]
        public async Task<IActionResult> AddRating(AddRatingCommandRequest request)
        {
            return Ok(await Mediator.Send(request));
        }

        [HttpGet("{name}")]
        [AllowAnonymous]
        public IActionResult GetImage(string name)
        {
            //Pojednostavljeno rešenje
            try
            {
                Byte[] b = System.IO.File.ReadAllBytes(@$"images\{name}.jpg");
                return File(b, "image/jpeg");
            }
            catch 
            {
                return NotFound();
            }
        }
    }
}
