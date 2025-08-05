using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace Persistence.Repositories
{
    internal static class SpecificationsEvaluator
    {
        public static IQueryable<T> CreateQuery<T>(IQueryable<T> inputQuery,ISpecifications<T> specifications) where T : class
        {
            var query = inputQuery;
            if(specifications.Criteria is not null)
                query= query.Where(specifications.Criteria);
            query = specifications.IncludeExpressions.Aggregate(query,
                (currentQuery, include) => currentQuery.Include(include));

            if(specifications.OrderBy is not null)
                query= query.OrderBy(specifications.OrderBy);
            else if(specifications.OrderByDescending is not null)
                query=query.OrderByDescending(specifications.OrderByDescending);

            if (specifications.IsPaginated)
                return query = query.Skip(specifications.Skip).Take(specifications.Take);

            if (specifications.Take != 0)
                return query = query.Take(specifications.Take);

                return query;
        }
        public static IQueryable<TDto> CreateQuery<T, TDto>(IQueryable<T> inputQuery, ISpecifications<T> specifications, IConfigurationProvider configuration) where T : class
            => CreateQuery<T>(inputQuery, specifications).ProjectTo<TDto>(configuration);

        public static IFindFluent<T,TDto> GetMongoQuery<T, TDto>(IMongoCollection<T> collection,ISpecifications<T> spec, ProjectionDefinition<T, TDto> projection) where T : class
        {
            var query = collection.Find(spec.Criteria);
            if (spec.OrderByDescending != null)
            {
                query = query.SortByDescending(spec.OrderByDescending);
            }

            if (spec.IsPaginated)
            {
                query = query.Skip(spec.Skip);
                query = query.Limit(spec.Take);
            }


            return query.Project(projection);

        }
    }
}
