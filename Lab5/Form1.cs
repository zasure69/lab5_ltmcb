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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Lab5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            using (SmtpClient smtpClient = new SmtpClient("127.0.0.1"))
            {
                if (tbFrom.Text.Length == 0 || tbTo.Text.Length == 0 || tbPwd.Text.Length == 0 || tbSubject.Text.Length == 0 || rtbBody.Text.Length == 0)
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                    return;
                }
                if (!tbFrom.Text.Contains("@"))
                {
                    MessageBox.Show("Thông tin nhập trường From phải là email");
                    return;
                }
                if (!tbFrom.Text.EndsWith("@nhomHH.nt106"))
                {
                    MessageBox.Show("Email gửi phải thuộc domain @nhomHH.nt106");
                    return;
                }
                if (!tbTo.Text.Contains("@"))
                {
                    MessageBox.Show("Thông tin nhập trường To phải là email");
                    return;
                }
                if (!tbTo.Text.EndsWith("@nhomHH.nt106"))
                {
                    MessageBox.Show("Email nhận phải thuộc domain @nhomHH.nt106");
                    return;
                }
                string mailfrom = tbFrom.Text.ToString().Trim();
                string mailto = tbTo.Text.ToString().Trim();
                string password = tbPwd.Text.ToString().Trim();
                var basicCredential = new NetworkCredential(mailfrom, password);
                try
                {
                    using (MailMessage message = new MailMessage())
                    {
                        MailAddress fromAddress = new MailAddress(mailfrom);
                        smtpClient.UseDefaultCredentials = false;
                        smtpClient.Credentials = basicCredential;
                        message.From = fromAddress;
                        message.Subject = tbSubject.Text.ToString().Trim();
                        // Set IsBodyHtml to true means you can send HTML email.
                        message.IsBodyHtml = true;
                        message.Body = rtbBody.Text.ToString();
                        message.To.Add(mailto);
                        tbFrom.Clear();
                        tbTo.Clear();
                        tbPwd.Clear();
                        tbSubject.Clear();
                        rtbBody.Clear();
                        try
                        {
                            smtpClient.Send(message);
                            MessageBox.Show("Mail gửi thành công");
                        }
                        catch (Exception ex)
                        {
                            //MessageBox.Show(ex.Message.ToString());
                            if (ex.Message.ToString().Contains("Authentication required"))
                            {
                                MessageBox.Show("Sai mật khẩu");
                            }
                        }
                    }
                } catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
                
            }
        }
    }
}
