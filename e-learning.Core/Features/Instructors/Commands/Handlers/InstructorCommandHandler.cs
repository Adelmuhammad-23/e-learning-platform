using AutoMapper;
using e_learning.Core.Bases;
using e_learning.Core.Features.Instructors.Commands.Models;
using e_learning.Data.Entities;
using e_learning.Services.Abstructs;
using MediatR;

namespace e_learning.Core.Features.Instructors.Commands.Handlers
{
    public class InstructorCommandHandler : ResponsesHandler,
        IRequestHandler<UpdateInstructorCommand, Responses<string>>,
        IRequestHandler<DeleteInstructorCommand, Responses<string>>,
        IRequestHandler<AddProfessionalInstructorCommand, Responses<string>>
    {
        private readonly IInstructorService _instructorService;
        private readonly IMapper _mapper;

        public InstructorCommandHandler(IInstructorService instructorService, IMapper mapper)
        {
            _instructorService = instructorService;
            _mapper = mapper;
        }

        public async Task<Responses<string>> Handle(UpdateInstructorCommand request, CancellationToken cancellationToken)
        {
            var instructor = _mapper.Map<Instructor>(request);
            var result = await _instructorService.UpdateInstructorAsync(request.Id, instructor, request.Image);

            if (!result)
                return NotFound<string>("Instructor not found");

            return Success("Instructor updated successfully");
        }

        public async Task<Responses<string>> Handle(DeleteInstructorCommand request, CancellationToken cancellationToken)
        {
            var result = await _instructorService.DeleteInstructorAsync(request.Id);

            if (!result)
                return NotFound<string>("Instructor not found");

            return Success("Instructor deleted successfully");
        }

        public async Task<Responses<string>> Handle(AddProfessionalInstructorCommand request, CancellationToken cancellationToken)
        {
            var instructor = _mapper.Map<Instructor>(request);
            var result = await _instructorService.AddProfessionalInstructorAsync(request.Id, instructor, request.Certificates);

            if (!result)
                return NotFound<string>("Instructor not found");

            return Success("Add Professional Instructor successfully");
        }
    }
}
