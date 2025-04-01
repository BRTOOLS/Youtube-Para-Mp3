using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YoutubeExplode;
using YoutubeExplode.Converter;
using YoutubeExplode.Videos;

namespace youtubetomp3
{
    public partial class Form1 : Form
    {

        private string url;

        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            url = textBox1.Text;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var videourl = url;
            var youtube = new YoutubeClient();
            var video = await youtube.Videos.GetAsync(url);
            string title = video.Title;

            using (var folderDialog = new FolderBrowserDialog())
            {
                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    string folderPath = folderDialog.SelectedPath;
                    string filePath = Path.Combine(folderPath, $"{SanitizeFileName(title)}.mp3");
                    await youtube.Videos.DownloadAsync(url, filePath);
                }
            }
        }

        private string SanitizeFileName(string fileName)
        {
            foreach (char c in Path.GetInvalidFileNameChars())
            {
                fileName = fileName.Replace(c, '_');
            }
            return fileName;
        }
    }
}
