using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;
public class CreateLeaveTypeHandler : IRequestHandler<CreateLeaveType, int>
{
  private readonly IMapper _mapper;
  private readonly ILeaveTypeRepository _leaveTypeRepository;

  public CreateLeaveTypeHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepository)
  {
    _mapper = mapper;
    _leaveTypeRepository = leaveTypeRepository;
  }
  public async Task<int> Handle(CreateLeaveType request, CancellationToken cancellationToken)
  {
    // Validation incoming data
    var validator = new CreateLeaveTypeCommandValidator(_leaveTypeRepository);
    var validationResult = await validator.ValidateAsync(request);

    if (validationResult.Errors.Any())
      throw new BadRequestException("Invalid LeaveType", validationResult);

    var leaveTypeToCreate = _mapper.Map<Domain.LeaveType>(request);
    await _leaveTypeRepository.CreateAsync(leaveTypeToCreate);
    return leaveTypeToCreate.Id;
  }
}
