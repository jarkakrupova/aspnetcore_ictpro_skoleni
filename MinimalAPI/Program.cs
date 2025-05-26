using Microsoft.Extensions.Logging.Abstractions;

//pro malá API, kde není nutnıch víc controllerù
//umí to .http pro testování API 
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//!!!POZOR!!! v .NET 9 není pøidanı Swagger, chybí balíèek Swashbuckle 
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline. - kudy prochází request
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();// zaškrtli jsme, e chceme HTTPS
//veškerı kód appky je v 1 souboru


//kod, ktery je bezne ve WeatherForecastControlleru
var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

//rucni mapovani, co se ma dit, kdyz prijde dotaz na https://...../weatherforecast
//https://localhost:44344/weatherforecast 
app.MapGet("/weatherforecast", () => {
    //Enumerable.Range vrati 1-5
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast"); //tohle pojmenovani je nepovinne, Swagger vezme metodu WithDescription a textem endpoint popíše

app.MapGet("/", () => $"API bìí, nyní je {DateTime.Now.ToLongDateString()}"); 


app.Run(); //spusti appku

//record je alternativa tridy, umi automaticky vypsat ToString
//neni spatne, kdyz WeatherForecast bude trida
//podobne jako struct

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary) 
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
