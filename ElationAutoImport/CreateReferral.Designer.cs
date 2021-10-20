
namespace ElationAutoImport
{
    partial class CreateReferral
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
            this.referralText = new System.Windows.Forms.TextBox();
            this.searchPatient = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.searchWorker = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // referralText
            // 
            this.referralText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.referralText.Location = new System.Drawing.Point(668, 29);
            this.referralText.Multiline = true;
            this.referralText.Name = "referralText";
            this.referralText.Size = new System.Drawing.Size(691, 712);
            this.referralText.TabIndex = 0;
            // 
            // searchPatient
            // 
            this.searchPatient.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.searchPatient.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.searchPatient.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchPatient.FormattingEnabled = true;
            this.searchPatient.Location = new System.Drawing.Point(37, 29);
            this.searchPatient.Name = "searchPatient";
            this.searchPatient.Size = new System.Drawing.Size(602, 32);
            this.searchPatient.TabIndex = 1;
            this.searchPatient.SelectedIndexChanged += new System.EventHandler(this.searchPatient_SelectedIndexChanged);
            this.searchPatient.TextChanged += new System.EventHandler(this.searchPatient_TextChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(178, 650);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(286, 61);
            this.button1.TabIndex = 2;
            this.button1.Text = "Create Referral";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // searchWorker
            // 
            this.searchWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.searchWorker_DoWork);
            this.searchWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.searchWorker_RunWorkerCompleted);
            // 
            // CreateReferral
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.searchPatient);
            this.Controls.Add(this.referralText);
            this.Name = "CreateReferral";
            this.Size = new System.Drawing.Size(1359, 744);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox referralText;
        private System.Windows.Forms.ComboBox searchPatient;
        private System.Windows.Forms.Button button1;
        private System.ComponentModel.BackgroundWorker searchWorker;
    }
}
