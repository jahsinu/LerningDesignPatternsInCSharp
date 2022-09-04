namespace MediatorPattern
{
    public partial class MediatorSampleForm : Form, IMediator
    {

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MediatorSampleForm()
        {
            // Colleagueたちの生成
            createColleagues();
            // 有効/無効の初期設定
            colleagueChanged();
        }

        /// <summary>
        /// Colleagueからの通知で各Colleagueの有効/無効を判定する
        /// </summary>
        public void colleagueChanged()
        {
            if (radioGuest.Checked)
            {   // Guestモード
                textUser.setColleagueEnabled(false);
                textPass.setColleagueEnabled(false);
                buttonOK.setColleagueEnabled(true);
            }
            else
            {   // Loginモード
                textUser.setColleagueEnabled(true);
                userpassChanged();
            }
        }

        /// <summary>
        /// textUserまたはtextPassの変更による、各Colleagueの有効/無効を判定する
        /// </summary>
        private void userpassChanged()
        {
            if (textUser.Text.Length > 0)
            {
                textPass.setColleagueEnabled(true);
                if (textPass.Text.Length > 0)
                {
                    buttonOK.setColleagueEnabled(true);
                }
                else
                {
                    buttonOK.setColleagueEnabled(false);
                }
            }
            else
            {
                textPass.setColleagueEnabled(false);
                buttonOK.setColleagueEnabled(false);
            }
        }
    }
}