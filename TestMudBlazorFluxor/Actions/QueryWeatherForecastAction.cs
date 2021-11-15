using Fluxor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using TestMudBlazorFluxor.Data;
using TestMudBlazorFluxor.States;

namespace TestMudBlazorFluxor.Actions
{
	public record QueryWeatherForecastAction
	{
		public int PageIndex { get; init; }
		public int PageSize { get; init; }
		public string SortColumn { get; init; }
		public string SortDirection { get; init; }
		public TaskCompletionSource<WeatherForecastRemoteData> WeatherForecastsSource { get; init; }

		[ReducerMethod]
		public static WeatherForecastState Reduce(WeatherForecastState weatherForecastState, QueryWeatherForecastAction startWeatherForecastQueryAction) =>
			weatherForecastState with
			{
				IsLoading = true,
				PageIndex = startWeatherForecastQueryAction.PageIndex,
				PageSize = startWeatherForecastQueryAction.PageSize,
				SortColumn = startWeatherForecastQueryAction.SortColumn,
				SortDirection = startWeatherForecastQueryAction.SortDirection,
			};

		private class Effect : Effect<QueryWeatherForecastAction>
		{
			private readonly WeatherForecastService _weatherForecastService;

			public Effect(WeatherForecastService weatherForecastService)
			{
				this._weatherForecastService = weatherForecastService;
			}

			public override async Task HandleAsync(QueryWeatherForecastAction action, IDispatcher dispatcher)
			{
				var remoteData = await _weatherForecastService.GetForecastAsync(action.PageIndex, action.PageSize, action.SortColumn, action.SortDirection);
				action.WeatherForecastsSource.SetResult(remoteData);

				dispatcher.Dispatch(new UpdateWeatherForecastAction
				{
					WeatherForecasts = remoteData,
				});
			}
		}
	}
}
