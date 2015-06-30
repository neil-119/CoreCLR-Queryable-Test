using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace QueryableProviderTest
{
    public class DbQueryProvider : QueryProvider
    {
        public DbQueryProvider()
        {
        }

        public override string GetQueryText(Expression expression)
        {
            return this.Translate(expression);
        }

        public override object Execute(Expression expression)
        {
            throw new NotImplementedException();
        }

        private string Translate(Expression expression)
        {
            return new QueryTranslator().Translate(expression);
        }
    }
}
