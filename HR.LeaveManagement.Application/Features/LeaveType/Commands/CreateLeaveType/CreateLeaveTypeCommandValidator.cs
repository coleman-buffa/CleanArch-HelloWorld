using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;
public class CreateLeaveTypeCommandValidator : AbstractValidator<CreateLeaveType>
{
  private readonly ILeaveTypeRepository _leaveTypeRepository;

  public CreateLeaveTypeCommandValidator(ILeaveTypeRepository leaveTypeRepository)
  {
    RuleFor(p => p.Name)
      .NotEmpty().WithMessage("{PropertyName} is required")
      .NotNull()
      .MaximumLength(70).WithMessage("{PropertyName} must not exceed 70 characters.");

    RuleFor(p => p.DefaultDays)
      .LessThan(100).WithMessage("{PropertyName} must not exceed 100 characters")
      .GreaterThan(1).WithMessage("{PropertyName} cannot be less than 1");

    RuleFor(q => q)
      .MustAsync(LeaveTypeNameUnique)
      .WithMessage("Leave type already exists");

    _leaveTypeRepository = leaveTypeRepository;
  }

  private Task<bool> LeaveTypeNameUnique(CreateLeaveType command, CancellationToken token)
  {
    return _leaveTypeRepository.IsLeaveTypeUnique(command.Name);
  }
}
