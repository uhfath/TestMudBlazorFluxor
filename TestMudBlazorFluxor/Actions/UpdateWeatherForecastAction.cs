using Fluxor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestMudBlazorFluxor.Data;
using TestMudBlazorFluxor.States;

namespace TestMudBlazorFluxor.Actions
{
	public record UpdateWeatherForecastAction
	{
		public WeatherForecastRemoteData WeatherForecasts { get; init; }
		public TaskCompletionSource<WeatherForecastRemoteData> WeatherForecastsSource { get; init; }

		[ReducerMethod]
		public static WeatherForecastState Reduce(WeatherForecastState weatherForecastState, UpdateWeatherForecastAction updateWeatherForecastAction) =>
			weatherForecastState with
			{
				IsLoading = false,
				WeatherForecasts = updateWeatherForecastAction.WeatherForecasts,
			};

		private class Effect : Effect<UpdateWeatherForecastAction>
		{
			public override Task HandleAsync(UpdateWeatherForecastAction action, IDispatcher dispatcher)
			{
				action.WeatherForecastsSource.SetResult(action.WeatherForecasts);
				return Task.CompletedTask;
			}
		}
	}
}
