namespace OfficeRentApp.Helpers { 
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;
    using OfficeRentApp.DTO;

    public class EmailSender
    {
        private readonly IConfiguration _config;
        public EmailSender(IConfiguration configuration) 
        {
            _config = configuration;
        }
        public void SendEmail(EmailDto emailDto, int sentCode)
        {
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress("Me", _config["EmailConfiguration:From"]));
            email.To.Add(new MailboxAddress("You", emailDto.Email));

            email.Subject = "Forgot Password?";
            email.Body = new TextPart(MimeKit.Text.TextFormat.Plain)
            {
                Text = $"Your reset code is: {sentCode}"
            };
            using (var smtp = new SmtpClient())
            {
                smtp.Connect(_config["EmailConfiguration:SmtpServer"], Convert.ToInt32(_config["EmailConfiguration:Port"]), false);
                smtp.Authenticate(_config["EmailConfiguration:From"], _config["EmailConfiguration:Password"]);
                // Note: only needed if the SMTP server requires authentication
                smtp.Send(email);
                smtp.Disconnect(true);
            }
        }
    }
}
