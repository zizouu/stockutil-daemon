using System;
using System.Windows.Forms;
using System.Threading;
using System.Threading.Tasks;


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
                    this.collectStart();
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

        private void collectStart()
        {
            StockDataCollector collector = new StockDataCollector(this.kiWoomApi);
            CollectorCheckJob checkJob = new CollectorCheckJob(this, collector);
            Thread collectorThread = new Thread(collector.startCollect);
            Thread checkerThread = new Thread(checkJob.checkCollectorStatus);
            collectorThread.Start();
            checkerThread.Start();
        }
        
        public String getCollectorStatus()
        {
            return this.statusBox.Text;
        }

        delegate void SetCollectorStatusDelegator(String text);

        public void setCollectorStatus(String text)
        {
            if (this.statusBox.InvokeRequired)
            {
                SetCollectorStatusDelegator call = new SetCollectorStatusDelegator(this.setCollectorStatus);
                this.Invoke(call, text);
            }
            else
            {
                this.statusBox.Text = text;
            }
        }
    }
}
