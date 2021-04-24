using System.Net;
using Newtonsoft.Json;
using Web.ViewModels.Helpers;

namespace Web.Helpers
{
    public static class BaseApiResultHelper
    {
        //CEK CEK source!=null
        public static TEntity GetData<TEntity>(this object source) where TEntity : new()
        {
            var data = new TEntity();
            if (source != null)
            {
                var model = JsonConvert.SerializeObject(source);
                data = JsonConvert.DeserializeObject<TEntity>(model);
            }

            return data;
        }

        public static BaseApiResultViewModel<TEntity> GetResult<TEntity>(this BaseApiResponseViewModel results, TEntity data)
        {
            var result = new BaseApiResultViewModel<TEntity>()
            {
                StatusCode = results.StatusCode,
                Data = data,
                Message = results.Message
            };

            return result;
        }

        public static TEntity MappingResult<TEntity>(this BaseApiResultViewModel<TEntity> results) where TEntity : new()
        {
            var data = new TEntity();

            if (results.StatusCode == (int)HttpStatusCode.OK)
            {
                data = results.Data;
            }

            return data;
        }
    }
}

