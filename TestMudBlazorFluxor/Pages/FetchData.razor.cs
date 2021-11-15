using Fluxor;
using Fluxor.Blazor.Web.Components;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;
using TestMudBlazorFluxor.Actions;
using TestMudBlazorFluxor.Data;
using TestMudBlazorFluxor.States;

namespace TestMudBlazorFluxor.Pages
{
	public partial class FetchData : FluxorComponent
	{
		[Inject]
		private IState<WeatherForecastState> WeatherForecastState { get; set; }

		[Inject]
		private IDispatcher Dispatcher { get; set; }

		private async Task<TableData<WeatherForecast>> OnDataLoad(TableState tableState)
		{
			var weatherForecastsSource = new TaskCompletionSource<WeatherForecastRemoteData>();

			Dispatcher.Dispatch(new QueryWeatherForecastAction
			{
				PageIndex = tableState.Page,
				PageSize = tableState.PageSize,
				SortColumn = tableState.SortLabel,
				SortDirection = tableState.SortDirection.ToString(),
				WeatherForecastsSource = weatherForecastsSource,
			});

			var remoteData = await weatherForecastsSource.Task;
			return new()
			{
				Items = remoteData.Items,
				TotalItems = remoteData.Total,
			};
		}
	}
}
