using System.Linq.Expressions;

namespace Domain.Contracts
{
    public interface ISpecifications<T> where T : class
    {
        Expression<Func<T, bool>> Criteria { get; } // Where
        List<Expression<Func<T,object>>> IncludeExpressions { get; } //Include
        Expression<Func<T,object>> OrderBy { get; }
        Expression<Func<T,object>> OrderByDescending { get; }
        int Skip { get; }
        int Take { get; }

        bool IsPaginated { get; }

    }
}
