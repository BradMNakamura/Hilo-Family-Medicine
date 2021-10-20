
namespace ElationAutoImport
{
    partial class MamogramReport
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MamogramReport));
            this.searchWorker = new System.ComponentModel.BackgroundWorker();
            this.panel1 = new System.Windows.Forms.Panel();
            this.patientFile = new AxAcroPDFLib.AxAcroPDF();
            this.nextButton = new System.Windows.Forms.Button();
            this.openFolder = new System.Windows.Forms.Button();
            this.prevButon = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.fileText = new System.Windows.Forms.TextBox();
            this.dateText = new System.Windows.Forms.TextBox();
            this.docBox = new System.Windows.Forms.ComboBox();
            this.reviewerText = new System.Windows.Forms.ComboBox();
            this.searchPatient = new System.Windows.Forms.ComboBox();
            this.button4 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.patientFile)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // searchWorker
            // 
            this.searchWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.searchWorker_DoWork);
            this.searchWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.searchWorker_RunWorkerCompleted);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.patientFile);
            this.panel1.Controls.Add(this.nextButton);
            this.panel1.Controls.Add(this.openFolder);
            this.panel1.Controls.Add(this.prevButon);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(678, 755);
            this.panel1.TabIndex = 4;
            // 
            // patientFile
            // 
            this.patientFile.Enabled = true;
            this.patientFile.Location = new System.Drawing.Point(0, 48);
            this.patientFile.Name = "patientFile";
            this.patientFile.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("patientFile.OcxState")));
            this.patientFile.Size = new System.Drawing.Size(669, 701);
            this.patientFile.TabIndex = 3;
            // 
            // nextButton
            // 
            this.nextButton.Location = new System.Drawing.Point(447, 0);
            this.nextButton.Name = "nextButton";
            this.nextButton.Size = new System.Drawing.Size(223, 51);
            this.nextButton.TabIndex = 2;
            this.nextButton.Text = "Next";
            this.nextButton.UseVisualStyleBackColor = true;
            this.nextButton.Click += new System.EventHandler(this.nextButton_Click);
            // 
            // openFolder
            // 
            this.openFolder.Location = new System.Drawing.Point(222, 1);
            this.openFolder.Name = "openFolder";
            this.openFolder.Size = new System.Drawing.Size(223, 51);
            this.openFolder.TabIndex = 1;
            this.openFolder.Text = "Select Folder";
            this.openFolder.UseVisualStyleBackColor = true;
            this.openFolder.Click += new System.EventHandler(this.openFolder_Click);
            // 
            // prevButon
            // 
            this.prevButon.Location = new System.Drawing.Point(-3, 1);
            this.prevButon.Name = "prevButon";
            this.prevButon.Size = new System.Drawing.Size(223, 51);
            this.prevButon.TabIndex = 0;
            this.prevButon.Text = "Previous";
            this.prevButon.UseVisualStyleBackColor = true;
            this.prevButon.Click += new System.EventHandler(this.prevButon_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.fileText);
            this.panel2.Controls.Add(this.dateText);
            this.panel2.Controls.Add(this.docBox);
            this.panel2.Controls.Add(this.reviewerText);
            this.panel2.Controls.Add(this.searchPatient);
            this.panel2.Controls.Add(this.button4);
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Location = new System.Drawing.Point(678, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(680, 755);
            this.panel2.TabIndex = 5;
            // 
            // fileText
            // 
            this.fileText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fileText.Location = new System.Drawing.Point(70, 238);
            this.fileText.Multiline = true;
            this.fileText.Name = "fileText";
            this.fileText.ReadOnly = true;
            this.fileText.Size = new System.Drawing.Size(586, 41);
            this.fileText.TabIndex = 8;
            // 
            // dateText
            // 
            this.dateText.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateText.Location = new System.Drawing.Point(100, 369);
            this.dateText.Name = "dateText";
            this.dateText.Size = new System.Drawing.Size(536, 29);
            this.dateText.TabIndex = 7;
            // 
            // docBox
            // 
            this.docBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.docBox.FormattingEnabled = true;
            this.docBox.Items.AddRange(new object[] {
            "Cardiac Report",
            "Consultation Report",
            "Hospital Report",
            "Imaging Report",
            "Laboratory Report",
            "Legal Report",
            "Medical Form Report",
            "Miscellaneous Report",
            "New Visit Note",
            "Nonvisit Report"});
            this.docBox.Location = new System.Drawing.Point(100, 414);
            this.docBox.Name = "docBox";
            this.docBox.Size = new System.Drawing.Size(556, 32);
            this.docBox.TabIndex = 6;
            // 
            // reviewerText
            // 
            this.reviewerText.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.reviewerText.FormattingEnabled = true;
            this.reviewerText.Location = new System.Drawing.Point(100, 324);
            this.reviewerText.Name = "reviewerText";
            this.reviewerText.Size = new System.Drawing.Size(556, 32);
            this.reviewerText.TabIndex = 5;
            // 
            // searchPatient
            // 
            this.searchPatient.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.searchPatient.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.searchPatient.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchPatient.FormattingEnabled = true;
            this.searchPatient.Location = new System.Drawing.Point(1, 3);
            this.searchPatient.Name = "searchPatient";
            this.searchPatient.Size = new System.Drawing.Size(671, 50);
            this.searchPatient.TabIndex = 4;
            this.searchPatient.TextChanged += new System.EventHandler(this.comboBox1_TextChanged);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(235, 652);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(223, 51);
            this.button4.TabIndex = 3;
            this.button4.Text = "Submit";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(1, 148);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(671, 483);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // MamogramReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "MamogramReport";
            this.Size = new System.Drawing.Size(1359, 755);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.patientFile)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.ComponentModel.BackgroundWorker searchWorker;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button nextButton;
        private System.Windows.Forms.Button openFolder;
        private System.Windows.Forms.Button prevButon;
        private System.Windows.Forms.ComboBox searchPatient;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.PictureBox pictureBox1;
        private AxAcroPDFLib.AxAcroPDF patientFile;
        private System.Windows.Forms.TextBox dateText;
        private System.Windows.Forms.ComboBox docBox;
        private System.Windows.Forms.ComboBox reviewerText;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.TextBox fileText;
    }
}
