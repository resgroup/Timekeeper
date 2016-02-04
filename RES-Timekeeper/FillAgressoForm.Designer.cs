namespace RES_Timekeeper
{
    partial class FillAgressoForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FillAgressoForm));
            this._dgvText = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this._dgvText)).BeginInit();
            this.SuspendLayout();
            // 
            // _dgvText
            // 
            this._dgvText.AllowUserToAddRows = false;
            this._dgvText.AllowUserToDeleteRows = false;
            this._dgvText.AllowUserToResizeColumns = false;
            this._dgvText.AllowUserToResizeRows = false;
            this._dgvText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._dgvText.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._dgvText.ColumnHeadersVisible = false;
            this._dgvText.Location = new System.Drawing.Point(12, 13);
            this._dgvText.Name = "_dgvText";
            this._dgvText.RowHeadersVisible = false;
            this._dgvText.Size = new System.Drawing.Size(811, 277);
            this._dgvText.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 13);
            this.label1.TabIndex = 0;
            // 
            // FillAgressoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(835, 302);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._dgvText);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FillAgressoForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "Fill Agresso";
            this.Shown += new System.EventHandler(this.FillAgressoForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this._dgvText)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView _dgvText;
        private System.Windows.Forms.Label label1;
    }
}