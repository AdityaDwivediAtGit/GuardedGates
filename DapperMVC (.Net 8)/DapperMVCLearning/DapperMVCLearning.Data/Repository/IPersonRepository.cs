using DapperMVCLearning.Data.Models.Domain;

namespace DapperMVCLearning.Data.Repository
{
    public interface IPersonRepository
    {
        // Creater
        Task<bool> AddAsync(Person person);
        
        // Updater
        Task<bool> UpdateAsync(Person person);
        
        // Deleter
        Task<bool> DeleteAsync(int id);
            
        // Single person Getter
        Task<Person?> GetByIdAsync(int id);
        
        // All People Getter
        Task<IEnumerable<Person>> GetAllAsync();
    }
}