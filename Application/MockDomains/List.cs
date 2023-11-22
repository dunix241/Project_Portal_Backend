using Application.Core;
using Application.MockDomains.DTOs;
using Domain.MockDomain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.MockDomains;

public class List
{
   public class Query : IRequest<Result<ListMockDomainResponseDto>>
   {
      public PagingParams QueryParams { get; set; }
   }
   
   public class Handler : IRequestHandler<Query, Result<ListMockDomainResponseDto>>
   {
      private readonly DataContext _context;

      public Handler(DataContext context)
      {
         _context = context;
      }

      public async Task<Result<ListMockDomainResponseDto>> Handle(Query request, CancellationToken cancellationToken)
      {
         var query = _context.MockDomains.AsQueryable();

         var mockDomains = new ListMockDomainResponseDto();
         await mockDomains.GetItemsAsync(query, request.QueryParams.PageNumber, request.QueryParams.PageSize);

        return Result<ListMockDomainResponseDto>.Success(mockDomains);
      }
   }
}