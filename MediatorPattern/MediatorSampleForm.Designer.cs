namespace MediatorPattern
{
    partial class MediatorSampleForm
    {
        private Label labelUser;
        private Label labelPass;
        private ColleagueRadioButton radioGuest;
        private ColleagueRadioButton radioLogin;
        private ColleagueTextbox textUser;
        private ColleagueTextbox textPass;
        private ColleagueButton buttonOK;
        private ColleagueButton buttonCancel;

        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Colleagueたちを生成する
        /// </summary>
        public void createColleagues()
        {
            this.radioGuest = new ColleagueRadioButton("Guest", true);
            this.radioLogin = new ColleagueRadioButton("Login", false);
            this.labelUser = new System.Windows.Forms.Label();
            this.labelPass = new System.Windows.Forms.Label();
            this.textUser = new ColleagueTextbox("", 10);
            this.textPass = new ColleagueTextbox("", 10);
            this.textPass.PasswordChar = '*';
            this.buttonOK = new ColleagueButton("OK");
            this.buttonCancel = new ColleagueButton("Cancel");
            this.SuspendLayout();
            // 
            // radioGuest
            // 
            this.radioGuest.AutoSize = true;
            this.radioGuest.Location = new System.Drawing.Point(12, 12);
            this.radioGuest.Name = "radioGuest";
            this.radioGuest.Size = new System.Drawing.Size(55, 19);
            this.radioGuest.TabIndex = 6;
            this.radioGuest.TabStop = true;
            this.radioGuest.Text = "Guest";
            this.radioGuest.UseVisualStyleBackColor = true;
            // 
            // radioLogin
            // 
            this.radioLogin.AutoSize = true;
            this.radioLogin.Location = new System.Drawing.Point(112, 12);
            this.radioLogin.Name = "radioLogin";
            this.radioLogin.Size = new System.Drawing.Size(55, 19);
            this.radioLogin.TabIndex = 7;
            this.radioLogin.TabStop = true;
            this.radioLogin.Text = "Login";
            this.radioLogin.UseVisualStyleBackColor = true;
            // 
            // labelUser
            // 
            this.labelUser.AutoSize = true;
            this.labelUser.Location = new System.Drawing.Point(12, 40);
            this.labelUser.Name = "labelUser";
            this.labelUser.Size = new System.Drawing.Size(62, 15);
            this.labelUser.TabIndex = 2;
            this.labelUser.Text = "Username:";
            // 
            // labelPass
            // 
            this.labelPass.AutoSize = true;
            this.labelPass.Location = new System.Drawing.Point(12, 69);
            this.labelPass.Name = "labelPass";
            this.labelPass.Size = new System.Drawing.Size(60, 15);
            this.labelPass.TabIndex = 3;
            this.labelPass.Text = "Password:";
            // 
            // textUser
            // 
            this.textUser.Location = new System.Drawing.Point(112, 37);
            this.textUser.Name = "textUser";
            this.textUser.Size = new System.Drawing.Size(100, 23);
            this.textUser.TabIndex = 4;
            // 
            // textPass
            // 
            this.textPass.Location = new System.Drawing.Point(112, 66);
            this.textPass.Name = "textPass";
            this.textPass.Size = new System.Drawing.Size(100, 23);
            this.textPass.TabIndex = 5;
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(12, 95);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(94, 23);
            this.buttonOK.TabIndex = 0;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(112, 95);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(100, 23);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // MediatorSampleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(223, 128);
            this.Controls.Add(this.radioLogin);
            this.Controls.Add(this.radioGuest);
            this.Controls.Add(this.textPass);
            this.Controls.Add(this.textUser);
            this.Controls.Add(this.labelPass);
            this.Controls.Add(this.labelUser);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Name = "MediatorSampleForm";
            this.Text = "Mediator Sample";
            this.ResumeLayout(false);
            this.PerformLayout();
            //
            // Mediatorのセット
            //
            radioGuest.setMediator(this);
            radioLogin.setMediator(this);
            textUser.setMediator(this);
            textPass.setMediator(this);
            buttonOK.setMediator(this);
            buttonCancel.setMediator(this);
        }

        #endregion

    }
}