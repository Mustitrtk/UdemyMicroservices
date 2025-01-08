using AutoMapper;
using FreeCourse.Services.Catalog.DTO;
using FreeCourse.Services.Catalog.Models;
using FreeCourse.Services.Catalog.Settings;
using FreeCourse.Shared.DTO;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreeCourse.Services.Catalog.Services
{
    public class CourseService
    {
        private readonly IMongoCollection<Course> _courseCollection;
        private readonly IMongoCollection<Category> _categoryCollection;
        private readonly IMapper _mapper;

        public CourseService(IMapper mapper, IDatabaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);

            _courseCollection = database.GetCollection<Course>(_databaseSettings.CourseCollectionName);

            _categoryCollection = database.GetCollection<Category>(_databaseSettings.CategoryCollectionName);

            _mapper = mapper;
        }

        public async Task<ResponseDTO<List<CourseDTO>>> GetAllAsync()
        {
            var courses = await _courseCollection.Find(course => true).ToListAsync();

            if (courses.Any())
            {
                foreach (var course in courses)
                {
                    course.Category = await _categoryCollection.Find<Category>(x => x.Id == course.CategoryId).FirstAsync();
                }
            }
            else
            {
                courses = new List<Course>();
            }

            return ResponseDTO<List<CourseDTO>>.Success(_mapper.Map<List<CourseDTO>>(courses),200);
        }

        public async Task<ResponseDTO<CourseDTO>> GetByIdAsync(string id) 
        {
            var course = await _courseCollection.Find<Course>(x => x.Id == id).FirstOrDefaultAsync(); //Arama yapılacağı zaman model belirtilir

            if (course == null)
            {
                return ResponseDTO<CourseDTO>.Fail("Data not found", 404);
            }

            course.Category = await _categoryCollection.Find<Category>(x => x.Id == course.CategoryId).FirstOrDefaultAsync();

            return ResponseDTO<CourseDTO>.Success(_mapper.Map<CourseDTO>(course), 200); //DTO - model dönüşümü
        }

        public async Task<ResponseDTO<List<CourseDTO>>> GetByUserId(string id)
        {
            var courses = await _courseCollection.Find<Course>(x => x.UserId == id).ToListAsync();

            if(courses.Any())
            {
                foreach (var course in courses)
                {
                    course.Category = await _categoryCollection.Find<Category>(c => c.Id == course.CategoryId).FirstAsync();
                }
            }
            else
            {
                courses = new List<Course>();
            }

            return ResponseDTO<List<CourseDTO>>.Success(_mapper.Map<List<CourseDTO>>(courses), 200);
        }

        public async Task<ResponseDTO<CourseDTO>> CreateAsync(CourseCreateDTO course)
        {
            var _course = _mapper.Map<Course>(course);
            
            _course.CreatedTime = DateTime.Now;
            await _courseCollection.InsertOneAsync(_course);

            return ResponseDTO<CourseDTO>.Success(_mapper.Map<CourseDTO>(_course),200);
        }

        public async Task<ResponseDTO<NoContent>> UpdateAsync(CourseUpdateDTO courseUpdateDTO)
        {
            var updateCourse = _mapper.Map<Course>(courseUpdateDTO);

            var result = await _courseCollection.FindOneAndReplaceAsync(x=> x.Id == courseUpdateDTO.Id,updateCourse);

            if(result == null)
            {
                return ResponseDTO<NoContent>.Fail("Course not found!",404);
            }

            return ResponseDTO<NoContent>.Success(204);
        }

        public async Task<ResponseDTO<NoContent>> DeleteAsync(string id)
        {
            var result = await _courseCollection.DeleteOneAsync(x=> x.Id == id);

            if (result.DeletedCount >0) 
            {
                return ResponseDTO<NoContent>.Fail("Data not found!",404);
            }

            return ResponseDTO<NoContent>.Success(200);
        }
    }
}
