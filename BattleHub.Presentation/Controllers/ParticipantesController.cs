using BattleHub.Application.Interfaces;
using BattleHub.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BattleHub.Presentation.Controllers;

public class ParticipantesController : Controller
{
    private readonly IParticipanteAppService _svc;

    public ParticipantesController(IParticipanteAppService svc) => _svc = svc;

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

    public IActionResult Create() => View(new ParticipanteViewModel());

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ParticipanteViewModel model)
    {
        if (!ModelState.IsValid) return View(model);
        await _svc.CriarAsync(model);
        TempData["ok"] = "Participante criado com sucesso.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(Guid id)
    {
        var item = await _svc.ObterPorIdAsync(id);
        if (item is null) return NotFound();
        return View(item);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, ParticipanteViewModel model)
    {
        if (id != model.Id) return BadRequest();
        if (!ModelState.IsValid) return View(model);

        await _svc.AtualizarAsync(model);
        TempData["ok"] = "Participante atualizado.";
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
        TempData["ok"] = "Participante removido.";
        return RedirectToAction(nameof(Index));
    }
}
