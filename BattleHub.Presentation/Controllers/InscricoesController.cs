using BattleHub.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BattleHub.Presentation.Controllers;

public class InscricoesController : Controller
{
    private readonly ITorneioAppService _torneios;
    private readonly IParticipanteAppService _participantes;

    public InscricoesController(ITorneioAppService torneios, IParticipanteAppService participantes)
    {
        _torneios = torneios;
        _participantes = participantes;
    }

    public async Task<IActionResult> Create(Guid? torneioId, CancellationToken ct)
    {
        var torList = await _torneios.ListarAsync(ct);
        var partList = await _participantes.ListarAsync(ct);

        ViewBag.Torneios = new SelectList(torList, "Id", "Nome", torneioId);
        ViewBag.Participantes = new SelectList(partList, "Id", "Nome");

        return View();
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Guid torneioId, Guid participanteId, CancellationToken ct)
    {
        if (torneioId == Guid.Empty || participanteId == Guid.Empty)
        {
            TempData["erro"] = "Selecione torneio e participante.";
            return RedirectToAction(nameof(Create), new { torneioId });
        }

        try
        {
            await _torneios.InscreverParticipanteAsync(torneioId, participanteId, ct);
            TempData["ok"] = "Participante inscrito com sucesso.";
            return RedirectToAction("Details", "Torneios", new { id = torneioId });
        }
        catch (Exception ex)
        {
            TempData["erro"] = ex.Message;
            return RedirectToAction(nameof(Create), new { torneioId });
        }
    }
}
