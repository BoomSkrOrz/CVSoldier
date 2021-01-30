
namespace CVSoldier
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btn_ChangePath = new System.Windows.Forms.Button();
            this.btn_Hook = new System.Windows.Forms.Button();
            this.btn_UnHook = new System.Windows.Forms.Button();
            this.lb_Msg = new System.Windows.Forms.Label();
            this.lb_postion = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btn_Output = new System.Windows.Forms.Button();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rtn_Excel = new System.Windows.Forms.RadioButton();
            this.rtn_TXT = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "保存路径：";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(75, 10);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(396, 21);
            this.textBox1.TabIndex = 1;
            // 
            // btn_ChangePath
            // 
            this.btn_ChangePath.Location = new System.Drawing.Point(477, 8);
            this.btn_ChangePath.Name = "btn_ChangePath";
            this.btn_ChangePath.Size = new System.Drawing.Size(31, 23);
            this.btn_ChangePath.TabIndex = 2;
            this.btn_ChangePath.Text = "...";
            this.btn_ChangePath.UseVisualStyleBackColor = true;
            this.btn_ChangePath.Click += new System.EventHandler(this.btn_ChangePath_Click);
            // 
            // btn_Hook
            // 
            this.btn_Hook.Location = new System.Drawing.Point(514, 8);
            this.btn_Hook.Name = "btn_Hook";
            this.btn_Hook.Size = new System.Drawing.Size(75, 23);
            this.btn_Hook.TabIndex = 3;
            this.btn_Hook.Text = "挂钩子";
            this.btn_Hook.UseVisualStyleBackColor = true;
            this.btn_Hook.Click += new System.EventHandler(this.btn_Hook_Click);
            // 
            // btn_UnHook
            // 
            this.btn_UnHook.Location = new System.Drawing.Point(595, 8);
            this.btn_UnHook.Name = "btn_UnHook";
            this.btn_UnHook.Size = new System.Drawing.Size(75, 23);
            this.btn_UnHook.TabIndex = 4;
            this.btn_UnHook.Text = "解钩子";
            this.btn_UnHook.UseVisualStyleBackColor = true;
            this.btn_UnHook.Click += new System.EventHandler(this.btn_UnHook_Click);
            // 
            // lb_Msg
            // 
            this.lb_Msg.AutoSize = true;
            this.lb_Msg.Location = new System.Drawing.Point(12, 395);
            this.lb_Msg.Name = "lb_Msg";
            this.lb_Msg.Size = new System.Drawing.Size(41, 12);
            this.lb_Msg.TabIndex = 5;
            this.lb_Msg.Text = "提示：";
            // 
            // lb_postion
            // 
            this.lb_postion.AutoSize = true;
            this.lb_postion.Location = new System.Drawing.Point(227, 395);
            this.lb_postion.Name = "lb_postion";
            this.lb_postion.Size = new System.Drawing.Size(47, 12);
            this.lb_postion.TabIndex = 6;
            this.lb_postion.Text = "Mouse：";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1});
            this.dataGridView1.Location = new System.Drawing.Point(15, 37);
            this.dataGridView1.MaximumSize = new System.Drawing.Size(663, 350);
            this.dataGridView1.MinimumSize = new System.Drawing.Size(663, 350);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(663, 350);
            this.dataGridView1.TabIndex = 7;
            // 
            // btn_Output
            // 
            this.btn_Output.Location = new System.Drawing.Point(603, 390);
            this.btn_Output.Name = "btn_Output";
            this.btn_Output.Size = new System.Drawing.Size(75, 23);
            this.btn_Output.TabIndex = 8;
            this.btn_Output.Text = "导出";
            this.btn_Output.UseVisualStyleBackColor = true;
            this.btn_Output.Click += new System.EventHandler(this.btn_Output_Click);
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column1.DataPropertyName = "Info";
            this.Column1.HeaderText = "信息";
            this.Column1.Name = "Column1";
            // 
            // rtn_Excel
            // 
            this.rtn_Excel.AutoSize = true;
            this.rtn_Excel.Checked = true;
            this.rtn_Excel.Location = new System.Drawing.Point(422, 394);
            this.rtn_Excel.Name = "rtn_Excel";
            this.rtn_Excel.Size = new System.Drawing.Size(53, 16);
            this.rtn_Excel.TabIndex = 9;
            this.rtn_Excel.TabStop = true;
            this.rtn_Excel.Text = "Excel";
            this.rtn_Excel.UseVisualStyleBackColor = true;
            // 
            // rtn_TXT
            // 
            this.rtn_TXT.AutoSize = true;
            this.rtn_TXT.Location = new System.Drawing.Point(477, 394);
            this.rtn_TXT.Name = "rtn_TXT";
            this.rtn_TXT.Size = new System.Drawing.Size(41, 16);
            this.rtn_TXT.TabIndex = 10;
            this.rtn_TXT.Text = "TXT";
            this.rtn_TXT.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(690, 418);
            this.Controls.Add(this.rtn_TXT);
            this.Controls.Add(this.rtn_Excel);
            this.Controls.Add(this.btn_Output);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.lb_postion);
            this.Controls.Add(this.lb_Msg);
            this.Controls.Add(this.btn_UnHook);
            this.Controls.Add(this.btn_Hook);
            this.Controls.Add(this.btn_ChangePath);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "CVSoldier";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btn_ChangePath;
        private System.Windows.Forms.Button btn_Hook;
        private System.Windows.Forms.Button btn_UnHook;
        private System.Windows.Forms.Label lb_Msg;
        private System.Windows.Forms.Label lb_postion;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btn_Output;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.RadioButton rtn_Excel;
        private System.Windows.Forms.RadioButton rtn_TXT;
    }
}

