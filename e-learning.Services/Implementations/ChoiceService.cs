using e_learning.Data.Entities;
using e_learning.infrastructure.Repositories;

namespace e_learning.Services.Implementations
{
    public class ChoiceService
    {
        private readonly IChoiceRepository _choiceRepository;

        public ChoiceService(IChoiceRepository choiceRepository)
        {
            _choiceRepository = choiceRepository;
        }

        public Task<List<Choice>> GetAllAsync() => _choiceRepository.GetAllAsync();
        public Task<Choice> GetByIdAsync(int id) => _choiceRepository.GetByIdAsync(id);
        public Task AddAsync(Choice choice) => _choiceRepository.AddAsync(choice);
        public Task UpdateAsync(Choice choice) => _choiceRepository.UpdateAsync(choice);
        public Task DeleteAsync(int id) => _choiceRepository.DeleteAsync(id);
    }
}
