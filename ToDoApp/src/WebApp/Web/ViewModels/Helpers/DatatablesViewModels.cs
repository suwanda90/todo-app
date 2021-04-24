using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Web.ViewModels.Helpers
{
    public enum DatatablesOrderTypeViewModel
    {
        ASC,
        DESC
    }

    public class DatatablesColumnViewModel
    {
        public DatatablesColumnViewModel() { }
        public string Data { get; set; }
        public string Name { get; set; }
        public bool Searchable { get; set; }
        public bool Orderable { get; set; }
    }

    public class DatatablesSearchViewModel
    {
        public DatatablesSearchViewModel() { }

        public string Value { get; set; }
        public bool Regex { get; set; }
    }

    public class DatatablesOrderViewModel
    {
        public DatatablesOrderViewModel() { }
        public int Column { get; set; }
        public DatatablesOrderTypeViewModel Dir { get; set; }
    }

    public class DatatablesParameterViewModel
    {
        public DatatablesParameterViewModel() { }
        public int Draw { get; set; }
        public DatatablesColumnViewModel[] Columns { get; set; }
        public DatatablesOrderViewModel[] Order { get; set; }
        public int Start { get; set; }
        public int Length { get; set; }
        public DatatablesSearchViewModel Search { get; set; }
        public string SortOrder { get; }
    }

    public class DatatablesParameterDashboardViewModel
    {
        public DatatablesParameterDashboardViewModel() { }
        public int Draw { get; set; }
        public DatatablesColumnViewModel[] Columns { get; set; }
        public DatatablesOrderViewModel[] Order { get; set; }
        public int Start { get; set; }
        public int Length { get; set; }
        public DatatablesSearchViewModel Search { get; set; }
        public string SortOrder { get; }
        public Guid? FkPortofolioId { get; set; }
        public Guid? FkPortofolioProgramId { get; set; }
        public Guid? FkProjectFunctionId { get; set; }
        public string Year { get; set; }
        public string Type { get; set; }
        public string TenantCode { get; set; }
    }

    public class DatatablesPagedResultsViewModel<T>
    {
        public IEnumerable<T> Items { get; set; }
        public int TotalSize { get; set; }
    }

    public class DatatablesResultViewModel<T>
    {
        [JsonProperty(PropertyName = "draw")]
        [JsonPropertyName("draw")]
        public int Draw { get; set; }
        [JsonProperty(PropertyName = "recordsTotal")]
        [JsonPropertyName("recordsTotal")]
        public int RecordsTotal { get; set; }
        [JsonProperty(PropertyName = "recordsFiltered")]
        [JsonPropertyName("recordsFiltered")]
        public int RecordsFiltered { get; set; }
        [JsonProperty(PropertyName = "data")]
        [JsonPropertyName("data")]
        public IEnumerable<T> Data { get; set; }
    }
}
