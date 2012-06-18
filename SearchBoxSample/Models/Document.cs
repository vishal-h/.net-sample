using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;

namespace SearchBoxSample.Models
{
    public class Document
    {
        public int DocumentId { get; set; }

        [Required(ErrorMessage = "A Document Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "An Document Text is required")]
        public string Text { get; set; }
    }
}