namespace RES_Timekeeper
{
    partial class TimeItemControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this._lblTime = new System.Windows.Forms.Label();
            this._tbDescription = new System.Windows.Forms.TextBox();
            this._btnProjectSelect = new System.Windows.Forms.Button();
            this._lblProject = new System.Windows.Forms.Label();
            this._cbConfirmed = new System.Windows.Forms.CheckBox();
            this._splitContainer = new System.Windows.Forms.SplitContainer();
            ((System.ComponentModel.ISupportInitialize)(this._splitContainer)).BeginInit();
            this._splitContainer.Panel1.SuspendLayout();
            this._splitContainer.Panel2.SuspendLayout();
            this._splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // _lblTime
            // 
            this._lblTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lblTime.Location = new System.Drawing.Point(0, 2);
            this._lblTime.Margin = new System.Windows.Forms.Padding(0);
            this._lblTime.Name = "_lblTime";
            this._lblTime.Size = new System.Drawing.Size(60, 17);
            this._lblTime.TabIndex = 0;
            this._lblTime.Text = "13:28 - 13:29";
            this._lblTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _tbDescription
            // 
            this._tbDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._tbDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._tbDescription.Location = new System.Drawing.Point(3, 2);
            this._tbDescription.Margin = new System.Windows.Forms.Padding(0);
            this._tbDescription.Name = "_tbDescription";
            this._tbDescription.Size = new System.Drawing.Size(295, 17);
            this._tbDescription.TabIndex = 2;
            // 
            // _btnProjectSelect
            // 
            this._btnProjectSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._btnProjectSelect.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._btnProjectSelect.Location = new System.Drawing.Point(262, 3);
            this._btnProjectSelect.Margin = new System.Windows.Forms.Padding(0);
            this._btnProjectSelect.Name = "_btnProjectSelect";
            this._btnProjectSelect.Size = new System.Drawing.Size(19, 17);
            this._btnProjectSelect.TabIndex = 3;
            this._btnProjectSelect.Text = "...";
            this._btnProjectSelect.UseVisualStyleBackColor = true;
            this._btnProjectSelect.Click += new System.EventHandler(this._btnProjectSelect_Click);
            // 
            // _lblProject
            // 
            this._lblProject.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._lblProject.AutoEllipsis = true;
            this._lblProject.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lblProject.Location = new System.Drawing.Point(60, 2);
            this._lblProject.Margin = new System.Windows.Forms.Padding(0);
            this._lblProject.Name = "_lblProject";
            this._lblProject.Size = new System.Drawing.Size(202, 17);
            this._lblProject.TabIndex = 4;
            this._lblProject.Text = "guff guff guff";
            this._lblProject.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _cbConfirmed
            // 
            this._cbConfirmed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._cbConfirmed.AutoSize = true;
            this._cbConfirmed.Location = new System.Drawing.Point(299, 5);
            this._cbConfirmed.Margin = new System.Windows.Forms.Padding(0);
            this._cbConfirmed.Name = "_cbConfirmed";
            this._cbConfirmed.Size = new System.Drawing.Size(15, 14);
            this._cbConfirmed.TabIndex = 5;
            this._cbConfirmed.UseVisualStyleBackColor = true;
            // 
            // _splitContainer
            // 
            this._splitContainer.BackColor = System.Drawing.SystemColors.ControlDark;
            this._splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this._splitContainer.Location = new System.Drawing.Point(0, 0);
            this._splitContainer.Margin = new System.Windows.Forms.Padding(0);
            this._splitContainer.Name = "_splitContainer";
            // 
            // _splitContainer.Panel1
            // 
            this._splitContainer.Panel1.BackColor = System.Drawing.SystemColors.ControlLight;
            this._splitContainer.Panel1.Controls.Add(this._lblTime);
            this._splitContainer.Panel1.Controls.Add(this._lblProject);
            this._splitContainer.Panel1.Controls.Add(this._btnProjectSelect);
            this._splitContainer.Panel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // _splitContainer.Panel2
            // 
            this._splitContainer.Panel2.BackColor = System.Drawing.SystemColors.ControlLight;
            this._splitContainer.Panel2.Controls.Add(this._tbDescription);
            this._splitContainer.Panel2.Controls.Add(this._cbConfirmed);
            this._splitContainer.Panel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._splitContainer.Size = new System.Drawing.Size(600, 20);
            this._splitContainer.SplitterDistance = 283;
            this._splitContainer.SplitterWidth = 2;
            this._splitContainer.TabIndex = 6;
            // 
            // TimeItemControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._splitContainer);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "TimeItemControl";
            this.Size = new System.Drawing.Size(600, 20);
            this._splitContainer.Panel1.ResumeLayout(false);
            this._splitContainer.Panel2.ResumeLayout(false);
            this._splitContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._splitContainer)).EndInit();
            this._splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label _lblTime;
        private System.Windows.Forms.TextBox _tbDescription;
        private System.Windows.Forms.Button _btnProjectSelect;
        private System.Windows.Forms.Label _lblProject;
        private System.Windows.Forms.CheckBox _cbConfirmed;
        private System.Windows.Forms.SplitContainer _splitContainer;
    }
}
