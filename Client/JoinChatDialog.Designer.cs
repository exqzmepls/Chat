namespace Client
{
    partial class JoinChatDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.loginTextBox = new System.Windows.Forms.TextBox();
            this.okButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.chatTextBox = new System.Windows.Forms.TextBox();
            this.serverComboBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // loginTextBox
            // 
            this.loginTextBox.Location = new System.Drawing.Point(59, 58);
            this.loginTextBox.MaxLength = 16;
            this.loginTextBox.Name = "loginTextBox";
            this.loginTextBox.Size = new System.Drawing.Size(168, 20);
            this.loginTextBox.TabIndex = 3;
            // 
            // okButton
            // 
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(152, 83);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 4;
            this.okButton.Text = "Join";
            this.okButton.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Login:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Server:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Chat:";
            // 
            // chatTextBox
            // 
            this.chatTextBox.Location = new System.Drawing.Point(59, 32);
            this.chatTextBox.MaxLength = 32;
            this.chatTextBox.Name = "chatTextBox";
            this.chatTextBox.Size = new System.Drawing.Size(168, 20);
            this.chatTextBox.TabIndex = 2;
            // 
            // serverComboBox
            // 
            this.serverComboBox.FormattingEnabled = true;
            this.serverComboBox.Items.AddRange(new object[] {
            "."});
            this.serverComboBox.Location = new System.Drawing.Point(59, 6);
            this.serverComboBox.Name = "serverComboBox";
            this.serverComboBox.Size = new System.Drawing.Size(168, 21);
            this.serverComboBox.TabIndex = 8;
            // 
            // JoinChatDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(246, 118);
            this.Controls.Add(this.serverComboBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.chatTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.loginTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "JoinChatDialog";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.JoinChatDialog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox loginTextBox;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox chatTextBox;
        private System.Windows.Forms.ComboBox serverComboBox;
    }
}