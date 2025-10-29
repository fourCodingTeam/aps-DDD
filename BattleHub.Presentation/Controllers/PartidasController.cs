using BattleHub.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BattleHub.Presentation.Controllers
{
    public class PartidasController : Controller
    {
        private readonly IPartidaAppService _svc;

        public PartidasController(IPartidaAppService svc) => _svc = svc;

        public async Task<IActionResult> Index(Guid torneioId)
        {
            var lista = await _svc.ListarPorTorneioAsync(torneioId);
            ViewBag.TorneioId = torneioId;
            return View(lista);
        }

        [HttpPost]
        public async Task<IActionResult> Reportar(Guid id, Guid vencedorId, Guid torneioId, string placar)
        {
            try
            {
                await _svc.ReportarVencedorAsync(id, vencedorId, placar);
                TempData["ok"] = "Resultado registrado.";
            }
            catch (Exception ex)
            {
                TempData["erro"] = ex.Message;
            }

            return RedirectToAction("Index", new { torneioId });
        }
    }
}
