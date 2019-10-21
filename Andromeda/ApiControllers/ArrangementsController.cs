using System.Collections.Generic;
using System.Threading.Tasks;
using Andromeda.Models;
using Microsoft.AspNetCore.Mvc;

namespace Andromeda.ApiControllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class ArrangementsController : ControllerBase
   {
      [HttpGet("")]
      public async Task<IActionResult> Get()
      {
         await Task.CompletedTask;

         var mockData = new List<ArrangementDetailsViewModel>
            {
                new ArrangementDetailsViewModel
                {
                    CityName = "Halmstad",
                    Type = "Gokart",
                    Name = "Gokarthallen",
                    CoverImage = "https://lh3.googleusercontent.com/p/AF1QipOSv7Haz7qwKgI0RBIsKICNtpvI0pRVryA1PBPW=w812-k"
                },
                new ArrangementDetailsViewModel
                {
                    CityName = "Göteborg",
                    Type = "Pyssel",
                    Name = "Pick-n-Paint",
                    CoverImage = "https://lh3.googleusercontent.com/p/AF1QipNdFBiEPjk00dY_mC4AiH4H-S0VXwO-KFmjEWqK=w812-k"
                },
                new ArrangementDetailsViewModel
                {
                    CityName = "Göteborg",
                    Type = "Bowling",
                    Name = "Rock & Bowl",
                    CoverImage = "https://lh3.googleusercontent.com/p/AF1QipN6POUxfmj-Y5SxGb823dFdGb9uXaQfrlpx6Buw=w812-k"
                },
                new ArrangementDetailsViewModel
                {
                    CityName = "Stockholm",
                    Type = "Bad",
                    Name = "Medley Nacka simhall",
                    CoverImage = "https://lh3.googleusercontent.com/p/AF1QipMrhVBKyauIUvfByaWF2zfjRz1A2qrV-EU_WZyy=w812-k"
                },
                new ArrangementDetailsViewModel
                {
                    CityName = "Stockholm",
                    Type = "Lekland",
                    Name = "PlayLand STHL",
                    CoverImage = "https://lh3.googleusercontent.com/p/AF1QipMW7fQpgzj20W79qc07Q6_lCV7NMZvOgLTVQyEf=w812-k"
                }
            };

         return Ok(mockData);
      }
   }
}
