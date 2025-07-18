using Domain.Models;

namespace Domain.Contracts
{
    public interface IStudentRepository
    {
        Task<IEnumerable<TDto>> GetAllProjectedAsync<TDto>(ISpecifications<ApplicationUser> specifications);
        Task<TDto?> GetProjectedAsync<TDto>(string studentId);
        Task<bool> isStudentExistsAsync(string studentId);
        Task<int> CountAsync(ISpecifications<ApplicationUser> specifications);
        Task<int> CountAsync();
        void Update(ApplicationUser user);
    }
}
