using ApplicationCore.Entities;
using ApplicationCore.Helpers.BaseEntity.Model;
using ApplicationCore.Helpers.Datatables.Model;
using ApplicationCore.Helpers.Select2.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IGroupTaskService
    {
        Task<DatatablesPagedResults<GroupTask>> DatatablesAsync(DatatablesParameter param);

        Task<IReadOnlyList<GroupTask>> GetAllAsync();

        Task<GroupTask> GetAsync(Guid id);

        Task<DatatablesPagedResults<GroupTask>> GetAsync(DataParameter param);

        Task<Guid?> PostAsync(GroupTask model);

        Task<Guid?> PutAsync(GroupTask model);

        Task DeleteAsync(Guid id);

        Task<bool> IsExistDataAsync(IDictionary<string, object> where);

        Task<bool> IsExistDataWithKeyAsync(ExistWithKeyModel model);

        Task<IReadOnlyList<Select2Binding>> BindingSelect2Async();

        Task<Select2Result> BindingSelect2Async(Guid id);
    }
}
