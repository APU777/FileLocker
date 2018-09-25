using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace FileLockerProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool _NotifyIconCheck = false;
        private bool _CheckLock = true;

        public MainWindow()
        {
            InitializeComponent();
        }

        Thread thread = new Thread(new ThreadStart(ActionFL.LockFile));

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _NotifyIconCheck = ActionFL.CheckingClosing(ref e, this);
          
            ActionFL.NotifyIcon(this, ref _NotifyIconCheck);

            if (_NotifyIconCheck)
            {
                if (ActionFL._BufferList.Count > 0)
                {
                    if (ActionFL._CheckOpen)
                        thread.Start();
                    ActionFL._CheckOpen = true;
                }
            }
            else
                thread.Abort();
        }

        private void B_Lock_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog OFD = new System.Windows.Forms.OpenFileDialog();
            OFD.ShowDialog();
           
            if (OFD.FileName.Length > 0)
            {
                TextBlock TB = new TextBlock();
                TB.FontSize = 20;
                TB.FontWeight = FontWeights.UltraBold;
                TB.Text = OFD.FileName;
                LB.Items.Add(TB);
                ActionFL._BufferList.Add(TB.Text);
                ActionFL._index = ActionFL._BufferList.Count - 1;
            }
        }

        private void B_Unlock_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                TextBlock _BufTB = LB.SelectedItem as TextBlock;
                MessageBox.Show(_BufTB.Text);
            }
            catch(Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
          
        }
    }
}
