using FreeCourse.Services.Catalog.DTO;
using FreeCourse.Shared.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreeCourse.Services.Catalog.Services
{
    public interface ICourseService
    {
        Task<ResponseDTO<List<CourseDTO>>> GetAllAsync();

        Task<ResponseDTO<CourseDTO>> GetByIdAsync(string id);

        Task<ResponseDTO<List<CourseDTO>>> GetByUserId(string id);

        Task<ResponseDTO<CourseDTO>> CreateAsync(CourseCreateDTO course);

        Task<ResponseDTO<NoContent>> UpdateAsync(CourseUpdateDTO courseUpdateDTO);

        Task<ResponseDTO<NoContent>> DeleteAsync(string id);
    }
}
