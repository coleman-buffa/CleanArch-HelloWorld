using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveType.Queries.GetLeaveTypeDetails;
public record GetLeaveTypeDetails(int Id) : IRequest<LeaveTypeDetailsDto>;
