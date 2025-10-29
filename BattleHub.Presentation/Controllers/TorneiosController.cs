using BattleHub.Application.Interfaces;
using BattleHub.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BattleHub.Presentation.Controllers;

public class TorneiosController : Controller
{
    private readonly ITorneioAppService _svc;

    public TorneiosController(ITorneioAppService svc) => _svc = svc;

    public async Task<IActionResult> Index(CancellationToken ct)
    {
        var itens = await _svc.ListarAsync(ct);
        return View(itens);
    }

    public async Task<IActionResult> Details(Guid id, CancellationToken ct)
    {
        var item = await _svc.ObterPorIdAsync(id, ct);
        if (item is null) return NotFound();
        return View(item);
    }

    public IActionResult Create() => View(new TorneioViewModel());

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(TorneioViewModel model, CancellationToken ct)
    {
        if (!ModelState.IsValid) return View(model);
        await _svc.CriarAsync(model, ct);
        TempData["ok"] = "Torneio criado com sucesso.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(Guid id, CancellationToken ct)
    {
        var item = await _svc.ObterPorIdAsync(id, ct);
        if (item is null) return NotFound();
        return View(item);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, TorneioViewModel model, CancellationToken ct)
    {
        if (id != model.Id) return BadRequest();
        if (!ModelState.IsValid) return View(model);

        await _svc.AtualizarAsync(model, ct);
        TempData["ok"] = "Torneio atualizado.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        var item = await _svc.ObterPorIdAsync(id, ct);
        if (item is null) return NotFound();
        return View(item);
    }

    [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id, CancellationToken ct)
    {
        await _svc.RemoverAsync(id, ct);
        TempData["ok"] = "Torneio removido.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Publicar(Guid id, CancellationToken ct)
    {
        try
        {
            await _svc.PublicarAsync(id, ct);
            TempData["ok"] = "Torneio publicado e partidas geradas.";
        }
        catch (Exception ex)
        {
            TempData["erro"] = ex.Message;
        }
        return RedirectToAction("Details", new { id });
    }
}
