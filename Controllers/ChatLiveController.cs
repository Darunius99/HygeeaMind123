// HygeeaMind/Controllers/ChatLiveController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization; // Adaugă acest using dacă vrei să restricționezi accesul

namespace HygeeaMind.Controllers
{
    public class ChatLiveController : Controller
    {
        // Acțiunea Index va returna View-ul Chat Live
        // Opțional: poți adăuga [Authorize] aici dacă vrei ca doar utilizatorii autentificați să acceseze chat-ul
        // [Authorize] 
        public IActionResult Index()
        {
            return View();
        }
    }
}