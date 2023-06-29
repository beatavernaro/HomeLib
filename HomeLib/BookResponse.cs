using System.Text.Json.Serialization;


namespace HomeLib
{
    public class BookResponse
    {
        [JsonPropertyName("totalItems")]
        public long TotalItems { get; set; }

        [JsonPropertyName("items")]
        public List<Item> Items { get; set; }
    }

    public partial class Item
    {

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("volumeInfo")]
        public VolumeInfo VolumeInfo { get; set; }

    }

    public partial class VolumeInfo
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("authors")]
        public List<string> Authors { get; set; }

        [JsonPropertyName("publishedDate")]
        public string PublishedDate { get; set; }

        [JsonPropertyName("pageCount")]
        public int PageCount { get; set; }

        [JsonPropertyName("categories")]
        public List<string> Categories { get; set; }

        [JsonPropertyName("language")]
        public string Language { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; } = " ";
    }
}


