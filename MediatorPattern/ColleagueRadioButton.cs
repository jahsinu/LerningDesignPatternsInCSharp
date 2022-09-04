
namespace MediatorPattern
{
    public class ColleagueRadioButton : RadioButton, IColleague
    {
        private IMediator? mediator;
        public ColleagueRadioButton(string caption, bool state)
        {
            this.Text = caption;
            this.Checked = state;
        }
        public void setMediator(IMediator mediator)
        {
            this.mediator = mediator;
        }
        public void setColleagueEnabled(bool enabled)
        {
            this.Enabled = enabled;
        }
        protected override void OnCheckedChanged(EventArgs e)
        {
            base.OnCheckedChanged(e);
            if(mediator != null)
            {
                mediator.colleagueChanged();
            }
        }
    }
}
