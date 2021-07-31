using System;
using System.IO;
using System.Windows.Forms;
//using IronOcr;

namespace ElationAutoImport
{
    public partial class MamogramReport : UserControl
    {
        string actualPath = "";
        public MamogramReport()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void MamogramReport_Load(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e) //Folder Path
        {
            FolderBrowserDialog FBD = new FolderBrowserDialog();
            
            if(FBD.ShowDialog() == DialogResult.OK)
            {
                actualPath = FBD.SelectedPath;
                string[] fileNames = Directory.GetFiles(FBD.SelectedPath);
                listBox1.Items.Clear();
                foreach(string patientFile in fileNames)
                {
                    listBox1.Items.Add(System.IO.Path.GetFileName(patientFile));
                }
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            ListBox lb = sender as ListBox;
            int index = listBox1.SelectedIndex;
            //Parse through the string and update the boxes accordingly.
            if (lb != null)
            {
                string fileName = listBox1.Items[index].ToString().TrimEnd('.','p','d','f');
                string[] formatPreview = fileName.Split('_');
                filenameBox.Text = @listBox1.Items[index].ToString();
                reviewerBox.Text = formatPreview[4];
                dateBox.Text = formatPreview[1].Replace('.','/');
                doctypeBox.Text = formatPreview[3];
                titleBox.Text = formatPreview[2];
            }
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click_2(object sender, EventArgs e)
        {
            
            int index = listBox1.SelectedIndex;
            if (index != -1)
            {
                System.Diagnostics.Process.Start(@actualPath + @"\" + @listBox1.Items[index].ToString());
            } else {
                MessageBox.Show("Please select a file!");
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void filenameBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
