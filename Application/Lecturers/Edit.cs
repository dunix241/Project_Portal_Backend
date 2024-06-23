using Application.Core;
using Application.Lecturers.DTOs;
using Application.Lecturers.Validation;
using AutoMapper;
using DocumentFormat.OpenXml.Office2010.Excel;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Persistence;

namespace Application.Lecturers
{
    public class Edit
    {
        public class Command : IRequest<Result<GetLecturerResponseDto>>
        {
            public string Id { get; set; }
            public EditLecturerRequestDto Lecturer { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<GetLecturerResponseDto>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            private readonly UserManager<User> _userManager;
            private readonly IMediator _mediator;

            public Handler(DataContext context, IMapper mapper, UserManager<User> userManager,IMediator mediator)
            {
                _context = context;
                _mapper = mapper;
                _userManager = userManager;
                _mediator = mediator;
            }

            public class CommandValidator : AbstractValidator<Command>
            {
                public CommandValidator()
                {
                    RuleFor(x => x.Lecturer).SetValidator(new LecturerEditValidator());
                }
            }

            public async Task<Result<GetLecturerResponseDto>> Handle(Command request, CancellationToken cancellationToken)
            {
                var validationResult = new CommandValidator().Validate(request);
                if (!validationResult.IsValid)
                {
                    return Result<GetLecturerResponseDto>.Failure("Validation failed, Name cannot be empty or contain numbers nor special characters.");
                }
                var lecturer = await _context.Lecturers.FindAsync(request.Id);
                if (lecturer == null)
                {
                    return Result<GetLecturerResponseDto>.Failure("Not found");
                }

                var success = true;
                using var transaction = _context.Database.BeginTransaction();
                try
                {
                    var user = await _userManager.FindByIdAsync(lecturer.UserId);
                    if (request.Lecturer.Email != user.Email)
                    {
                        success &= (await _userManager.SetEmailAsync(user, request.Lecturer.Email)).Succeeded;
                    }
                    
                    _mapper.Map(request.Lecturer, lecturer);
                    _mapper.Map(request.Lecturer, user);
                    await _userManager.UpdateAsync(user);
                    
                   await transaction.CommitAsync(cancellationToken);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw e;
                }

                var response =await _mediator.Send(new Details.Query { Id = lecturer.UserId });
                return success ? Result<GetLecturerResponseDto>.Success(response.Value) : Result<GetLecturerResponseDto>.Failure("Problem editing lecturer");
            }
        }
    }
}
