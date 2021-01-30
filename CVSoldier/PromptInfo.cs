using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace CVSoldier
{
    public class PromptInfo
    {
        //API原型 
        [DllImport("user32.dll")]
        public static extern bool AnimateWindow(IntPtr hwnd, int dateTime, int dwFlags);//hwnd窗口句柄.dateTime:动画时长.dwFlags:动画类型组合
        /// <summary>
        /// //激活窗口，在使用了AW_HIDE标志后不要使用这个标志 
        /// </summary>
        public static int AW_ACTIVE = 0x20000; 
        /// <summary>
        /// //隐藏窗口 
        /// </summary>
        public static int AW_HIDE = 0x10000;
        /// <summary>
        /// // 使用淡入淡出效果 
        /// </summary>
        public static int AW_BLEND = 0x80000;
        /// <summary>
        /// //使用滑动类型动画效果，默认为滚动动画类型，当使用AW_CENTER标志时，这个标志就被忽略 
        /// </summary>
        public static int AW_SLIDE = 0x40000;
        /// <summary>
        /// //若使用了AW_HIDE标志，则使窗口向内重叠；否则向外扩展 
        /// </summary>
        public static int AW_CENTER = 0x0010;
        /// <summary>
        /// //自左向右显示窗口，该标志可以在滚动动画和滑动动画中使用。使用AW_CENTER标志时忽略该标志 
        /// </summary>
        public static int AW_HOR_POSITIVE = 0x0001;
        /// <summary>
        /// //自右向左显示窗口，该标志可以在滚动动画和滑动动画中使用。使用AW_CENTER标志时忽略该标志 
        /// </summary>
        public static int AW_HOR_NEGATIVE = 0x0002;
        /// <summary>
        /// //自顶向下显示窗口，该标志可以在滚动动画和滑动动画中使用。使用AW_CENTER标志时忽略该标志 
        /// </summary>
        public static int AW_VER_POSITIVE = 0x0004;
        /// <summary>
        /// //自下向上显示窗口，该标志可以在滚动动画和滑动动画中使用。使用AW_CENTER标志时忽略该标志
        /// </summary>
        public static int AW_VER_NEGATIVE = 0x0008;
    }
}
