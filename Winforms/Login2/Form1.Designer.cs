namespace Login2
{
    partial class Form1
    {
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            banner = new Label();
            pictureBox1 = new PictureBox();
            LoginLabel = new Label();
            PassLabel = new Label();
            Login = new TextBox();
            Pass = new TextBox();
            button1 = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // banner
            // 
            banner.AutoSize = true;
            banner.Font = new Font("Segoe UI", 22F);
            banner.Location = new Point(135, 9);
            banner.Name = "banner";
            banner.Size = new Size(188, 41);
            banner.TabIndex = 0;
            banner.Text = "LOGOWANIE";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.FontAwesome_User_Lock_icon;
            pictureBox1.Location = new Point(12, 53);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(158, 157);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // LoginLabel
            // 
            LoginLabel.AutoSize = true;
            LoginLabel.Font = new Font("Segoe UI", 16F);
            LoginLabel.Location = new Point(176, 53);
            LoginLabel.Name = "LoginLabel";
            LoginLabel.Size = new Size(202, 30);
            LoginLabel.TabIndex = 2;
            LoginLabel.Text = "Nazwa użytkownika";
            // 
            // PassLabel
            // 
            PassLabel.AutoSize = true;
            PassLabel.Font = new Font("Segoe UI", 16F);
            PassLabel.Location = new Point(176, 138);
            PassLabel.Name = "PassLabel";
            PassLabel.Size = new Size(68, 30);
            PassLabel.TabIndex = 3;
            PassLabel.Text = "Hasło";
            // 
            // Login
            // 
            Login.Location = new Point(176, 90);
            Login.Name = "Login";
            Login.Size = new Size(274, 23);
            Login.TabIndex = 4;
            // 
            // Pass
            // 
            Pass.Location = new Point(176, 171);
            Pass.Name = "Pass";
            Pass.PasswordChar = '*';
            Pass.Size = new Size(274, 23);
            Pass.TabIndex = 5;
            // 
            // button1
            // 
            button1.Font = new Font("Segoe UI", 16F);
            button1.Location = new Point(350, 220);
            button1.Name = "button1";
            button1.Size = new Size(100, 50);
            button1.TabIndex = 6;
            button1.Text = "Ok";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(462, 282);
            Controls.Add(button1);
            Controls.Add(Pass);
            Controls.Add(Login);
            Controls.Add(PassLabel);
            Controls.Add(LoginLabel);
            Controls.Add(pictureBox1);
            Controls.Add(banner);
            Name = "Form1";
            Text = "Panel Logowania";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label banner;
        private PictureBox pictureBox1;
        private Label LoginLabel;
        private Label PassLabel;
        private TextBox Login;
        private TextBox Pass;
        private Button button1;
    }
}
