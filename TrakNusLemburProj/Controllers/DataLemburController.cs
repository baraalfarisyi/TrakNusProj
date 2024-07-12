using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrakNusLemburProj.Data;
using TrakNusLemburProj.Models;
using TrakNusLemburProj.Models.ViewModels;
using Stimulsoft.Report;
using Stimulsoft.Report.Mvc;
using Stimulsoft.Report.Web;
using static Stimulsoft.Report.StiOptions;
using Microsoft.AspNetCore.Hosting;
using Stimulsoft.Blockly.Model;
using iTextSharp.text.pdf;
using iTextSharp.text;

namespace TrakNusLemburProj.Controllers
{
    public class DataLemburController : Controller
    {
        protected readonly DBLemburContextSample _dbContext;
        
        public DataLemburController(DBLemburContextSample dbContext)
        {
            _dbContext = dbContext;
            
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
            var dataLembur = await _dbContext.DataLemburs.ToListAsync();
            return View(dataLembur);
        }

        [HttpPost]
        public async Task<ActionResult> Insert(DataLemburInput dataLemburInput)
        {
            var dataLembur = new DataLembur()
            {
                NRP = dataLemburInput.NRP,
                Name = dataLemburInput.Name,
                Divisi = dataLemburInput.Divisi,
                Department = dataLemburInput.Department,
                TglLembur = dataLemburInput.TglLembur,
                MulaiLembur = dataLemburInput.MulaiLembur,
                AkhirLembur = dataLemburInput.AkhirLembur
            };

            //var dataLemburReport = new LemburReportViewModel()
            //{
            //    NRP = dataLemburInput.NRP,
            //    Name = dataLemburInput.Name,
            //    Divisi = dataLemburInput.Divisi,
            //    Department = dataLemburInput.Department,
            //    Bulan = dataLemburInput.TglLembur.Month,
            //    Tahun = dataLemburInput.TglLembur.Year,
            //    TotalJamLembur = (decimal)(dataLemburInput.MulaiLembur - dataLemburInput.AkhirLembur).TotalHours
            //};

            await _dbContext.DataLemburs.AddAsync(dataLembur);
            //await _dbContext.LemburReportViewModels.AddAsync(dataLemburReport);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Update(DataLemburInput dataLemburInput)
        {
            var dataLembur = await _dbContext.DataLemburs
                                       .FirstOrDefaultAsync(dk => dk.NRP == dataLemburInput.NRP);

            if (dataLembur == null)
            {
                return NotFound();
            }

            dataLembur.NRP = dataLemburInput.NRP;
            dataLembur.Name = dataLemburInput.Name;
            dataLembur.Divisi = dataLemburInput.Divisi;
            dataLembur.Department = dataLemburInput.Department;
            dataLembur.TglLembur = dataLemburInput.TglLembur;
            dataLembur.MulaiLembur = dataLemburInput.MulaiLembur;
            dataLembur.AkhirLembur = dataLemburInput.AkhirLembur;

            _dbContext.DataLemburs.Update(dataLembur);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DataLemburInput dataLemburInput)
        {
            var dataLembur = await _dbContext.DataLemburs
                                       .FirstOrDefaultAsync(dk => dk.NRP == dataLemburInput.NRP);

            if (dataLembur == null)
            {
                return NotFound();
            }

            _dbContext.DataLemburs.Remove(dataLembur);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Report()
        {
            var lemburs = _dbContext.DataLemburs
                                    .GroupBy(l => new { l.NRP, l.Name, l.Divisi, l.Department })
                                    .Select(g => new LemburReportViewModel
                                    {
                                        NRP = g.Key.NRP,
                                        Name = g.Key.Name,
                                        Divisi = g.Key.Divisi,
                                        Department = g.Key.Department,
                                        TotalJamLembur = g.Sum(l => EF.Functions.DateDiffHour(l.MulaiLembur, l.AkhirLembur))
                                    })
                                    .ToList();

            return View(lemburs);
        }

        [HttpPost]
        public ActionResult ExportToPDF()
        {
            var lemburs = _dbContext.DataLemburs
                                    .GroupBy(l => new { l.NRP, l.Name, l.Divisi, l.Department })
                                    .Select(g => new LemburReportViewModel
                                    {
                                        NRP = g.Key.NRP,
                                        Name = g.Key.Name,
                                        Divisi = g.Key.Divisi,
                                        Department = g.Key.Department,
                                        TotalJamLembur = g.Sum(l => EF.Functions.DateDiffHour(l.MulaiLembur, l.AkhirLembur))
                                    })
                                    .ToList();

            // Ensure the correct encoding
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            using (MemoryStream workStream = new MemoryStream())
            {
                Document doc = new Document();
                PdfWriter writer = PdfWriter.GetInstance(doc, workStream);
                writer.CloseStream = false;

                doc.Open();
                doc.Add(new Paragraph("Laporan Jam Lembur Karyawan"));

                PdfPTable table = new PdfPTable(5);
                table.AddCell("NRP");
                table.AddCell("Nama Karyawan");
                table.AddCell("Divisi");
                table.AddCell("Department");
                table.AddCell("Total Jam Lembur");

                foreach (var item in lemburs)
                {
                    table.AddCell(item.NRP);
                    table.AddCell(item.Name);
                    table.AddCell(item.Divisi);
                    table.AddCell(item.Department);
                    table.AddCell(item.TotalJamLembur.ToString());
                }
                doc.Add(table);

                doc.Close();

                byte[] byteInfo = workStream.ToArray();
                workStream.Write(byteInfo, 0, byteInfo.Length);
                workStream.Position = 0;

                return File(workStream, "application/pdf", "LemburReport.pdf");
            }
        }


    }
}
