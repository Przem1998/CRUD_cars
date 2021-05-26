using System.Data;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using UsedCarDealer.Data;
using UsedCarDealer.Models;

namespace UsedCarDealer.Controllers
{
    public class CarController : Controller
    {
     //   private readonly UsedCarDealerContext _context;
        
        private readonly IConfiguration _config;
        public CarController(IConfiguration config)
        {
           _config = config;

        }

        public IActionResult Index()
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection conection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                conection.Open();
                SqlDataAdapter sqlData = new SqlDataAdapter("ShowAllOrParticularCar", conection);
                sqlData.SelectCommand.Parameters.AddWithValue("CarId", 0);
                sqlData.SelectCommand.CommandType = CommandType.StoredProcedure;

                sqlData.Fill(dataTable);
            }

            return View(dataTable);
        }

        public IActionResult AddOrEdit(int? id)
        {
            CarViewModel car = new CarViewModel();

            if (id > 0)
                car = FetchCarById(id);

            return View(car);
        }
        public CarViewModel FetchCarById(int? id)
        {
            CarViewModel carViewModel = new CarViewModel();

            
            using (SqlConnection conection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                DataTable dataTable = new DataTable();

                conection.Open();
                SqlDataAdapter sqlData = new SqlDataAdapter("ShowAllOrParticularCar", conection);
                sqlData.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlData.SelectCommand.Parameters.AddWithValue("CarId", id);
              
              
                sqlData.Fill(dataTable);
                if(dataTable.Rows.Count==1)
                {
                    carViewModel.Id =(int)dataTable.Rows[0]["Id"];
                    carViewModel.Brand =(string)dataTable.Rows[0]["Brand"];

                    carViewModel.Model =(string)dataTable.Rows[0]["Model"];

                    carViewModel.YearProduction =(int)dataTable.Rows[0]["YearProduction"];
                    carViewModel.Price =(decimal)dataTable.Rows[0]["Price"];
                }
            }
            return carViewModel;
        }

        // POST: CarViewModels/AddOrEdit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddOrEdit(int id, [Bind("Id,Brand,Model,YearProduction,Price")] CarViewModel carViewModel)
        {

            if (ModelState.IsValid)
            {
                using(SqlConnection conection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
                {
                    conection.Open();
                    SqlCommand sqlCommand = new SqlCommand("AddOrEditCar", conection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("CarId", carViewModel.Id);

                    sqlCommand.Parameters.AddWithValue("CarBrand", carViewModel.Brand);

                    sqlCommand.Parameters.AddWithValue("CarModel", carViewModel.Model);

                    sqlCommand.Parameters.AddWithValue("YearProduction", carViewModel.YearProduction);

                    sqlCommand.Parameters.AddWithValue("Price", carViewModel.Price);
                    sqlCommand.ExecuteNonQuery();
                }
                   
                return RedirectToAction(nameof(Index));
            }
            return View(carViewModel);
        }

        // GET: CarViewModels/Delete/5
        public IActionResult Delete(int? id)
        {
            
            CarViewModel carViewModel = FetchCarById(id);

            return View(carViewModel);
        }

        // POST: CarViewModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
         
            using (SqlConnection conection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                conection.Open();
                SqlCommand sqlCommand = new SqlCommand("DeleteCarById", conection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("CarId", id);
                sqlCommand.ExecuteNonQuery();
            }


            return RedirectToAction(nameof(Index));
        }

        //private bool CarViewModelExists(int id)
        //{
        //    return _context.CarViewModel.Any(e => e.Id == id);
        //}
    }
}
