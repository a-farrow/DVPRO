using DVPRO.DATA.EF.Models;
using DVPRO.UI.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using ChartJSCore.Models;
using ChartJSCore.Helpers;

namespace DVPRO.UI.MVC.Controllers
{
    [Authorize(Roles = "Admin, HR, Accounting")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly AtomicContext _context;

        public HomeController(ILogger<HomeController> logger, AtomicContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {

            var salesData = _context.Sales
                .Include(s => s.Customer)
                .Include(s => s.Customer.Country)
                .Include(s => s.Customer.Country.Region)
                .Include(s => s.Salesperson)
                .Where(s => s.SaleDate.Year == 2022)
                .OrderBy(s => s.SaleDate)
                .ToList();

            List<decimal?> monthlySales = salesData
                .GroupBy(sale => new { sale.SaleDate.Year, sale.SaleDate.Month })
                .Select(group => group.Sum(sale => sale.SaleTotal))
                .ToList();

            //Y-Axis: 
            List<double?> monthlyTotalSales = monthlySales.ConvertAll(s => (double?)s);

            //X-Axis: month names
            List<string> monthNames = salesData
                .Select(sale => sale.SaleDate.ToString("MMM"))
                .Distinct()
                .ToList();

            Chart salesBarChart = new Chart();

            salesBarChart.Type = Enums.ChartType.Bar;

            ChartJSCore.Models.Data barChartData = new ChartJSCore.Models.Data();

            barChartData.Labels = monthNames;

            BarDataset barDataset = new BarDataset()
            {
                Label = "Sales",
                Data = monthlyTotalSales,
                BackgroundColor = new List<ChartColor>
                {
                    ChartColor.FromRgb(28, 200, 138)
                },
                BorderColor = new List<ChartColor>
                {
                    ChartColor.FromRgb(28, 200, 138)
                },
                BorderWidth = new List<int>() { 1 },
                BarPercentage = 1,
                BarThickness = 50,
                MaxBarThickness = 50,
                MinBarLength = 2
            };

            barChartData.Datasets = new List<Dataset>();

            barChartData.Datasets.Add(barDataset);

            salesBarChart.Data = barChartData;

            var options = new Options
            {
                Responsive = true,
                MaintainAspectRatio = false
            };

            ViewData["BarChart"] = salesBarChart;
                
            
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        

    }

}
