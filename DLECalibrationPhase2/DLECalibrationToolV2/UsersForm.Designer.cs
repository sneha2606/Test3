namespace DLECalibrationToolV2
{
    partial class UsersForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnEdit = new System.Windows.Forms.Button();
            this.dataGridViewUserList = new System.Windows.Forms.DataGridView();
            this.btnAdd = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewUserList)).BeginInit();
            this.SuspendLayout();
            // 
            // btnEdit
            // 
            this.btnEdit.Enabled = false;
            this.btnEdit.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEdit.Location = new System.Drawing.Point(883, 47);
            this.btnEdit.Margin = new System.Windows.Forms.Padding(4);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(85, 28);
            this.btnEdit.TabIndex = 1;
            this.btnEdit.Text = "Edit User";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // dataGridViewUserList
            // 
            this.dataGridViewUserList.AllowUserToResizeColumns = false;
            this.dataGridViewUserList.AllowUserToResizeRows = false;
            this.dataGridViewUserList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.Padding = new System.Windows.Forms.Padding(0, 0, 0, 5);
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewUserList.DefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewUserList.Location = new System.Drawing.Point(41, 108);
            this.dataGridViewUserList.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridViewUserList.MultiSelect = false;
            this.dataGridViewUserList.Name = "dataGridViewUserList";
            this.dataGridViewUserList.ReadOnly = true;
            this.dataGridViewUserList.RowHeadersVisible = false;
            this.dataGridViewUserList.RowHeadersWidth = 50;
            this.dataGridViewUserList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewUserList.Size = new System.Drawing.Size(927, 575);
            this.dataGridViewUserList.TabIndex = 0;
            this.dataGridViewUserList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewUserList_CellClick);
            this.dataGridViewUserList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewUserList_CellContentClick);
            this.dataGridViewUserList.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewUserList_CellContentDoubleClick);
            this.dataGridViewUserList.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridViewUserList_ColumnHeaderMouseClick);
            this.dataGridViewUserList.Sorted += new System.EventHandler(this.dataGridViewUserList_Sorted);
            // 
            // btnAdd
            // 
            this.btnAdd.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.Location = new System.Drawing.Point(749, 47);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(4);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(85, 28);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "Add User";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // UsersForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1010, 730);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.dataGridViewUserList);
            this.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UsersForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Users";
            this.Load += new System.EventHandler(this.ViewUsers_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewUserList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.DataGridView dataGridViewUserList;
        private System.Windows.Forms.Button btnAdd;


    }
}