﻿namespace RES_Timekeeper
{
    partial class WorkorderSelector
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this._btnCancel = new System.Windows.Forms.Button();
            this._btnOK = new System.Windows.Forms.Button();
            this._btnEdit = new System.Windows.Forms.Button();
            this._dgvProjects = new System.Windows.Forms.DataGridView();
            this.colProjectCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colProjectTitle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._rbShowAll = new System.Windows.Forms.RadioButton();
            this._rbShowRecent = new System.Windows.Forms.RadioButton();
            this._tbFilter = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this._dgvProjects)).BeginInit();
            this.SuspendLayout();
            // 
            // _btnCancel
            // 
            this._btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._btnCancel.Location = new System.Drawing.Point(370, 238);
            this._btnCancel.Name = "_btnCancel";
            this._btnCancel.Size = new System.Drawing.Size(75, 23);
            this._btnCancel.TabIndex = 1;
            this._btnCancel.Text = "Cancel";
            this._btnCancel.UseVisualStyleBackColor = true;
            this._btnCancel.Click += new System.EventHandler(this._btnCancel_Click);
            // 
            // _btnOK
            // 
            this._btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._btnOK.Location = new System.Drawing.Point(289, 238);
            this._btnOK.Name = "_btnOK";
            this._btnOK.Size = new System.Drawing.Size(75, 23);
            this._btnOK.TabIndex = 2;
            this._btnOK.Text = "OK";
            this._btnOK.UseVisualStyleBackColor = true;
            this._btnOK.Click += new System.EventHandler(this._btnOK_Click);
            // 
            // _btnEdit
            // 
            this._btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._btnEdit.Location = new System.Drawing.Point(1, 238);
            this._btnEdit.Name = "_btnEdit";
            this._btnEdit.Size = new System.Drawing.Size(75, 23);
            this._btnEdit.TabIndex = 3;
            this._btnEdit.Text = "Edit...";
            this._btnEdit.UseVisualStyleBackColor = true;
            this._btnEdit.Click += new System.EventHandler(this._btnEdit_Click);
            // 
            // _dgvProjects
            // 
            this._dgvProjects.AllowUserToAddRows = false;
            this._dgvProjects.AllowUserToDeleteRows = false;
            this._dgvProjects.AllowUserToResizeColumns = false;
            this._dgvProjects.AllowUserToResizeRows = false;
            this._dgvProjects.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._dgvProjects.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._dgvProjects.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colProjectCode,
            this.colProjectTitle});
            this._dgvProjects.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this._dgvProjects.Location = new System.Drawing.Point(0, 21);
            this._dgvProjects.MultiSelect = false;
            this._dgvProjects.Name = "_dgvProjects";
            this._dgvProjects.ReadOnly = true;
            this._dgvProjects.RowHeadersVisible = false;
            this._dgvProjects.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._dgvProjects.RowTemplate.Height = 17;
            this._dgvProjects.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this._dgvProjects.Size = new System.Drawing.Size(446, 211);
            this._dgvProjects.TabIndex = 4;
            this._dgvProjects.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this._dgvProjects_CellDoubleClick);
            this._dgvProjects.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this._dgvProjects_ColumnHeaderMouseClick);
            this._dgvProjects.ColumnSortModeChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this._dgvProjects_ColumnSortModeChanged);
            this._dgvProjects.SelectionChanged += new System.EventHandler(this._dgvProjects_SelectionChanged);
            // 
            // colProjectCode
            // 
            this.colProjectCode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colProjectCode.DefaultCellStyle = dataGridViewCellStyle1;
            this.colProjectCode.HeaderText = "Code";
            this.colProjectCode.Name = "colProjectCode";
            this.colProjectCode.ReadOnly = true;
            this.colProjectCode.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colProjectCode.Width = 57;
            // 
            // colProjectTitle
            // 
            this.colProjectTitle.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colProjectTitle.DefaultCellStyle = dataGridViewCellStyle2;
            this.colProjectTitle.HeaderText = "Workorder";
            this.colProjectTitle.MinimumWidth = 50;
            this.colProjectTitle.Name = "colProjectTitle";
            this.colProjectTitle.ReadOnly = true;
            this.colProjectTitle.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // _rbShowAll
            // 
            this._rbShowAll.Location = new System.Drawing.Point(2, 0);
            this._rbShowAll.Name = "_rbShowAll";
            this._rbShowAll.Size = new System.Drawing.Size(68, 20);
            this._rbShowAll.TabIndex = 2;
            this._rbShowAll.TabStop = true;
            this._rbShowAll.Text = "Show All";
            this._rbShowAll.UseVisualStyleBackColor = true;
            this._rbShowAll.CheckedChanged += new System.EventHandler(this._rbShowAll_CheckedChanged);
            // 
            // _rbShowRecent
            // 
            this._rbShowRecent.Location = new System.Drawing.Point(83, 0);
            this._rbShowRecent.Name = "_rbShowRecent";
            this._rbShowRecent.Size = new System.Drawing.Size(91, 20);
            this._rbShowRecent.TabIndex = 5;
            this._rbShowRecent.TabStop = true;
            this._rbShowRecent.Text = "Show Recent";
            this._rbShowRecent.UseVisualStyleBackColor = true;
            this._rbShowRecent.CheckedChanged += new System.EventHandler(this._rbShowRecent_CheckedChanged);
            // 
            // _tbFilter
            // 
            this._tbFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._tbFilter.Location = new System.Drawing.Point(318, 0);
            this._tbFilter.Name = "_tbFilter";
            this._tbFilter.Size = new System.Drawing.Size(128, 20);
            this._tbFilter.TabIndex = 6;
            this._tbFilter.TextChanged += new System.EventHandler(this._tbFilter_TextChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Location = new System.Drawing.Point(262, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 20);
            this.label1.TabIndex = 7;
            this.label1.Text = "Filter:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // WorkorderSelector
            // 
            this.AcceptButton = this._btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._btnCancel;
            this.ClientSize = new System.Drawing.Size(446, 262);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._tbFilter);
            this.Controls.Add(this._rbShowRecent);
            this.Controls.Add(this._rbShowAll);
            this.Controls.Add(this._dgvProjects);
            this.Controls.Add(this._btnEdit);
            this.Controls.Add(this._btnOK);
            this.Controls.Add(this._btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "WorkorderSelector";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Workorder Selector";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.ProjectSelector_Load);
            ((System.ComponentModel.ISupportInitialize)(this._dgvProjects)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button _btnCancel;
        private System.Windows.Forms.Button _btnOK;
        private System.Windows.Forms.Button _btnEdit;
        private System.Windows.Forms.DataGridView _dgvProjects;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProjectCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProjectTitle;
        private System.Windows.Forms.RadioButton _rbShowAll;
        private System.Windows.Forms.RadioButton _rbShowRecent;
        private System.Windows.Forms.TextBox _tbFilter;
        private System.Windows.Forms.Label label1;
    }
}