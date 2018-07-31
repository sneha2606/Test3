namespace DLECalibrationToolV2
{
    partial class TestEmailForm
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
            this.comboBox_EmailID = new System.Windows.Forms.ComboBox();
            this.label_EmailID = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // comboBox_EmailID
            // 
            this.comboBox_EmailID.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox_EmailID.FormattingEnabled = true;
            this.comboBox_EmailID.Location = new System.Drawing.Point(136, 84);
            this.comboBox_EmailID.Name = "comboBox_EmailID";
            this.comboBox_EmailID.Size = new System.Drawing.Size(315, 27);
            this.comboBox_EmailID.TabIndex = 0;
            // 
            // label_EmailID
            // 
            this.label_EmailID.AutoSize = true;
            this.label_EmailID.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_EmailID.Location = new System.Drawing.Point(51, 87);
            this.label_EmailID.Name = "label_EmailID";
            this.label_EmailID.Size = new System.Drawing.Size(63, 19);
            this.label_EmailID.TabIndex = 1;
            this.label_EmailID.Text = "Email ID";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(136, 144);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(120, 30);
            this.button1.TabIndex = 2;
            this.button1.Text = "Send Email";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // TestEmailForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(524, 241);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label_EmailID);
            this.Controls.Add(this.comboBox_EmailID);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TestEmailForm";
            this.Text = "TestEmail";
            this.Load += new System.EventHandler(this.TestEmailForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox_EmailID;
        private System.Windows.Forms.Label label_EmailID;
        private System.Windows.Forms.Button button1;
    }
}