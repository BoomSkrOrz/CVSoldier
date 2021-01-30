using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace MouseKeyWorker
{
    public static class MouseEvent
    {
        public static int Sceen_X { set; get; } = 1920;
        public static int Sceen_Y { set; get; } = 1080;

        /// <summary>
        /// 控制鼠标滑轮滚动，count代表滚动的值，负数代表向下，正数代表向上，如-100代表向下滚动100的y坐标  mouse_event(MOUSEEVENTF_WHEEL, 0, 0, count, 0);

        /// </summary>
        /// <param name="mouseevent"></param>
        /// <param name="dx"></param>
        /// <param name="dy"></param>
        /// <param name="cButtons"></param>
        /// <param name="dwExtraInfo"></param>
        /// <returns></returns>
        [DllImport("user32")]
        private static extern int mouse_event(int mouseevent, int dx, int dy, int cButtons, int dwExtraInfo);
        [DllImport("User32")]
        public extern static void SetCursorPos(int x, int y);

        [DllImport("user32.dll", EntryPoint = "keybd_event", SetLastError = true)]
        public static extern void keybd_event(Keys bVk, byte bScan, uint dwFlags, uint dwExtraInfo);


        /// <summary>
        /// 移动鼠标
        /// </summary>
        public static readonly int MOUSEEVENTF_MOVE = 0x0001;
        /// <summary>
        /// 模拟鼠标左键按下
        /// </summary>
        public static readonly int MOUSEEVENTF_LEFTDOWN = 0x0002;
        /// <summary>
        /// 模拟鼠标左键抬起
        /// </summary>
        public static readonly int MOUSEEVENTF_LEFTUP = 0x0004;
        /// <summary>
        /// 模拟鼠标右键按下
        /// </summary>
        public static readonly int MOUSEEVENTF_RIGHTDOWN = 0x0008;
        /// <summary>
        /// 模拟鼠标右键抬起
        /// </summary>
        public static readonly int MOUSEEVENTF_RIGHTUP = 0x0010;
        /// <summary>
        /// 模拟鼠标中键按下
        /// </summary>
        public static readonly int MOUSEEVENTF_MIDDLEDOWN = 0x0020;
        /// <summary>
        /// 模拟鼠标中键抬起
        /// </summary>
        public static readonly int MOUSEEVENTF_MIDDLEUP = 0x0040;
        /// <summary>
        /// 模拟滚轮滑动
        /// </summary>
        public static readonly int MOUSEEVENTF_WHEEL = 0x800;
        //标示是否采用绝对坐标 
        public static readonly int MOUSEEVENTF_ABSOLUTE = 0x8000;
        /// <summary>
        /// 移动鼠标到指定位置
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static int MouseMove(int x, int y)
        {
            return mouse_event(MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_MOVE, x * 65535 / Sceen_X, y * 65535 / Sceen_Y, 0, 0);
        }

        public static int Work(this WorkType work)
        {
            switch (work.Type)
            {
                case E_workType.滚轮滑动:
                    {
                        return mouse_event(MOUSEEVENTF_WHEEL, 0, 0, work.Delta, 0);
                    }
                case E_workType.键盘输入:
                    {
                        //System.Windows.Forms.SendKeys.SendWait(work.Obj as string);
                        keybd_event((Keys)work.Key, 0, 0, 0);
                        return 1;
                    }
                case E_workType.鼠标中键抬起:
                    {
                        return mouse_event(MOUSEEVENTF_MIDDLEUP, 0, 0, 0, 0);
                    }
                case E_workType.鼠标中键按下:
                    {
                        return mouse_event(MOUSEEVENTF_MIDDLEDOWN, 0, 0, 0, 0);
                    }
                case E_workType.鼠标右键抬起:
                    {
                        return mouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
                    }
                case E_workType.鼠标右键按下:
                    {
                        return mouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0);
                    }
                case E_workType.鼠标左键抬起:
                    {
                        return mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                    }
                case E_workType.鼠标左键按下:
                    {
                        return mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
                    }
                case E_workType.鼠标移动:
                    {
                        //System.Drawing.Point point = (System.Drawing.Point)work.Obj;
                        return MouseMove(work.Point.X, work.Point.Y);
                    }
                default: return 0;
            }
        }
    }
}
