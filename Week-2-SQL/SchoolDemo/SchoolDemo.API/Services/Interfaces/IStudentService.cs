using SchoolDemo.Models;

namespace SchoolDemo.Services
{
    public interface IStudentService
    {
        Task<IEnumerable<Student>> GetAllAsync();
        Task<Student?> GetByIdAsync(int id);
        Task CreateAsync(Student student);
    }
}
