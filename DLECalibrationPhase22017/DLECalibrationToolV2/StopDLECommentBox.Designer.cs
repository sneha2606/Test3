namespace DLECalibrationToolV2
{
    partial class StopDLECommentBox
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
            this.labelMax100Char = new System.Windows.Forms.Label();
            this.textBoxReason = new System.Windows.Forms.TextBox();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.labelMin10Char = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelMax100Char
            // 
            this.labelMax100Char.AutoSize = true;
            this.labelMax100Char.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMax100Char.Location = new System.Drawing.Point(34, 245);
            this.labelMax100Char.Name = "labelMax100Char";
            this.labelMax100Char.Size = new System.Drawing.Size(161, 17);
            this.labelMax100Char.TabIndex = 8;
            this.labelMax100Char.Text = "   Maximum 100 characters";
            // 
            // textBoxReason
            // 
            this.textBoxReason.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxReason.Location = new System.Drawing.Point(37, 43);
            this.textBoxReason.MaxLength = 100;
            this.textBoxReason.Multiline = true;
            this.textBoxReason.Name = "textBoxReason";
            this.textBoxReason.Size = new System.Drawing.Size(405, 182);
            this.textBoxReason.TabIndex = 5;
            // 
            // btnSubmit
            // 
            this.btnSubmit.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSubmit.Location = new System.Drawing.Point(357, 281);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(85, 28);
            this.btnSubmit.TabIndex = 6;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // labelMin10Char
            // 
            this.labelMin10Char.AutoSize = true;
            this.labelMin10Char.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMin10Char.Location = new System.Drawing.Point(34, 228);
            this.labelMin10Char.Name = "labelMin10Char";
            this.labelMin10Char.Size = new System.Drawing.Size(152, 17);
            this.labelMin10Char.TabIndex = 9;
            this.labelMin10Char.Text = "* Minimum 10 characters";
            // 
            // StopDLECommentBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(485, 338);
            this.Controls.Add(this.labelMin10Char);
            this.Controls.Add(this.labelMax100Char);
            this.Controls.Add(this.textBoxReason);
            this.Controls.Add(this.btnSubmit);
            this.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "StopDLECommentBox";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Reason";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.StopDLECommentBox_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelMax100Char;
        private System.Windows.Forms.TextBox textBoxReason;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Label labelMin10Char;

    }
}