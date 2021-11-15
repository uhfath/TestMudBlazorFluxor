using Fluxor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestMudBlazorFluxor.Data;

namespace TestMudBlazorFluxor.States
{
	[FeatureState]
	public record WeatherForecastState
	{
		public bool IsLoading { get; init; }
		public int PageIndex { get; init; }
		public int PageSize { get; init; }
		public string SortColumn { get; init; }
		public string SortDirection { get; init; }
		public Task<WeatherForecastRemoteData> WeatherForecasts { get; init; } = Task.FromResult<WeatherForecastRemoteData>(new());
	}
}
