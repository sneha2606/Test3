namespace DLECalibrationToolV2
{
    partial class AnnotatePITag
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
            this.textBox_Annotate = new System.Windows.Forms.TextBox();
            this.label_tagcomments = new System.Windows.Forms.Label();
            this.button_Submit = new System.Windows.Forms.Button();
            this.label_min10 = new System.Windows.Forms.Label();
            this.label_max100 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBox_Annotate
            // 
            this.textBox_Annotate.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_Annotate.Location = new System.Drawing.Point(35, 83);
            this.textBox_Annotate.MaxLength = 100;
            this.textBox_Annotate.Multiline = true;
            this.textBox_Annotate.Name = "textBox_Annotate";
            this.textBox_Annotate.Size = new System.Drawing.Size(510, 140);
            this.textBox_Annotate.TabIndex = 0;
            // 
            // label_tagcomments
            // 
            this.label_tagcomments.AutoSize = true;
            this.label_tagcomments.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_tagcomments.Location = new System.Drawing.Point(32, 53);
            this.label_tagcomments.Name = "label_tagcomments";
            this.label_tagcomments.Size = new System.Drawing.Size(0, 17);
            this.label_tagcomments.TabIndex = 1;
            // 
            // button_Submit
            // 
            this.button_Submit.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_Submit.Location = new System.Drawing.Point(470, 273);
            this.button_Submit.Name = "button_Submit";
            this.button_Submit.Size = new System.Drawing.Size(75, 27);
            this.button_Submit.TabIndex = 2;
            this.button_Submit.Text = "Submit";
            this.button_Submit.UseVisualStyleBackColor = true;
            this.button_Submit.Click += new System.EventHandler(this.button_Submit_Click);
            // 
            // label_min10
            // 
            this.label_min10.AutoSize = true;
            this.label_min10.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_min10.Location = new System.Drawing.Point(32, 226);
            this.label_min10.Name = "label_min10";
            this.label_min10.Size = new System.Drawing.Size(152, 17);
            this.label_min10.TabIndex = 3;
            this.label_min10.Text = "* Minimum 10 characters";
            // 
            // label_max100
            // 
            this.label_max100.AutoSize = true;
            this.label_max100.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_max100.Location = new System.Drawing.Point(32, 243);
            this.label_max100.Name = "label_max100";
            this.label_max100.Size = new System.Drawing.Size(161, 17);
            this.label_max100.TabIndex = 4;
            this.label_max100.Text = "   Maximum 100 characters";
            // 
            // AnnotatePITag
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(586, 329);
            this.Controls.Add(this.label_max100);
            this.Controls.Add(this.label_min10);
            this.Controls.Add(this.button_Submit);
            this.Controls.Add(this.label_tagcomments);
            this.Controls.Add(this.textBox_Annotate);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AnnotatePITag";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Annotate";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_Annotate;
        private System.Windows.Forms.Label label_tagcomments;
        private System.Windows.Forms.Button button_Submit;
        private System.Windows.Forms.Label label_min10;
        private System.Windows.Forms.Label label_max100;
    }
}