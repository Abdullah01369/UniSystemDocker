using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using WorkerService.Abstracts;
using WorkerService.EmailModels;

namespace WorkerService.Services
{
    public class EmailServiceClient : IEmailServiceClient
    {
        private readonly EmailSettings _settings;

        public EmailServiceClient(IOptions<EmailSettings> options)
        {
            _settings = options.Value;

        }
        public async Task SendMailAsync(string emailcontent, string to)
        {
            string style = @" 
<head>
<meta charset=""UTF-8"">
<meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
<title>Şifre Sıfırlama</title>
<style>
  body {
    font-family: Arial, sans-serif;
    margin: 0;
    padding: 0;
    background-color: #f4f4f4;
  }
  .container {
    max-width: 600px;
    margin: 20px auto;
    padding: 20px;
    background-color: #ffffff;
    border-radius: 8px;
    box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
  }
  .header {
    background-color: #28a745;
    color: #ffffff;
    text-align: center;
    padding: 15px 0;
    border-radius: 8px 8px 0 0;
  }
  .header h1 {
    margin: 0;
    font-size: 24px;
  }
  .content {
    padding: 20px;
    color: #333333;
    line-height: 1.6;
  }
  .content a {
    display: inline-block;
    margin: 20px 0;
    padding: 10px 20px;
    background-color: #007bff;
    color: #ffffff;
    text-decoration: none;
    border-radius: 5px;
    font-weight: bold;
  }
  .content a:hover {
    background-color: #0056b3;
  }
  .footer {
    background-color: #f8f9fa;
    color: #6c757d;
    padding: 10px;
    text-align: center;
    font-size: 12px;
    border-radius: 0 0 8px 8px;
  }
</style>
</head>
 ";

            string body = $@"<body>
<div class=""container"">
  <div class=""header"">
    <h1>Şifre Sıfırlama</h1>
  </div>
  <div class=""content"">
    <p>Merhaba,</p>
    <p>Hesabınızın şifresini sıfırlamak için aşağıdaki butona tıklayın:</p>
  {emailcontent}
    <p>Eğer bu işlemi siz başlatmadıysanız, lütfen bu e-postayı dikkate almayın.</p>
    <p>Teşekkürler,<br>Destek Ekibi</p>
  </div>
  <div class=""footer"">
    <p>Bu e-posta otomatik olarak oluşturulmuştur, lütfen cevaplamayın.</p>
  </div>
</div>
</body>";


            string content = $@"

<!DOCTYPE html>
<html lang=""en"">
<h3> bu bir deneme mailidir, yanlış mail girilmesi durumunda size gelmiş olabilir, ciddiye almayın lütfen,öğrenciyim; sistemi öğrenmeye çalışıyorum :)</h3>
{style}
{body}
</html>
";

            var smtpclient = new SmtpClient();
            to = to ?? string.Empty;
            smtpclient.Host = _settings.host;
            smtpclient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpclient.UseDefaultCredentials = false;
            smtpclient.Port = 587;
            smtpclient.Credentials = new NetworkCredential(_settings.email, _settings.password);
            smtpclient.EnableSsl = true;

            var mailmessage = new MailMessage();


            mailmessage.From = new MailAddress(_settings.email);
            mailmessage.To.Add(to);
            mailmessage.Subject = "Şifre Sıfırlama";
            mailmessage.Body = content;

            mailmessage.IsBodyHtml = true;

            await smtpclient.SendMailAsync(mailmessage);

        }
    }
}
