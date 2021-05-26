using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsedCarDealer.Data;
using UsedCarDealer.Entities;

namespace UsedCarDealer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarBaseController : ControllerBase
    {
        private readonly UsedCarDealerContext _context;

        public CarBaseController(UsedCarDealerContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Car>>> GetCars()
        {
            try
            {
                return await _context.Car.ToListAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                ex.Message);
            }
        }



        [HttpGet("{id}")]
        public async Task<ActionResult<Car>> GetCar(int id)
        {
            try
            {
                return await _context.Car.FindAsync(id);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, ex.Message);

            }
        }


        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Car value)
        {

            string year = value.YearProduction.ToString();
            if (value.YearProduction.GetType() == typeof(int) && year.Length == 4)
            {

                await _context.AddAsync(value);
                await _context.SaveChangesAsync();
                return Ok($"Record {value.Brand} {value.Model} {value.YearProduction} {value.YearProduction} added successfully");
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                  "Error creating new car record");

            }

        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Car value)
        {
            var car = await _context.Car.FindAsync(id);
            if (value != null && car != null)
            {
                if (value.Brand != null)
                    car.Brand = value.Brand;
                if (value.Model != null)
                    car.Model = value.Model;
                if (value.YearProduction != 1885)
                    car.YearProduction = value.YearProduction;
                if (value.Price != 0)
                    car.Price = value.Price;

                await _context.SaveChangesAsync();
                return Ok("Record updated");
            }

            return StatusCode(StatusCodes.Status500InternalServerError,
            "Error update car record");


        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (id > 0)
            {
                _context.Car.Remove(_context.Car.Find(id));
                await _context.SaveChangesAsync();
                return Ok("deleted record successfully");
            }
            return StatusCode(StatusCodes.Status500InternalServerError,
               "Error update car record");
        }


    }
}
