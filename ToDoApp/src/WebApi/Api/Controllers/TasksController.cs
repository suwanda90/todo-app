using ApplicationCore.Entities;
using ApplicationCore.Helpers.Api.Response;
using ApplicationCore.Helpers.ApiResponse;
using ApplicationCore.Helpers.BaseEntity;
using ApplicationCore.Helpers.BaseEntity.Model;
using ApplicationCore.Helpers.Datatables.Model;
using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.Logging;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Controllers
{
    public class TasksController : BaseApiController
    {
        private readonly ITasksService _service;
        private readonly IAppLogger<Tasks> _loger;

        public TasksController(ITasksService service, IAppLogger<Tasks> loger)
        {
            _service = service;
            _loger = loger;
        }

        [HttpPost("get/datatables")]
        public async Task<IActionResult> LoadDatatablesAsync([FromBody] DatatablesParameter param)
        {
            if (param == null)
            {
                _loger.LogError("Bad request parameter");

                var error = new BadRequest("Parameter is null", new { param });
                return error.ReturnResponse();
            }

            var data = await _service.DatatablesAsync(param);

            var result = new OK("Success load datatables", data);
            return result.ReturnResponse();
        }

        [HttpGet("get/all")]
        public async Task<IActionResult> GetAllAsync()
        {
            var data = await _service.GetAllAsync();
            var result = new OK("Success get all data", data);
            return result.ReturnResponse();
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var data = await _service.GetAsync(id);
            if (data == null)
            {
                _loger.LogError("Data not found for id : " + id.ToString());

                var error = new NotFound("Data not found for id : " + id.ToString(), new { id });
                return error.ReturnResponse();
            }

            var result = new OK("Success get data by id = " + id.ToString(), data);
            return result.ReturnResponse();
        }

        [HttpGet("get/param")]
        public async Task<IActionResult> GetAsync(DataParameter param)
        {
            if (param.Start <= 0 || param.Length <= 0)
            {
                _loger.LogError("Bad request parameter");

                var error = new BadRequest("Start page > 0 and page size > 0", new { param.Start, param.Length });
                return error.ReturnResponse();
            }

            var data = await _service.GetAsync(param);
            var result = new OK("Success get data by paging", data);
            return result.ReturnResponse();
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] Tasks data)
        {
            if (data == null)
            {
                _loger.LogError("Bad request parameter");

                var error = new BadRequest("Parameter is null", new { data });
                return error.ReturnResponse();
            }

            var id = await _service.PostAsync(data);
            if (id == null)
            {
                _loger.LogError("Duplicate data for Name : " + data.Name);

                var error = new Forbidden("Duplicate data for Name : " + data.Name, new { data });
                return error.ReturnResponse();
            }

            var result = new OK("Success add data", new { id });
            return result.ReturnResponse();
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync([FromBody] Tasks data)
        {
            if (data == null)
            {
                _loger.LogError("Bad request parameter");

                var error = new BadRequest("Parameter is null", new { data });
                return error.ReturnResponse();
            }

            var id = await _service.PutAsync(data);
            if (id == null)
            {
                _loger.LogError("Duplicate data for Name : " + data.Name);

                var error = new Forbidden("Duplicate data for Name : " + data.Name, new { data });
                return error.ReturnResponse();
            }

            var result = new OK("Success update data", new { id });
            return result.ReturnResponse();
        }

        [HttpPut("completed/task/{id}")]
        public async Task<IActionResult> PutCompletedTaskAsync(Guid id)
        {
            var data = await _service.GetAsync(id);
            if (data == null)
            {
                _loger.LogError("Bad request parameter");

                var error = new BadRequest("Parameter is null", new { data });
                return error.ReturnResponse();
            }

            var fkTaskId = await _service.PutCompletedTaskAsync(id);
            if (fkTaskId == null)
            {
                _loger.LogError("Bad request parameter");

                var error = new BadRequest("Parameter is null", new { data });
                return error.ReturnResponse();
            }

            var result = new OK("Success completed task data", new { id });
            return result.ReturnResponse();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var data = await _service.GetAsync(id);
            if (data == null)
            {
                _loger.LogError("Not found data for id : " + id.ToString());

                var error = new NotFound("Not found data for id : " + id.ToString(), new { id });
                return error.ReturnResponse();
            }

            await _service.DeleteAsync(id);
            var result = new OK("Success delete data " + data.Name, new { id });
            return result.ReturnResponse();
        }

        [HttpPost("exist")]
        public async Task<IActionResult> IsExistValueAsync([FromBody] IDictionary<string, object> whereData)
        {
            if (whereData == null)
            {
                _loger.LogError("Bad request parameter");

                var error = new BadRequest("Bad request parameter", new { whereData });
                return error.ReturnResponse();
            }

            var exist = await _service.IsExistDataAsync(whereData);

            var result = new OK("Success check exist data", new { exist });
            return result.ReturnResponse();
        }

        [HttpPost("exist/key")]
        public async Task<IActionResult> IsExistValueWithKeyAsync([FromBody] ExistWithKeyModel model)
        {
            if (string.IsNullOrEmpty(model.FieldName)
                || string.IsNullOrEmpty(model.FieldName))
            {
                _loger.LogError("Bad Request Parameter");

                var error = new BadRequest("Bad Request Parameter", new { model });
                return error.ReturnResponse();
            }
            else if (typeof(Tasks).HasProperty(model.KeyName.ToLower()) && typeof(Tasks).HasProperty(model.FieldName.ToLower()))
            {
                _loger.LogError("Property name does not exist");

                var error = new BadRequest("Property name does not exist", new { model });
                return error.ReturnResponse();
            }

            var exist = await _service.IsExistDataWithKeyAsync(model);

            var result = new OK("Success check exist data with key " + model.FieldName + " : " + model.FieldValue, new { exist });
            return result.ReturnResponse();
        }

        [HttpGet("binding/select2")]
        public async Task<IActionResult> BindingSelect2Async()
        {
            var data = await _service.BindingSelect2Async();

            var result = new OK("Success binding select2", data);

            return result.ReturnResponse();
        }

        [HttpGet("binding/select2/{id}")]
        public async Task<IActionResult> BindingSelect2Async(Guid id)
        {
            var data = await _service.BindingSelect2Async(id);

            var result = new OK("Success binding select2", data);
            return result.ReturnResponse();
        }

        [HttpGet("binding/select2/group-task/{fkGroupTaskId}")]
        public async Task<IActionResult> BindingSelect2FilterAsync(Guid fkGroupTaskId)
        {
            var data = await _service.BindingSelect2FilterAsync(fkGroupTaskId);

            var result = new OK("Success binding select2", data);

            return result.ReturnResponse();
        }

        [HttpGet("binding/select2/group-task/{fkGroupTaskId}/{id}")]
        public async Task<IActionResult> BindingSelect2FilterAsync(Guid fkGroupTaskId, Guid id)
        {
            var data = await _service.BindingSelect2FilterAsync(fkGroupTaskId, id);

            var result = new OK("Success binding select2", data);
            return result.ReturnResponse();
        }
    }
}
