﻿namespace Server
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.logsRichTextBox = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // logsRichTextBox
            // 
            this.logsRichTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logsRichTextBox.Location = new System.Drawing.Point(0, 0);
            this.logsRichTextBox.Name = "logsRichTextBox";
            this.logsRichTextBox.ReadOnly = true;
            this.logsRichTextBox.Size = new System.Drawing.Size(800, 450);
            this.logsRichTextBox.TabIndex = 0;
            this.logsRichTextBox.Text = "";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.logsRichTextBox);
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox logsRichTextBox;
    }
}

