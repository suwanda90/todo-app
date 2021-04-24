using System;
using System.Threading.Tasks;
using Web.ViewModels.Config;
using Web.ViewModels.Helpers;

namespace Web.Interfaces.Config
{
    public interface IClientApiService
    {
        Task<DatatablesPagedResultsViewModel<ClientApiViewModel>> LoadDatatablesAsync(DatatablesParameterViewModel param);
        Task<BaseApiResultViewModel<ClientApiViewModel>> GetAsync(Guid id);
        Task<BaseApiResultViewModel<ClientApiViewModel>> PostAsync(ClientApiViewModel model);
        Task<BaseApiResultViewModel<ClientApiViewModel>> PutAsync(ClientApiViewModel model);
        Task<BaseApiResultViewModel<ClientApiViewModel>> DeleteAsync(Guid id);
    }
}