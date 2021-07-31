
namespace ElationAutoImport
{
    partial class PatientFiles
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
            this.fileList = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // fileList
            // 
            this.fileList.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fileList.FormattingEnabled = true;
            this.fileList.ItemHeight = 25;
            this.fileList.Location = new System.Drawing.Point(-7, 1);
            this.fileList.Name = "fileList";
            this.fileList.Size = new System.Drawing.Size(473, 429);
            this.fileList.TabIndex = 0;
            this.fileList.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // PatientFiles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(465, 450);
            this.Controls.Add(this.fileList);
            this.Name = "PatientFiles";
            this.Text = "Patient Files";
            this.Load += new System.EventHandler(this.PatientFiles_Load);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.ListBox fileList;
    }
}