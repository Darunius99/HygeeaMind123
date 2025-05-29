using HygeeaMind.Data; // Pentru a accesa ApplicationDbContext
using HygeeaMind.Models; // Pentru a accesa modelele Article și Specialist
using HygeeaMind.ViewModels; // Pentru a accesa HomeViewModel
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // Necesare pentru metodele ToListAsync, Contains etc.
using System.Diagnostics; // Pentru ErrorViewModel

namespace HygeeaMind.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context; // Instanța contextului bazei de date

        // Constructorul injectează ILogger și ApplicationDbContext
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        // Acțiunea Index pentru pagina principală
        public async Task<IActionResult> Index(string searchString)
        {
            // Interogare pentru articole
            var articles = from a in _context.Articles
                           select a;

            // Logica pentru căutarea articolelor (cerința 3)
            if (!String.IsNullOrEmpty(searchString))
            {
                // Căutare case-insensitive după titlu sau conținut
                articles = articles.Where(s => s.Title.Contains(searchString) || s.Content.Contains(searchString));
            }

            // Preluarea tuturor specialiștilor
            var specialists = await _context.Specialists.ToListAsync();

            // Crearea ViewModel-ului cu datele preluate
            var viewModel = new HomeViewModel
            {
                Articles = await articles.ToListAsync(), // Execută interogarea pentru articole
                Specialists = specialists,
                SearchString = searchString // Păstrează stringul de căutare pentru afișare în view
            };

            // Returnează view-ul cu ViewModel-ul populat
            return View(viewModel);
        }

        // Acțiune pentru pagina de confidențialitate (generată implicit)
        public IActionResult Privacy()
        {
            return View();
        }

        // Acțiune pentru pagina About (Despre Noi)
        public IActionResult About()
        {
            return View();
        }

        // Acțiune pentru pagina Contact
        public IActionResult Contact()
        {
            return View();
        }

        // Acțiune pentru pagina de chat (va fi implementată ulterior)
        public IActionResult Chat()
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