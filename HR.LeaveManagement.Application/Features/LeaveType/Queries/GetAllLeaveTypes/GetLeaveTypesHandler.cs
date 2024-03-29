﻿using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;
public class GetLeaveTypesHandler : IRequestHandler<GetLeaveTypes, List<LeaveTypeDto>>
{
  private readonly IMapper _mapper;
  private readonly ILeaveTypeRepository _leaveTypeRepository;

  public GetLeaveTypesHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepository)
  {
    _mapper = mapper;
    _leaveTypeRepository = leaveTypeRepository;
  }
  public async Task<List<LeaveTypeDto>> Handle(GetLeaveTypes request, CancellationToken cancellationToken)
  {
    var leaveTypes = await _leaveTypeRepository.GetAsync();
    var data = _mapper.Map<List<LeaveTypeDto>>(leaveTypes);
    return data;
  }
}
