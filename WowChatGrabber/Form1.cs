using DeepCopy;
using howto_bitmap32;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tesseract;
using System;
using IronOcr;

namespace ResponseChecker
{
    public partial class Form1 : Form
    {


        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);
        [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetForegroundWindow(IntPtr hWnd);
        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }
        private Bitmap TakeScreenshot()
        {
            var size = new Size(SelectedArea.Width, SelectedArea.Height);
            var bmpScreenshot = new Bitmap(SelectedArea.Width,
                               SelectedArea.Height, PixelFormat.Format32bppArgb);
            
            var gfxScreenshot = Graphics.FromImage(bmpScreenshot);
            gfxScreenshot.CopyFromScreen(SelectedArea.Left, SelectedArea.Top, 0, 0, size);
            ConvertBitmapToGrayscale(bmpScreenshot, false);
            return bmpScreenshot;
        }



        public static Rectangle SelectedArea;


        HubConnection connection;
        public Bitmap ConvertToBitmap(string fileName)
        {
            Bitmap bitmap;
            using (Stream bmpStream = System.IO.File.Open(fileName, System.IO.FileMode.Open))
            {
                Image image = Image.FromStream(bmpStream);

                bitmap = new Bitmap(image);

            }
            return bitmap;
        }

        tessnet2.Tesseract ocr;
        //ocr.SetVariable("tessedit_char_whitelist", "0123456789abcdefghijkLmnOpqrstuvwxyz.,$-/#&=()\"'\\:?"); // Accepted characters
        
