namespace DLECalibrationToolV2
{
    partial class EmailRecipientsForm
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
            this.dataGridViewEmailRecipients = new System.Windows.Forms.DataGridView();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEmailRecipients)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewEmailRecipients
            // 
            this.dataGridViewEmailRecipients.AllowUserToResizeColumns = false;
            this.dataGridViewEmailRecipients.AllowUserToResizeRows = false;
            this.dataGridViewEmailRecipients.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewEmailRecipients.Location = new System.Drawing.Point(39, 80);
            this.dataGridViewEmailRecipients.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridViewEmailRecipients.MultiSelect = false;
            this.dataGridViewEmailRecipients.Name = "dataGridViewEmailRecipients";
            this.dataGridViewEmailRecipients.ReadOnly = true;
            this.dataGridViewEmailRecipients.RowHeadersVisible = false;
            this.dataGridViewEmailRecipients.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewEmailRecipients.Size = new System.Drawing.Size(601, 349);
            this.dataGridViewEmailRecipients.TabIndex = 0;
            this.dataGridViewEmailRecipients.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewEmailRecipients_CellClick);
            this.dataGridViewEmailRecipients.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewEmailRecipients_CellContentClick);
            this.dataGridViewEmailRecipients.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewEmailRecipients_CellContentDoubleClick);
            this.dataGridViewEmailRecipients.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridViewEmailRecipients_ColumnHeaderMouseClick);
            this.dataGridViewEmailRecipients.Sorted += new System.EventHandler(this.dataGridViewEmailRecipients_Sorted);
            // 
            // btnAdd
            // 
            this.btnAdd.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.Location = new System.Drawing.Point(354, 32);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(4);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(108, 29);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.Text = "Add Recipient";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Enabled = false;
            this.btnDelete.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.Location = new System.Drawing.Point(517, 32);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(4);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(123, 29);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "Delete Recipient";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // EmailRecipientsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(672, 461);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.dataGridViewEmailRecipients);
            this.Controls.Add(this.btnAdd);
            this.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EmailRecipientsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Email Recipients";
            this.Load += new System.EventHandler(this.ViewEmailRecipients_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEmailRecipients)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewEmailRecipients;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnDelete;
    }
}