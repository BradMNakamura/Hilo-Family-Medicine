
namespace ElationAutoImport
{
    partial class Rename_File
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Rename_File));
            this.button1 = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.tasksCompleted = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.prcedureText = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.fileText = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.reviewingText = new System.Windows.Forms.TextBox();
            this.documentText = new System.Windows.Forms.TextBox();
            this.orderingText = new System.Windows.Forms.TextBox();
            this.serviceText = new System.Windows.Forms.TextBox();
            this.patientText = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.convertFile = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.patientFile = new AxAcroPDFLib.AxAcroPDF();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.patientFile)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(711, 14);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(178, 53);
            this.button1.TabIndex = 6;
            this.button1.Text = "Rename Files";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(895, 14);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(457, 53);
            this.progressBar1.TabIndex = 8;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // tasksCompleted
            // 
            this.tasksCompleted.AutoSize = true;
            this.tasksCompleted.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tasksCompleted.Location = new System.Drawing.Point(1106, 70);
            this.tasksCompleted.Name = "tasksCompleted";
            this.tasksCompleted.Size = new System.Drawing.Size(0, 25);
            this.tasksCompleted.TabIndex = 9;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.prcedureText);
            this.panel3.Controls.Add(this.label7);
            this.panel3.Controls.Add(this.fileText);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Controls.Add(this.reviewingText);
            this.panel3.Controls.Add(this.documentText);
            this.panel3.Controls.Add(this.orderingText);
            this.panel3.Controls.Add(this.serviceText);
            this.panel3.Controls.Add(this.patientText);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.button5);
            this.panel3.Controls.Add(this.button4);
            this.panel3.Controls.Add(this.button3);
            this.panel3.Location = new System.Drawing.Point(686, 121);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(656, 638);
            this.panel3.TabIndex = 1;
            // 
            // prcedureText
            // 
            this.prcedureText.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.prcedureText.Location = new System.Drawing.Point(36, 285);
            this.prcedureText.Multiline = true;
            this.prcedureText.Name = "prcedureText";
            this.prcedureText.Size = new System.Drawing.Size(617, 71);
            this.prcedureText.TabIndex = 0;
            this.prcedureText.TextChanged += new System.EventHandler(this.prcedureText_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(31, 257);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(172, 25);
            this.label7.TabIndex = 24;
            this.label7.Text = "Procedure Desc.";
            // 
            // fileText
            // 
            this.fileText.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.5F);
            this.fileText.Location = new System.Drawing.Point(38, 387);
            this.fileText.Multiline = true;
            this.fileText.Name = "fileText";
            this.fileText.Size = new System.Drawing.Size(617, 69);
            this.fileText.TabIndex = 23;
            this.fileText.TextChanged += new System.EventHandler(this.fileText_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(32, 359);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(109, 25);
            this.label6.TabIndex = 22;
            this.label6.Text = "File Name";
            // 
            // reviewingText
            // 
            this.reviewingText.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.reviewingText.Location = new System.Drawing.Point(217, 222);
            this.reviewingText.Name = "reviewingText";
            this.reviewingText.Size = new System.Drawing.Size(436, 31);
            this.reviewingText.TabIndex = 25;
            // 
            // documentText
            // 
            this.documentText.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.documentText.Location = new System.Drawing.Point(217, 171);
            this.documentText.Name = "documentText";
            this.documentText.Size = new System.Drawing.Size(436, 31);
            this.documentText.TabIndex = 26;
            // 
            // orderingText
            // 
            this.orderingText.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.orderingText.Location = new System.Drawing.Point(217, 124);
            this.orderingText.Name = "orderingText";
            this.orderingText.Size = new System.Drawing.Size(436, 31);
            this.orderingText.TabIndex = 27;
            // 
            // serviceText
            // 
            this.serviceText.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.serviceText.Location = new System.Drawing.Point(217, 79);
            this.serviceText.Name = "serviceText";
            this.serviceText.Size = new System.Drawing.Size(436, 31);
            this.serviceText.TabIndex = 28;
            // 
            // patientText
            // 
            this.patientText.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.patientText.Location = new System.Drawing.Point(217, 42);
            this.patientText.Name = "patientText";
            this.patientText.Size = new System.Drawing.Size(436, 31);
            this.patientText.TabIndex = 29;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(31, 217);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(180, 25);
            this.label5.TabIndex = 16;
            this.label5.Text = "Reviewing Doctor";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(32, 165);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(163, 25);
            this.label3.TabIndex = 15;
            this.label3.Text = "Document Type";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(31, 119);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(164, 25);
            this.label4.TabIndex = 14;
            this.label4.Text = "Ordering Doctor";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(32, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(135, 25);
            this.label2.TabIndex = 13;
            this.label2.Text = "Service Date";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(31, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(141, 25);
            this.label1.TabIndex = 12;
            this.label1.Text = "Patient Name";
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(243, 482);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(178, 53);
            this.button5.TabIndex = 11;
            this.button5.Text = "Choose Folder";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(374, 555);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(178, 53);
            this.button4.TabIndex = 9;
            this.button4.Text = "Deny";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(113, 555);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(178, 53);
            this.button3.TabIndex = 8;
            this.button3.Text = "Approve";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(686, 117);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(645, 635);
            this.panel1.TabIndex = 5;
            // 
            // convertFile
            // 
            this.convertFile.AutoSize = true;
            this.convertFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.convertFile.Location = new System.Drawing.Point(1106, 70);
            this.convertFile.Name = "convertFile";
            this.convertFile.Size = new System.Drawing.Size(0, 25);
            this.convertFile.TabIndex = 11;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(1106, 70);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(0, 25);
            this.label8.TabIndex = 12;
            // 
            // patientFile
            // 
            this.patientFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.patientFile.Enabled = true;
            this.patientFile.Location = new System.Drawing.Point(0, 0);
            this.patientFile.Name = "patientFile";
            this.patientFile.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("patientFile.OcxState")));
            this.patientFile.Size = new System.Drawing.Size(680, 755);
            this.patientFile.TabIndex = 10;
            this.patientFile.Enter += new System.EventHandler(this.patientFile_Enter);
            // 
            // Rename_File
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label8);
            this.Controls.Add(this.convertFile);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.patientFile);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tasksCompleted);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.button1);
            this.Name = "Rename_File";
            this.Size = new System.Drawing.Size(1359, 755);
            this.Load += new System.EventHandler(this.Rename_File_Load);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.patientFile)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Label tasksCompleted;
        private System.Windows.Forms.Panel panel3;
        public System.Windows.Forms.TextBox prcedureText;
        private System.Windows.Forms.Label label7;
        public System.Windows.Forms.TextBox fileText;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.TextBox reviewingText;
        public System.Windows.Forms.TextBox documentText;
        public System.Windows.Forms.TextBox orderingText;
        public System.Windows.Forms.TextBox serviceText;
        public System.Windows.Forms.TextBox patientText;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Panel panel1;
        public AxAcroPDFLib.AxAcroPDF patientFile;
        private System.Windows.Forms.Label convertFile;
        private System.Windows.Forms.Label label8;
    }
}
