using Application.Core;
using Application.Image.DTOs;
using Application.Projects.DTOs;
using AutoMapper;
using Domain.Project;
using MediatR;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageEntity = Domain.Image;

namespace Application.Image
{
    public class Create
    {
        public class Command : IRequest<Result<Unit>>
        {
            public CreateImgRequestDto imageRequest { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var img = new ImageEntity();
                _mapper.Map(request.imageRequest, img);
                _context.Images.Add(img);
                await _context.SaveChangesAsync();

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
