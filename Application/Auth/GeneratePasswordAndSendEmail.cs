using Application.Core;
using Application.Mail;
using Domain;
using Domain.Mail;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using SmartFormat;

namespace Application.Users;

public class GeneratePasswordAndSendEmail
{
    public class Command : IRequest<Result<Unit>>
    {
        public MailData MailData { get; set; }
        public Func<string, Task<bool>> Func { get; set; }
    }

    public class Handler : IRequestHandler<Command, Result<Unit>>
    {
        private readonly IMediator _mediator;

        public Handler(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            var arg = new
            {
                Password = $"A{Guid.NewGuid().ToString().Replace("-", "").Substring(16)}_"
            };

            var success = await request.Func(arg.Password);

            if (success)
            {
                request.MailData.BodyBuilder.HtmlBody = Smart.Format(request.MailData.BodyBuilder.HtmlBody, arg);
                request.MailData.BodyBuilder.TextBody = Smart.Format(request.MailData.BodyBuilder.TextBody, arg);

                var result = await _mediator.Send(new Send.Command
                {
                    MailData = request.MailData
                });
                
                success &= result.IsSuccess;
            }

            return success ? Result<Unit>.Success(Unit.Value) : Result<Unit>.Failure("Error generating password");
        }
    }
}