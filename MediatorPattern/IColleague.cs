
namespace MediatorPattern
{
    public interface IColleague
    {
        public abstract void setMediator(IMediator mediator);
        public abstract void setColleagueEnabled(bool enabled);
    }
}
