using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ViewModels
{
    public class DataTableResponse<T>
    {
        public IEnumerable<T> Result { get; set; }
        public int CountFiltered { get; set; }
        public int CountTotal { get; set; }
    }

    public class DataTableJS
    {
        [JsonProperty("draw")]
        public int Draw { get; set; }
        [JsonProperty("columns")]
        public List<Column> Columns { get; set; }
        [JsonProperty("start")]
        public int Start { get; set; }
        [JsonProperty("isProduct")]
        public bool IsProduct { get; set; }
        [JsonProperty("length")]
        public int Length { get; set; }
        [JsonProperty("order")]
        public List<Order> Order { get; set; }
        [JsonProperty("search")]
        public Search Search { get; set; }
        [JsonProperty("searchFilter")]
        public string SearchFilter { get; set; }

        [JsonProperty("userId")]
        public string UserId { get; set; }

        [JsonProperty("date")]
        public DateTime Date { get; set; }

        [JsonProperty("typeAction")]

        public string DateStart { get; set; }

        public string DateEndString { get; set; }
        public DateTime DateEnd { get; set; }

        public string roleId { get; set; }
        public string estado { get; set; }

        public int PatioId { get; set; }

        public int? TipoZonaId { get; set; }
    }
    public class Column
    {
        [JsonProperty("data")]
        public int Data { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("searchable")]
        public bool Searchable { get; set; }
        [JsonProperty("orderable")]
        public bool Orderable { get; set; }
        [JsonProperty("search")]
        public Search search { get; set; }
        [JsonProperty("searchFilter")]
        public string SearchFilter { get; set; }
    }

    public class Search
    {
        [JsonProperty("value")]
        public string Value { get; set; }
        [JsonProperty("regex")]
        public bool Regex { get; set; }
    }

    public class Order
    {
        [JsonProperty("column")]
        public int Column { get; set; }
        [JsonProperty("dir")]
        public string Dir { get; set; }
    }
}
