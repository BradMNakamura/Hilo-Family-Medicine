using System;
using System.Drawing;
using System.Windows.Forms;

namespace ElationAutoImport
{
    public partial class Form1 : Form
    {
        private Form activeForm;
        private bool mouseDown;
        private Point lastLocation;

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.Location = new Point(
                    (this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);

                this.Update();
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }
        public Form1()
        {
            InitializeComponent();
            homePage1.BringToFront();
        }

        private void OpenChildForm(Form childForm, object btnSender)
        {
            if (activeForm != null)
                activeForm.Close();
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            //this.panelDesktopPane.Controls.Add(childForm);
            childForm.BringToFront();
            childForm.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            homePage1.BringToFront();
        }
        private void button5_Click(object sender, EventArgs e) //Mamogram
        {
            mamogramReport1.BringToFront();
        }
        private void button4_Click(object sender, EventArgs e) //Cpap
        {
            rename_File2.BringToFront();
        }
        private void dexaReport_Click(object sender, EventArgs e) //Dexa
        {
            customFile1.BringToFront();
        }
        private void dreReport_Click(object sender, EventArgs e)
        {
            patientSummary1.BringToFront();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void settingButtom_Click(object sender, EventArgs e)
        {
           settingsPanel1.BringToFront();
        }

        private void papReport_Click(object sender, EventArgs e)
        {
            createReferral1.BringToFront();
        }
    }
}
