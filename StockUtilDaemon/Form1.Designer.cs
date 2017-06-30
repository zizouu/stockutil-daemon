namespace StockUtilDaemon
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.axKHOpenAPI1 = new AxKHOpenAPILib.AxKHOpenAPI();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.codeReqButton = new System.Windows.Forms.Button();
            this.kospiCodeListBox = new System.Windows.Forms.ListBox();
            this.kosdaqCodeListBox = new System.Windows.Forms.ListBox();
            this.elwCodeListBox = new System.Windows.Forms.ListBox();
            this.etfCodeListBox = new System.Windows.Forms.ListBox();
            this.highYieldCodeListBox = new System.Windows.Forms.ListBox();
            this.reitCodeListBox = new System.Windows.Forms.ListBox();
            this.mutualFundCodeListBox = new System.Windows.Forms.ListBox();
            this.newStockCodeListBox = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.axKHOpenAPI1)).BeginInit();
            this.SuspendLayout();
            // 
            // axKHOpenAPI1
            // 
            this.axKHOpenAPI1.Enabled = true;
            this.axKHOpenAPI1.Location = new System.Drawing.Point(-1, 660);
            this.axKHOpenAPI1.Name = "axKHOpenAPI1";
            this.axKHOpenAPI1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axKHOpenAPI1.OcxState")));
            this.axKHOpenAPI1.Size = new System.Drawing.Size(10, 10);
            this.axKHOpenAPI1.TabIndex = 0;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(107, 19);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(75, 16);
            this.listBox1.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "로그인";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(31, 649);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(151, 21);
            this.textBox1.TabIndex = 3;
            // 
            // codeReqButton
            // 
            this.codeReqButton.Location = new System.Drawing.Point(872, 634);
            this.codeReqButton.Name = "codeReqButton";
            this.codeReqButton.Size = new System.Drawing.Size(75, 23);
            this.codeReqButton.TabIndex = 4;
            this.codeReqButton.Text = "코드요청";
            this.codeReqButton.UseVisualStyleBackColor = true;
            // 
            // kospiCodeListBox
            // 
            this.kospiCodeListBox.FormattingEnabled = true;
            this.kospiCodeListBox.ItemHeight = 12;
            this.kospiCodeListBox.Location = new System.Drawing.Point(570, 80);
            this.kospiCodeListBox.Name = "kospiCodeListBox";
            this.kospiCodeListBox.Size = new System.Drawing.Size(179, 544);
            this.kospiCodeListBox.TabIndex = 5;
            // 
            // kosdaqCodeListBox
            // 
            this.kosdaqCodeListBox.FormattingEnabled = true;
            this.kosdaqCodeListBox.ItemHeight = 12;
            this.kosdaqCodeListBox.Location = new System.Drawing.Point(762, 80);
            this.kosdaqCodeListBox.Name = "kosdaqCodeListBox";
            this.kosdaqCodeListBox.Size = new System.Drawing.Size(185, 544);
            this.kosdaqCodeListBox.TabIndex = 6;
            // 
            // elwCodeListBox
            // 
            this.elwCodeListBox.FormattingEnabled = true;
            this.elwCodeListBox.ItemHeight = 12;
            this.elwCodeListBox.Location = new System.Drawing.Point(385, 80);
            this.elwCodeListBox.Name = "elwCodeListBox";
            this.elwCodeListBox.Size = new System.Drawing.Size(179, 268);
            this.elwCodeListBox.TabIndex = 7;
            // 
            // etfCodeListBox
            // 
            this.etfCodeListBox.FormattingEnabled = true;
            this.etfCodeListBox.ItemHeight = 12;
            this.etfCodeListBox.Location = new System.Drawing.Point(385, 356);
            this.etfCodeListBox.Name = "etfCodeListBox";
            this.etfCodeListBox.Size = new System.Drawing.Size(179, 268);
            this.etfCodeListBox.TabIndex = 8;
            // 
            // highYieldCodeListBox
            // 
            this.highYieldCodeListBox.FormattingEnabled = true;
            this.highYieldCodeListBox.ItemHeight = 12;
            this.highYieldCodeListBox.Location = new System.Drawing.Point(200, 80);
            this.highYieldCodeListBox.Name = "highYieldCodeListBox";
            this.highYieldCodeListBox.Size = new System.Drawing.Size(179, 268);
            this.highYieldCodeListBox.TabIndex = 9;
            // 
            // listBox2
            // 
            this.reitCodeListBox.FormattingEnabled = true;
            this.reitCodeListBox.ItemHeight = 12;
            this.reitCodeListBox.Location = new System.Drawing.Point(200, 356);
            this.reitCodeListBox.Name = "listBox2";
            this.reitCodeListBox.Size = new System.Drawing.Size(179, 268);
            this.reitCodeListBox.TabIndex = 10;
            // 
            // listBox3
            // 
            this.mutualFundCodeListBox.FormattingEnabled = true;
            this.mutualFundCodeListBox.ItemHeight = 12;
            this.mutualFundCodeListBox.Location = new System.Drawing.Point(12, 80);
            this.mutualFundCodeListBox.Name = "listBox3";
            this.mutualFundCodeListBox.Size = new System.Drawing.Size(179, 268);
            this.mutualFundCodeListBox.TabIndex = 11;
            // 
            // listBox4
            // 
            this.newStockCodeListBox.FormattingEnabled = true;
            this.newStockCodeListBox.ItemHeight = 12;
            this.newStockCodeListBox.Location = new System.Drawing.Point(15, 356);
            this.newStockCodeListBox.Name = "listBox4";
            this.newStockCodeListBox.Size = new System.Drawing.Size(179, 268);
            this.newStockCodeListBox.TabIndex = 12;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(959, 669);
            this.Controls.Add(this.newStockCodeListBox);
            this.Controls.Add(this.mutualFundCodeListBox);
            this.Controls.Add(this.reitCodeListBox);
            this.Controls.Add(this.highYieldCodeListBox);
            this.Controls.Add(this.etfCodeListBox);
            this.Controls.Add(this.elwCodeListBox);
            this.Controls.Add(this.kosdaqCodeListBox);
            this.Controls.Add(this.kospiCodeListBox);
            this.Controls.Add(this.codeReqButton);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.axKHOpenAPI1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.axKHOpenAPI1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private AxKHOpenAPILib.AxKHOpenAPI axKHOpenAPI1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button codeReqButton;
        private System.Windows.Forms.ListBox kospiCodeListBox;
        private System.Windows.Forms.ListBox kosdaqCodeListBox;
        private System.Windows.Forms.ListBox elwCodeListBox;
        private System.Windows.Forms.ListBox etfCodeListBox;
        private System.Windows.Forms.ListBox highYieldCodeListBox;
        private System.Windows.Forms.ListBox reitCodeListBox;
        private System.Windows.Forms.ListBox mutualFundCodeListBox;
        private System.Windows.Forms.ListBox newStockCodeListBox;
    }
}

