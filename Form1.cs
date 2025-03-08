//using System.Net.Sockets;
//using System.Text;

//namespace ChatClientGUI
//{
//    public partial class Form1 : Form
//    {
//        public Form1()
//        {
//            InitializeComponent();
//        }

//        private void button1_Click(object sender, EventArgs e)
//        {

//        }

//    }
//}
using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace ChatClientGUI
{
    public partial class Form1 : Form
    {
        private TcpClient client;
        private NetworkStream stream;
        private string username;

        public Form1()
        {
            InitializeComponent();
            AskForUsername();
            ConnectToServer();
        }

        private void AskForUsername()
        {
            username = Microsoft.VisualBasic.Interaction.InputBox("Enter your username:", "Chat Login", "Guest");
            if (string.IsNullOrWhiteSpace(username)) username = "Guest";
        }

        private void ConnectToServer()
        {
            try
            {
                client = new TcpClient("127.0.0.1", 5000);
                stream = client.GetStream();

                // Send username
                byte[] nameBuffer = Encoding.UTF8.GetBytes(username);
                stream.Write(nameBuffer, 0, nameBuffer.Length);

                Thread thread = new Thread(ReceiveMessages);
                thread.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error connecting to server: " + ex.Message);
            }
        }

        private void ReceiveMessages()
        {
            byte[] buffer = new byte[1024];

            while (true)
            {
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                if (bytesRead == 0) break;
                string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                Invoke((MethodInvoker)delegate
                {
                    textBox1.AppendText(message + Environment.NewLine);
                });
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            string message = richTextBox2.Text;
            if (string.IsNullOrWhiteSpace(message)) return;

            byte[] buffer = Encoding.UTF8.GetBytes(message);
            stream.Write(buffer, 0, buffer.Length);

            richTextBox2.AppendText("Me: " + message + Environment.NewLine);
            richTextBox2.Clear();
        }
        private void RichTextBox()
        {
            byte[] buffer = new byte[1024];

            while (true)
            {
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                if (bytesRead == 0) break;
                string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                Invoke((MethodInvoker)delegate
                {
                    richTextBox2.SelectionColor = message.Contains("joined") || message.Contains("left") ? Color.Gray :
                                                 message.StartsWith(username) ? Color.Blue :
                                                 Color.Black;
                    richTextBox2.AppendText(message + Environment.NewLine);
                });
            }
        }
        private string ReplaceEmojis(string text)
        {
                return text.Replace(":smile:", "??")
               .Replace(":heart:", "??")
               .Replace(":thumbs:", "??");
        }

        private void Button(object sender, EventArgs e)
        {
            string message = ReplaceEmojis(button1.Text);
            if (string.IsNullOrWhiteSpace(message)) return;

            byte[] buffer = Encoding.UTF8.GetBytes(message);
            stream.Write(buffer, 0, buffer.Length);

            richTextBox2.AppendText("Me: " + message + Environment.NewLine);
            richTextBox2.Clear();
        }
        private void btnSendFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                byte[] fileData = System.IO.File.ReadAllBytes(ofd.FileName);
                byte[] filePrefix = Encoding.UTF8.GetBytes("[FILE] " + ofd.SafeFileName + "|");
                stream.Write(filePrefix, 0, filePrefix.Length);
                stream.Write(fileData, 0, fileData.Length);
                textBox1.AppendText("Me: Sent file " + ofd.SafeFileName + Environment.NewLine);
            }
            //if (message.StartsWith("[FILE]"))
            //{
            //    string[] parts = message.Split('|');
            //    string fileName = parts[0].Replace("[FILE] ", "").Trim();
            //    byte[] fileData = new byte[bytesRead - parts[0].Length - 1];
            //    Array.Copy(buffer, parts[0].Length + 1, fileData, 0, fileData.Length);

            //    System.IO.File.WriteAllBytes("Received_" + fileName, fileData);
            //    BroadcastMessage(username + " sent a file: " + fileName, client);
            //}
        }

    }
}
