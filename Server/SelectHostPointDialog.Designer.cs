namespace Server
{
    partial class SelectHostPointDialog
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
            this.hostComboBox = new System.Windows.Forms.ComboBox();
            this.portNumericUpDown = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.portNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // hostComboBox
            // 
            this.hostComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.hostComboBox.FormattingEnabled = true;
            this.hostComboBox.Location = new System.Drawing.Point(41, 12);
            this.hostComboBox.Name = "hostComboBox";
            this.hostComboBox.Size = new System.Drawing.Size(121, 21);
            this.hostComboBox.TabIndex = 0;
            // 
            // portNumericUpDown
            // 
            this.portNumericUpDown.Location = new System.Drawing.Point(208, 12);
            this.portNumericUpDown.Maximum = new decimal(new int[] {
            49151,
            0,
            0,
            0});
            this.portNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.portNumericUpDown.Name = "portNumericUpDown";
            this.portNumericUpDown.ReadOnly = true;
            this.portNumericUpDown.Size = new System.Drawing.Size(120, 20);
            this.portNumericUpDown.TabIndex = 1;
            this.portNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // SelectHostPointDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.portNumericUpDown);
            this.Controls.Add(this.hostComboBox);
            this.Name = "SelectHostPointDialog";
            this.Text = "SelectHostPointDialog";
            this.Load += new System.EventHandler(this.SelectHostPointDialog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.portNumericUpDown)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox hostComboBox;
        private System.Windows.Forms.NumericUpDown portNumericUpDown;
    }
}