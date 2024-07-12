using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Policy;
using TrakNusProj.Data;
using TrakNusProj.Handler;
using TrakNusProj.Models;
using TrakNusProj.Models.ViewModels;

namespace TrakNusProj.Controllers
{
    public class DataKaryawanController : Controller
    {
        protected readonly DBContextSample _dbContext;
        //private readonly RabbitMQPublisher _publisher;
        public DataKaryawanController(DBContextSample dbContext)
        {
            _dbContext = dbContext;
            //_publisher = publisher;
        }

        [HttpGet]
        public ActionResult Insert()
        {

            return View();
        }

        [HttpGet]
        public ActionResult Update()
        {

            return View();
        }

        [HttpGet]
        public ActionResult Delete()
        {

            return View();
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var dataKaryawan = await _dbContext.DataKaryawans.ToListAsync();
            return View(dataKaryawan);
        }

        [HttpPost]
        public async Task<ActionResult> Insert(DataKaryawanInput dataKaryawanInput)
        {
            var dataKaryawan = new DataKaryawan()
            {
                NRP = dataKaryawanInput.NRP,
                Name = dataKaryawanInput.Name,
                Divisi = dataKaryawanInput.Divisi,
                Department = dataKaryawanInput.Department
            };

            await _dbContext.DataKaryawans.AddAsync(dataKaryawan);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Update(DataKaryawanInput dataKaryawanInput)
        {
            var dataKaryawan = await _dbContext.DataKaryawans
                                       .FirstOrDefaultAsync(dk => dk.NRP == dataKaryawanInput.NRP);
            if (dataKaryawan == null)
            {
                return NotFound();
            }

            dataKaryawan.NRP = dataKaryawanInput.NRP;
            dataKaryawan.Name = dataKaryawanInput.Name;
            dataKaryawan.Divisi = dataKaryawanInput.Divisi;
            dataKaryawan.Department = dataKaryawanInput.Department;

            _dbContext.DataKaryawans.Update(dataKaryawan);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DataKaryawanInput dataKaryawanInput)
        {
            var dataKaryawan = await _dbContext.DataKaryawans
                                       .FirstOrDefaultAsync(dk => dk.NRP == dataKaryawanInput.NRP);
            if (dataKaryawan == null)
            {
                return NotFound();
            }

            _dbContext.DataKaryawans.Remove(dataKaryawan);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }

    }
}
