using BattleHub.Application.Interfaces;
using BattleHub.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BattleHub.Presentation.Controllers;

public class ParticipantesController : Controller
{
    private readonly IParticipanteAppService _svc;

    public ParticipantesController(IParticipanteAppService svc) => _svc = svc;

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

    public IActionResult Create() => View(new ParticipanteViewModel());

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ParticipanteViewModel model, CancellationToken ct)
    {
        if (!ModelState.IsValid) return View(model);
        await _svc.CriarAsync(model, ct);
        TempData["ok"] = "Participante criado com sucesso.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(Guid id, CancellationToken ct)
    {
        var item = await _svc.ObterPorIdAsync(id, ct);
        if (item is null) return NotFound();
        return View(item);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, ParticipanteViewModel model, CancellationToken ct)
    {
        if (id != model.Id) return BadRequest();
        if (!ModelState.IsValid) return View(model);

        await _svc.AtualizarAsync(model, ct);
        TempData["ok"] = "Participante atualizado.";
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
        TempData["ok"] = "Participante removido.";
        return RedirectToAction(nameof(Index));
    }
}
