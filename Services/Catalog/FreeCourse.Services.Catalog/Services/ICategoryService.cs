using FreeCourse.Services.Catalog.DTO;
using FreeCourse.Services.Catalog.Models;
using FreeCourse.Shared.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreeCourse.Services.Catalog.Services
{
    public interface ICategoryService
    {
        Task<ResponseDTO<List<CategoryDTO>>> GetAll();
        Task<ResponseDTO<CategoryDTO>> CreateAsync(CategoryDTO category);
        Task<ResponseDTO<CategoryDTO>> GetByIdAsync(string id);
    }
}
