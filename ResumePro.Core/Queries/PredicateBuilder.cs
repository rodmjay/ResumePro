#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace ResumePro.Core.Queries;

[ExcludeFromCodeCoverage]
public static class PredicateBuilder
{
    public static Expression<Func<T, bool>> True<T>()
    {
        return f => true;
    }

    [ExcludeFromCodeCoverage]
    public static Expression<Func<T, bool>> False<T>()
    {
        return f => false;
    }

    [ExcludeFromCodeCoverage]
    public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expr1,
        Expression<Func<T, bool>> expr2)
    {
        var invokedExpr = Expression.Invoke(expr2, expr1.Parameters);
        return Expression.Lambda<Func<T, bool>>
            (Expression.OrElse(expr1.Body, invokedExpr), expr1.Parameters);
    }

    public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expr1,
        Expression<Func<T, bool>> expr2)
    {
        var invokedExpr = Expression.Invoke(expr2, expr1.Parameters);
        return Expression.Lambda<Func<T, bool>>
            (Expression.AndAlso(expr1.Body, invokedExpr), expr1.Parameters);
    }

    public static Expression<Func<T, bool>> BuildLikeExpression<T>(string[] keywords,
        Expression<Func<T, string>> action)
    {
        var body = (MemberExpression) action.Body;
        var name = body.Member.Name;

        Expression expression = null!;
        var parameterExpression = Expression.Parameter(typeof(T), "x");
        var property = Expression.Property(parameterExpression, name);
        foreach (var keyword in keywords)
        {
            var normalized = $"%{keyword}";
            var constant = Expression.Constant(normalized);
            var methodCallExpression = Expression.Call(typeof(DbFunctionsExtensions),
                nameof(DbFunctionsExtensions.Like), null, Expression.Constant(EF.Functions), property, constant);

            if (expression == null)
                expression = methodCallExpression;
            else
                expression = Expression.OrElse(expression, methodCallExpression);
        }

        return Expression.Lambda<Func<T, bool>>(expression, parameterExpression);
    }
}