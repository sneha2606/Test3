namespace DLECalibrationToolV2
{
    partial class HistoryForm
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.comboBoxFileNames = new System.Windows.Forms.ComboBox();
            this.btnOpen = new System.Windows.Forms.Button();
            this.labelFileName = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnGetFiles = new System.Windows.Forms.Button();
            this.labelDateTo = new System.Windows.Forms.Label();
            this.labelDateFrom = new System.Windows.Forms.Label();
            this.dateTimePickerDateTo = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerDateFrom = new System.Windows.Forms.DateTimePicker();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBoxLogFileContents = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 21.3693F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 78.63071F));
            this.tableLayoutPanel1.Controls.Add(this.comboBoxFileNames, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnOpen, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.labelFileName, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(24, 135);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 53.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 46.66667F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(482, 104);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // comboBoxFileNames
            // 
            this.comboBoxFileNames.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFileNames.Enabled = false;
            this.comboBoxFileNames.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxFileNames.FormattingEnabled = true;
            this.comboBoxFileNames.Location = new System.Drawing.Point(107, 4);
            this.comboBoxFileNames.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxFileNames.Name = "comboBoxFileNames";
            this.comboBoxFileNames.Size = new System.Drawing.Size(371, 26);
            this.comboBoxFileNames.TabIndex = 0;
            // 
            // btnOpen
            // 
            this.btnOpen.Enabled = false;
            this.btnOpen.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOpen.Location = new System.Drawing.Point(107, 59);
            this.btnOpen.Margin = new System.Windows.Forms.Padding(4);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(85, 28);
            this.btnOpen.TabIndex = 1;
            this.btnOpen.Text = "Open";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // labelFileName
            // 
            this.labelFileName.AutoSize = true;
            this.labelFileName.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelFileName.Location = new System.Drawing.Point(4, 0);
            this.labelFileName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelFileName.Name = "labelFileName";
            this.labelFileName.Size = new System.Drawing.Size(71, 18);
            this.labelFileName.TabIndex = 2;
            this.labelFileName.Text = "File Name";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnGetFiles);
            this.groupBox1.Controls.Add(this.labelDateTo);
            this.groupBox1.Controls.Add(this.labelDateFrom);
            this.groupBox1.Controls.Add(this.dateTimePickerDateTo);
            this.groupBox1.Controls.Add(this.dateTimePickerDateFrom);
            this.groupBox1.Controls.Add(this.tableLayoutPanel1);
            this.groupBox1.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(32, 33);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(783, 257);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Logs File Selection";
            // 
            // btnGetFiles
            // 
            this.btnGetFiles.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGetFiles.Location = new System.Drawing.Point(131, 91);
            this.btnGetFiles.Name = "btnGetFiles";
            this.btnGetFiles.Size = new System.Drawing.Size(85, 28);
            this.btnGetFiles.TabIndex = 5;
            this.btnGetFiles.Text = "Get Files";
            this.btnGetFiles.UseVisualStyleBackColor = true;
            this.btnGetFiles.Click += new System.EventHandler(this.btnGetFiles_Click);
            // 
            // labelDateTo
            // 
            this.labelDateTo.AutoSize = true;
            this.labelDateTo.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDateTo.Location = new System.Drawing.Point(420, 40);
            this.labelDateTo.Name = "labelDateTo";
            this.labelDateTo.Size = new System.Drawing.Size(54, 18);
            this.labelDateTo.TabIndex = 4;
            this.labelDateTo.Text = "Date To";
            // 
            // labelDateFrom
            // 
            this.labelDateFrom.AutoSize = true;
            this.labelDateFrom.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDateFrom.Location = new System.Drawing.Point(27, 40);
            this.labelDateFrom.Name = "labelDateFrom";
            this.labelDateFrom.Size = new System.Drawing.Size(72, 18);
            this.labelDateFrom.TabIndex = 3;
            this.labelDateFrom.Text = "Date From";
            // 
            // dateTimePickerDateTo
            // 
            this.dateTimePickerDateTo.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePickerDateTo.Location = new System.Drawing.Point(501, 40);
            this.dateTimePickerDateTo.Name = "dateTimePickerDateTo";
            this.dateTimePickerDateTo.Size = new System.Drawing.Size(219, 26);
            this.dateTimePickerDateTo.TabIndex = 2;
            this.dateTimePickerDateTo.TabStop = false;
            // 
            // dateTimePickerDateFrom
            // 
            this.dateTimePickerDateFrom.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePickerDateFrom.Location = new System.Drawing.Point(131, 40);
            this.dateTimePickerDateFrom.Name = "dateTimePickerDateFrom";
            this.dateTimePickerDateFrom.Size = new System.Drawing.Size(220, 26);
            this.dateTimePickerDateFrom.TabIndex = 1;
            this.dateTimePickerDateFrom.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textBoxLogFileContents);
            this.groupBox2.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(32, 314);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(786, 472);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Logs File Contents";
            // 
            // textBoxLogFileContents
            // 
            this.textBoxLogFileContents.BackColor = System.Drawing.Color.White;
            this.textBoxLogFileContents.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxLogFileContents.Location = new System.Drawing.Point(27, 50);
            this.textBoxLogFileContents.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxLogFileContents.Multiline = true;
            this.textBoxLogFileContents.Name = "textBoxLogFileContents";
            this.textBoxLogFileContents.ReadOnly = true;
            this.textBoxLogFileContents.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxLogFileContents.Size = new System.Drawing.Size(737, 386);
            this.textBoxLogFileContents.TabIndex = 0;
            // 
            // HistoryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(847, 790);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "HistoryForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "History";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ComboBox comboBoxFileNames;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Label labelFileName;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox textBoxLogFileContents;
        private System.Windows.Forms.Label labelDateTo;
        private System.Windows.Forms.Label labelDateFrom;
        private System.Windows.Forms.DateTimePicker dateTimePickerDateTo;
        private System.Windows.Forms.DateTimePicker dateTimePickerDateFrom;
        private System.Windows.Forms.Button btnGetFiles;
    }
}