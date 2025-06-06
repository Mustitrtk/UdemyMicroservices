﻿using System;

namespace FreeCourse.Services.Catalog.DTO
{
    public class CourseCreateDTO
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string UserId { get; set; }

        public string Picture { get; set; }

        public FeatureDTO Feature { get; set; }

        public string CategoryId { get; set; }
    }
}
