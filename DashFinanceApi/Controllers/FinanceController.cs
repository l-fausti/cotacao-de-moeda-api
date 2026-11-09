using Microsoft.AspNetCore.Mvc;
using DashFinanceApi.Models;
using DashFinanceApi.Services;

namespace DashFinanceApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FinanceController : ControllerBase
{
    private readonly CotacaoService _cotacaoService;

    public FinanceController(CotacaoService cotacaoService)
    {
        _cotacaoService = cotacaoService;
    }

    [HttpGet("cotacao/{deMoeda}/{paraMoeda}")]
    public async Task<ActionResult<CotacaoMoeda>> GetCotacao(string deMoeda, string paraMoeda)
    {
        var cotacao = await _cotacaoService.ObterCotacaoAsync(deMoeda, paraMoeda);

        if (cotacao.ValorReais == 0)
            return NotFound(new { mensagem = $"Par '{deMoeda}/{paraMoeda}' não encontrado ou serviço indisponível." });

        return Ok(cotacao);
    }

    [HttpGet("dashboard")]
    public async Task<ActionResult<ResumoDashboard>> GetDashboard()
    {
        var dashboard = await _cotacaoService.ObterDashboardParaleloAsync();
        return Ok(dashboard);
    }
}