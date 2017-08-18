namespace StockUtilDaemon
{
    partial class Login
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            this.kiWoomApi = new AxKHOpenAPILib.AxKHOpenAPI();
            this.loginButton = new System.Windows.Forms.Button();
            this.loginStatus = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.kiWoomApi)).BeginInit();
            this.SuspendLayout();
            // 
            // kiWoomApi
            // 
            this.kiWoomApi.Enabled = true;
            this.kiWoomApi.Location = new System.Drawing.Point(105, 147);
            this.kiWoomApi.Name = "kiWoomApi";
            this.kiWoomApi.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("kiWoomApi.OcxState")));
            this.kiWoomApi.Size = new System.Drawing.Size(10, 10);
            this.kiWoomApi.TabIndex = 0;
            // 
            // loginButton
            // 
            this.loginButton.Location = new System.Drawing.Point(12, 118);
            this.loginButton.Name = "loginButton";
            this.loginButton.Size = new System.Drawing.Size(203, 23);
            this.loginButton.TabIndex = 2;
            this.loginButton.Text = "로그인";
            this.loginButton.UseVisualStyleBackColor = true;
            this.loginButton.Click += new System.EventHandler(this.loginButtonClick);
            // 
            // loginStatus
            // 
            this.loginStatus.Location = new System.Drawing.Point(12, 39);
            this.loginStatus.Name = "loginStatus";
            this.loginStatus.Size = new System.Drawing.Size(203, 21);
            this.loginStatus.TabIndex = 3;
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(227, 162);
            this.Controls.Add(this.loginStatus);
            this.Controls.Add(this.loginButton);
            this.Controls.Add(this.kiWoomApi);
            this.Name = "Login";
            this.Text = "Login";
            ((System.ComponentModel.ISupportInitialize)(this.kiWoomApi)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private AxKHOpenAPILib.AxKHOpenAPI kiWoomApi;
        private System.Windows.Forms.Button loginButton;
        private System.Windows.Forms.TextBox loginStatus;
    }
}

