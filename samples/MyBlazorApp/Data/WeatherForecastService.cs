namespace MyBlazorApp.Data;

public class WeatherForecastService
{
    public Task<WeatherForecast[]> GetForecastAsync(DateOnly startDate)
    {
        return Task.FromResult(Enumerable.Range(1, 5).Select(index =>
        {
            int temp = Random.Shared.Next(-50, 50);
            string summary = temp switch
            {
                <= -40 => "SIBIR!! 🥶",
                <= -30 => "Blåswix ❄️ ",
                <= -20 => "Chilly 🌨️",
                <= -10 => "Cool 🧣",
                <= -0 => "Warm 🌨️",
                <= 10 => "Balmy 😎 ",
                <= 20 => "Hot ☀️☀️☀️",
                <= 30 => "Sweltering 😓",
                _ => "Scorching 🔥🔥🔥"
            };
            return new WeatherForecast
            {
                Date = startDate.AddDays(index),
                TemperatureC = temp,
                Summary = summary
            };
        }).ToArray());
    }
}
