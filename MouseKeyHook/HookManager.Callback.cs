using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace AdrHook
{
    /// <summary>
    /// 功能说明：HookManager分部类，钩子事件处理
    /// </summary>
    public static partial class HookManager
    {
        /// <summary>
        /// 该CallWndProc钩子程序与调用SetWindowsHookEx函数一起使用的应用程序定义的或者库定义的回调函数。
        /// 该HOOKPROC类型定义了一个指向此回调函数。
        /// CallWndProc是一个占位符的应用程序定义的或者库定义的函数名。
        /// </summary>
        private delegate int HookProc(int nCode, int wParam, IntPtr lParam);

        #region 鼠标钩子处理

        /// <summary>
        /// 此字段不客观需要的，但我们需要保持一个供参考的将被传递给非托管代码的委托。为了避免GC把它清理干净。
        /// </summary>
        private static HookProc s_MouseDelegate;

        /// <summary>
        /// 存储句柄的鼠标钩子程序
        /// </summary>
        private static int s_MouseHookHandle;

        /// <summary>
        /// 鼠标检测活动将被称为每次回调函数
        /// </summary>
        private static int MouseHookProc(int nCode, int wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                //Marshall 从回调的数据
                MouseLLHookStruct mouseHookStruct = (MouseLLHookStruct)Marshal.PtrToStructure(lParam, typeof(MouseLLHookStruct));

                //检测按钮点击
                MouseButtons button = MouseButtons.None;
                short mouseDelta = 0;
                int clickCount = 0;

                bool isDown = false;

                switch (wParam)
                {
                    case WM_LBUTTONDOWN:
                        button = MouseButtons.Left;
                        isDown = true;
                        clickCount = 1;
                        break;
                    case WM_LBUTTONUP:
                        button = MouseButtons.Left;
                        isDown = false;
                        clickCount = 1;
                        break;
                    case WM_LBUTTONDBLCLK:
                        button = MouseButtons.Left;
                        clickCount = 2;
                        break;
                    case WM_RBUTTONDOWN:
                        button = MouseButtons.Right;
                        isDown = true;
                        clickCount = 1;
                        break;
                    case WM_RBUTTONUP:
                        button = MouseButtons.Right;
                        isDown = false;
                        clickCount = 1;
                        break;
                    case WM_RBUTTONDBLCLK:
                        button = MouseButtons.Right;
                        clickCount = 2;
                        break;

                    case WM_MBUTTONUP:
                        button = MouseButtons.Middle;
                        isDown = false;
                        clickCount = 1;
                        break;
                    case WM_MBUTTONDOWN:
                        button = MouseButtons.Middle;
                        isDown = true;
                        clickCount = 1;
                        break;

                    case WM_MOUSEWHEEL:
                        //如果消息是WM_MOUSEWHEEL，MouseData成员是滚轮。
                        //一个轮击定义为WHEEL_DELTA，这是120。
                        //(value >> 16) & 0xffff; 从给定的32位值检索高位字。
                        mouseDelta = (short)((mouseHookStruct.MouseData >> 16) & 0xffff);

                        //TODO: X BUTTONS (这个按钮暂时没有测试)
                        break;
                }

                //生成事件
                MouseEventExtArgs e = new MouseEventExtArgs(
                                                   button,
                                                   clickCount,
                                                   mouseHookStruct.Point.X,
                                                   mouseHookStruct.Point.Y,
                                                   mouseDelta,isDown);


                _mouseEvent?.Invoke(null, e);

                if (e.Handled)
                {
                    return -1;
                }
            }

            //调用下一个钩子
            return CallNextHookEx(s_MouseHookHandle, nCode, wParam, lParam);
        }

        private static void EnsureSubscribedToGlobalMouseEvents()
        {
            // 安装鼠标钩子
            if (s_MouseHookHandle == 0)
            {
                //为了避免GC把它清理干净。
                s_MouseDelegate = MouseHookProc;
                //安装钩子
                s_MouseHookHandle = SetWindowsHookEx(
                    WH_MOUSE_LL,
                    s_MouseDelegate,
                    GetModuleHandle(System.Diagnostics.Process.GetCurrentProcess().MainModule.ModuleName),
                    //Marshal.GetHINSTANCE(
                    //    Assembly.GetExecutingAssembly().GetModules()[0]),
                    0);
                //如果SetWindowsHookEx函数将失败。
                if (s_MouseHookHandle == 0)
                {
                    //返回由上一个非托管函数使用平台调用称为具有DllImportAttribute.SetLastError标志设置返回的错误代码。
                    int errorCode = Marshal.GetLastWin32Error();

                    //初始化并抛出初始化Win32Exception类的新实例使用指定的错误。 
                    throw new Win32Exception(errorCode);
                }
            }
        }

        private static void TryUnsubscribeFromGlobalMouseEvents()
        {
            ForceUnsunscribeFromGlobalMouseEvents();
        }

        private static void ForceUnsunscribeFromGlobalMouseEvents()
        {
            if (s_MouseHookHandle != 0)
            {
                //卸载钩子
                int result = UnhookWindowsHookEx(s_MouseHookHandle);
                //复位无效句柄
                s_MouseHookHandle = 0;
                //释放用于GC
                s_MouseDelegate = null;
                //如果失败，异常必须抛出
                if (result == 0)
                {
                    int errorCode = Marshal.GetLastWin32Error();
                    throw new Win32Exception(errorCode);
                }
            }
        }

        #endregion

        #region 键盘钩子程序

        /// <summary>
        /// 此字段不客观需要的，但我们需要保持一个供参考的将被传递给非托管代码的委托。
        /// 为了避免GC把它清理干净。
        /// </summary>
        private static HookProc s_KeyboardDelegate;

        /// <summary>
        /// 存储句柄键盘钩子程序。
        /// </summary>
        private static int s_KeyboardHookHandle;

        /// <summary>
        /// 键盘检测活动将被称为每次回调函数。
        /// </summary>
        private static int KeyboardHookProc(int nCode, Int32 wParam, IntPtr lParam)
        {
            //表示如有underlaing事件设置e.Handled标志
            bool handled = false;

            if (nCode >= 0)
            {
                //在lParam中读取KeyboardHookStruct结构
                KeyboardHookStruct MyKeyboardHookStruct = (KeyboardHookStruct)Marshal.PtrToStructure(lParam, typeof(KeyboardHookStruct));
                if ((wParam == WM_KEYDOWN || wParam == WM_SYSKEYDOWN))
                {
                    Keys keyData = (Keys)MyKeyboardHookStruct.VirtualKeyCode;
                    KeyEventArgs e = new KeyEventArgs(keyData);
                    _keyPress.Invoke(null, e);
                    handled = e.Handled;
                }

                //// 按键按下
                //if (wParam == WM_KEYDOWN)
                //{
                //    bool isDownShift = ((GetKeyState(VK_SHIFT) & 0x80) == 0x80 ? true : false);
                //    bool isDownCapslock = (GetKeyState(VK_CAPITAL) != 0 ? true : false);

                //    byte[] keyState = new byte[256];
                //    GetKeyboardState(keyState);
                //    byte[] inBuffer = new byte[2];
                //    if (ToAscii(MyKeyboardHookStruct.VirtualKeyCode,
                //              MyKeyboardHookStruct.ScanCode,
                //              keyState,
                //              inBuffer,
                //              MyKeyboardHookStruct.Flags) == 1)
                //    {
                //        char key = (char)inBuffer[0];
                //        if ((isDownCapslock ^ isDownShift) && Char.IsLetter(key)) key = Char.ToUpper(key);
                //        KeyPressEventArgs e = new KeyPressEventArgs(key);
                //        _keyPress?.Invoke(null, e);
                //        handled = handled || e.Handled;
                //    }
                //}

                //// 按键弹起
                //if ((wParam == WM_KEYUP || wParam == WM_SYSKEYUP))
                //{
                //    Keys keyData = (Keys)MyKeyboardHookStruct.VirtualKeyCode;
                //    KeyEventArgs e = new KeyEventArgs(keyData);
                //    _keyPress?.Invoke(null, e);
                //    handled = handled || e.Handled;
                //}

            }

            //如果事件在应用程序处理的不换手到其他听众
            if (handled)
                return -1;

            //转发到其它应用程序
            return CallNextHookEx(s_KeyboardHookHandle, nCode, wParam, lParam);
        }

        private static void EnsureSubscribedToGlobalKeyboardEvents()
        {
            // 安装键盘钩子，只有当它没有安装，必须安装
            if (s_KeyboardHookHandle == 0)
            {
                //为了避免GC把它清理干净。
                s_KeyboardDelegate = KeyboardHookProc;
                //安装钩子
                s_KeyboardHookHandle = SetWindowsHookEx(
                    WH_KEYBOARD_LL,
                    s_KeyboardDelegate,
                    GetModuleHandle(System.Diagnostics.Process.GetCurrentProcess().MainModule.ModuleName),
                    //Marshal.GetHINSTANCE(
                    //    Assembly.GetExecutingAssembly().GetModules()[0]),
                    0);
                //如果SetWindowsHookEx函数将失败。
                if (s_KeyboardHookHandle == 0)
                {
                    //返回由上一个非托管函数使用平台调用称为具有DllImportAttribute.SetLastError标志设置返回的错误代码. 
                    int errorCode = Marshal.GetLastWin32Error();

                    //初始化并抛出初始化Win32Exception类的新实例使用指定的错误。
                    throw new Win32Exception(errorCode);
                }
            }
        }

        private static void TryUnsubscribeFromGlobalKeyboardEvents()
        {
            ForceUnsunscribeFromGlobalKeyboardEvents();
        }

        private static void ForceUnsunscribeFromGlobalKeyboardEvents()
        {
            if (s_KeyboardHookHandle != 0)
            {
                //卸载钩子
                int result = UnhookWindowsHookEx(s_KeyboardHookHandle);
                //重置句柄
                s_KeyboardHookHandle = 0;
                //清理
                s_KeyboardDelegate = null;
                //如果失败，异常必须抛出
                if (result == 0)
                {
                    int errorCode = Marshal.GetLastWin32Error();
                    throw new Win32Exception(errorCode);
                }
            }
        }

        #endregion

        private static event MouseEventHandler _mouseEvent;
        /// <summary>
        /// 鼠标事件
        /// </summary>
        public static event MouseEventHandler MouseEvent
        {
            add
            {
                EnsureSubscribedToGlobalMouseEvents();
                _mouseEvent += value;
            }
            remove
            {
                _mouseEvent -= value;
                TryUnsubscribeFromGlobalMouseEvents();
            }
        }

        private static event KeyEventHandler _keyPress;
        /// <summary>
        /// 键盘事件
        /// </summary>
        public static event KeyEventHandler KeyPress
        {
            add
            {
                EnsureSubscribedToGlobalKeyboardEvents();
                _keyPress += value;
            }
            remove
            {
                _keyPress -= value;
                TryUnsubscribeFromGlobalKeyboardEvents();
            }
        }
    }
}
