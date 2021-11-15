using Fluxor;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using TestMudBlazorFluxor.Actions;
using TestMudBlazorFluxor.Data;
using TestMudBlazorFluxor.States;

namespace TestMudBlazorFluxor.Pages
{
	public partial class FetchData : ComponentBase
	{
		[Inject]
		private IState<WeatherForecastState> WeatherForecastState { get; set; }

		[Inject]
		private IDispatcher Dispatcher { get; set; }

		private async Task<TableData<WeatherForecast>> OnDataLoad(TableState tableState)
		{
			await Task.Yield(); //workaround

			Dispatcher.Dispatch(new QueryWeatherForecastAction
			{
				PageIndex = tableState.Page,
				PageSize = tableState.PageSize,
				SortColumn = tableState.SortLabel,
				SortDirection = tableState.SortDirection.ToString(),
			});

			var remoteData = await WeatherForecastState.Value.WeatherForecasts;
			return new TableData<WeatherForecast>
			{
				Items = remoteData.Items,
				TotalItems = remoteData.Total,
			};
		}
	}
}
