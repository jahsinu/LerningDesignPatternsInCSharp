namespace MediatorPattern
{
    public partial class MediatorSampleForm : Form, IMediator
    {

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        public MediatorSampleForm()
        {
            // Colleague�����̐���
            createColleagues();
            // �L��/�����̏����ݒ�
            colleagueChanged();
        }

        /// <summary>
        /// Colleague����̒ʒm�ŊeColleague�̗L��/�����𔻒肷��
        /// </summary>
        public void colleagueChanged()
        {
            if (radioGuest.Checked)
            {   // Guest���[�h
                textUser.setColleagueEnabled(false);
                textPass.setColleagueEnabled(false);
                buttonOK.setColleagueEnabled(true);
            }
            else
            {   // Login���[�h
                textUser.setColleagueEnabled(true);
                userpassChanged();
            }
        }

        /// <summary>
        /// textUser�܂���textPass�̕ύX�ɂ��A�eColleague�̗L��/�����𔻒肷��
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