using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab5
{
    public partial class Bai3 : Form
    {
        public Bai3()
        {
            InitializeComponent();
        }

        private void btnAttach_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    tbAttach.Text = ofd.FileName;
                }
            }  
        }

        private void CleanUp()
        {
            tbFrom.Text = "";
            tbTo.Text = "";
            tbSubject.Text = "";
            tbPassword.Text = "";
            rtbBody.Text = "";
            tbAttach.Text = "";
        }

        private void SendMail(string subject, string body, string mailto, string mailfrom, string password, string attach)
        {
            using (SmtpClient smtpClient = new SmtpClient("smtp.gmail.com"))
            {
                smtpClient.Port = 587;
                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(mailfrom, password);

                using (MailMessage message = new MailMessage())
                {
                    MailAddress fromAddress = new MailAddress(mailfrom);
                    message.From = fromAddress;
                    message.Subject = subject;
                    message.IsBodyHtml = true;
                    message.To.Add(mailto);
                    message.Body = body;

                    if (attach != "")
                    {
                        Attachment attachment = new Attachment(attach);
                        message.Attachments.Add(attachment);
                    }

                    try
                    {
                        
                        smtpClient.Send(message);
                        MessageBox.Show("Send successfully!");
                        

                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.ToString());
                        CleanUp();
                    }
                }
            }

        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            string mailfrom = tbFrom.Text.Trim();
            string mailto = tbTo.Text.Trim();
            string password = tbPassword.Text.Trim();
            string subject = tbSubject.Text.Trim();
            string body = rtbBody.Text.Trim();
            string attach = tbAttach.Text.Trim();
            if (tbFrom.Text.Length == 0 && tbTo.Text.Length == 0 && tbSubject.Text.Length == 0 && rtbBody.Text.Length == 0)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!","Warning");
                return;
            }
            else if (mailfrom == "")
            {
                MessageBox.Show("Hãy điền email từ", "Warning");
                return;
            }
            else if (mailto == "")
            {
                MessageBox.Show("Hãy điền email tới", "Warning");
                return;
            }
            else if (password == "")
            {
                MessageBox.Show("Hãy điền password!", "Warning");
                return;
            }
            else if (subject == "")
            {
                MessageBox.Show("Hãy điền chủ đề!", "Warning");
                return;
            }
            else if (body == "")
            {
                MessageBox.Show("Hãy điền nội dung vô phần body", "Warning");
                return;
            }

            CleanUp();

            try
            {
                SendMail(subject, body, mailto, mailfrom, password, attach);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tbSubject_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
