﻿using QueryFiltering.AntlrGenerated;
using QueryFiltering.Helpers;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace QueryFiltering.Visitors
{
    internal class TopVisitor : QueryFilteringBaseVisitor<IQueryable>
    {
        private readonly IQueryable _sourceQueryable;
        private readonly ParameterExpression _parameter;

        public TopVisitor(IQueryable sourceQueryable, ParameterExpression parameter)
        {
            _sourceQueryable = sourceQueryable;
            _parameter = parameter;
        }

        public override IQueryable VisitTop(QueryFilteringParser.TopContext context)
        {
            MethodInfo take = ReflectionCache.Take
                .MakeGenericMethod(_parameter.Type);

            int count = int.Parse(context.count.Text);

            return (IQueryable)take.Invoke(null, new object[] { _sourceQueryable, count });
        }
    }
}
