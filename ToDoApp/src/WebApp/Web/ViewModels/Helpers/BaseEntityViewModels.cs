using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace Web.ViewModels.Helpers
{
    public class ExistWithKeyModel
    {
        public string KeyName { get; set; }
        public object KeyValue { get; set; }
        public string FieldName { get; set; }
        public object FieldValue { get; set; }
        public IDictionary<string, object> WhereData { get; set; }
    }

    public class UploadFileViewModel
    {
        public Guid? Id { get; set; }

        public string Name { get; set; }

        public string Note { get; set; }

        public IFormFile File { get; set; }

        public Guid? FkTransactionId { get; set; }

        public Guid? FkDocumentId { get; set; }

        public bool IsActive { get; set; }

        public string CreatedBy { get; set; }

        public string ModifiedBy { get; set; }
    }
}
