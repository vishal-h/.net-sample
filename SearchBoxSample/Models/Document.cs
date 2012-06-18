using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using Nest;
using Newtonsoft.Json;

namespace SearchBoxSample.Models
{
    // You need to set IdProperty if you are using an id without name "id"
    [ElasticType(IdProperty = "DocumentId")]
    public class Document
    {
        //DocumentId will be used as id, no need to index it as a property
        [JsonIgnore]
        public int DocumentId { get; set; }

        [Required(ErrorMessage = "A Document Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "An Document Text is required")]
        public string Text { get; set; }
    }
}