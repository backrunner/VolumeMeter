using AudioSwitcher.AudioApi;
using AudioSwitcher.AudioApi.CoreAudio;
using System;
using System.Timers;
using System.Windows;
using Microsoft.Win32;

namespace VolumeMeter
{
    public partial class MainWindow : Window
    {
        private CoreAudioDevice defaultPlaybackDevice;
        private VolumeChangedObserver observer = new VolumeChangedObserver();
        private Timer timer = new Timer();
        private System.Windows.Forms.NotifyIcon ni;
        public MainWindow()
        {
            InitializeComponent();
            // 获取音频设备，订阅事件
            defaultPlaybackDevice = new CoreAudioController().DefaultPlaybackDevice;
            this.Meter.Value = defaultPlaybackDevice.Volume;
            observer.Subscribe(defaultPlaybackDevice.VolumeChanged);
            observer.VolumeChangedEvent += Observer_VolumeChangedEvent;
            // 设置计时器
            timer.Interval = 1200;
            timer.Elapsed += Timer_Elapsed;
            // 开机启动
            RegistryKey rk = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            rk.SetValue("VolumeMeter", System.Reflection.Assembly.GetExecutingAssembly().Location);
            // Tray
            System.Windows.Forms.ContextMenu contextMenu = new System.Windows.Forms.ContextMenu();
            System.Windows.Forms.MenuItem menuItem = new System.Windows.Forms.MenuItem();
            // Initialize contextMenu
            contextMenu.MenuItems.AddRange(
                        new System.Windows.Forms.MenuItem[] { menuItem });
            menuItem.Index = 0;
            menuItem.Text = "退出";
            menuItem.Click += MenuItem_Click; ;
            ni = new System.Windows.Forms.NotifyIcon();
            ni.Icon = new System.Drawing.Icon("volumeMeter_32.ico");
            ni.Visible = true;
            ni.ContextMenu = contextMenu;
        }

        private void MenuItem_Click(object sender, EventArgs e)
        {
            ni.Visible = false;
            this.Close();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            this.Dispatcher.Invoke(new Action(() =>
            {
                this.Visibility = Visibility.Hidden;
                this.timer.Stop();
            }));
        }

        private void Observer_VolumeChangedEvent(object sender, DeviceVolumeChangedArgs args)
        {
            if (args.Device.FullName == defaultPlaybackDevice.FullName)
            {
                this.Dispatcher.Invoke(new Action(() => { 
                    this.Visibility = Visibility.Visible;
                    timer.Stop();
                    timer.Start();
                }));
                this.Meter.Dispatcher.Invoke(new Action(() => { this.Meter.Value = args.Volume; }));
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // 调整窗体位置
            this.Left = SystemParameters.PrimaryScreenWidth - this.Width - 24;
            this.Top = SystemParameters.PrimaryScreenHeight - this.Height - 72;
        }
    }
}
