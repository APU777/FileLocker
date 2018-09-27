using System;

namespace FileLockerProject
{
    class WorkShopAction
    {
        public static void ContextMenu_NI(System.Windows.Forms.ContextMenu _CM, System.Windows.Forms.NotifyIcon _NotifyIcon, MainWindow _THIS) //Create menu for Tray (NotifyIcon)
        {
            System.Windows.Forms.MenuItem _MI = new System.Windows.Forms.MenuItem();
            System.Windows.Forms.MenuItem _MI1 = new System.Windows.Forms.MenuItem();
            System.Windows.Forms.MenuItem _MI2 = new System.Windows.Forms.MenuItem();


            _CM.MenuItems.AddRange(items: new System.Windows.Forms.MenuItem[] { _MI });
            _CM.MenuItems.AddRange(items: new System.Windows.Forms.MenuItem[] { _MI1 });
            _CM.MenuItems.AddRange(items: new System.Windows.Forms.MenuItem[] { _MI2 });


            LockMWindowFromTray(_MI, _NotifyIcon, _THIS); 
            UnlockMWindowFromTray(_MI1, _NotifyIcon, _THIS);
            CloseMWindowFromTray(_MI2, _NotifyIcon, _THIS);//Close after click by "Close" item.
        }

        private static void LockMWindowFromTray(System.Windows.Forms.MenuItem _MI, System.Windows.Forms.NotifyIcon _NotifyIcon, MainWindow _THIS) //Show after click by the "Show" item.
        {
            _MI.Index = 0;
            _MI.Text = "Lock File";
            _MI.Click += delegate (object Sender, EventArgs args)
            {
                _THIS.Lock();
                _THIS.LockOn();
            };
        }

        private static void UnlockMWindowFromTray(System.Windows.Forms.MenuItem _MI, System.Windows.Forms.NotifyIcon _NotifyIcon, MainWindow _THIS) //Close Window after click by the "Close" item.
        {
            _MI.Index = 1;
            _MI.Text = "Unlock File";
            _MI.Click += delegate (object Sender, EventArgs args)
            {
                _THIS.Show();
            };
        }

        private static void CloseMWindowFromTray(System.Windows.Forms.MenuItem _MI, System.Windows.Forms.NotifyIcon _NotifyIcon, MainWindow _THIS) //Close Window after click by the "Close" item.
        {
            _MI.Index = 2;
            _MI.Text = "Exit and STOP LOCKING.";
            _MI.Click += delegate (object Sender, EventArgs args)
            {
                _THIS.Close();
            };
        }
    }
}
