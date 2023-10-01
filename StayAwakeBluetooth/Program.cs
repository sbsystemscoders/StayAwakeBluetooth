using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;



namespace StayAwakeBluetooth
{

    /// <summary>
    /// Sam Sherwin.  Fed up with new bluetooth speaker going into energy saving mode, even when plugged in,
    /// so wrote this little console app to play a silent mp3 every 10 mins to keep it awake.
    /// Run and minimize.
    /// </summary>
    
    internal class Program
    {
        static void Main(string[] args)
        {
            Timer timer = new Timer(600000); // 10 minutes in milliseconds
            //Timer timer = new Timer(10000); // 10 secs 
            timer.Elapsed += PlayAudio;
            timer.AutoReset = true;
            timer.Enabled = true;

            Console.WriteLine("Press [Enter] to exit the application.");
            Console.ReadLine();
        }

        private static void PlayAudio(object sender, ElapsedEventArgs e)
        {
            Console.WriteLine("Playing audio to keep Bluetooth speaker alive...");

            // Access the MP3 file as a byte array from the Resources
            byte[] mp3Data = Properties.Resources._2;  

            // Convert the byte array to a memory stream
            using (MemoryStream mp3Stream = new MemoryStream(mp3Data))
            {
                using (var mp3Reader = new Mp3FileReader(mp3Stream))
                {
                    using (var waveOut = new WaveOutEvent())
                    {
                        waveOut.Init(mp3Reader);
                        waveOut.Play();
                        while (waveOut.PlaybackState == PlaybackState.Playing)
                        {
                            System.Threading.Thread.Sleep(100);
                        }
                    }
                }
            }
        }


    }
}
