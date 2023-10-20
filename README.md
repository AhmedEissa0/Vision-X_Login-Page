# Vision-X_Login-Page
this project is a login page made with windows forms for a specific project 
the login page designed to cross a python code with c# .NET environment 
i have reached to a point that the login page connected to a Database with MSSql and when the login is successful it fetches the .exe for the python code that it can fetch the directory of that py code automatic even fetches the username of the pc automatic also
after that, it can execute the code at this stage.

---------------------------------Full_Code-----------------------------
using Ionic.Zip;
using Python.Runtime;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Runtime.InteropServices;


namespace Database_login_page
{
    public partial class Form1 : Form

    {


        public object FileExtensions { get; private set; }

        [LibraryImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static partial IntPtr CreateRoundRectRgn
        (
            int nLeftRect,     // x-coordinate of upper-left corner
            int nTopRect,      // y-coordinate of upper-left corner
            int nRightRect,    // x-coordinate of lower-right corner
            int nBottomRect,   // y-coordinate of lower-right corner
            int nWidthEllipse, // width of ellipse
            int nHeightEllipse // height of ellipse
        );

#pragma warning disable CA1041 // Provide ObsoleteAttribute message
        [Obsolete]
#pragma warning restore CA1041 // Provide ObsoleteAttribute message
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Form1()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
            pictureBox3.Hide();

        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void Label2_Click(object sender, EventArgs e)
        {

        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        [Obsolete]
        private void Button1_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\Ahmed Saied\Documents\Login_Info.mdf"";Integrated Security=True;Connect Timeout=30");
            SqlDataAdapter sda = new("SELECT COUNT(*) FROM users_info WHERE username='" + textBox1.Text + "' AND password='" + textBox2.Text + "'", connection);
            /* In the above line, the program is selecting the whole data from the table and matching it with the username and password provided by the user. */

            DataTable dt = new(); //Creating a virtual table

            sda.Fill(dt);

            if (dt.Rows[0][0].ToString() == "1")
            {
                this.Enabled = false;
                Form3 topForm = new();
                topForm.ShowDialog();
                //-------------------------------------------------------Py & C# Integration-----------------------------------------------------------------------//
                string username = Environment.UserName;
                Runtime.PythonDLL = $@"C:\Users\{username}\AppData\Local\Programs\Python\Python311\python311.dll";

                // Initialize Python.NET
                PythonEngine.Initialize();
                PythonEngine.PythonPath = $@"C:\Users\{username}\AppData\Local\Programs\Python\Python311";

                //------------------------------------Create Dir, Download, Extract, Execute----------------------------------//
                //---------------------------------if i Create Dir in C i move files into it!???----------------------------------------------//
                // Check if the folder doesn't exist, then create it
                //string folderPath = @"C:\Vision X";                                                                     // Replace with the desired folder path
                //if (!Directory.Exists(folderPath))
                //{
                //    Directory.CreateDirectory(folderPath);
                //}
                //---------------------------------Check ZIP location and extract it!----------------------------------------------//

                string path = $@"C:\Users\{username}\Desktop";
                string zipName = "bin.zip";
                string password = "012386497";

                foreach (string filepath in Directory.EnumerateFiles(path, zipName, SearchOption.AllDirectories))
                {
                    string extractPath = filepath.Substring(0, filepath.Length - zipName.Length);

                    if (Path.GetExtension(filepath).Equals(".zip", StringComparison.OrdinalIgnoreCase))
                    {
                        if (Path.GetFileName(filepath) == "bin.zip")
                        {

                            //Extract//
                            using ZipFile zip = ZipFile.Read(filepath);
                            zip.Password = password;
                            foreach (var entry in zip)
                            {
                                entry.Extract(extractPath, ExtractExistingFileAction.OverwriteSilently);
                            }

                            // Set the hidden attribute for the file
                            File.SetAttributes(filepath, File.GetAttributes(filepath) | FileAttributes.Hidden);

                            //Execute//
                            string exePath = extractPath + @"bin\log\Exe Files\output\calc.exe";
                            using (Py.GIL())                            // Acquire the Global Interpreter Lock (GIL)
                            {
                                if (File.Exists(exePath))
                                {
                                    ProcessStartInfo startInfo = new()
                                    {
                                        FileName = exePath,        // Set the file path
                                        UseShellExecute = true,     // Use the system's default shell (e.g., open with the associated program)
                                    };

                                    // Start the process
                                    using Process process = new();
                                    process.StartInfo = startInfo;
                                    process.Start();
                                }

                                else
                                {
                                    MessageBox.Show("Error in directory, Please ajust the directory of python executable file!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    Application.Exit();
                                }
                            }

                            // Finalize Python.NET
                            PythonEngine.Shutdown();
                        }
                    }
                }
            }

            else
            {
                Console.WriteLine("\a");
                var f = MessageBox.Show("Invalid username or password!", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Enabled = false;
                if (f == DialogResult.OK)
                {
                    this.Enabled = true;
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.Username != string.Empty)
            {
                textBox1.Text = Properties.Settings.Default.Username;
                textBox2.Text = Properties.Settings.Default.Password;

                if (pictureBox4.Visible)
                {
                    pictureBox3.Hide();
                }
            }
        }

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {

        }


        private void Label3_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Label4_Click(object sender, EventArgs e)
        {

        }

        private void PictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.Username = textBox1.Text;
            Properties.Settings.Default.Password = textBox2.Text;
            Properties.Settings.Default.Save();
        }

        private void PictureBox3_Click(object sender, EventArgs e)
        {
            textBox2.UseSystemPasswordChar = true;
            pictureBox3.Hide();
            pictureBox4.Show();

        }

        private void PictureBox4_Click(object sender, EventArgs e)
        {
            textBox2.UseSystemPasswordChar = false;
            pictureBox4.Hide();
            pictureBox3.Show();
        }

        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Sure to exit?", "Caution!", MessageBoxButtons.YesNo);
            switch (dr)
            {
                case DialogResult.Yes:
                    Application.Exit();

                    break;
                case DialogResult.No:
                    break;
            }
        }

        private void Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Panel4_Paint(object sender, PaintEventArgs e)
        {
        }

        private void PictureBox6_Click(object sender, EventArgs e)
        {

        }

        private void Button3_Click(object sender, EventArgs e)
        {

        }
    }
}
