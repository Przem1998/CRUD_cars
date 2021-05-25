using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Data;

namespace WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly StoreContext _context;

        public CarController(StoreContext context) 
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Car>>> GetCars()
        {
            try
            {
                return await _context.Cars.ToListAsync();
            }catch(SqliteException ex)
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
                return await _context.Cars.FindAsync(id);
   
            }
            catch (SqliteException ex)
            {
                return StatusCode(StatusCodes.Status404NotFound,ex.Message);

            }
        }


        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Car value)
        {
    
            string year = value.YearProduction.ToString();
            if(value.YearProduction.GetType()== typeof(int) && year.Length==4)
            {

                await _context.AddAsync(value);
                await _context.SaveChangesAsync();
                return Ok($"Record {value.CarCompany} {value.CarModel} {value.YearProduction} added successfully");
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                  "Error creating new car record");

            }

        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody]Car value)
        {
            var car = await _context.Cars.FindAsync(id);
            if (value!=null && car !=null)
            {
                if (value.CarCompany != null)
                    car.CarCompany = value.CarCompany;
                if (value.CarModel != null)
                    car.CarModel = value.CarModel;
                if (value.YearProduction!=0)
                    car.YearProduction = value.YearProduction;

                await _context.SaveChangesAsync();
                return Ok("Record updated");
            }
            
                return StatusCode(StatusCodes.Status500InternalServerError,
                "Error update car record");
           

        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            if(id>0)
            {
               _context.Cars.Remove(_context.Cars.Find(id));
                await _context.SaveChangesAsync();
                return Ok("deleted record successfully");
            }
            return StatusCode(StatusCodes.Status500InternalServerError,
               "Error update car record");
        }


    }
}
