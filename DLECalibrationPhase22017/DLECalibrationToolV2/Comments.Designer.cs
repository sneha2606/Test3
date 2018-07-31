namespace DLECalibrationToolV2
{
    partial class Comments
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
            this.btnSubmit = new System.Windows.Forms.Button();
            this.textBox_comments = new System.Windows.Forms.TextBox();
            this.labelTextBelowComments = new System.Windows.Forms.Label();
            this.label_msg = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnSubmit
            // 
            this.btnSubmit.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSubmit.Location = new System.Drawing.Point(263, 321);
            this.btnSubmit.Margin = new System.Windows.Forms.Padding(4);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(85, 28);
            this.btnSubmit.TabIndex = 2;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // textBox_comments
            // 
            this.textBox_comments.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_comments.Location = new System.Drawing.Point(42, 59);
            this.textBox_comments.Margin = new System.Windows.Forms.Padding(4);
            this.textBox_comments.MaxLength = 100;
            this.textBox_comments.Multiline = true;
            this.textBox_comments.Name = "textBox_comments";
            this.textBox_comments.Size = new System.Drawing.Size(417, 192);
            this.textBox_comments.TabIndex = 1;
            // 
            // labelTextBelowComments
            // 
            this.labelTextBelowComments.AutoSize = true;
            this.labelTextBelowComments.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTextBelowComments.Location = new System.Drawing.Point(42, 272);
            this.labelTextBelowComments.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelTextBelowComments.Name = "labelTextBelowComments";
            this.labelTextBelowComments.Size = new System.Drawing.Size(161, 17);
            this.labelTextBelowComments.TabIndex = 4;
            this.labelTextBelowComments.Text = "   Maximum 100 characters";
            // 
            // label_msg
            // 
            this.label_msg.AutoSize = true;
            this.label_msg.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_msg.Location = new System.Drawing.Point(42, 39);
            this.label_msg.Name = "label_msg";
            this.label_msg.Size = new System.Drawing.Size(0, 18);
            this.label_msg.TabIndex = 5;
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(384, 321);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 28);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(42, 255);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(152, 17);
            this.label1.TabIndex = 7;
            this.label1.Text = "* Minimum 10 characters";
            // 
            // Comments
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(503, 394);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.label_msg);
            this.Controls.Add(this.labelTextBelowComments);
            this.Controls.Add(this.textBox_comments);
            this.Controls.Add(this.btnSubmit);
            this.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Comments";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Comments";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AnnotationBox_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.TextBox textBox_comments;
        private System.Windows.Forms.Label labelTextBelowComments;
        private System.Windows.Forms.Label label_msg;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label1;

    }
}