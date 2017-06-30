using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace StockUtilDaemon
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            this.axKHOpenAPI1.OnEventConnect += this.axKHOpenAPI_OnEventConnect;
            this.axKHOpenAPI1.OnReceiveTrData += this.axKHOpenAPI_OnReceiveTrData;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(axKHOpenAPI1.CommConnect() == 0)
            {
                this.listBox1.Items.Add("로그인 시작");
            }
            else
            {
                this.listBox1.Items.Add("로그인 실패");
            }
        }

        private void axKHOpenAPI_OnEventConnect(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnEventConnectEvent e)
        {
            if (e.nErrCode == 0)
            {
                this.listBox1.Items.Add("로그인 성공");
                if(this.axKHOpenAPI1.GetConnectState() == 1)
                {
                    this.listBox1.Items.Add("접속상태 : 연결중");
                }
                else if(this.axKHOpenAPI1.GetConnectState() == 0)
                {
                    this.listBox1.Items.Add("접속상태 : 미연결");
                }
                //String codList = axKHOpenAPI1.GetCodeListByMarket("0");
                String code = "009680";
                String name = axKHOpenAPI1.GetMasterCodeName(code);
                String price = axKHOpenAPI1.GetMasterLastPrice(code);
                //this.textBox1.AppendText(name + " 의어제 가격 : " + price);

                axKHOpenAPI1.SetInputValue("종목코드", "109740");
                axKHOpenAPI1.CommRqData("테스트", "OPT10001", 0, "1001");
            }
            else
            {
                this.listBox1.Items.Add("로그인 실패");
            }
        }

        private void axKHOpenAPI_OnReceiveTrData(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveTrDataEvent e)
        {
            if(e.sRQName == "테스트")
            {
                this.listBox1.Items.Add("종목코드" + axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "종목코드"));
                this.listBox1.Items.Add("종목명" + axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "종목명"));
                this.listBox1.Items.Add("거래량" + axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "거래량"));
                this.listBox1.Items.Add("시가" + axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "시가"));
                this.listBox1.Items.Add("종가" + axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "종가"));
                this.listBox1.Items.Add("등락율" + axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "등락율"));
            }
        }
    }
}
