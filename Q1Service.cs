using System;
using System.Configuration;
using System.ServiceProcess;
using System.Threading;
using System.IO;
using System.Net;
using System.Net.Mail;
using Newtonsoft.Json;
using System.ComponentModel;

namespace K181185_QS1
{
    [RunInstaller(true)]
    public partial class Q1Service : ServiceBase
    {
        int ScheduleInterval = Convert.ToInt32(ConfigurationManager.AppSettings["ThreadTime"]);

        public Thread worker = null;
        public Q1Service()
        {
            InitializeComponent();
        }

       

        protected override void OnStart(string[] args)
        {
            try
            {
                ThreadStart start = new ThreadStart(Working);
                worker = new Thread(start);
                worker.Start();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void Working()
        {
            while (true)
            {
                ReadJsonFile();
                Thread.Sleep(ScheduleInterval * 60 * 1000);
            }
        }

        public void ReadJsonFile()
        {

            string path = ConfigurationManager.AppSettings["path"].ToString(); //give path to directory

            foreach (string file in Directory.EnumerateFiles(path, "*.json"))
            {
                string jsonString = File.ReadAllText(file);
                JSONClass jsonObject = JsonConvert.DeserializeObject<JSONClass>(jsonString);

                SendMail(jsonObject.To, jsonObject.Subject, jsonObject.MessageBody);
            }
        }


        public void SendMail(string toAddress, string subject, string MessageBody)
        {
            //reading from web.config
            var fromAddr = ConfigurationManager.AppSettings["FromMail"].ToString();
            var Password = ConfigurationManager.AppSettings["Password"].ToString();

            MailMessage mailMsg = new MailMessage(); //creating new MailMessage object
            mailMsg.To.Add(new MailAddress(toAddress)); //defined in json file
            mailMsg.From = new MailAddress(fromAddr); //defined in web.config
            mailMsg.Subject = subject; //read from json file
            mailMsg.Body = MessageBody; //read from json file

            var smtp = new SmtpClient
            {

                Host = ConfigurationManager.AppSettings["Host"].ToString(),
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddr, Password)
            };
            {
                smtp.Send(mailMsg);
            }
        }


            public void onDebug()
        {
            OnStart(null);
        }

        protected override void OnStop()
        {
            try
            {
                if ((worker != null) & worker.IsAlive)
                {
                    worker.Abort();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            // 
            // button1
            // 
            this.button1.Name = "button1";
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // Q1Service
            // 
            this.ServiceName = "Q1Service";

        }
    }
}
