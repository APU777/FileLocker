using Microsoft.Win32;
using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Runtime;

namespace FileLockerProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool _NotifyIconCheck = true;
        private Thread[] thread = new Thread[100];
        private int ThreadIndex = 0;

        public MainWindow()
        {
            InitializeComponent();
            ActionFL.NotifyIcon(this, ref _NotifyIconCheck);
            Hide();

            RegistryWrite();
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////
        public void Lock()
        {
            System.Windows.Forms.OpenFileDialog OFD = new System.Windows.Forms.OpenFileDialog();
            OFD.ShowDialog();

            if (OFD.FileName.Length > 0)
            {
                if (ActionFL._BufferList.Contains(OFD.FileName) == false)
                {
                    TextBlock TB = new TextBlock();
                    TB.FontSize = 20;
                    TB.FontWeight = FontWeights.UltraBold;
                    TB.Text = OFD.FileName;
                    LB.Items.Add(TB);
                    ActionFL._BufferList.Add(TB.Text);
                    ActionFL._index = ActionFL._BufferList.Count - 1;
                    ThreadIndex = ActionFL._index;
                    ActionFL._CheckOpen = true;
                }
            }
        }/////////////////////////////////////////////////////////////////////////////////////
        public void LockOn()
        {
            try
            {
                ActionFL.NotifyIcon(this, ref _NotifyIconCheck);

                if (_NotifyIconCheck)
                {
                    if (ActionFL._BufferList.Count > 0)
                    {
                        if (ActionFL._CheckOpen)
                        {
                            MessageBox.Show(ThreadIndex.ToString());
                            thread[ThreadIndex] = new Thread(new ThreadStart(ActionFL.LockFile));
                            thread[ThreadIndex].Start();
                        }
                    }
                }
                else
                    thread[ThreadIndex].Abort();
            }
            catch (Exception exc)
            {
                // MessageBox.Show(exc.Message);
            }
        }//////////////////////////////////////////////////////////////////////////////////
        public void RegistryWrite()
        {
            string RegKeyName = "MyApplication";
            RegistryKey RK = Registry.CurrentUser;
            RegistryKey DS = RK.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
            try
            {
                MessageBox.Show(DS.GetValue(RegKeyName).ToString());
            }
            catch
            {
                DS.SetValue(RegKeyName, System.Windows.Forms.Application.ExecutablePath);
                DS.Close();
                RK.Close();
            }
           // DS.DeleteValue(RegKeyName);
        }///////////////////////////////////////////////////////////////////////////
         //////////////////////////////////////////////////////////////////////////////////////////////////////

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _NotifyIconCheck = ActionFL.CheckingClosing(ref e, this);

            LockOn();
        }

        private void B_Lock_Click(object sender, RoutedEventArgs e)
        {
            Lock();
        }

        private void B_Unlock_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                TextBlock _BufTB = LB.SelectedItem as TextBlock;
                int ItemIndex = LB.Items.IndexOf(_BufTB);
                ThreadIndex = ItemIndex;
                LB.Items.RemoveAt(ItemIndex);
                ActionFL._BufferList.Remove(_BufTB.Text);
                ActionFL._LFS[ItemIndex].Close();
                ActionFL._LFS.RemoveAt(ItemIndex);
            }
            catch(Exception exc)
            {
                //MessageBox.Show(exc.Message);
            }
        }

        private void B_Hide_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            LockOn();
        }
    }
}