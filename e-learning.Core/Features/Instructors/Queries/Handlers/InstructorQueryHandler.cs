using AutoMapper;
using e_learning.Core.Bases;
using e_learning.Core.Features.Instructors.Queries.Models;
using e_learning.Core.Features.Instructors.Queries.Responses;
using e_learning.Data.Entities.Identity;
using e_learning.Services.Abstructs;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace e_learning.Core.Features.Instructors.Queries.Handlers
{
    public class InstructorQueryHandler : ResponsesHandler,
        IRequestHandler<GetAllInstructorsQuery, Responses<List<InstructorQueryResponse>>>,
        IRequestHandler<GetInstructorByIdQuery, Responses<InstructorQueryResponse>>
    {
        private readonly IInstructorService _instructorService;
        private readonly UserManager<User> _userManager;

        private readonly IMapper _mapper;

        public InstructorQueryHandler(IInstructorService instructorService, IMapper mapper)
        {
            _instructorService = instructorService;
            _mapper = mapper;
        }

        public async Task<Responses<List<InstructorQueryResponse>>> Handle(GetAllInstructorsQuery request, CancellationToken cancellationToken)
        {
            var instructors = await _instructorService.GetAllInstructorsAsync();
            var result = _mapper.Map<List<InstructorQueryResponse>>(instructors);
            return Success(result);
        }

        public async Task<Responses<InstructorQueryResponse>> Handle(GetInstructorByIdQuery request, CancellationToken cancellationToken)
        {
            var instructor = await _instructorService.GetInstructorByIdAsync(request.Id);
            if (instructor is null)
                return NotFound<InstructorQueryResponse>("Instructor not found");

            var result = _mapper.Map<InstructorQueryResponse>(instructor);
            return Success(result);
        }
    }
}