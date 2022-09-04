
namespace MediatorPattern
{
    public class ColleagueTextbox : TextBox, IColleague
    {
        private IMediator? mediator;
        public ColleagueTextbox(string text, int len)
        {
            this.Text = text;
            this.MaxLength = len;
        }
        public void setMediator(IMediator mediator)
        {
            this.mediator = mediator;
        }
        public void setColleagueEnabled(bool enabled)
        {
            this.Enabled = enabled;
        }
        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            if (mediator != null)
            {
                mediator.colleagueChanged();
            }
        }
    }
}
