namespace DashFinanceApi.Models;

public class CotacaoMoeda
{
    public string Moeda { get; set; } = string.Empty;
    public decimal ValorReais { get; set; }
    public DateTime DataConsulta { get; set; }
}

public class ResumoDashboard
{
    public CotacaoMoeda Dolar { get; set; } = new CotacaoMoeda();
    public CotacaoMoeda Euro { get; set; } = new CotacaoMoeda();
    public CotacaoMoeda RealEmDolar { get; set; } = new CotacaoMoeda();
    public string StatusServico { get; set; } = string.Empty;
    public long TempoRespostaMs { get; set; }
}