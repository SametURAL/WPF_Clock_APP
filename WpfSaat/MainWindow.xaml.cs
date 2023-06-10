using System;
using System.Windows;
using System.Windows.Threading;

namespace WpfSaat
{
    public partial class MainWindow : Window
    {
        DispatcherTimer timer;
        DateTime targetTime;
        bool countdownStarted;

        public MainWindow()
        {
            InitializeComponent();

            // Gerçek zamanlı saat güncellemesini başlat
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();

            countdownStarted = false;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // Gerçek zamanlı saat güncellemesi
            tblSaat.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        private void StartTimerButton_Click(object sender, RoutedEventArgs e)
        {
            if (!countdownStarted)
            {
                int dakika;
                if (int.TryParse(txtDakika.Text, out dakika))
                {
                    // Başlatma tuşuna basıldığında geri sayımı başlat
                    targetTime = DateTime.Now.AddMinutes(dakika);

                    DispatcherTimer countdownTimer = new DispatcherTimer();
                    countdownTimer.Interval = TimeSpan.FromSeconds(1);
                    countdownTimer.Tick += CountdownTimer_Tick;
                    countdownTimer.Start();

                    countdownStarted = true;
                }
                else
                {
                    MessageBox.Show("Lütfen geçerli bir sayı girin.");
                }
            }
        }

        private void CountdownTimer_Tick(object sender, EventArgs e)
        {
            // Geri sayımı güncelle
            TimeSpan remainingTime = targetTime - DateTime.Now;

            if (remainingTime.TotalSeconds <= 0)
            {
                // Geri sayım tamamlandı
                tblGeriSayim.Text = "Geri sayım tamamlandı!";
                countdownStarted = false;
            }
            else
            {
                // Geri sayımı göster
                tblGeriSayim.Text = remainingTime.ToString(@"hh\:mm\:ss");
            }
        }
    }
}
