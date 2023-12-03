using Application.Core;
using Application.Projects.DTOs;
using AutoMapper;
using Domain.Project;
using MediatR;
using Persistence;

namespace Application.Projects;

public class Create
{
   public class Command : IRequest<Result<Unit>>
   {
      public CreateProjectRequestDto Project { get; set; }
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
         var project = new Project();
         _mapper.Map(request.Project, project);
         _context.Projects.Add(project);
         await _context.SaveChangesAsync();

         return Result<Unit>.Success(Unit.Value);
      }
   }
}