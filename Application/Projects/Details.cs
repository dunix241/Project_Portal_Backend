using Application.Core;
using Application.Projects.DTOs;
using MediatR;
using Persistence;

namespace Application.Projects;

public class Details
{
   public class Query : IRequest<Result<GetProjectResponseDto>>
   {
      public Guid Id { get; set; }
   } 
   
   public class Handler : IRequestHandler<Query, Result<GetProjectResponseDto>>
   {
      private readonly DataContext _context;

      public Handler(DataContext context)
      {
         _context = context;
      }

      public async Task<Result<GetProjectResponseDto>> Handle(Query request, CancellationToken cancellationToken)
      {
         var project = await _context.Projects.FindAsync(request.Id);
         if (project == null)
         {
            return null;
         }
         return Result<GetProjectResponseDto>.Success(new GetProjectResponseDto{Project = project});
      }
   }
}