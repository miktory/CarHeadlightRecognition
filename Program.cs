using System;
using System.Threading;
using System.Diagnostics;
using System.Drawing.Common;
using System.Drawing;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            ScreenshotProcess screenshotProcess = new ScreenshotProcess(@"C:\Screenshots");
            screenshotProcess.Start();
        }


public class ScreenshotProcess : IAsyncResult
    {
        private const int interval = 30000;
        private string location;

        public bool IsCompleted => throw new NotImplementedException();

        public WaitHandle AsyncWaitHandle => throw new NotImplementedException();

        public object AsyncState => throw new NotImplementedException();

        public bool CompletedSynchronously => throw new NotImplementedException();

        public ScreenshotProcess(string location)
        {
            this.location = location;
        }

        public void Start()
        {
            Thread thread = new Thread(new ThreadStart(TakeScreenshot));
            thread.Start();
        }

        private void TakeScreenshot()
        {
            while (true)
            {
                Bitmap screenshot = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
                Graphics graphics = Graphics.FromImage(screenshot);
                graphics.CopyFromScreen(0, 0, 0, 0, Screen.PrimaryScreen.Bounds.Size);

                string fileName = Path.Combine(location, DateTime.Now.ToString("yyyyMMddHHmmss") + ".png");
                screenshot.Save(fileName, ImageFormat.Png);

                Thread.Sleep(interval);
            }
        }
    }


}
}
