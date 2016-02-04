namespace RES_Timekeeper
{
    partial class TimekeeperForm
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this._dgvItems = new System.Windows.Forms.DataGridView();
            this.projectCodeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colProjButton = new System.Windows.Forms.DataGridViewButtonColumn();
            this.startTimeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.endTimeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.confirmedDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.notesDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._itemListBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this._dgvItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._itemListBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // _dgvItems
            // 
            this._dgvItems.AllowUserToAddRows = false;
            this._dgvItems.AllowUserToDeleteRows = false;
            this._dgvItems.AllowUserToResizeColumns = false;
            this._dgvItems.AllowUserToResizeRows = false;
            this._dgvItems.AutoGenerateColumns = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this._dgvItems.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this._dgvItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._dgvItems.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.projectCodeDataGridViewTextBoxColumn,
            this.colProjButton,
            this.startTimeDataGridViewTextBoxColumn,
            this.endTimeDataGridViewTextBoxColumn,
            this.confirmedDataGridViewCheckBoxColumn,
            this.notesDataGridViewTextBoxColumn});
            this._dgvItems.DataMember = "Items";
            this._dgvItems.DataSource = this._itemListBindingSource;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this._dgvItems.DefaultCellStyle = dataGridViewCellStyle4;
            this._dgvItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this._dgvItems.Location = new System.Drawing.Point(0, 0);
            this._dgvItems.Name = "_dgvItems";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this._dgvItems.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this._dgvItems.RowHeadersVisible = false;
            this._dgvItems.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this._dgvItems.Size = new System.Drawing.Size(625, 80);
            this._dgvItems.TabIndex = 0;
            this._dgvItems.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this._dgvItems_CellContentClick);
            this._dgvItems.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this._dgvItems_CellContentDoubleClick);
            this._dgvItems.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this._dgvItems_CellFormatting);
            this._dgvItems.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this._dgvItems_RowsAdded);
            // 
            // projectCodeDataGridViewTextBoxColumn
            // 
            this.projectCodeDataGridViewTextBoxColumn.HeaderText = "Code";
            this.projectCodeDataGridViewTextBoxColumn.MinimumWidth = 120;
            this.projectCodeDataGridViewTextBoxColumn.Name = "projectCodeDataGridViewTextBoxColumn";
            this.projectCodeDataGridViewTextBoxColumn.ReadOnly = true;
            this.projectCodeDataGridViewTextBoxColumn.Width = 120;
            // 
            // colProjButton
            // 
            this.colProjButton.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colProjButton.HeaderText = "";
            this.colProjButton.MinimumWidth = 20;
            this.colProjButton.Name = "colProjButton";
            this.colProjButton.Width = 20;
            // 
            // startTimeDataGridViewTextBoxColumn
            // 
            this.startTimeDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.startTimeDataGridViewTextBoxColumn.DataPropertyName = "StartTime";
            dataGridViewCellStyle2.Format = "HH:mm";
            this.startTimeDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.startTimeDataGridViewTextBoxColumn.HeaderText = "Start";
            this.startTimeDataGridViewTextBoxColumn.Name = "startTimeDataGridViewTextBoxColumn";
            this.startTimeDataGridViewTextBoxColumn.ReadOnly = true;
            this.startTimeDataGridViewTextBoxColumn.Width = 50;
            // 
            // endTimeDataGridViewTextBoxColumn
            // 
            this.endTimeDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.endTimeDataGridViewTextBoxColumn.DataPropertyName = "EndTime";
            dataGridViewCellStyle3.Format = "HH:mm";
            this.endTimeDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle3;
            this.endTimeDataGridViewTextBoxColumn.HeaderText = "End";
            this.endTimeDataGridViewTextBoxColumn.Name = "endTimeDataGridViewTextBoxColumn";
            this.endTimeDataGridViewTextBoxColumn.ReadOnly = true;
            this.endTimeDataGridViewTextBoxColumn.Width = 46;
            // 
            // confirmedDataGridViewCheckBoxColumn
            // 
            this.confirmedDataGridViewCheckBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.confirmedDataGridViewCheckBoxColumn.DataPropertyName = "Confirmed";
            this.confirmedDataGridViewCheckBoxColumn.HeaderText = "Confirm";
            this.confirmedDataGridViewCheckBoxColumn.Name = "confirmedDataGridViewCheckBoxColumn";
            this.confirmedDataGridViewCheckBoxColumn.Width = 44;
            // 
            // notesDataGridViewTextBoxColumn
            // 
            this.notesDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.notesDataGridViewTextBoxColumn.DataPropertyName = "Notes";
            this.notesDataGridViewTextBoxColumn.HeaderText = "Notes";
            this.notesDataGridViewTextBoxColumn.Name = "notesDataGridViewTextBoxColumn";
            // 
            // _itemListBindingSource
            // 
            this._itemListBindingSource.DataSource = typeof(RES_Timekeeper.Data.ItemList);
            // 
            // TimekeeperForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.ClientSize = new System.Drawing.Size(625, 80);
            this.Controls.Add(this._dgvItems);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(200, 20);
            this.Name = "TimekeeperForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "RES Timekeeper";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this._dgvItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._itemListBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView _dgvItems;
        private System.Windows.Forms.BindingSource _itemListBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn projectCodeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewButtonColumn colProjButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn startTimeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn endTimeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn confirmedDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn notesDataGridViewTextBoxColumn;
    }
}