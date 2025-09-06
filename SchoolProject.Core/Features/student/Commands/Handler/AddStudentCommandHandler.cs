using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Azure;
using MediatR;
using SchoolProject.Core.bases;
using SchoolProject.Core.Features.student.Commands.models;
using SchoolProject.Data.Entities;
using SchoolProject.Service.Absract;
using ProjectResponse = SchoolProject.Core.bases.Response<string>;

namespace SchoolProject.Core.Features.student.Commands.Handler
{
    public class AddStudentCommandHandler : ResponseHandler, IRequestHandler<AddStudentCommand, ProjectResponse>
    {
        private readonly IstudentService _service;
        private readonly IMapper _mapper;


        public AddStudentCommandHandler(IstudentService service,IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
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
    }
}
