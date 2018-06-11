using System;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;

/// <summary>
/// Status Codes:
/// 1 - Main Menu - B1:Login Test - B2:Show AW Analytics - B3:Show A Analytics - B4:   Database
/// </summary>

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private string mainLabelTextReceivePath = @"textBridgeReceive.tmp";
        private string mainLabelTextSendPath = @"textBridgeSend.tmp";
        private string DisplayedText = "";
        short status = 1;

        public Form1()
        {
            InitializeComponent();
            status = 1;
            DisplayedText = "Welcome to the product management database! \nTo continue,please login or use the search function for quick searches.";
            mainLabel_updateText();
            button1.Text = "";
            button2.Text = "Login";
            button3.Text = "Search";
            button4.Text = "Exit";
            button1.Visible = false;
            button2.Visible = true;
            button3.Visible = true;
            button4.Visible = true;
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            statusLabel_displayText("B1");
        }
        private void Button2_Click(object sender, EventArgs e)
        {
            statusLabel_displayText("B2");
            if(status == 1)
            {
                string enteredUsername = Prompt.ShowDialogUsername();
                statusLabel_displayText("Login: " + enteredUsername);
                string enteredPassword = "";
                if (enteredUsername != "")
                {
                    enteredPassword = Prompt.ShowDialogPassword();
                    statusLabel_displayText("Attempting login...");
                    if (enteredPassword != "")
                    {
                        File.WriteAllText(mainLabelTextSendPath, "Login\n" + enteredUsername + "\n" + enteredPassword);
                        status = 2;
                        startBackend();
                        statusLabel_displayText("Querying Backend");
                        System.Threading.Thread.Sleep(100);
                        DisplayedText = File.ReadAllText(mainLabelTextReceivePath);
                        mainLabel_updateText();
                        statusLabel_displayText("");
                        button1.Text = "Products";
                        button2.Text = "Users";
                        button3.Text = "";
                        button4.Text = "Logout";
                        button1.Visible = true;
                        button2.Visible = true;
                        button3.Visible = false;
                        button4.Visible = true;
                    }
                    else
                        statusLabel_displayText("Error at login.Contact administrator");
                }
                else
                    statusLabel_displayText("Error at login.Contact administrator");
            }
        }
        private void Button3_Click(object sender, EventArgs e)
        {
            statusLabel_displayText("B3");
        }
        private void Button4_Click(object sender, EventArgs e)
        {
            statusLabel_displayText("B4");
            if (status == 2)
            {
                status = 1;
                DisplayedText = "Welcome to the product management database! \nTo continue,please login or use the search function for quick searches.";
                mainLabel_updateText();
                button1.Text = "";
                button2.Text = "Login";
                button3.Text = "Search";
                button4.Text = "Exit";
                button1.Visible = false;
                button2.Visible = true;
                button3.Visible = true;
                button4.Visible = true;
            }
        }
        private void mainLabel_updateText() { mainLabel.Text = DisplayedText;  }
        private void statusLabel_displayText(string text) { statusLabel.Text = text; }
        private void startBackend()
        {
            // Use ProcessStartInfo class
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.CreateNoWindow = false;
            startInfo.UseShellExecute = false;
            startInfo.FileName = "start";
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.Arguments = "/MIN backend.exe";

            try
            {
                // Start the process with the info we specified.
                // Call WaitForExit and then the using statement will close.
                using (Process exeProcess = Process.Start(startInfo))
                {
                    exeProcess.WaitForExit();
                }
            }
            catch
            {
                // Log error.
            }
        }
        public static class Prompt
        {
            public static string ShowDialogUsername()
            {
                Form prompt = new Form()
                {
                    Width = 370,
                    Height = 125,
                    FormBorderStyle = FormBorderStyle.FixedDialog,
                    Text = "Login Required",
                    StartPosition = FormStartPosition.CenterScreen
                };
                Label textLabel = new Label() { Left = 25, Top = 10, Text = "Enter username" };
                TextBox textBox = new TextBox() { Left = 25, Top = 25, Width = 300 };
                Button confirmation = new Button() { Text = "Cancel", Left = 272, Width = 55, Top = 45, DialogResult = DialogResult.OK };
                confirmation.Click += (sender, e) => { prompt.Close(); };
                prompt.Controls.Add(textBox);
                prompt.Controls.Add(confirmation);
                prompt.Controls.Add(textLabel);
                prompt.AcceptButton = confirmation;

                return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : "";
            }
            public static string ShowDialogPassword()
            {
                Form prompt = new Form()
                {
                    Width = 370,
                    Height = 125,
                    FormBorderStyle = FormBorderStyle.FixedDialog,
                    Text = "Login Required",
                    StartPosition = FormStartPosition.CenterScreen
                };
                Label textLabel = new Label() { Left = 25, Top = 10, Text = "Enter password" };
                TextBox textBox = new TextBox() { Left = 25, Top = 25, Width = 300,UseSystemPasswordChar = true };
                Button confirmation = new Button() { Text = "Cancel", Left = 272, Width = 55, Top = 45, DialogResult = DialogResult.OK };
                confirmation.Click += (sender, e) => { prompt.Close(); };
                prompt.Controls.Add(textBox);
                prompt.Controls.Add(confirmation);
                prompt.Controls.Add(textLabel);
                prompt.AcceptButton = confirmation;
                return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : "";
            }
        }
        
    }
}