
namespace MediatorPattern
{
    public class ColleagueButton : Button, IColleague
    {
        private IMediator? mediator;
        public ColleagueButton(string caption)
        {
            this.Text = caption;
        }
        public void setMediator(IMediator mediator)
        {
            this.mediator = mediator;
        }
        public void setColleagueEnabled(bool enabled)
        {
            this.Enabled = enabled;
        }
    }
}
