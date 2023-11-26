using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ViewModels
{
    public class MailMessageViewModel
    {
        public string SmtpCliente { get; set; } = "smtp.office365.com";
        public string UserSender { get; set; } = "";
        public string UserPass { get; set; } = "";
        public string SendTo { get; set; }
        public string Title { get; set; }
        public string HTMLContent { get; set; }
    }
}
