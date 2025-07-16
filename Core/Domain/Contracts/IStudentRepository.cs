using Domain.Models;

namespace Domain.Contracts
{
    public interface IStudentRepository
    {
        Task<IEnumerable<TDto>> GetAllProjectedAsync<TDto>(ISpecifications<ApplicationUser> specifications);
        Task<int> CountAsync(ISpecifications<ApplicationUser> specifications);
        void Update(ApplicationUser user);
    }
}
