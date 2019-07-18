using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace FastBookmarks
{
    public partial class Form1 : Form
    {
        string Dir = "", Path = "";
        public Form1()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                string[] lines = File.ReadAllLines(Path);
                lines = lines.Where(w => w != lines[lstURLs.SelectedIndex]).ToArray();
                File.WriteAllLines(Path, lines);

                lstURLs.Items.RemoveAt(lstURLs.SelectedIndex);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Please select an item to remove", "Error Removing");
            }
        }

        private void btnOpenAll_Click(object sender, EventArgs e)
        {
            for(int i = 0; i < lstURLs.Items.Count; i++)
                System.Diagnostics.Process.Start(lstURLs.Items[i].ToString());
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            StreamWriter textOut =
                    new StreamWriter(
                    new FileStream(Path, FileMode.Create, FileAccess.Write));

            for (int i = 0; i < lstURLs.Items.Count; i++)
            {
                textOut.WriteLine(lstURLs.Items[i].ToString());
            }

            textOut.WriteLine(txtNewURL.Text);
            textOut.Close();

            lstURLs.Items.Add(txtNewURL.Text);

            txtNewURL.Clear();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Dir = @"C:\FastBookmarks\Files\";
            if (!Directory.Exists(Dir))
            {
                Directory.CreateDirectory(Dir);
            }

            Path = Dir + "Bookmarks.txt";
            if (!File.Exists(Path))
            {
                File.Create(Path);
            }

            StreamReader textIn =
                new StreamReader(
                new FileStream(Path, FileMode.OpenOrCreate, FileAccess.Read));

            while (textIn.Peek() != -1)
            {
                lstURLs.Items.Add(textIn.ReadLine());
            }
            textIn.Close();
        }
    }
}
