﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Net.Mail;
using System.Net;

namespace Main
{
    public partial class ForgotPassForm : Form
    {
        string newPass = "";
        public ForgotPassForm()
        {
            InitializeComponent();
        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void btnClick_Click(object sender, EventArgs e)
        {
            string emailRecovery = "hoangduytran24@gmail.com";
            string email = txtEmail.Text.Trim();
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            bool isValidEmail = Regex.IsMatch(email, pattern);

            if (isValidEmail && email == emailRecovery)
            {
                // Gửi email khôi phục mật khẩu nếu email hợp lệ
                SendRecoveryEmail(email);
                Function.UpdateDataQuery("UPDATE TaiKhoan SET matKhau = '"+ newPass + "' WHERE maTaiKhoan = 'PB123'");
            }
            else
            {
                MessageBox.Show("Email khôi phục không đúng hoặc không hợp lệ.", "Thông báo");
            }
        }

        private void SendRecoveryEmail(string recipientEmail)
        {
            try
            {
                var fromAddress = new MailAddress("damminhchien220204@gmail.com", "ChienLoveYou!");
                var fromPassword = "gpnwcmxfmekowuvv"; // Mật khẩu ứng dụng email
                string newPassword = GenerateNewPassword(); // Tạo mật khẩu mới
                newPass = newPassword;
                var subject = "Khôi phục mật khẩu";
                var body = $"Mật khẩu mới của bạn là: {newPassword}";

                // Cấu hình SMTP
                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com", // Máy chủ SMTP (ví dụ: smtp.gmail.com cho Gmail)
                    Port = 587, // Cổng SMTP (587 cho TLS)
                    EnableSsl = true, // Bật SSL
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };

                // Gửi email
                using (var message = new MailMessage(fromAddress, new MailAddress(recipientEmail))
                {
                    Subject = subject,
                    Body = body
                })
                {
                    smtp.Send(message);
                }

                MessageBox.Show("Email khôi phục mật khẩu đã được gửi thành công!", "Thông báo");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Có lỗi xảy ra khi gửi email: {ex.Message}", "Lỗi");
            }
        }

        private string GenerateNewPassword()
        {
            var random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, 8)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private void label3_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
            this.Close();
        }
    }
}