using System;
using System.Drawing;
using System.Windows.Forms;

namespace BlackScreenPopup
{
    partial class Overlay
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Button minimizeButton;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.Button transparencyButton;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.closeButton = new System.Windows.Forms.Button();
            this.minimizeButton = new System.Windows.Forms.Button();
            this.transparencyButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // closeButton
            // 
            this.closeButton.FlatAppearance.BorderSize = 0;
            this.closeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.closeButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 4F);
            this.closeButton.ForeColor = System.Drawing.Color.White;
            this.closeButton.Location = new System.Drawing.Point(286, 0);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(14, 14);
            this.closeButton.TabIndex = 0;
            this.closeButton.Text = "x";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            this.closeButton.MouseEnter += new System.EventHandler(this.closeButton_MouseEnter);
            this.closeButton.MouseLeave += new System.EventHandler(this.closeButton_MouseLeave);
            // 
            // minimizeButton
            // 
            this.minimizeButton.FlatAppearance.BorderSize = 0;
            this.minimizeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.minimizeButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 4F);
            this.minimizeButton.ForeColor = System.Drawing.Color.White;
            this.minimizeButton.Location = new System.Drawing.Point(272, 0);
            this.minimizeButton.Name = "minimizeButton";
            this.minimizeButton.Size = new System.Drawing.Size(14, 14);
            this.minimizeButton.TabIndex = 1;
            this.minimizeButton.Text = "-";
            this.minimizeButton.UseVisualStyleBackColor = true;
            this.minimizeButton.Click += new System.EventHandler(this.minimizeButton_Click);
            this.minimizeButton.MouseEnter += new System.EventHandler(this.minimizeButton_MouseEnter);
            this.minimizeButton.MouseLeave += new System.EventHandler(this.minimizeButton_MouseLeave);
            // 
            // transparencyButton
            // 
            this.transparencyButton.FlatAppearance.BorderSize = 0;
            this.transparencyButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.transparencyButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F);
            this.transparencyButton.ForeColor = System.Drawing.Color.White;
            this.transparencyButton.Location = new System.Drawing.Point(256, 0);
            this.transparencyButton.Name = "transparencyButton";
            this.transparencyButton.Size = new System.Drawing.Size(14, 14);
            this.transparencyButton.TabIndex = 2;
            this.transparencyButton.Text = "~";
            this.transparencyButton.UseVisualStyleBackColor = true;
            this.transparencyButton.Click += new System.EventHandler(this.transparencyButton_Click);
            this.transparencyButton.MouseEnter += new System.EventHandler(this.transparencyButton_MouseEnter);
            this.transparencyButton.MouseLeave += new System.EventHandler(this.transparencyButton_MouseLeave);
            // 
            // Overlay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(300, 200);
            this.Controls.Add(this.transparencyButton);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.minimizeButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Overlay";
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void closeButton_MouseEnter(object sender, EventArgs e)
        {
            closeButton.BackColor = ColorTranslator.FromHtml("#FF0000");
            closeButton.ForeColor = ColorTranslator.FromHtml("#000000");
        }

        private void closeButton_MouseLeave(object sender, EventArgs e)
        {
            closeButton.BackColor = ColorTranslator.FromHtml("#000000");
            closeButton.ForeColor = ColorTranslator.FromHtml("#FFFFFF");
        }

        private void minimizeButton_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void minimizeButton_MouseEnter(object sender, EventArgs e)
        {
            minimizeButton.BackColor = ColorTranslator.FromHtml("#B9FF42");
            minimizeButton.ForeColor = ColorTranslator.FromHtml("#000000");
        }

        private void minimizeButton_MouseLeave(object sender, EventArgs e)
        {
            minimizeButton.BackColor = ColorTranslator.FromHtml("#000000");
            minimizeButton.ForeColor = ColorTranslator.FromHtml("#FFFFFF");
        }


        private void transparencyButton_MouseEnter(object sender, EventArgs e)
        {
            transparencyButton.BackColor = ColorTranslator.FromHtml("#B4ABE2");
            transparencyButton.ForeColor = ColorTranslator.FromHtml("#000000");
        }

        private void transparencyButton_MouseLeave(object sender, EventArgs e)
        {
            transparencyButton.BackColor = ColorTranslator.FromHtml("#000000");
            transparencyButton.ForeColor = ColorTranslator.FromHtml("#FFFFFF");
        }
    }
}
