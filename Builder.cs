using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AmazingCy.LinqBuilder
{
    /// <summary>
    /// Linq Builer
    /// Author:Cy Time:2017.9.28
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Builder<T>
    {
        public Builder() {

        }

        public Builder(Expression<Func<T, bool>> predicate)
        {
            this.Expression = predicate;
        }
        /// <summary>
        /// 
        /// </summary>
        public Expression<Func<T, bool>> Expression { get; }

        /// <summary>
        ///  Create condition
        /// </summary>
        public static Builder<T> Create(Expression<Func<T, bool>> predicate)
        {
            return new Builder<T>(predicate);
        }
        public static Builder<T> operator &(Builder<T> left, Builder<T> right)
        {
            return Builder<T>.Compose(left.Expression, right.Expression, System.Linq.Expressions.Expression.And);
        }
        public static Builder<T> operator|(Builder<T> left, Builder<T> right)
        {
            return Builder<T>.Compose(left.Expression, right.Expression, System.Linq.Expressions.Expression.Or);

        }
        /// <summary>
        /// Compose
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <param name="merge"></param>
        /// <returns></returns>
        internal static Builder<T> Compose(Expression<Func<T, bool>> first, Expression<Func<T, bool>> second, Func<Expression, Expression, Expression> merge)
        {
            // zip parameters (map from parameters of second to parameters of first)
            var map = first.Parameters.Select((f, i) => new { f, s = second.Parameters[i] }).ToDictionary(p => p.s, p => p.f);
            // replace parameters in the second lambda expression with the parameters in the first
            var secondBody = ParameterRebinder.ReplaceParameters(map, second.Body);
            // create a merged lambda expression with parameters from the first expression
            return new Builder<T>(System.Linq.Expressions.Expression.Lambda<Func<T, bool>>(merge(first.Body, secondBody), first.Parameters));
        }
    }
}
