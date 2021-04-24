using ApplicationCore.Entities;
using ApplicationCore.Helpers.BaseEntity.Model;
using ApplicationCore.Helpers.Datatables.Model;
using ApplicationCore.Helpers.Select2;
using ApplicationCore.Helpers.Select2.Model;
using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.BaseEntity;
using ApplicationCore.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationCore.Services.Config
{
    public class TasksService : ITasksService
    {
        private readonly IEfRepository<Tasks, Guid> _repository;

        public TasksService(IEfRepository<Tasks, Guid> repository)
        {
            _repository = repository;
        }

        public async Task<DatatablesPagedResults<Tasks>> DatatablesAsync(DatatablesParameter param)
        {
            var spec = new TasksSpecification();
            return await _repository.DatatablesAsync(param, spec);
        }

        public async Task<DatatablesPagedResults<Tasks>> GetAsync(DataParameter param)
        {
            var spec = new TasksSpecification();
            var source = await _repository.GetAllAsync(spec);

            return await _repository.GetByPagingAsync(source, param.Start, param.Length);
        }

        public async Task<IReadOnlyList<Tasks>> GetAllAsync()
        {
            var spec = new TasksSpecification();
            return await _repository.GetAllAsync(spec);
        }

        public async Task<IReadOnlyList<Tasks>> GetAllAsync(Guid FkGroupTaskId)
        {
            var spec = new TasksSpecification();
            var data = await _repository.GetAllAsync(spec);

            return data.Where(x => x.FkGroupTaskId == FkGroupTaskId).ToList();
        }

        public async Task<Tasks> GetAsync(Guid id)
        {
            var spec = new TasksSpecification();
            return await _repository.GetAsync(spec, id);
        }

        public async Task<Guid?> PostAsync(Tasks model)
        {
            Guid? id = null;

            var where = new Dictionary<string, object>
            {
                { nameof(model.Name), model.Name}
            };

            if (model.FkGroupTaskId.HasValue)
            {
                where.Add(nameof(model.FkGroupTaskId), model.FkGroupTaskId);
            }

            if (!await _repository.IsExistDataAsync(where))
            {
                var data = await _repository.AddAsync(model);
                id = data.Id;
            }

            return id;
        }

        public async Task<Guid?> PutAsync(Tasks model)
        {
            Guid? id = null;

            var where = new Dictionary<string, object>
            {
                { nameof(model.Name), model.Name}
            };

            if (model.FkGroupTaskId.HasValue)
            {
                where.Add(nameof(model.FkGroupTaskId), model.FkGroupTaskId);
            }

            var param = new ExistWithKeyModel
            {
                KeyName = nameof(model.Id),
                KeyValue = model.Id,
                FieldName = nameof(model.Name),
                FieldValue = model.Name,
                WhereData = where
            };

            if (!await _repository.IsExistDataWithKeyAsync(param))
            {
                await _repository.UpdateAsync(model);
                id = model.Id;
            }

            return id;
        }

        public async Task<Guid?> PutCompletedTaskAsync(Guid id)
        {
            Guid? fkTaskId = null;
            var model = await GetAsync(id);

            if (model != null)
            {
                model.IsCompleted = true;

                await _repository.UpdateAsync(model);
                fkTaskId = model.Id;
            }

            return fkTaskId;
        }

        public async Task DeleteAsync(Guid id)
        {
            var data = await _repository.GetAsync(id);
            await _repository.DeleteAsync(data);
        }

        public async Task<bool> IsExistDataAsync(IDictionary<string, object> where)
        {
            return await _repository.IsExistDataAsync(where);
        }

        public async Task<bool> IsExistDataWithKeyAsync(ExistWithKeyModel model)
        {
            return await _repository.IsExistDataWithKeyAsync(model);
        }

        public async Task<IReadOnlyList<Select2Binding>> BindingSelect2Async()
        {
            var spec = new TasksSpecification(true);
            var source = await _repository.GetAllAsync(spec);

            var result = source.Where(x => x.IsActive == true)
            .Select(x => new Select2Binding
            {
                Id = x.Id,
                Text = x.Name
            }).ToList();

            return result;
        }

        public async Task<Select2Result> BindingSelect2Async(Guid id)
        {
            var spec = new TasksSpecification(true);
            var data = await _repository.GetAsync(id);
            var text = string.Empty;

            if (data != null)
            {
                text = data.Name;
            }

            var source = await _repository.GetAllAsync(spec);
            var result = source.Where(x => x.IsActive == true)
                .Select(x => new Select2Binding
                {
                    Id = x.Id,
                    Text = x.Name
                }).ToList();

            return result.BindingSelect2Edit(id, text);
        }

        public async Task<IReadOnlyList<Select2Binding>> BindingSelect2FilterAsync(Guid fkGroupTaskId)
        {
            var models = await GetAllAsync();
            var data = models
                .Where(x => x.IsActive == true && x.GroupTask.IsActive == true && x.FkGroupTaskId == fkGroupTaskId)
                .OrderBy(x => x.Name)
                .Select(x => new Select2Binding
                {
                    Id = x.Id,
                    Text = x.GroupTask.Name + " - " + x.Name
                }).ToList();

            return data;
        }

        public async Task<Select2Result> BindingSelect2FilterAsync(Guid fkGroupTaskId, Guid id)
        {
            var data = await _repository.GetAsync(id);
            var text = string.Empty;

            if (data != null)
            {
                text = data.GroupTask.Name + " - " + data.Name;
            }

            var models = await GetAllAsync();
            var result = models
                .Where(x => x.IsActive == true && x.GroupTask.IsActive == true && x.FkGroupTaskId == fkGroupTaskId)
                .OrderBy(x => x.Name)
                .Select(x => new Select2Binding
                {
                    Id = x.Id,
                    Text = x.GroupTask.Name + " - " + x.Name
                }).ToList();

            return result.BindingSelect2Edit(id, text);
        }
    }
}
