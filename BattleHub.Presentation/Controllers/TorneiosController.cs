using BattleHub.Application.Interfaces;
using BattleHub.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BattleHub.Presentation.Controllers;

public class TorneiosController : Controller
{
    private readonly ITorneioAppService _svc;

    public TorneiosController(ITorneioAppService svc) => _svc = svc;

    public async Task<IActionResult> Index()
    {
        var itens = await _svc.ListarAsync();
        return View(itens);
    }

    public async Task<IActionResult> Details(Guid id)
    {
        var item = await _svc.ObterPorIdAsync(id);
        if (item is null) return NotFound();
        return View(item);
    }

    public IActionResult Create() => View(new TorneioViewModel());

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(TorneioViewModel model)
    {
        if (!ModelState.IsValid) return View(model);
        await _svc.CriarAsync(model);
        TempData["ok"] = "Torneio criado com sucesso.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(Guid id)
    {
        var item = await _svc.ObterPorIdAsync(id);
        if (item is null) return NotFound();
        return View(item);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, TorneioViewModel model)
    {
        if (id != model.Id) return BadRequest();
        if (!ModelState.IsValid) return View(model);

        await _svc.AtualizarAsync(model);
        TempData["ok"] = "Torneio atualizado.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(Guid id)
    {
        var item = await _svc.ObterPorIdAsync(id);
        if (item is null) return NotFound();
        return View(item);
    }

    [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        await _svc.RemoverAsync(id);
        TempData["ok"] = "Torneio removido.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Publicar(Guid id)
    {
        try
        {
            await _svc.PublicarAsync(id);
            TempData["ok"] = "Torneio publicado e partidas geradas.";
        }
        catch (Exception ex)
        {
            TempData["erro"] = ex.Message;
        }
        return RedirectToAction("Details", new { id });
    }
}
