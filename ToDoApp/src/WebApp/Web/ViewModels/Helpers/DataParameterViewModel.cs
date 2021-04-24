using System;

namespace Web.ViewModels.Helpers
{
    public class DataParameterViewModel
    {
        public string Keyword { get; set; }
        public int Start { get; set; }
        public int Length { get; set; }
        public string TenantCode { get; set; }
        public string UserEmail { get; set; }
        public string Stage { get; set; }
        public string FkTenatId { get; set; }
        public string FkPortofolioId { get; set; }
        public string FkPortofolioProgramId { get; set; }
        public string ProjectId { get; set; }
    }
}
