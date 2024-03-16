﻿using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.DeleteLeaveType;
internal class DeleteLeaveTypeCommandHandler : IRequestHandler<DeleteLeaveTypeCommand, Unit>
{
  private readonly ILeaveTypeRepository _leaveTypeRepository;

  public DeleteLeaveTypeCommandHandler(ILeaveTypeRepository leaveTypeRepository)
  {

    _leaveTypeRepository = leaveTypeRepository;
  }

  public async Task<Unit> Handle(DeleteLeaveTypeCommand request, CancellationToken cancellationToken)
  {
    var leaveTypeToDelete = await _leaveTypeRepository.GetByIdAsync(request.Id);

    // Verify that the record exists
    if (leaveTypeToDelete == null)
      throw new NotFoundException(nameof(LeaveType), request.Id);

    await _leaveTypeRepository.DeleteAsync(leaveTypeToDelete);
    return Unit.Value;
  }
}
