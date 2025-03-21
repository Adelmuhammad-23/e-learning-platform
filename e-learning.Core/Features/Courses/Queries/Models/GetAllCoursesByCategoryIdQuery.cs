﻿using e_learning.Core.Bases;
using e_learning.Core.Features.Courses.Queries.Responses;
using MediatR;

namespace e_learning.Core.Features.Courses.Queries.Models
{
    public class GetAllCoursesByCategoryIdQuery : IRequest<Responses<List<AllCoursesByCategoryIdResponse>>>
    {
        public int Id { get; set; }
        public GetAllCoursesByCategoryIdQuery(int id)
        {
            Id = id;
        }
    }
}
