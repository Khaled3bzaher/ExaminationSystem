using AutoMapper;
using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Persistence.Repositories
{
    public class StudentRepository(ExaminationDbContext context, IMapper mapper) : IStudentRepository
    {
        public async Task<IEnumerable<TDto>> GetAllProjectedAsync<TDto>(ISpecifications<ApplicationUser> specifications)
            => await SpecificationsEvaluator.CreateQuery<ApplicationUser, TDto>(context.Set<ApplicationUser>(), specifications, mapper.ConfigurationProvider).ToListAsync();
        public async Task<int> CountAsync(ISpecifications<ApplicationUser> specifications)
            => await SpecificationsEvaluator.CreateQuery(context.Set<ApplicationUser>(), specifications).CountAsync();

        public void Update(ApplicationUser user)
            => context.Set<ApplicationUser>().Update(user);
    }
}
