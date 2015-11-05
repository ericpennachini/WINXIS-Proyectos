using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassLibraryNHibernate.Domain
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public bool Discontinued { get; set; }
    }
}
