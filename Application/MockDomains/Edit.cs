using Application.Core;
using Application.MockDomains.DTOs;
using AutoMapper;
using Domain.MockDomain;
using MediatR;
using Persistence;

namespace Application.MockDomains;

public class Edit
{
    public class Command : IRequest<Result<Unit>>
    {
        public Guid Id { get; set; }
        public EditMockDomainRequestDto MockDomain { get; set; }
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
            var mockDomain = await _context.MockDomains.FindAsync(request.Id);
            if (mockDomain == null)
            {
                return null;
            }

            _mapper.Map(request.MockDomain, mockDomain);

            await _context.SaveChangesAsync();
            
            return Result<Unit>.Success(Unit.Value);
        }
    }
}