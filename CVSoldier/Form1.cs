using AdrHook;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CVSoldier
{
    public partial class Form1 : Form
    {
        private BindingList<Display> dataSource = new BindingList<Display>();
        public Form1()
        {
            InitializeComponent();
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            dataGridView1.DataSource = dataSource;
        }
        private bool LoadHook()
        {
            try
            {
                HookManager.KeyPress += HookManager_KeyPress;
                HookManager.MouseEvent += HookManager_MouseEvent;
                btn_Hook.Enabled = false;
                btn_UnHook.Enabled = true;
                //this.WindowState = FormWindowState.Minimized;
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hook异常:{ex.Message}");
                HookManager.KeyPress -= HookManager_KeyPress;
                HookManager.MouseEvent -= HookManager_MouseEvent;
                return false;
            }
        }

        private bool UnloadHook()
        {
            try
            {
                HookManager.KeyPress -= HookManager_KeyPress;
                HookManager.MouseEvent -= HookManager_MouseEvent;
                btn_Hook.Enabled = true;
                btn_UnHook.Enabled = false;
                //this.WindowState = FormWindowState.Normal;
                //this.Activate();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hook异常:{ex.Message}");
                return false;
            }
        }
        private void DisplayMsg(string msg)
        {
            Invoke(new Action(() => lb_Msg.Text = $"提示：{msg}"));
        }
        private void Mouseposition(int x, int y)
        {
            Invoke(new Action(() => lb_postion.Text = $"X：{x},Y:{y}"));
        }
        private void HookManager_MouseEvent(object sender, MouseEventArgs evevt)
        {
            try
            {
                Mouseposition(evevt.X, evevt.Y);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void HookManager_KeyPress(object sender, KeyEventArgs e)
        {
            try
            {
                string value = string.Empty;
                switch (e.KeyCode)
                {
                    case Keys.Escape:
                        UnloadHook();
                        break;
                    case Keys.F2:
                        value = Clipboard.GetText();
                        if (!string.IsNullOrWhiteSpace(value))
                        {                            
                            Display info = dataSource.LastOrDefault();
                            if(info==null)
                            {
                                info = new Display();
                                info.Info = value;
                                dataSource.Add(info);
                            }
                            else
                            {
                                if(string.IsNullOrWhiteSpace(info.Info))
                                {
                                    info.Info = value;
                                }
                                else
                                {
                                    info.Info += $",,{value}";
                                }                                
                            }
                            this.Refresh();
                            FormMsg.ShowMsg($"添加:{value}");
                            Clipboard.Clear();
                        }
                        break;
                    case Keys.F3:
                        value = Clipboard.GetText();
                        if (!string.IsNullOrWhiteSpace(value))
                        {                            
                            //value += ";;";
                            Display info = dataSource.LastOrDefault();
                            if (info == null)
                            {
                                info = new Display();
                                info.Info = value;
                                dataSource.Add(info);
                            }
                            else
                            {
                                if (string.IsNullOrWhiteSpace(info.Info))
                                {
                                    info.Info = value;
                                }
                                else
                                {
                                    info.Info += $",,{value}";
                                }
                            }
                            dataSource.Add(new Display());
                            this.Refresh();
                            FormMsg.ShowMsg($"换行:{value}");
                            Clipboard.Clear();
                        }
                        break;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btn_Hook_Click(object sender, EventArgs e)
        {
            LoadHook();
        }

        private void btn_UnHook_Click(object sender, EventArgs e)
        {
            UnloadHook();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string infoPath = Path.Combine(Application.StartupPath, "SavePathInfo.ini");
            if (File.Exists(infoPath))
            {
                textBox1.Text = File.ReadAllText(infoPath);
            }
            else
            {
                textBox1.Text = Application.StartupPath;
                File.WriteAllText(infoPath, textBox1.Text);
            }
        }

        private void btn_ChangePath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "请选择文件路径";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string infoPath = Path.Combine(Application.StartupPath, "SavePathInfo.ini");
                textBox1.Text = Path.GetFullPath(dialog.SelectedPath);
                File.WriteAllText(infoPath, textBox1.Text);
            }
        }

        private void btn_Output_Click(object sender, EventArgs e)
        {
            if(rtn_Excel.Checked)
            {
                bool isOK= OutputExcel(Path.Combine(textBox1.Text, $"{DateTime.Now:yyyyMMddHHmmss}.xlsx"));
                MessageBox.Show(isOK ? "导出Excel成功" : "导出Excel失败");
            }
            else if(rtn_TXT.Checked)
            {
                bool isOK = OutputTxt(Path.Combine(textBox1.Text, $"{DateTime.Now:yyyyMMddHHmmss}.txt"));
                MessageBox.Show(isOK ? "导出Txt成功" : "导出Txt失败");
            }
        }

        private bool OutputExcel(string path)
        {
            try
            {
                if (dataSource.Count == 0) return false;
                if (File.Exists(path)) File.Delete(path);
                using (var excel = new ExcelPackage(new FileInfo(path)))
                {
                    var ws = excel.Workbook.Worksheets.Add("Sheet1");
                    for (int i = 0; i < dataSource.Count; ++i)
                    {
                        string info = dataSource[i].Info;
                        if (string.IsNullOrWhiteSpace(info)) continue;
                        string[] rows = info.Split(new String[] { ",," }, StringSplitOptions.None);
                        if (rows.Length == 0) continue;
                        for (int j = 0; j < rows.Length; ++j)
                        {
                            ws.Cells[i + 1, j + 1].Value = rows[j];
                        }
                        ws.Column(i + 1).AutoFit();
                    }
                    excel.Save();
                }
                return true;
            }
            catch(Exception ex)
            {
                MessageBox.Show($"导出失败：{ex.Message}");
                return false;
            }
        }
        private bool OutputTxt(string path)
        {
            try
            {
                if (dataSource.Count == 0) return false;
                if (File.Exists(path)) File.Delete(path);
                StringBuilder builder = new StringBuilder();
                foreach(Display display in dataSource)
                {
                    builder.AppendLine(display.Info);
                }
                File.WriteAllText(path, builder.ToString());
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"导出失败：{ex.Message}");
                return false;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (rtn_Excel.Checked)
            {
                bool isOK = OutputExcel(Path.Combine(textBox1.Text, $"{DateTime.Now:yyyyMMddHHmmss}.xlsx"));
                MessageBox.Show(isOK ? "导出Excel成功" : "导出Excel失败");
            }
            else if (rtn_TXT.Checked)
            {
                bool isOK = OutputTxt(Path.Combine(textBox1.Text, $"{DateTime.Now:yyyyMMddHHmmss}.txt"));
                MessageBox.Show(isOK ? "导出Txt成功" : "导出Txt失败");
            }
            UnloadHook();
        }
    }


    public class Display
    {
        public string Info { set; get; }
    }
}
