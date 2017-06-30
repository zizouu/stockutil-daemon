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

                ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
                ////                                                                                                       //// 
                ////                                            Market Code                                                //// 
                ////   0 : 장내, 3 : ELW, 4 : 뮤추얼펀드, 5 : 신주인수권, 6 : 리츠, 8 : ETF, 9 : 하이일펀드, 10 : 코스닥   ////
                ////                                                                                                       ////
                ///////////////////////////////////////////////////////////////////////////////////////////////////////////////

                Dictionary<String, String> kospiDic = getCodeDictionaryByMarketNum("0");
                Dictionary<String, String> elwDic = getCodeDictionaryByMarketNum("3");
                Dictionary<String, String> mutualFundDic = getCodeDictionaryByMarketNum("4");
                Dictionary<String, String> newStockDic = getCodeDictionaryByMarketNum("5");
                Dictionary<String, String> reitDic = getCodeDictionaryByMarketNum("6");
                Dictionary<String, String> etfDic = getCodeDictionaryByMarketNum("8");
                Dictionary<String, String> highYieldDic = getCodeDictionaryByMarketNum("9");
                Dictionary<String, String> kosdaqDic = getCodeDictionaryByMarketNum("10");

                Dictionary<String, Dictionary<String, String>> codeListDic = new Dictionary<String, Dictionary<String, String>>();
                codeListDic.Add("0", kospiDic);
                codeListDic.Add("3", elwDic);
                codeListDic.Add("4", mutualFundDic);
                codeListDic.Add("5", newStockDic);
                codeListDic.Add("6", reitDic);
                codeListDic.Add("8", etfDic);
                codeListDic.Add("9", highYieldDic);
                codeListDic.Add("10", kosdaqDic);

                removeCodeByMarketNum(codeListDic, "0", "4");
                removeCodeByMarketNum(codeListDic, "0", "8");
                
                setCodeListBox(kospiCodeListBox, kospiDic, "0");
                setCodeListBox(elwCodeListBox, elwDic, "3");
                setCodeListBox(mutualFundCodeListBox, mutualFundDic, "4");
                setCodeListBox(newStockCodeListBox, newStockDic, "5");
                setCodeListBox(reitCodeListBox, reitDic, "6");
                setCodeListBox(etfCodeListBox, etfDic, "8");
                setCodeListBox(highYieldCodeListBox, highYieldDic, "9");
                //setCodeListBox(kosdaqCodeListBox, kosdaqDic, "10");

                requestStockDataByCode("249420", "20170628");
            }
            else
            {
                this.listBox1.Items.Add("로그인 실패");
            }
        }

        public void requestStockDataByCode(String code, String date)
        {
            axKHOpenAPI1.SetInputValue("종목코드", code);
            axKHOpenAPI1.SetInputValue("기준일자", date);
            axKHOpenAPI1.SetInputValue("수정주가구분", "0");
            axKHOpenAPI1.CommRqData("일봉조회", "opt10081", 0, "1002");
        }

        public void removeCodeByMarketNum(Dictionary<String, Dictionary<String, String>> codeDic, String fromMarketNum, String toMarketNum)
        {
            Dictionary<String, String> fromDic = codeDic[fromMarketNum];
            Dictionary<String, String> toDic = codeDic[toMarketNum];

            foreach (KeyValuePair<String, String> data in toDic)
            {
                fromDic.Remove(data.Key);
            }
        }

        public Dictionary<String, String> getCodeDictionaryByMarketNum(String marketNum)
        {
            DataUtil util = new DataUtil();
            String codeData = axKHOpenAPI1.GetCodeListByMarket(marketNum);
            List<String> codeList = util.parseCodeData(codeData);
            Dictionary<String, String> codeDic = new Dictionary<String, String>();
            codeList.RemoveAt(codeList.Count - 1);

            foreach (String code in codeList)
            {
                String name = axKHOpenAPI1.GetMasterCodeName(code);
                if (marketNum.Equals("0") && code[0] == '5')
                {
                    continue;
                }
                codeDic.Add(code, name);
            }
            return codeDic;
        }

        public void setCodeListBox(ListBox listBox, Dictionary<String, String> codeDic, String marketNum)
        {
            listBox.Items.Add("     " + getMarketName(marketNum) + " 종목수 : " + codeDic.Count);
            listBox.Items.Add(" ");

            foreach (KeyValuePair<String, String> data in codeDic)
            {
                listBox.Items.Add(data.Key + "   " + data.Value);
            }
        }

        public String getMarketName(String marketNum)
        {
            String marketName;
            switch (marketNum)
            {
                case "0":
                    marketName = "KOSPI";
                    break;
                case "3":
                    marketName = "ELW";
                    break;
                case "4":
                    marketName = "뮤추얼펀드";
                    break;
                case "5":
                    marketName = "신주인수권";
                    break;
                case "6":
                    marketName = "리츠";
                    break;
                case "8":
                    marketName = "ETF";
                    break;
                case "9":
                    marketName = "하이일드펀드";
                    break;
                case "10":
                    marketName = "코스닥";
                    break;
                default:
                    marketName = "NULL";
                    break;
            }
            return marketName;
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
            }else if(e.sRQName == "일봉조회")
            {
                int cnt = axKHOpenAPI1.GetRepeatCnt(e.sTrCode, e.sRQName);
                for(int idx = 0; idx < cnt; idx++)
                {
                    this.kosdaqCodeListBox.Items.Add("종목코드" + getReceiveDataByName(e, idx, "종목코드"));
                    this.kosdaqCodeListBox.Items.Add("현재가" + getReceiveDataByName(e, idx, "현재가"));
                    this.kosdaqCodeListBox.Items.Add("거래량" + getReceiveDataByName(e, idx, "거래량"));
                    this.kosdaqCodeListBox.Items.Add("거래대금" + getReceiveDataByName(e, idx, "거래대금"));
                    this.kosdaqCodeListBox.Items.Add("일자" + getReceiveDataByName(e, idx, "일자"));
                    this.kosdaqCodeListBox.Items.Add("고가" + getReceiveDataByName(e, idx, "고가"));
                    this.kosdaqCodeListBox.Items.Add("저가" + getReceiveDataByName(e, idx, "저가"));
                    this.kosdaqCodeListBox.Items.Add("종목정보" + getReceiveDataByName(e, idx, "종목정보"));
                    this.kosdaqCodeListBox.Items.Add("대업종구분" + getReceiveDataByName(e, idx, "대업종구분"));
                    this.kosdaqCodeListBox.Items.Add("소업종구분" + getReceiveDataByName(e, idx, "소업종구분"));
                }
                
            }
        }

        public String getReceiveDataByName(AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveTrDataEvent e, int index, String name)
        {
            return axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, index, name);
        }
    }
}
