using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;

namespace TestMudBlazorFluxor.Data
{
	public class WeatherForecastService
	{
		private static readonly string[] Summaries = new[]
		{
			"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
		};

		private static readonly WeatherForecast[] WeatherForecasts;

		static WeatherForecastService()
		{
			var random = new Random();

			WeatherForecasts = Enumerable.Range(1, 1000).Select(index => new WeatherForecast
			{
				Date = DateTime.Now.AddDays(index),
				TemperatureC = random.Next(-20, 55),
				Summary = Summaries[random.Next(Summaries.Length)]
			}).ToArray();
		}

		public async Task<(IEnumerable<WeatherForecast> items, int total)> GetForecastAsync(int pageIndex, int pageSize, string sortColumn, string sortDirection)
		{
			await Task.Delay(500); //simulate long fetch

			var items = WeatherForecasts
				.AsQueryable()
			;

			items = sortDirection?.ToLower() switch
			{
				"ascending" => items.OrderBy(sortColumn),
				"descending" => items.OrderByDescending(sortColumn),
				_ => items,
			};

			items = items
				.Skip(pageIndex * pageSize)
				.Take(pageSize)
			;

			return (items: items, total: WeatherForecasts.Length);
		}
	}
}
