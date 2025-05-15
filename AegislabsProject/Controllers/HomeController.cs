using System.Diagnostics;
using AegislabsProject.Models;
using AegislabsProject.Services;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Rotativa.AspNetCore;

namespace AegislabsProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly KRSService _service;

        public HomeController(KRSService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _service.GetAllAsync();

            return View(data);
        }

        public async Task<IActionResult> ExportExcel()
        {
            var data = await _service.GetAllAsync();

            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("DataKRS");

            worksheet.Cell(1, 1).Value = "Mahasiswa";
            worksheet.Cell(1, 2).Value = "Mata Kuliah";
            worksheet.Cell(1, 3).Value = "SKS";
            worksheet.Cell(1, 4).Value = "Semester";
            worksheet.Cell(1, 5).Value = "Tahun Ajaran";

            for (int i = 0; i < data.Count; i++)
            {
                var row = i + 2;
                worksheet.Cell(row, 1).Value = data[i].NamaMahasiswa;
                worksheet.Cell(row, 2).Value = data[i].NamaMataKuliah;
                worksheet.Cell(row, 3).Value = data[i].SKS;
                worksheet.Cell(row, 4).Value = data[i].Semester;
                worksheet.Cell(row, 5).Value = data[i].TahunAjaran;
            }

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            stream.Seek(0, SeekOrigin.Begin);

            return File(stream.ToArray(),
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "KRS.xlsx");
        }

        public async Task<IActionResult> ExportPDF()
        {
            var data = await _service.GetAllAsync();
            return new ViewAsPdf("Print", data)
            {
                FileName = "KRS.pdf",
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                PageSize = Rotativa.AspNetCore.Options.Size.A4
            };
        }
    }
}
