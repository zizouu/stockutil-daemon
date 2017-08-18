using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace StockUtilDaemon
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            this.kiWoomApi.OnEventConnect += this.onLoginSuccess;
        }

        private void loginButtonClick(object sender, EventArgs e)
        {
            if(kiWoomApi.CommConnect() == 0)
            {
                this.loginStatus.Text = "로그인 시작";
            }
            else
            {
                this.loginStatus.Text = "로그인 실패";
            }
        }

        private void onLoginSuccess(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnEventConnectEvent e)
        {
            if (e.nErrCode == 0)
            {
                this.loginStatus.Text = "로그인 성공";
                if(this.kiWoomApi.GetConnectState() == 1)
                {
                    this.loginStatus.Text = "접속상태 : 연결중";
                    LogUtil.logConsole("Login.cs onLoginSuccess");
                    StockDataCollector collector = new StockDataCollector(kiWoomApi);
                    collector.startCollect();
                }
                else if(this.kiWoomApi.GetConnectState() == 0)
                {
                    this.loginStatus.Text = "접속상태 : 미연결";
                }
            }
            else
            {
                this.loginStatus.Text = "로그인 실패";
            }
        }
    }
}
