﻿using System.Linq;
using GeoCoordinatePortable;
using Microsoft.AspNetCore.Mvc;
using Pixel.Kidsparties.Core.Interfaces;

namespace Pixel.Kidsparties.Web.ApiControllers
{
    [Route("api/cities")]
    public class CitiesController : Controller
    {
        private readonly ICityRepository _cityRepository;

        public CitiesController(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }

        [Route("closest")]
        public IActionResult GetClosest(decimal latitude, decimal longitude)
        {
            var targetCoordinate = new GeoCoordinate((double) latitude, (double) longitude);
            var cities = _cityRepository.GetAll();

            var closestCity = cities
                .Select(c => new
                {
                    Name = c.Name,
                    Slug = c.Slug,
                    Coordinate = new GeoCoordinate((double) c.Latitude, (double) c.Longitude)
                })
                .OrderBy(c => c.Coordinate.GetDistanceTo(targetCoordinate))
                .FirstOrDefault();
            
            return Ok(closestCity);
        }
    }
}
