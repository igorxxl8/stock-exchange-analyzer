using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace CurrencyComApi.Queries
{
    public class ApiQueryBase
    {
        public string BaseURL { get; set; }

        public string Address { get; set; }

        public string Method { get; set; }

        public QueryHeaders Headers { get; set; } = new QueryHeaders();

        public QueryOptions Options { get; set; } = new QueryOptions();

        public override string ToString()
        {
            return $"curl {Headers} -X {Method} {BaseURL}{Address}{Options}";
        }
    }

    public class QueryHeader
    {
        public string Name { get; set; }

        public string Value { get; set; }
    }

    public abstract class QueryElementsCollection<T>
    {
        protected readonly IList<T> _elements;

        public QueryElementsCollection()
        {
            _elements = new List<T>();
        }

        public void Add(T element)
        {
            _elements.Add(element);
        }

        public abstract void Add(string name, string value);
    }

    public class QueryHeaders : QueryElementsCollection<QueryHeader>
    {
        private IList<QueryHeader> Headers => _elements;

        public override void Add(string name, string value)
        {
            Headers.Add(new QueryHeader
            {
                Name = name,
                Value = value
            });
        }

        public override string ToString()
        {
            if (Headers.Count < 1)
            {
                return string.Empty;
            }

            var sb = new StringBuilder();
            foreach (var header in Headers)
            {
                sb.Append($"-H \"{header.Name}:{header.Value}\" ");
            }

            return sb.ToString();
        }
    }

    public class QueryOption
    {
        public string Key { get; set; }

        public string Value { get; set; }
    }

    public class QueryOptions : QueryElementsCollection<QueryOption>
    {
        private IList<QueryOption> Options => _elements;

        public override void Add(string key, string value)
        {
            Options.Add(new QueryOption
            {
                Key = key,
                Value = value
            });
        }

        public override string ToString()
        {
            var counter = Options.Count;
            if (counter < 1)
            {
                return string.Empty;
            }

            var sb = new StringBuilder("?");
            foreach (var header in Options)
            {
                var joiner = counter-- == 1 ? string.Empty : "&";
                sb.Append($"{header.Key}={header.Value}{joiner}");
            }

            return sb.ToString();
        }
    }
}
