using NasleGhalam.Common;
using NasleGhalam.ServiceLayer.Services;
using NasleGhalam.ViewModels.User;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
        private readonly EducationTreeService _educationTreeService;
        private readonly WebService _webService;

        public Main(LessonService lessonService, EducationTreeService educationTreeService, WebService WebService)
        {
            _lessonService = lessonService;
            _educationTreeService = educationTreeService;
            _webService = WebService;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            QuestionGroup form = new QuestionGroup(_lessonService,_educationTreeService);
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
                label_loginStatus.Text = "ورود با موفقیت انجام شده.";
            }
            else
            {
                label_loginStatus.ForeColor = Color.Red;
                label_loginStatus.Text = "ورود انجام نشده است لطفا اول به سامانه وارد شوید.";
            }

        }
    }
}
