using ApplicationCore.Entities;
using ApplicationCore.Helpers.BaseEntity.Model;
using ApplicationCore.Helpers.Datatables.Model;
using ApplicationCore.Helpers.Select2.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface ITasksService
    {
        Task<DatatablesPagedResults<Tasks>> DatatablesAsync(DatatablesParameter param);

        Task<IReadOnlyList<Tasks>> GetAllAsync();

        Task<IReadOnlyList<Tasks>> GetAllAsync(Guid fkGroupTaskId);

        Task<Tasks> GetAsync(Guid id);

        Task<DatatablesPagedResults<Tasks>> GetAsync(DataParameter param);

        Task<Guid?> PostAsync(Tasks model);

        Task<Guid?> PutAsync(Tasks model);

        Task<Guid?> PutCompletedTaskAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<bool> IsExistDataAsync(IDictionary<string, object> where);

        Task<bool> IsExistDataWithKeyAsync(ExistWithKeyModel model);

        Task<IReadOnlyList<Select2Binding>> BindingSelect2Async();

        Task<Select2Result> BindingSelect2Async(Guid id);

        Task<IReadOnlyList<Select2Binding>> BindingSelect2FilterAsync(Guid fkTenantId);

        Task<Select2Result> BindingSelect2FilterAsync(Guid fkTenantId, Guid id);
    }
}
