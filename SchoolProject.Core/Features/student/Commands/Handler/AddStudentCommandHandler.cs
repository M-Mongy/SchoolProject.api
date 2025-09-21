using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Azure;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.bases;
using SchoolProject.Core.Features.student.Commands.models;
using SchoolProject.Core.SharedResources;
using SchoolProject.Data.Entities;
using SchoolProject.Service.Absract;
using ProjectResponse = SchoolProject.Core.bases.Response<string>;

namespace SchoolProject.Core.Features.student.Commands.Handler
{
    public class AddStudentCommandHandler : ResponseHandler,
                                            IRequestHandler<AddStudentCommand, ProjectResponse>,
                                            IRequestHandler<EditStudentCommand, ProjectResponse>,
                                            IRequestHandler<DeleteStudentCommand, ProjectResponse>
    {
        private readonly IstudentService _service;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResource> _stringLocalizer ;


        public AddStudentCommandHandler(IstudentService service,
            IMapper mapper, IStringLocalizer<SharedResource> stringLocalizer ) : base(stringLocalizer)
        {
            _service = service;
            _mapper = mapper;
            _stringLocalizer = stringLocalizer;
        }

        public async Task<ProjectResponse> Handle(AddStudentCommand request, CancellationToken cancellationToken)
        {
            var studentMapp = _mapper.Map<Student>(request);

            var result = await _service.AddAsync(studentMapp);

            if (result == "already Exist")
            {
                return BadRequest<string>("Student already exists");
            }

            return Created("Student Added Successfully");
        }

        public async Task<ProjectResponse> Handle(EditStudentCommand request, CancellationToken cancellationToken)
        {
            var student = await _service.GetStudentByIdAsync(request.id);
            if (student == null) return NotFound<string>("Student not found");
   
            var sudentmapper=_mapper.Map<Student>(request);

            var result = await _service.EditAsync(sudentmapper);

            if (result == "Success")
            {
                return Success("Student Edited Successfully");
            }
            else return BadRequest<string>();


        }
        public async Task<ProjectResponse> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
        {
            // 1- Validate Id before processing
            if (request.Id <= 0)
            {
                return BadRequest<string>("Invalid student id");
            }

            // 2- Check if student exists
            var student = await _service.GetStudentByIdAsync(request.Id);
            if (student == null)
            {
                return NotFound<string>("Student not found");
            }

            // 3- Delete student
            await _service.DeleteAsync(student);
            return Deleted<string>();

        }
    }
}
