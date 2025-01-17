using AutoMapper;
using FreeCourse.Services.Catalog.DTO;
using FreeCourse.Services.Catalog.Models;
using FreeCourse.Services.Catalog.Settings;
using FreeCourse.Shared.DTO;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreeCourse.Services.Catalog.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IMongoCollection<Category> _categoryCollection;

        private readonly IMapper _mapper;

        public CategoryService(IMapper mapper, IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);

            var database = client.GetDatabase(databaseSettings.DatabaseName);

            _categoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);
            _mapper = mapper;
        }

        public async Task<ResponseDTO<List<CategoryDTO>>> GetAll()
        {
            var categories = await _categoryCollection.Find(category => true).ToListAsync();
            return ResponseDTO<List<CategoryDTO>>.Success(_mapper.Map<List<CategoryDTO>>(categories), 200);
        }

        public async Task<ResponseDTO<CategoryDTO>> CreateAsync(CategoryDTO categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);
            await _categoryCollection.InsertOneAsync(category);

            return ResponseDTO<CategoryDTO>.Success(_mapper.Map<CategoryDTO>(category),200);
            //return ResponseDTO<CategoryDTO>.Success(200);
        }

        public async Task<ResponseDTO<CategoryDTO>> GetByIdAsync(string id)
        {
            var category = await _categoryCollection.Find(x=> x.Id == id).FirstOrDefaultAsync();

            if (category == null)
            {
                return ResponseDTO<CategoryDTO>.Fail("Record not found!",404);
            }
            return ResponseDTO<CategoryDTO>.Success(_mapper.Map<CategoryDTO>(category),200);
        }
    }
}
