﻿using ApplicationCore.Entities;
using ApplicationCore.Helpers.BaseEntity.Model;
using ApplicationCore.Helpers.Datatables.Model;
using ApplicationCore.Helpers.Select2;
using ApplicationCore.Helpers.Select2.Model;
using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.BaseEntity;
using ApplicationCore.Specifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationCore.Services.Config
{
    public class GroupTaskService : IGroupTaskService
    {
        private readonly IEfRepository<GroupTask, Guid> _repository;
        private readonly ITasksService _tasksService;

        public GroupTaskService(IEfRepository<GroupTask, Guid> repository, ITasksService tasksService)
        {
            _repository = repository;
            _tasksService = tasksService;
        }

        public async Task<DatatablesPagedResults<GroupTask>> DatatablesAsync(DatatablesParameter param)
        {
            var spec = new GroupTaskSpecification();
            return await _repository.DatatablesAsync(param, spec);
        }

        public async Task<DatatablesPagedResults<GroupTask>> GetAsync(DataParameter param)
        {
            var spec = new GroupTaskSpecification();
            var source = await _repository.GetAllAsync(spec);

            return await _repository.GetByPagingAsync(source, param.Start, param.Length);
        }

        public async Task<IReadOnlyList<GroupTask>> GetAllAsync()
        {
            var spec = new GroupTaskSpecification();
            return await _repository.GetAllAsync(spec);
        }

        public async Task<GroupTask> GetAsync(Guid id)
        {
            var spec = new GroupTaskSpecification();
            return await _repository.GetAsync(spec, id);
        }

        public async Task<Guid?> PostAsync(GroupTask model)
        {
            Guid? id = null;

            var where = new Dictionary<string, object>
            {
                { nameof(model.Name), model.Name}
            };

            if (!await _repository.IsExistDataAsync(where))
            {
                var data = await _repository.AddAsync(model);
                id = data.Id;
            }

            return id;
        }

        public async Task<Guid?> PutAsync(GroupTask model)
        {
            Guid? id = null;

            var where = new Dictionary<string, object>
            {
                { nameof(model.Name), model.Name}
            };

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

        public async Task DeleteAsync(Guid id)
        {
            var tasks = await _tasksService.GetAllAsync(id);
            foreach (var task in tasks)
            {
                await _tasksService.DeleteAsync(task.Id);
            }

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
            var spec = new GroupTaskSpecification(true);
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
            var spec = new GroupTaskSpecification(true);
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
    }
}