        public Form1()
        {
            ocr = new tessnet2.Tesseract();
            ocr.SetVariable("tessedit_char_whitelist", "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz.,$-/\\#&=()\"':?"); // Accepted characters
            ocr.Init(@"C:\Programmer\WowChatGrabber\WowChatGrabber\Content\tessdata", "eng", false); // Directory of your tessdata folder

            InitializeComponent();
            if (File.Exists(LastSelectedAreaFile)) SetRegion(JsonConvert.DeserializeObject<Rectangle>(File.ReadAllText(LastSelectedAreaFile)), true);
            connection = new HubConnectionBuilder()
              .WithUrl("http://192.168.10.111:50486/JukyHub")
              .Build();
            connection.Closed += async (error) =>
            {
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await connection.StartAsync();
            };
            connection.StartAsync();

            Timer watchTimer = new Timer();
            watchTimer.Interval = 250;
            watchTimer.Tick += (l, a) =>
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();

                int Xvalue = 0;
                int Yvalue = 0;
                if (int.TryParse(XValue.Text, out Xvalue) && int.TryParse(YValue.Text, out Yvalue))
                {
                    if (IsGameRunning() || true)
                    {
                        var screenshot = TakeScreenshot();
                        //var screenshot = ConvertToBitmap(@"grey.jpg");

                        var firstPix = screenshot.GetPixel(1, 1);
                        // if is dark screenshot
                        if (firstPix.A == 255 && firstPix.R == 0 && firstPix.G == 0 && firstPix.B == 0)
                        {
                            //SaveScreenshot(screenshot, "testttt");
                            var text = GetTextFromImage2(screenshot);//.Replace("\n", "");

                            if (text.Contains("=") && text.Contains("#"))
                            {
                                var split = text.Split('=');
                                var userAndTimestamp = split.FirstOrDefault().Replace(" ", "");
                                var userAndTimestampSplit = userAndTimestamp.Split('#');

                                var user = userAndTimestampSplit.LastOrDefault();
                                var time = userAndTimestampSplit.FirstOrDefault();

                                var message = text.Replace(split.FirstOrDefault() + "=", "");
                                var TimeAndUserAndMsg = $"[{time}]{user}: {message}";

                                bool exists = false;
                                foreach (ListViewItem item in listView1.Items)
                                {
                                    if (item.Text.ToLower() == TimeAndUserAndMsg.ToLower())
                                    {
                                        exists = true;
                                        break;
                                    }
                                }

                                if (!exists)
                                {
                                    PostMessage(284843089384701952, message, user);
                                    //SaveScreenshot(GreyScreenshot, "grey");
                                    listView1.Items.Add(TimeAndUserAndMsg);
                                    if (listView1.Items.Count > 9)
                                    {
                                        listView1.Items.RemoveAt(0);
                                    }
                                }
                            }
                        }
                    }
                }
                sw.Stop();
                Console.WriteLine(sw.ElapsedMilliseconds);
            };
            watchTimer.Start();
        }

        private async void PostMessage(long discord_id, string message, string user)
        {
            if(connection.State != HubConnectionState.Connected)
            {
                connection.StartAsync();
            }
            
            await connection.InvokeAsync("PostMessage", discord_id, message.ToLower(), user, "THisiSDEadminKey21");
        }

        private void GetLinesFromChat(string text)
        {
            if (text.Contains("[") && text.Contains("]") && text.Contains(":"))
            {

            }
        }
        private void ConvertBitmapToGrayscale(Bitmap bm, bool use_average)
        {
            // Make a Bitmap24 object.
            Bitmap32 bm32 = new Bitmap32(bm);

            // Lock the bitmap.
            bm32.LockBitmap();

            // Process the pixels.
            for (int x = 0; x < bm.Width; x++)
            {
                for (int y = 0; y < bm.Height; y++)
                {
                    byte r = bm32.GetRed(x, y);
                    byte g = bm32.GetGreen(x, y);
                    byte b = bm32.GetBlue(x, y);
                    byte gray = (use_average ?
                        (byte)((r + g + b) / 3) :
                        (byte)(0.3 * r + 0.5 * g + 0.2 * b));
                    bm32.SetPixel(x, y, gray, gray, gray, 255);
                }
            }

            // Unlock the bitmap.
            bm32.UnlockBitmap();
        }
        private void ConvertToGreyImage(Bitmap bitmap)
        {
            Color c;

            for (int y = 0; y < bitmap.Height; y++)
            {
                for (int x = 0; x < bitmap.Width; x++)
                {
                    c = bitmap.GetPixel(x, y);
                    Color newColor = Color.FromArgb(c.R, 0, 0);
                    bitmap.SetPixel(x, y, newColor);
                }
            }
        }

        private string GetTextFromImage2(Bitmap screenshot)
        {
            
            var engine = new TesseractEngine(@"./tessdata", "eng");
            Page page = engine.Process(screenshot, PageSegMode.SingleLine);
            string result = page.GetText();
            return result;
            

            /*
            var Ocr = new AutoOcr();
            var Result = Ocr.Read(screenshot);
            return (Result.Text);
            */
            return null;
        }
        private string GetTextFromImage1(Bitmap screenshot)
        {
            try
            {
                
                List<tessnet2.Word> result = ocr.DoOCR(screenshot, System.Drawing.Rectangle.Empty);
                var text = string.Join(" ", result.Select(x => x.Text));
                return text;

            }
            catch (Exception)
            {
                return "";
            }
        }

        private void SaveScreenshot(Bitmap screenshot, string name)
        {
            screenshot.Save(name + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
        }

        public string GameProcessName = "WowB";
        public bool IsGameRunning()
        {
            return Process.GetProcessesByName(GameProcessName).Count() != 0;
        }
        public Process GetGameProcess()
        {
            return Process.GetProcessesByName(GameProcessName).FirstOrDefault();
        }

        public void Post(string url)
        {
            HttpClient client = new HttpClient();
            var response = client.GetAsync(url);
        }
        public RegionSelector regionSelector;
        private void buttonstart_Click(object sender, EventArgs e)
        {
            regionSelector = new RegionSelector();
            regionSelector.FormClosed += Re_FormClosed;
        }
        public void SetRegion(Rectangle rect, bool DontSave = false)
        {
            SelectedArea = rect;
            XValue.Text = rect.X.ToString();
            YValue.Text = rect.Y.ToString();
            if (!DontSave)
            {
                File.WriteAllText(LastSelectedAreaFile, JsonConvert.SerializeObject(SelectedArea));
            }

        }
        private void Re_FormClosed(object sender, FormClosedEventArgs e)
        {
            SetRegion(regionSelector.rect);
        }
        private string LastSelectedAreaFile = "LastSelectedArea.json";
    }
}
