using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Web.Helpers;
using Web.Interfaces.Config;
using Web.ViewModels;
using Web.ViewModels.Config;
using Web.ViewModels.Helpers;

namespace Web.Services.Config
{
    [Authorize(Policy = "Bearer")]
    public class ClientApiService : IClientApiService
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _config;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AppSettingsViewModel _appSettings;

        public ClientApiService(IConfiguration config, IHttpContextAccessor httpContextAccessor, AppSettingsViewModel appSettings)
        {
            _config = config;
            _httpContextAccessor = httpContextAccessor;
            _appSettings = appSettings;

            _client = AuthHelper.ClientBearear(_httpContextAccessor, _appSettings);
        }

        public async Task<DatatablesPagedResultsViewModel<ClientApiViewModel>> LoadDatatablesAsync(DatatablesParameterViewModel param)
        {
            var response = await _client.PostAsync("ClientApi/get/datatables", new StringContent(JsonConvert.SerializeObject(param), Encoding.UTF8, "application/json"));
            var contents = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<BaseApiResponseViewModel>(contents);
            var data = JsonConvert.SerializeObject(result.Data);

            return JsonConvert.DeserializeObject<DatatablesPagedResultsViewModel<ClientApiViewModel>>(data);
        }

        public async Task<BaseApiResultViewModel<ClientApiViewModel>> GetAsync(Guid id)
        {
            var response = await _client.GetAsync("ClientApi/get/" + id);
            var contents = await response.Content.ReadAsStringAsync();
            var results = JsonConvert.DeserializeObject<BaseApiResponseViewModel>(contents);
            var data = results.Data.GetData<ClientApiViewModel>();
            var result = results.GetResult(data);

            return result;
        }

        public async Task<BaseApiResultViewModel<ClientApiViewModel>> PostAsync(ClientApiViewModel param)
        {
            var response = await _client.PostAsync("ClientApi", new StringContent(JsonConvert.SerializeObject(param), Encoding.UTF8, "application/json"));
            var contents = await response.Content.ReadAsStringAsync();
            var results = JsonConvert.DeserializeObject<BaseApiResponseViewModel>(contents);
            var data = results.Data.GetData<ClientApiViewModel>();
            var result = results.GetResult(data);

            return result;
        }
       
        public async Task<BaseApiResultViewModel<ClientApiViewModel>> PutAsync(ClientApiViewModel param)
        {
            var response = await _client.PutAsync("ClientApi", new StringContent(JsonConvert.SerializeObject(param), Encoding.UTF8, "application/json"));
            var contents = await response.Content.ReadAsStringAsync();
            var results = JsonConvert.DeserializeObject<BaseApiResponseViewModel>(contents);
            var data = results.Data.GetData<ClientApiViewModel>();
            var result = new BaseApiResultViewModel<ClientApiViewModel>()
            {
                StatusCode = results.StatusCode,
                Data = data,
                Message = results.Message
            };

            return result;
        }

        public async Task<BaseApiResultViewModel<ClientApiViewModel>> DeleteAsync(Guid id)
        {
            var response = await _client.DeleteAsync("ClientApi/" + id);
            var contents = await response.Content.ReadAsStringAsync();
            var results = JsonConvert.DeserializeObject<BaseApiResponseViewModel>(contents);
            var data = results.Data.GetData<ClientApiViewModel>();
            var result = new BaseApiResultViewModel<ClientApiViewModel>()
            {
                StatusCode = results.StatusCode,
                Data = data,
                Message = results.Message
            };

            return result;
        }
    }
}
