using System.Windows;
using System.Windows.Forms;

namespace Mp4toMp3
{
    public partial class MainWindow : Window
    {
        string mp4Path, mp4Name, mp3Path, mp3Name;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnFilePath_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog() { Title = "Select a video", Multiselect = false, Filter = "MP4 File|*.mp4" };
            if (ofd.ShowDialog() == true)
            {
                mp4Path = ofd.FileName;
                mp4Name = ofd.SafeFileName;
                lblFilePath.Content = mp4Path;
            }
        }

        private void BtnSavePath_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace((string)lblFilePath.Content))
            {
                System.Windows.Forms.MessageBox.Show("You must select a file to convert.");
            }
            else
            {
                FolderBrowserDialog fbd = new FolderBrowserDialog();
                if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    mp3Path = fbd.SelectedPath;
                    mp3Name = mp4Name.Substring(0, mp4Name.Length - 4);
                    mp3Path += ("\\" + mp3Name + ".mp3");
                    lblSavePath.Content = mp3Path;
                }
            }
        }

        private void BtnConvert_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace((string)lblFilePath.Content))
            {
                System.Windows.Forms.MessageBox.Show("You must select a file to convert.");
            }
            else if (string.IsNullOrWhiteSpace((string)lblSavePath.Content))
            {
                System.Windows.Forms.MessageBox.Show("You must select a location to save.");
            }
            else
            {
                var convert = new NReco.VideoConverter.FFMpegConverter();
                convert.ConvertMedia(mp4Path, mp3Path, "mp3");
                ResetWindow();
            }
        }

        private void ResetWindow()
        {
            mp4Path = null;
            mp4Name = null;
            mp3Path = null;
            mp3Name = null;
            lblFilePath.Content = null;
            lblSavePath.Content = null;
        }
    }
}
