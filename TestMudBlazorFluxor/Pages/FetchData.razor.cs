using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using TestMudBlazorFluxor.Data;

namespace TestMudBlazorFluxor.Pages
{
	public partial class FetchData : ComponentBase
	{
		[Inject]
		private WeatherForecastService ForecastService { get; set; }

		private bool _isLoading;

		private async Task<TableData<WeatherForecast>> OnDataLoad(TableState tableState)
		{
			_isLoading = true;
			StateHasChanged();

			try
			{
				var data = await ForecastService.GetForecastAsync(tableState.Page, tableState.PageSize, tableState.SortLabel, tableState.SortDirection.ToString());
				return new TableData<WeatherForecast>
				{
					Items = data.items,
					TotalItems = data.total,
				};
			}
			finally
			{
				_isLoading = false;
				StateHasChanged();
			}
		}
	}
}
