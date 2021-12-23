using DynamicData;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaSelectionProblemExample.Models
{
    public class DataStorage
    {
        private readonly SourceList<string> _items;
        public IObservable<IChangeSet<string>> Characters => _items.Connect();
        public DataStorage()
        {
            var data = new List<string>()
            {
                "A",
                "BB",
                "CCC",
                "DDDD"
            };

            _items = new SourceList<string>();
            _items.AddRange(data);
        }
    }
}
