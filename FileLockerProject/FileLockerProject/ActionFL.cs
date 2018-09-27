using System;
using System.Windows;
using System.IO;
using System.Collections.Generic;

namespace FileLockerProject
{
    static class ActionFL
    {
        public static int _CheckIndex = -1;
        public static bool Control_NI = true;
        public static int _index = 0;
        public static bool _CheckOpen = true;
        public static List<string> _BufferList = new List<string>();
        public static List <FileStream> _LFS = new List<FileStream>();

        public static void NotifyIcon(MainWindow _THIS, ref bool _NotifyIconCheck)
        {
            if (_NotifyIconCheck)
            {
                if (Control_NI)
                {
                    System.Windows.Forms.NotifyIcon _NI = new System.Windows.Forms.NotifyIcon();

                    System.Windows.Forms.ContextMenu _CM = new System.Windows.Forms.ContextMenu();

                    WorkShopAction.ContextMenu_NI(_CM, _NI, _THIS);

                    Stream _SourceIcon = Application.GetResourceStream(new Uri("pack://application:,,,/FileLockerProject;component/Image/Locker.ico")).Stream;

                    _NI.Icon = new System.Drawing.Icon(_SourceIcon);

                    Control_NI = false;

                    _NI.ContextMenu = _CM;

                    _NI.Visible = true;
                }
                //WorkShopAction.NIDoubleClick(_NotifyIcon: _NI, _THIS: _THIS);
            }
        }

        public static bool CheckingClosing(ref System.ComponentModel.CancelEventArgs _E, MainWindow _THIS)
        {
            MessageBoxResult _Result = MessageBox.Show("Really close?", "Warning", MessageBoxButton.YesNo); //_Result become Yes || No.

            if (_Result == MessageBoxResult.No) //Check result.
            {
                if (_THIS.IsVisible == false)
                {
                    _E.Cancel = true;
                    return false;
                }
                _E.Cancel = true; // Cancel close the window.
                _THIS.Hide(); // Hide _THIS.%Window.
                return true;
            }
            return false;
        }

        public static void LockFile()
        {
            if (_BufferList.Count > 0)
            {
                while (true)
                {
                    if (_LFS.Count == 0)
                        _CheckIndex = -1;
                    if (_CheckOpen && _CheckIndex != _index)
                    {
                       _CheckIndex = _index;
                       _LFS.Add(File.Open(_BufferList[_index], FileMode.Open));
                        _CheckOpen = false;
                    }
                }
            }
        }
    }
}