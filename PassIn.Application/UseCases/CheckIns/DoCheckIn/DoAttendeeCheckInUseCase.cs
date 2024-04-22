using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infrastructure;
using PassIn.Infrastructure.Entities;

namespace PassIn.Application.UseCases.CheckIns.DoCheckIn;
public class DoAttendeeCheckInUseCase
{
    private readonly PassInDbContext _dbcontext;

    public DoAttendeeCheckInUseCase()
    {
        _dbcontext = new PassInDbContext();
    }

    public ResponseRegisteredJson Execute(Guid attendeeId)
    {
        Validate(attendeeId);

        var entity = new CheckIn
        {
            AttendeeId = attendeeId,
            Created_at = DateTime.UtcNow,
        };
        
        _dbcontext.Check_Ins.Add(entity);
        _dbcontext.SaveChanges();

        return new ResponseRegisteredJson
        {
            Id = entity.Id,
        };
    }

    private void Validate(Guid attendeeId)
    {
        var existAttendee = _dbcontext.Attendees.Any(attendee => attendee.Id == attendeeId);
        if (existAttendee == false)
        {
            throw new NotFoundException("The attendee with this Id was not found");
        }

        var existCheckin = _dbcontext.Check_Ins.Any(ch => ch.AttendeeId == attendeeId);
        if (existCheckin)
        {
            throw new ConflictException("Attendee can not do checking twice in the same event.");
        }
    }
}
