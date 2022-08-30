using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;


namespace HomeLib
{
    public class BookData
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;

        [JsonPropertyName("authors")]
        public string Authors { get; set; }

        [JsonPropertyName("publishedDate")]
        public string PublishedDate { get; set; } = string.Empty;

        [JsonPropertyName("pageCount")]
        public int PageCount { get; set; }

        [JsonPropertyName("categorie")]
        public string Categorie { get; set; } = string.Empty;

        [JsonPropertyName("language")]
        public string Language { get; set; } = string.Empty;

        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;

    }
}


