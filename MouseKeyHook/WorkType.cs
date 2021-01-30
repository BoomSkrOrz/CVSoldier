using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MouseKeyWorker
{
    /// <summary>
    /// 操作类型
    /// </summary>
    public enum E_workType
    {
        鼠标移动,
        鼠标左键按下,
        鼠标左键抬起,
        鼠标右键按下,
        鼠标右键抬起,
        鼠标中键按下,
        鼠标中键抬起,
        鼠标左键双击,
        鼠标右键双击,
        滚轮滑动,
        键盘输入,
    }
    
    /// <summary>
    /// 基本操作类
    /// </summary>
    public class WorkType
    {
        public WorkType() { }
        public WorkType(E_workType type, int delay = 1, int delta = 0, Keys keys = Keys.None, Point point = new Point())
        {
            this.type = type;
            this.delay = delay;
            Delta = delta;
            Key = keys;
            Point = point;
        }
        private E_workType type;
        private int delay = 1;
        public int Delta { set; get; } = 0;
        public Keys Key { set; get; }
        public Point Point { set; get; }
        /// <summary>
        /// 操作类型
        /// </summary>
        public E_workType Type
        {
            set => type = value;
            get => type;
        }
        /// <summary>
        /// 操作完延时
        /// </summary>
        public int Delay
        {
            set => delay = value;
            get => delay;
        }
    }
}
