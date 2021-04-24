using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;
using Web.Interfaces.Config;
using Web.ViewModels.Config;
using Web.ViewModels.Helpers;

namespace Web.Controllers
{
    //[Authorize]
    public class ClientApiController : Controller
    {
        private readonly IClientApiService _service;
        public ClientApiController(IClientApiService service) 
        {
            _service = service;
        }

        [TempData]
        public int Code { get; set; }

        [TempData]
        public string Message { get; set; }

        public IActionResult Index()
        {
            var messages = new BaseMessageResponseViewModel
            {
                StatusCode = Code,
                Message = Message
            };

            return View(messages);
        }

        [HttpPost]
        public async Task<IActionResult> LoadDatatables([FromBody] DatatablesParameterViewModel param)
        {
            var data = await _service.LoadDatatablesAsync(param);

            return new JsonResult(new DatatablesResultViewModel<ClientApiViewModel>
            {
                Draw = param.Draw,
                Data = data.Items,
                RecordsFiltered = data.TotalSize,
                RecordsTotal = data.TotalSize
            });
        }

        public IActionResult Create()
        {
            var model = new BaseApiResultViewModel<ClientApiViewModel>()
            {
                StatusCode = Code,
                Data = null,
                Message = Message
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync(BaseApiResultViewModel<ClientApiViewModel> baseApiResult)
        {
            if (ModelState.IsValid)
            {
                if (baseApiResult != null)
                {
                    baseApiResult.Data.ClientId = baseApiResult.Data.ClientId.Replace(" ", "");
                    baseApiResult.Data.ClientSecret = baseApiResult.Data.ClientId;
                    baseApiResult.Data.CreatedBy = "system";
                    baseApiResult.Data.ModifiedBy = "system";

                    var result = await _service.PostAsync(baseApiResult.Data);

                    Code = result.StatusCode;
                    Message = result.Message;

                    if (result.StatusCode == (int)HttpStatusCode.OK)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
            }

            var model = new BaseApiResultViewModel<ClientApiViewModel>()
            {
                StatusCode = Code,
                Data = baseApiResult.Data,
                Message = Message
            };

            return View(model);
        }

        public async Task<IActionResult> EditAsync(Guid id)
        {
            var result = await _service.GetAsync(id);

            if (result.StatusCode != (int)HttpStatusCode.OK)
            {
                Code = result.StatusCode;
                Message = result.Message;
                return RedirectToAction(nameof(Index));
            }

            var model = new BaseApiResultViewModel<ClientApiViewModel>()
            {
                StatusCode = Code,
                Data = result.Data,
                Message = Message
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAsync(BaseApiResultViewModel<ClientApiViewModel> baseApiResult)
        {
            if (ModelState.IsValid)
            {
                if (baseApiResult != null)
                {
                    baseApiResult.Data.ClientId = baseApiResult.Data.ClientId.Replace(" ", "");
                    baseApiResult.Data.ClientSecret = baseApiResult.Data.ClientId;
                    baseApiResult.Data.ModifiedBy = "system";

                    var result = await _service.PutAsync(baseApiResult.Data);

                    Code = result.StatusCode;
                    Message = result.Message;

                    if (result.StatusCode == (int)HttpStatusCode.OK)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
            }

            var model = new BaseApiResultViewModel<ClientApiViewModel>()
            {
                StatusCode = Code,
                Data = baseApiResult.Data,
                Message = Message
            };

            return View(model);
        }

        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var result = await _service.DeleteAsync(id);

            Code = result.StatusCode;
            Message = result.Message;

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DetailsAsync(Guid id)
        {
            var result = await _service.GetAsync(id);

            if (result.StatusCode != (int)HttpStatusCode.OK)
            {
                Code = result.StatusCode;
                Message = result.Message;

                return RedirectToAction(nameof(Index));
            }

            return View(result.Data);
        }
    }
}