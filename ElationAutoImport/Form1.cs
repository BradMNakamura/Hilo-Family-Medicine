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
            rename_File1.BringToFront();
        }
        private void dexaReport_Click(object sender, EventArgs e) //Dexa
        {
            customFile1.BringToFront();
        }
        private void dreReport_Click(object sender, EventArgs e)
        {

        }
        private void papReport_Click(object sender, EventArgs e)
        {

        }
        private void button8_Click(object sender, EventArgs e)
        {

        }
        private void settingButtom_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void mamogramReport1_Load(object sender, EventArgs e)
        {

        }



        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }


        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {

        }

        private void homePage1_Load(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click_2(object sender, EventArgs e)
        {

        }
    }
}
