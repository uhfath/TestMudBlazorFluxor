using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestMudBlazorFluxor.Data
{
	public record WeatherForecastRemoteData
	{
		public IEnumerable<WeatherForecast> Items { get; init; }
		public int Total { get; init; }
	}
}
