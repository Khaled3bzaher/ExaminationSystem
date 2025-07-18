using AutoMapper;
using AutoMapper.QueryableExtensions;
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
        public async Task<TDto?> GetProjectedAsync<TDto>(string studentId)
            => await context.Set<ApplicationUser>().Where(s=>s.Id==studentId).ProjectTo<TDto>(mapper.ConfigurationProvider).FirstOrDefaultAsync();
        public async Task<int> CountAsync(ISpecifications<ApplicationUser> specifications)
            => await SpecificationsEvaluator.CreateQuery(context.Set<ApplicationUser>(), specifications).CountAsync();

        public void Update(ApplicationUser user)
            => context.Set<ApplicationUser>().Update(user);

        public async Task<bool> isStudentExistsAsync(string studentId)
            => await context.Set<ApplicationUser>().AnyAsync(s => s.Id == studentId);

        public async Task<int> CountAsync()
         => await context.Set<ApplicationUser>().CountAsync();
    }
}
