using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Web.ViewModels.Helpers
{
    public class Select2BindingViewModel
    {
        [JsonPropertyName("id")]
        public object Id { get; set; }

        [JsonPropertyName("text")]
        public string Text { get; set; }
    }

    public class Select2ResultViewModel
    {
        [JsonPropertyName("result")]
        public IList<Select2BindingViewModel> Result { get; set; }

        [JsonPropertyName("key")]
        public object Key { get; set; }

        [JsonPropertyName("value")]
        public string Value { get; set; }
    }
}
