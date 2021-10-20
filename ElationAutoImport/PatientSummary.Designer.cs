
namespace ElationAutoImport
{
    partial class PatientSummary
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
            this.saveButton = new System.Windows.Forms.Button();
            this.recordButton = new System.Windows.Forms.Button();
            this.stopButton = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timerLabel = new System.Windows.Forms.Label();
            this.timerWorker = new System.ComponentModel.BackgroundWorker();
            this.copyText = new System.Windows.Forms.RichTextBox();
            this.volumeBar = new System.Windows.Forms.ProgressBar();
            this.deviceList = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(861, 557);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(121, 40);
            this.saveButton.TabIndex = 1;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // recordButton
            // 
            this.recordButton.Location = new System.Drawing.Point(410, 557);
            this.recordButton.Name = "recordButton";
            this.recordButton.Size = new System.Drawing.Size(121, 40);
            this.recordButton.TabIndex = 2;
            this.recordButton.Text = "Record";
            this.recordButton.UseVisualStyleBackColor = true;
            this.recordButton.Click += new System.EventHandler(this.RecordButton_Click);
            // 
            // stopButton
            // 
            this.stopButton.Location = new System.Drawing.Point(637, 557);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(121, 40);
            this.stopButton.TabIndex = 4;
            this.stopButton.Text = "Stop";
            this.stopButton.UseVisualStyleBackColor = true;
            this.stopButton.Click += new System.EventHandler(this.StopButton_Click);
            // 
            // timerLabel
            // 
            this.timerLabel.AutoSize = true;
            this.timerLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 72F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.timerLabel.Location = new System.Drawing.Point(560, 277);
            this.timerLabel.Name = "timerLabel";
            this.timerLabel.Size = new System.Drawing.Size(284, 108);
            this.timerLabel.TabIndex = 7;
            this.timerLabel.Text = "Show";
            this.timerLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.timerLabel.Click += new System.EventHandler(this.timerLabel_Click);
            // 
            // timerWorker
            // 
            this.timerWorker.WorkerSupportsCancellation = true;
            this.timerWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.timerWorker_DoWork);
            this.timerWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.timerWorker_ProgressChanged);
            // 
            // copyText
            // 
            this.copyText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.copyText.Location = new System.Drawing.Point(410, 99);
            this.copyText.Name = "copyText";
            this.copyText.Size = new System.Drawing.Size(572, 452);
            this.copyText.TabIndex = 8;
            this.copyText.Text = "";
            // 
            // volumeBar
            // 
            this.volumeBar.Location = new System.Drawing.Point(410, 481);
            this.volumeBar.Name = "volumeBar";
            this.volumeBar.Size = new System.Drawing.Size(572, 42);
            this.volumeBar.TabIndex = 9;
            this.volumeBar.RegionChanged += new System.EventHandler(this.volumeBar_RegionChanged);
            this.volumeBar.Click += new System.EventHandler(this.volumeBar_Click);
            // 
            // deviceList
            // 
            this.deviceList.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deviceList.FormattingEnabled = true;
            this.deviceList.Location = new System.Drawing.Point(1041, 99);
            this.deviceList.Name = "deviceList";
            this.deviceList.Size = new System.Drawing.Size(236, 28);
            this.deviceList.TabIndex = 10;
            // 
            // PatientSummary
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.deviceList);
            this.Controls.Add(this.volumeBar);
            this.Controls.Add(this.timerLabel);
            this.Controls.Add(this.stopButton);
            this.Controls.Add(this.recordButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.copyText);
            this.Name = "PatientSummary";
            this.Size = new System.Drawing.Size(1359, 755);
            this.Load += new System.EventHandler(this.PatientSummary_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button recordButton;
        private System.Windows.Forms.Button stopButton;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label timerLabel;
        private System.ComponentModel.BackgroundWorker timerWorker;
        private System.Windows.Forms.RichTextBox copyText;
        private System.Windows.Forms.ProgressBar volumeBar;
        private System.Windows.Forms.ComboBox deviceList;
    }
}
