using NasleGhalam.Common;
using NasleGhalam.ServiceLayer.Services;
using NasleGhalam.ViewModels.User;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Deployment.Application;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NasleGhalam.WindowsApp
{
    public partial class Main : Form
    {
        private readonly LessonService _lessonService;
        private readonly WriterService _writerService;
        private readonly EducationTreeService _educationTreeService;
        private readonly WebService _webService;

        public Main(LessonService lessonService, EducationTreeService educationTreeService, WebService WebService , WriterService writerService)
        {
            _lessonService = lessonService;
            _educationTreeService = educationTreeService;
            _webService = WebService;
            _writerService = writerService;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            QuestionGroup form = new QuestionGroup(_lessonService,_educationTreeService,_webService , _writerService);
            form.ShowDialog();

        }

        private async void button2_Click(object sender, EventArgs e)
        {

            var result = await _webService.Login(new LoginViewModel()
            {
                UserName = textBox_username.Text,
                Password = textBox_password.Text
            });

            if(result.MessageType == MessageType.Success)
            {
                label_loginStatus.ForeColor = Color.Green;
                button1.Enabled = true;
                label_loginStatus.Text = "ورود با موفقیت انجام شده.";
            }
            else
            {
                label_loginStatus.ForeColor = Color.Red;
                label_loginStatus.Text = "ورود انجام نشده است لطفا اول به سامانه وارد شوید.";
            }

        }

        private void Main_Load(object sender, EventArgs e)
        {
                string path = System.Windows.Forms.Application.StartupPath + "\\content\\";
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
                System.IO.Directory.CreateDirectory(System.IO.Path.Combine(path, "questionGroupTemp"));
            }
        }
    }
}
