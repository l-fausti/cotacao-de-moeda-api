using System.Diagnostics;
using System.Text.Json;
using DashFinanceApi.Models;

namespace DashFinanceApi.Services;

public class CotacaoService
{
    private readonly IHttpClientFactory _clientFactory;

    public CotacaoService(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
    }

    public async Task<CotacaoMoeda> ObterCotacaoAsync(string deMoeda, string paraMoeda)
    {
        var client = _clientFactory.CreateClient();
        string par = $"{deMoeda.ToUpper()}-{paraMoeda.ToUpper()}";
    string url = $"https://economia.awesomeapi.com.br/last/{par}";

        var response = await client.GetAsync(url);

        if (!response.IsSuccessStatusCode)
            return new CotacaoMoeda { Moeda = deMoeda, ValorReais = 0, DataConsulta = DateTime.Now };

        var content = await response.Content.ReadAsStringAsync();
        using var jsonDoc = JsonDocument.Parse(content);

        var chave = $"{deMoeda.ToUpper()}{paraMoeda.ToUpper()}";
        var bidString = jsonDoc.RootElement.GetProperty(chave).GetProperty("bid").GetString();

        return new CotacaoMoeda
        {
            Moeda = $"{deMoeda}/{paraMoeda}",
            ValorReais = decimal.Parse(bidString ?? "0", System.Globalization.CultureInfo.InvariantCulture),
            DataConsulta = DateTime.Now
        };
    }

    public async Task<ResumoDashboard> ObterDashboardParaleloAsync()
    {
        var stopwatch = Stopwatch.StartNew();

        Task<CotacaoMoeda> taskDolar = ObterCotacaoAsync("USD", "BRL");
        Task<CotacaoMoeda> taskEuro = ObterCotacaoAsync("EUR", "BRL");
        Task<CotacaoMoeda> taskRealDolar = ObterCotacaoAsync("BRL", "USD");

        await Task.WhenAll(taskDolar, taskEuro, taskRealDolar);

        stopwatch.Stop();

        return new ResumoDashboard
        {
            Dolar = await taskDolar,
            Euro = await taskEuro,
            RealEmDolar = await taskRealDolar,
            StatusServico = "Online",
            TempoRespostaMs = stopwatch.ElapsedMilliseconds
        };
    }

    internal async Task ObterCotacaoAsync(object deMoeda, object paraMoeda)
    {
        throw new NotImplementedException();
    }
}