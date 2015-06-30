using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QueryableProviderTest
{
    public class Program
    {
        class SimpleModel
        {
            public string TestString { get; set; }
        }

        public void Main(string[] args)
        {
            var query = new Query<SimpleModel>(new DbQueryProvider());
            Console.WriteLine(query.Where(x => x.TestString == "Test"));
        }
    }
}
