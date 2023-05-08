using System;
using System.Configuration;
using System.Windows.Forms;

namespace OTPGenerator
{
    public partial class FrmMain : Form
    {
        private readonly Dragonfly.Core.Security.IOtpService _service;
        private readonly Dragonfly.Core.Security.User _user;

        public FrmMain()
        {
            InitializeComponent();
            _service = new Dragonfly.Core.Security.OtpService();
            var key = ConfigurationManager.AppSettings["otp"];
            var user = ConfigurationManager.AppSettings["userName"];
            _user = new Dragonfly.Core.Security.User
            {
                OtpKey = key,
                OtpValidTime = 60
            };
            progressBar1.Maximum = _user.OtpValidTime;
            lblOTP.Text = _service.GetOtp(_user);
            progressBar1.Value = DateTime.Now.Second;
            timer1.Enabled = true;
            lblUserName.Text = user;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblOTP.Text = _service.GetOtp(_user);
            progressBar1.Value = DateTime.Now.Second;
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            TopMost = true;
        }

        private void lblOTP_Click(object sender, EventArgs e)
        {

        }
    }
}
