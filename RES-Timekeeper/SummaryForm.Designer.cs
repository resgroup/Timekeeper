namespace RES_Timekeeper
{
    partial class SummaryForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SummaryForm));
            this._gbProjects = new System.Windows.Forms.GroupBox();
            this._btnFillAgresso = new System.Windows.Forms.Button();
            this._btnRight = new System.Windows.Forms.Button();
            this._btnLeft = new System.Windows.Forms.Button();
            this._cbRound = new System.Windows.Forms.CheckBox();
            this._dgvProjectWork = new System.Windows.Forms.DataGridView();
            this._colProjectCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._colProjectDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._colMonday = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._colTuesday = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._colWednesday = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._colThursday = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._colFriday = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._colSaturday = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._colSunday = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Total = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._splitContainer = new System.Windows.Forms.SplitContainer();
            this._dgvWeeksItems = new System.Windows.Forms.DataGridView();
            this._colStartTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._colEndTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._colProject = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._colDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._panelWarning = new System.Windows.Forms.Panel();
            this._pbWarning = new System.Windows.Forms.PictureBox();
            this._lblWarning = new System.Windows.Forms.Label();
            this._gbProjects.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._dgvProjectWork)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._splitContainer)).BeginInit();
            this._splitContainer.Panel1.SuspendLayout();
            this._splitContainer.Panel2.SuspendLayout();
            this._splitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._dgvWeeksItems)).BeginInit();
            this._panelWarning.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._pbWarning)).BeginInit();
            this.SuspendLayout();
            // 
            // _gbProjects
            // 
            this._gbProjects.Controls.Add(this._panelWarning);
            this._gbProjects.Controls.Add(this._btnFillAgresso);
            this._gbProjects.Controls.Add(this._btnRight);
            this._gbProjects.Controls.Add(this._btnLeft);
            this._gbProjects.Controls.Add(this._cbRound);
            this._gbProjects.Controls.Add(this._dgvProjectWork);
            this._gbProjects.Dock = System.Windows.Forms.DockStyle.Fill;
            this._gbProjects.Location = new System.Drawing.Point(0, 0);
            this._gbProjects.Margin = new System.Windows.Forms.Padding(6);
            this._gbProjects.Name = "_gbProjects";
            this._gbProjects.Padding = new System.Windows.Forms.Padding(6);
            this._gbProjects.Size = new System.Drawing.Size(707, 213);
            this._gbProjects.TabIndex = 2;
            this._gbProjects.TabStop = false;
            this._gbProjects.Text = "Project Summary";
            // 
            // _btnFillAgresso
            // 
            this._btnFillAgresso.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._btnFillAgresso.Location = new System.Drawing.Point(85, 186);
            this._btnFillAgresso.Name = "_btnFillAgresso";
            this._btnFillAgresso.Size = new System.Drawing.Size(99, 23);
            this._btnFillAgresso.TabIndex = 4;
            this._btnFillAgresso.Text = "Send To Agresso";
            this._btnFillAgresso.UseVisualStyleBackColor = true;
            this._btnFillAgresso.Click += new System.EventHandler(this._btnFillAgresso_Click);
            // 
            // _btnRight
            // 
            this._btnRight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._btnRight.Font = new System.Drawing.Font("Arial Narrow", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._btnRight.Location = new System.Drawing.Point(628, 186);
            this._btnRight.Name = "_btnRight";
            this._btnRight.Size = new System.Drawing.Size(75, 23);
            this._btnRight.TabIndex = 3;
            this._btnRight.Text = "→";
            this._btnRight.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this._btnRight.UseVisualStyleBackColor = true;
            this._btnRight.Click += new System.EventHandler(this._btnRight_Click);
            // 
            // _btnLeft
            // 
            this._btnLeft.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._btnLeft.Location = new System.Drawing.Point(4, 186);
            this._btnLeft.Name = "_btnLeft";
            this._btnLeft.Size = new System.Drawing.Size(75, 23);
            this._btnLeft.TabIndex = 2;
            this._btnLeft.Text = "←";
            this._btnLeft.UseVisualStyleBackColor = true;
            this._btnLeft.Click += new System.EventHandler(this._btnLeft_Click);
            // 
            // _cbRound
            // 
            this._cbRound.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._cbRound.AutoSize = true;
            this._cbRound.Location = new System.Drawing.Point(564, 2);
            this._cbRound.Name = "_cbRound";
            this._cbRound.Size = new System.Drawing.Size(132, 17);
            this._cbRound.TabIndex = 1;
            this._cbRound.Text = "Round times to ¼ hour";
            this._cbRound.UseVisualStyleBackColor = true;
            this._cbRound.CheckedChanged += new System.EventHandler(this._cbRound_CheckedChanged);
            // 
            // _dgvProjectWork
            // 
            this._dgvProjectWork.AllowUserToAddRows = false;
            this._dgvProjectWork.AllowUserToDeleteRows = false;
            this._dgvProjectWork.AllowUserToResizeRows = false;
            this._dgvProjectWork.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._dgvProjectWork.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._dgvProjectWork.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this._colProjectCode,
            this._colProjectDescription,
            this._colMonday,
            this._colTuesday,
            this._colWednesday,
            this._colThursday,
            this._colFriday,
            this._colSaturday,
            this._colSunday,
            this.Total});
            this._dgvProjectWork.Location = new System.Drawing.Point(4, 19);
            this._dgvProjectWork.Name = "_dgvProjectWork";
            this._dgvProjectWork.RowHeadersVisible = false;
            this._dgvProjectWork.Size = new System.Drawing.Size(699, 161);
            this._dgvProjectWork.TabIndex = 0;
            this._dgvProjectWork.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this._dgvProjectWork_CellFormatting);
            this._dgvProjectWork.SortCompare += new System.Windows.Forms.DataGridViewSortCompareEventHandler(this._dgvProjectWork_SortCompare);
            // 
            // _colProjectCode
            // 
            this._colProjectCode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this._colProjectCode.DefaultCellStyle = dataGridViewCellStyle1;
            this._colProjectCode.HeaderText = "Code";
            this._colProjectCode.Name = "_colProjectCode";
            this._colProjectCode.ReadOnly = true;
            this._colProjectCode.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this._colProjectCode.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this._colProjectCode.Width = 38;
            // 
            // _colProjectDescription
            // 
            this._colProjectDescription.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this._colProjectDescription.DefaultCellStyle = dataGridViewCellStyle2;
            this._colProjectDescription.HeaderText = "Description";
            this._colProjectDescription.Name = "_colProjectDescription";
            this._colProjectDescription.ReadOnly = true;
            this._colProjectDescription.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // _colMonday
            // 
            this._colMonday.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this._colMonday.DefaultCellStyle = dataGridViewCellStyle3;
            this._colMonday.HeaderText = "Mon";
            this._colMonday.Name = "_colMonday";
            this._colMonday.ReadOnly = true;
            this._colMonday.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this._colMonday.Width = 34;
            // 
            // _colTuesday
            // 
            this._colTuesday.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this._colTuesday.DefaultCellStyle = dataGridViewCellStyle4;
            this._colTuesday.HeaderText = "Tue";
            this._colTuesday.Name = "_colTuesday";
            this._colTuesday.ReadOnly = true;
            this._colTuesday.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this._colTuesday.Width = 32;
            // 
            // _colWednesday
            // 
            this._colWednesday.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this._colWednesday.DefaultCellStyle = dataGridViewCellStyle5;
            this._colWednesday.HeaderText = "Wed";
            this._colWednesday.Name = "_colWednesday";
            this._colWednesday.ReadOnly = true;
            this._colWednesday.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this._colWednesday.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this._colWednesday.Width = 36;
            // 
            // _colThursday
            // 
            this._colThursday.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this._colThursday.DefaultCellStyle = dataGridViewCellStyle6;
            this._colThursday.HeaderText = "Thur";
            this._colThursday.Name = "_colThursday";
            this._colThursday.ReadOnly = true;
            this._colThursday.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this._colThursday.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this._colThursday.Width = 35;
            // 
            // _colFriday
            // 
            this._colFriday.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this._colFriday.DefaultCellStyle = dataGridViewCellStyle7;
            this._colFriday.HeaderText = "Fri";
            this._colFriday.Name = "_colFriday";
            this._colFriday.ReadOnly = true;
            this._colFriday.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this._colFriday.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this._colFriday.Width = 24;
            // 
            // _colSaturday
            // 
            this._colSaturday.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.Black;
            this._colSaturday.DefaultCellStyle = dataGridViewCellStyle8;
            this._colSaturday.HeaderText = "Sat";
            this._colSaturday.Name = "_colSaturday";
            this._colSaturday.ReadOnly = true;
            this._colSaturday.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this._colSaturday.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this._colSaturday.Width = 29;
            // 
            // _colSunday
            // 
            this._colSunday.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle9.ForeColor = System.Drawing.Color.Black;
            this._colSunday.DefaultCellStyle = dataGridViewCellStyle9;
            this._colSunday.HeaderText = "Sun";
            this._colSunday.Name = "_colSunday";
            this._colSunday.ReadOnly = true;
            this._colSunday.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this._colSunday.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this._colSunday.Width = 32;
            // 
            // Total
            // 
            this.Total.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.Color.Black;
            this.Total.DefaultCellStyle = dataGridViewCellStyle10;
            this.Total.HeaderText = "Total";
            this.Total.Name = "Total";
            this.Total.ReadOnly = true;
            this.Total.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Total.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Total.Width = 37;
            // 
            // _splitContainer
            // 
            this._splitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._splitContainer.Location = new System.Drawing.Point(2, 2);
            this._splitContainer.Name = "_splitContainer";
            this._splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // _splitContainer.Panel1
            // 
            this._splitContainer.Panel1.Controls.Add(this._gbProjects);
            // 
            // _splitContainer.Panel2
            // 
            this._splitContainer.Panel2.Controls.Add(this._dgvWeeksItems);
            this._splitContainer.Size = new System.Drawing.Size(707, 426);
            this._splitContainer.SplitterDistance = 213;
            this._splitContainer.TabIndex = 3;
            // 
            // _dgvWeeksItems
            // 
            this._dgvWeeksItems.AllowUserToAddRows = false;
            this._dgvWeeksItems.AllowUserToDeleteRows = false;
            this._dgvWeeksItems.AllowUserToResizeColumns = false;
            this._dgvWeeksItems.AllowUserToResizeRows = false;
            this._dgvWeeksItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._dgvWeeksItems.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this._colStartTime,
            this._colEndTime,
            this._colProject,
            this._colDescription});
            this._dgvWeeksItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this._dgvWeeksItems.Location = new System.Drawing.Point(0, 0);
            this._dgvWeeksItems.Name = "_dgvWeeksItems";
            this._dgvWeeksItems.ReadOnly = true;
            this._dgvWeeksItems.RowHeadersVisible = false;
            this._dgvWeeksItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this._dgvWeeksItems.Size = new System.Drawing.Size(707, 209);
            this._dgvWeeksItems.TabIndex = 0;
            // 
            // _colStartTime
            // 
            this._colStartTime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this._colStartTime.HeaderText = "Start";
            this._colStartTime.Name = "_colStartTime";
            this._colStartTime.ReadOnly = true;
            this._colStartTime.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this._colStartTime.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this._colStartTime.Width = 35;
            // 
            // _colEndTime
            // 
            this._colEndTime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this._colEndTime.HeaderText = "End";
            this._colEndTime.Name = "_colEndTime";
            this._colEndTime.ReadOnly = true;
            this._colEndTime.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this._colEndTime.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this._colEndTime.Width = 32;
            // 
            // _colProject
            // 
            this._colProject.HeaderText = "Project";
            this._colProject.Name = "_colProject";
            this._colProject.ReadOnly = true;
            // 
            // _colDescription
            // 
            this._colDescription.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this._colDescription.HeaderText = "Description";
            this._colDescription.Name = "_colDescription";
            this._colDescription.ReadOnly = true;
            this._colDescription.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this._colDescription.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // _panelWarning
            // 
            this._panelWarning.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._panelWarning.Controls.Add(this._lblWarning);
            this._panelWarning.Controls.Add(this._pbWarning);
            this._panelWarning.Location = new System.Drawing.Point(190, 181);
            this._panelWarning.Name = "_panelWarning";
            this._panelWarning.Size = new System.Drawing.Size(432, 30);
            this._panelWarning.TabIndex = 5;
            // 
            // _pbWarning
            // 
            this._pbWarning.Image = ((System.Drawing.Image)(resources.GetObject("_pbWarning.Image")));
            this._pbWarning.InitialImage = null;
            this._pbWarning.Location = new System.Drawing.Point(0, 0);
            this._pbWarning.Name = "_pbWarning";
            this._pbWarning.Size = new System.Drawing.Size(30, 30);
            this._pbWarning.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this._pbWarning.TabIndex = 0;
            this._pbWarning.TabStop = false;
            // 
            // _lblWarning
            // 
            this._lblWarning.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._lblWarning.Location = new System.Drawing.Point(36, 0);
            this._lblWarning.Name = "_lblWarning";
            this._lblWarning.Size = new System.Drawing.Size(393, 30);
            this._lblWarning.TabIndex = 1;
            this._lblWarning.Text = "There are overlapping times totalling XXX minutes in this week.\r\nPlease take this" +
    " into account when entering times into Agresso.";
            this._lblWarning.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // SummaryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(711, 430);
            this.Controls.Add(this._splitContainer);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SummaryForm";
            this.Text = "Summary";
            this._gbProjects.ResumeLayout(false);
            this._gbProjects.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._dgvProjectWork)).EndInit();
            this._splitContainer.Panel1.ResumeLayout(false);
            this._splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._splitContainer)).EndInit();
            this._splitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._dgvWeeksItems)).EndInit();
            this._panelWarning.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._pbWarning)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox _gbProjects;
        private System.Windows.Forms.Button _btnRight;
        private System.Windows.Forms.Button _btnLeft;
        private System.Windows.Forms.CheckBox _cbRound;
        private System.Windows.Forms.DataGridView _dgvProjectWork;
        private System.Windows.Forms.SplitContainer _splitContainer;
        private System.Windows.Forms.DataGridViewTextBoxColumn _colProjectCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn _colProjectDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn _colMonday;
        private System.Windows.Forms.DataGridViewTextBoxColumn _colTuesday;
        private System.Windows.Forms.DataGridViewTextBoxColumn _colWednesday;
        private System.Windows.Forms.DataGridViewTextBoxColumn _colThursday;
        private System.Windows.Forms.DataGridViewTextBoxColumn _colFriday;
        private System.Windows.Forms.DataGridViewTextBoxColumn _colSaturday;
        private System.Windows.Forms.DataGridViewTextBoxColumn _colSunday;
        private System.Windows.Forms.DataGridViewTextBoxColumn Total;
        private System.Windows.Forms.DataGridView _dgvWeeksItems;
        private System.Windows.Forms.DataGridViewTextBoxColumn _colStartTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn _colEndTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn _colProject;
        private System.Windows.Forms.DataGridViewTextBoxColumn _colDescription;
        private System.Windows.Forms.Button _btnFillAgresso;
        private System.Windows.Forms.Panel _panelWarning;
        private System.Windows.Forms.Label _lblWarning;
        private System.Windows.Forms.PictureBox _pbWarning;
    }
}