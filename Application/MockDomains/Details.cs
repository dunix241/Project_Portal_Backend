using Application.Core;
using Application.MockDomains.DTOs;
using Domain.MockDomain;
using MediatR;
using Persistence;

namespace Application.MockDomains;

public class Details
{
   public class Query : IRequest<Result<GetMockDomainResponseDto>>
   {
      public Guid Id { get; set; }
   } 
   
   public class Handler : IRequestHandler<Query, Result<GetMockDomainResponseDto>>
   {
      private readonly DataContext _context;

      public Handler(DataContext context)
      {
         _context = context;
      }

      public async Task<Result<GetMockDomainResponseDto>> Handle(Query request, CancellationToken cancellationToken)
      {
         var mockDomain = await _context.MockDomains.FindAsync(request.Id);
         if (mockDomain == null)
         {
            return null;
         }
         return Result<GetMockDomainResponseDto>.Success(new GetMockDomainResponseDto{MockDomain = mockDomain});
      }
   }
}