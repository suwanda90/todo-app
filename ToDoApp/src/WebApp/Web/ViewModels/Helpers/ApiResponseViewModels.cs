using System.Collections.Generic;

namespace Web.ViewModels.Helpers
{
    public class BaseMessageResponseViewModel
    {
        public int StatusCode { get; set; }

        public string Message { get; set; }
    }

    public class BaseApiResponseViewModel
    {
        public int StatusCode { get; set; }
        public string StatusText { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
        public void SetBaseApiResponseViewModel(int statusCode, string statusText, string message, object data)
        {
            StatusCode = statusCode;
            StatusText = statusText;
            Message = message;
            Data = data;
        }
    }

    public class BaseApiResultViewModel<TEntity>
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public TEntity Data { get; set; }
    }

    public class BaseApiResultFileViewModel
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
    }

    public class BaseApiResultsViewModel<TEntity>
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public IReadOnlyList<TEntity> Data { get; set; }
    }

    public class BaseApiResultWithPagedViewModel<TEntity>
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public IReadOnlyList<TEntity> Data { get; set; }
        public int Length { get; set; }
        public int TotalSize { get; set; }
        public int Page { get; set; }
        public int PageCount { get; set; }
    }
}
