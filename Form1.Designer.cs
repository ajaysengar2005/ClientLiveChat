namespace ChatClientGUI
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            richTextBox2 = new RichTextBox();
            textBox1 = new TextBox();
            button1 = new Button();
            button2 = new Button();
            SuspendLayout();
            // 
            // richTextBox2
            // 
            richTextBox2.Location = new Point(78, 34);
            richTextBox2.Name = "richTextBox2";
            richTextBox2.ReadOnly = true;
            richTextBox2.Size = new Size(608, 125);
            richTextBox2.TabIndex = 2;
            richTextBox2.Text = "";
            // 
            // textBox1
            // 
            textBox1.Dock = DockStyle.Bottom;
            textBox1.Location = new Point(0, 423);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(769, 27);
            textBox1.TabIndex = 3;
            // 
            // button1
            // 
            button1.Location = new Point(78, 199);
            button1.Name = "button1";
            button1.Size = new Size(110, 49);
            button1.TabIndex = 4;
            button1.Text = "Send";
            button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Location = new Point(78, 285);
            button2.Name = "button2";
            button2.Size = new Size(110, 46);
            button2.TabIndex = 5;
            button2.Text = "Send File";
            button2.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(769, 450);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(textBox1);
            Controls.Add(richTextBox2);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private RichTextBox richTextBox2;
        private TextBox textBox1;
        private Button button1;
        private Button button2;
    }
}
