namespace RES_Timekeeper
{
    partial class EditItemForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditItemForm));
            this._dtStart = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this._dtEnd = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this._lblProjectCode = new System.Windows.Forms.Label();
            this._btnProjectBrowse = new System.Windows.Forms.Button();
            this._tbNotes = new System.Windows.Forms.TextBox();
            this._lblOverlapWarning = new System.Windows.Forms.Label();
            this._btnCancel = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // _dtStart
            // 
            this._dtStart.CustomFormat = "HH:mm";
            this._dtStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this._dtStart.Location = new System.Drawing.Point(65, 12);
            this._dtStart.Name = "_dtStart";
            this._dtStart.ShowUpDown = true;
            this._dtStart.Size = new System.Drawing.Size(54, 20);
            this._dtStart.TabIndex = 0;
            this._dtStart.ValueChanged += new System.EventHandler(this._dtStart_ValueChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(27, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Start:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _dtEnd
            // 
            this._dtEnd.CustomFormat = "HH:mm";
            this._dtEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this._dtEnd.Location = new System.Drawing.Point(65, 38);
            this._dtEnd.Name = "_dtEnd";
            this._dtEnd.ShowUpDown = true;
            this._dtEnd.Size = new System.Drawing.Size(54, 20);
            this._dtEnd.TabIndex = 2;
            this._dtEnd.ValueChanged += new System.EventHandler(this._dtEnd_ValueChanged);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(27, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "End:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(12, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "Project:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(13, 86);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 20);
            this.label4.TabIndex = 5;
            this.label4.Text = "Notes:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _lblProjectCode
            // 
            this._lblProjectCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._lblProjectCode.Location = new System.Drawing.Point(95, 61);
            this._lblProjectCode.Name = "_lblProjectCode";
            this._lblProjectCode.Size = new System.Drawing.Size(207, 20);
            this._lblProjectCode.TabIndex = 6;
            this._lblProjectCode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _btnProjectBrowse
            // 
            this._btnProjectBrowse.Location = new System.Drawing.Point(65, 61);
            this._btnProjectBrowse.Name = "_btnProjectBrowse";
            this._btnProjectBrowse.Size = new System.Drawing.Size(24, 20);
            this._btnProjectBrowse.TabIndex = 7;
            this._btnProjectBrowse.Text = "...";
            this._btnProjectBrowse.UseVisualStyleBackColor = true;
            this._btnProjectBrowse.Click += new System.EventHandler(this._btnProjectBrowse_Click);
            // 
            // _tbNotes
            // 
            this._tbNotes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._tbNotes.Location = new System.Drawing.Point(65, 87);
            this._tbNotes.Name = "_tbNotes";
            this._tbNotes.Size = new System.Drawing.Size(237, 20);
            this._tbNotes.TabIndex = 8;
            // 
            // _lblOverlapWarning
            // 
            this._lblOverlapWarning.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._lblOverlapWarning.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lblOverlapWarning.ForeColor = System.Drawing.Color.Red;
            this._lblOverlapWarning.Location = new System.Drawing.Point(137, 12);
            this._lblOverlapWarning.Name = "_lblOverlapWarning";
            this._lblOverlapWarning.Size = new System.Drawing.Size(165, 46);
            this._lblOverlapWarning.TabIndex = 9;
            this._lblOverlapWarning.Text = "The time period that you have entered overlaps with an existing time period. Plea" +
    "se adjust the times to the left.";
            this._lblOverlapWarning.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this._lblOverlapWarning.Visible = false;
            // 
            // _btnCancel
            // 
            this._btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._btnCancel.Location = new System.Drawing.Point(227, 113);
            this._btnCancel.Name = "_btnCancel";
            this._btnCancel.Size = new System.Drawing.Size(75, 23);
            this._btnCancel.TabIndex = 10;
            this._btnCancel.Text = "Cancel";
            this._btnCancel.UseVisualStyleBackColor = true;
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(146, 113);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 11;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this._btnOK_Click);
            // 
            // EditItemForm
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._btnCancel;
            this.ClientSize = new System.Drawing.Size(322, 153);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this._btnCancel);
            this.Controls.Add(this._lblOverlapWarning);
            this.Controls.Add(this._tbNotes);
            this.Controls.Add(this._btnProjectBrowse);
            this.Controls.Add(this._lblProjectCode);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this._dtEnd);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._dtStart);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(640, 180);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(330, 180);
            this.Name = "EditItemForm";
            this.Text = "Edit Item";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker _dtStart;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker _dtEnd;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label _lblProjectCode;
        private System.Windows.Forms.Button _btnProjectBrowse;
        private System.Windows.Forms.TextBox _tbNotes;
        private System.Windows.Forms.Label _lblOverlapWarning;
        private System.Windows.Forms.Button _btnCancel;
        private System.Windows.Forms.Button okButton;
    }
}