using Application.Core;
using Application.Projects.DTOs;
using MediatR;
using Persistence;

namespace Application.Projects;

public class List
{
   public class Query : IRequest<Result<ListProjectResponseDto>>
   {
      public PagingParams QueryParams { get; set; }
   }
   
   public class Handler : IRequestHandler<Query, Result<ListProjectResponseDto>>
   {
      private readonly DataContext _context;

      public Handler(DataContext context)
      {
         _context = context;
      }

      public async Task<Result<ListProjectResponseDto>> Handle(Query request, CancellationToken cancellationToken)
      {
         var query = _context.Projects.AsQueryable();

         var projects = new ListProjectResponseDto();
         await projects.GetItemsAsync(query, request.QueryParams.PageNumber, request.QueryParams.PageSize);

        return Result<ListProjectResponseDto>.Success(projects);
      }
   }
}