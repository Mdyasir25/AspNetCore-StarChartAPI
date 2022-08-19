using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using StarChart.Data;
using StarChart.Models;

namespace StarChart.Controllers
{
    [ApiController]
    [Route("")]
    public class CelestialObjectController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public CelestialObjectController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var obj = new CelestialObject();
            
            foreach (var celestialObject in _context.CelestialObjects)
            {
                if (celestialObject.Id == id)
                {
                    obj = celestialObject;
                }
            }
            foreach (var celestialObject in _context.CelestialObjects)
            {
                if (celestialObject.OrbitedObjectId == id)
                {
                    //obj.Satellites.Add(celestialObject);
                    celestialObject.Satellites = new List<CelestialObject>();
                }
            }
            if (obj == null) return NotFound();
            return Ok(obj);
        }

        [HttpGet("{name}")]
        public IActionResult GetByName(string name)
        {
            var obj = new CelestialObject();

            foreach (var celestialObject in _context.CelestialObjects)
            {
                if (celestialObject.Name == name)
                {
                    obj = celestialObject;
                }
            }
            foreach (var celestialObject in _context.CelestialObjects)
            {
                if (celestialObject.OrbitedObjectId == obj.Id)
                {
                    //obj.Satellites.Add(celestialObject);
                    celestialObject.Satellites = new List<CelestialObject>();
                }
            }
            if (obj == null) return NotFound();
            return Ok(obj);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            foreach (var celestialObject in _context.CelestialObjects)
            {
                celestialObject.Satellites = new List<CelestialObject>();
            }
            return Ok(_context);
        }
    }
}
