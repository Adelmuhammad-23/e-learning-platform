using e_learning.Data.Entities;
using e_learning.Data.Helpers;

namespace e_learning.Services.Abstructs
{
    public interface IModuleService
    {
        Task<Module> GetModuleByIdAsync(int id);
        Task<List<Module>> GetByCourseIdAsync(int courseId);

        public Task<string> AddVideoToModuleAsync(CreateVideoDto dto);
        public Task<string> DeleteVideoFromModuleAsync(int videoId);
        public Task<string> AddModuleAsync(Module module);
        Task<string> DeleteAsync(int id);
        Task<string> UpdateAsync(Module module);

    }
}
