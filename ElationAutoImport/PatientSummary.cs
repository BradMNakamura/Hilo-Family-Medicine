using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using NAudio.CoreAudioApi;
using NAudio.Wave;
using Google.Cloud.Speech.V1;
using Google.Cloud.Storage.V1;
using Google.LongRunning;


namespace ElationAutoImport
{

    public partial class PatientSummary : UserControl
    {
        //private static extern long mciSendString(string command, StringBuilder retstring, int returnLength, IntPtr callBack); 
        //private NAudio.Wave.WaveInBuffer sourceStream = null;
        //private NAudio.Wave.DirectSoundOut waveOut = null;
        //private System.IO.Stream outputFolder;
        //private System.IO.Stream outputFilePat;
        private BackgroundWorker audioWorker;
        private NAudio.Wave.WaveInEvent waveIn;
        static NAudio.Wave.WaveFileWriter writer;
        private int timeDuration;
        private TimeSpan ts;
        private bool recordStart;
        private bool isPlay;
        private bool timeStop;
        private bool foundRecorder;
        private int currVolume;
        public PatientSummary()
        {
            InitializeComponent();
            audioWorker = new BackgroundWorker();
            audioWorker.DoWork += AudioWorker_DoWork;
            writer = null;
            recordStart = false;
            isPlay = true;
            copyText.Hide();
            timerLabel.Text = "00:00";
            timerWorker.RunWorkerCompleted += timeWorker_RunWorkerCompleted;
            timeDuration = 0;
            timeStop = false;
            foundRecorder = false;
            volumeBar.Minimum = 0;
            volumeBar.Maximum = 50;
            currVolume = 0;
            //create enumerator
            int waveInDevices = WaveInEvent.DeviceCount;
            string[] audioList = new string[waveInDevices];
            for (int i = 0; i < waveInDevices; i++)
            {
                WaveInCapabilities deviceInfo = WaveInEvent.GetCapabilities(i);
                deviceList.Items.Add(deviceInfo.ProductName);
                //MessageBox.Show(deviceInfo.ProductName);
            }
            if(waveInDevices >= 1)
            {
                deviceList.SelectedIndex = 0;
            }
        }

        ~PatientSummary()
        {
            if (writer != null)
            {
                writer?.Dispose();
                writer = null;
            }
        }
        private void PatientSummary_Load(object sender, EventArgs e)
        {
        
        }


        private void RecordButton_Click(object sender, EventArgs e)
        {
            if (recordStart)
            {
                return;
            }
            if (writer == null)
            {
                copyText.Hide();
                timerLabel.Show();
                volumeBar.Show();
                timerLabel.Text = "00:00";
                writer?.Dispose();
                var outputFolder1 = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "NAudio");
                string path = outputFolder1.ToString() + @"\recorded2.wav";
                if (File.Exists(path))
                {
                    System.GC.Collect();
                    System.GC.WaitForPendingFinalizers();
                    File.Delete(path);
                }
                waveIn = new NAudio.Wave.WaveInEvent();
                var outputFilePath1 = Path.Combine(outputFolder1.ToString(), "recorded2.wav");
                waveIn.DeviceNumber = deviceList.SelectedIndex;
                waveIn.WaveFormat = new WaveFormat(20000, 1);
                if(!foundRecorder)
                    waveIn.DataAvailable += DataAvailable;
                writer = new WaveFileWriter(outputFilePath1, waveIn.WaveFormat);
            }
            volumeBar.Value = 4;
            if(!timerWorker.IsBusy)
            {
                timeStop = false;
                timerWorker.RunWorkerAsync();
            }
            timerLabel.ForeColor = Color.Green;
            waveIn.StartRecording();
            recordStart = true;
        }
        void DataAvailable(object s, WaveInEventArgs e)
        {
            if (recordStart)
            {
                writer.Write(e.Buffer, 0, e.BytesRecorded);
                writer.Flush();
            }
            float max = 0;
            // interpret as 16 bit audio
            for (int index = 0; index < e.BytesRecorded; index += 2)
            {
                short sample = (short)((e.Buffer[index + 1] << 8) |
                                        e.Buffer[index + 0]);
                // to floating point
                var sample32 = sample / 32768f;
                // absolute value 
                if (sample32 < 0) sample32 = -sample32;
                // is this the max value?
                if (sample32 > max) max = sample32;
            }
            if (!audioWorker.IsBusy)
            {
                currVolume = (int)(100 * max);
                Console.WriteLine(currVolume);
                audioWorker.RunWorkerAsync();
            }
            //Console.WriteLine((int)(100 * max));
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            timeStop = true;
            timerLabel.ForeColor = Color.Red;
            recordStart = false;
            waveIn.StopRecording();
        }

        private void AudioWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            AudioWorker_ProgressChanged(null, null);
        }

        private void AudioWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.Invoke(new MethodInvoker(delegate
            {
                volumeBar.Refresh();
                volumeBar.Value = currVolume;
                volumeBar.Refresh();

            }));
            AudioWorker_RunWorkerCompleted(null, null);
        }

        private void AudioWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
          
        }
        private async void SaveButton_Click(object sender, EventArgs e)
        {
            if (recordStart)
            {
                recordStart = false;
                waveIn.StopRecording();
            }
            writer?.Dispose();
            writer = null;
            timeDuration = 0;
            timerLabel.ForeColor = Color.Blue;
            timerLabel.Text = "Converting...";
            if(timeDuration < 60)
            {
                Task<string> task = new Task<string>(ProcessShortSpeech);
                task.Start();
                copyText.Text = await task;
            } else
            {
                Task<string> task = new Task<string>(ProcessLongSpeech);
                task.Start();
                copyText.Text = await task;
            }


            //MessageBox.Show(print);
            //copyText.Text = print;
            writer?.Dispose();
            writer = null;
            timerLabel.Hide();
            volumeBar.Hide();
            copyText.Show();
            Clipboard.SetText(copyText.Text);
        }

        private string ProcessShortSpeech()
        {
            string returnAns = "";

            string cred = @"C:\Users\david\OneDrive\Desktop\key.json";
            var outPutDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
            var savedPath = Path.Combine(outPutDirectory, @"ResourceFiles\key.json").Replace(@"file:\", "");
            System.Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", savedPath);
            if (File.Exists(savedPath))
            {
                UploadFileSample test = new UploadFileSample();
                test.UploadFile("elationautoimporter-bucket", @"C:\Users\david\OneDrive\Desktop\NAudio\recorded2.wav", "test.wav");
            }
            else return returnAns;
            var speech = SpeechClient.Create();
            var config = new RecognitionConfig
            {
                Encoding = RecognitionConfig.Types.AudioEncoding.Linear16,
                SampleRateHertz = 20000,
                LanguageCode = LanguageCodes.English.UnitedStates,
                EnableAutomaticPunctuation = true,
                UseEnhanced = true,
            };
            var audio = RecognitionAudio.FromStorageUri("gs://elationautoimporter-bucket/test.wav");
            var response = speech.Recognize(config, audio);
            List<string> translated = new List<string>();
            foreach (var results in response.Results)
            {
                foreach (var alternative in results.Alternatives)
                {
                    char x = '"';
                    string check = alternative.ToString().Remove(0, 17);
                    string[] getWord;
                    //x.ToString() + ','.ToString() + x.ToString() + 
                    if (check.Contains(x.ToString() + ", " + x.ToString() + "confidence" + x))
                    {
                        getWord = check.Split(new string[] { x.ToString() + ", " + x.ToString() + "confidence" + x }, StringSplitOptions.None);
                        translated.Add(getWord[0]);
                    }
                }
            }
            returnAns = "Appointment Date: " + DateTime.Now.ToString("g") + "\n\n";
            foreach (string x in translated)
            {
                returnAns += x;
            }
                
            return returnAns;
        }

        private string ProcessLongSpeech()
        {
            string returnAns = "";
            string cred = @"C:\Users\david\OneDrive\Desktop\key.json";
            System.Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", cred);
            if (File.Exists(@"C:\Users\david\OneDrive\Desktop\key.json"))
            {
                UploadFileSample test = new UploadFileSample();
                test.UploadFile("elationautoimporter-bucket", @"C:\Users\david\OneDrive\Desktop\NAudio\recorded2.wav", "test.wav");
                //timerLabel.ForeColor = Color.Blue;
                //timerLabel.Text = "File Uploaded!";
            }
            else
            {
                MessageBox.Show("lmao");
            }

            var speech = SpeechClient.Create();
            var config = new RecognitionConfig
            {
                Encoding = RecognitionConfig.Types.AudioEncoding.Linear16,
                SampleRateHertz = 20000,
                LanguageCode = LanguageCodes.English.UnitedStates,
                EnableAutomaticPunctuation = true,
                UseEnhanced = true,
            };
            var audio = RecognitionAudio.FromStorageUri("gs://elationautoimporter-bucket/test.wav");
            var response = speech.LongRunningRecognize(config, audio);
            Operation<LongRunningRecognizeResponse, LongRunningRecognizeMetadata> completedResponse = response.PollUntilCompleted();

            LongRunningRecognizeResponse result = completedResponse.Result;
            // Or get the name of the operation
            string operationName = response.Name;
            // This name can be stored, then the long-running operation retrieved later by name
            Operation<LongRunningRecognizeResponse, LongRunningRecognizeMetadata> retrievedResponse = speech.PollOnceLongRunningRecognize(operationName);
            // Check if the retrieved long-running operation has completed
            if (retrievedResponse.IsCompleted)
            {
                // If it has completed, then access the result
                LongRunningRecognizeResponse retrievedResult = retrievedResponse.Result;
                //var response = operation.
                List<string> translated = new List<string>();
                foreach (var results in retrievedResult.Results)
                {
                    foreach (var alternative in results.Alternatives)
                    {
                        string temp = "";
                        char x = '"';
                        string check = alternative.ToString().Remove(0, 17);
                        string[] getWord;
                        //x.ToString() + ','.ToString() + x.ToString() + 
                        if (check.Contains(x.ToString() + ", " + x.ToString() + "confidence" + x))
                        {
                            getWord = check.Split(new string[] { x.ToString() + ", " + x.ToString() + "confidence" + x }, StringSplitOptions.None);
                            translated.Add(getWord[0]);
                        }
                    }
                }
                returnAns = "Appointment Date: " + DateTime.Now.ToString("g") + "\n\n";
                foreach (string x in translated)
                {
                    returnAns += x;
                }
            }
            return returnAns;
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Up)
            {
                // Handle key at form level.
                // Do not send event to focused control by returning true.
                //MessageBox.Show("Recording");
                if (isPlay)
                {
                    this.RecordButton_Click(null, null);
                }
                else
                {
                    this.StopButton_Click(null, null);
                }
                isPlay = !isPlay;
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void timerWorker_DoWork(object sender, DoWorkEventArgs e)
        {
                if(timerWorker.CancellationPending)
                {
                    e.Cancel = true;
                    this.timeWorker_RunWorkerCompleted(null, null);
                }
                Thread.Sleep(1000);
        }

        private void timerWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ts = TimeSpan.FromSeconds(timeDuration);
            timerLabel.Text = string.Format("{0:00}:{1:00}", ts.Minutes, ts.Seconds);
        }

        private void timeWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (timeStop)
            {
                return;
            }
            ++timeDuration;
            ts = TimeSpan.FromSeconds(timeDuration);
            timerLabel.Text = string.Format("{0:00}:{1:00}", ts.Minutes, ts.Seconds);
            timerWorker.RunWorkerAsync();

        }

        private void volumeBar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("cringe");

        }

        private void timerLabel_Click(object sender, EventArgs e)
        {

        }

        private void volumeBar_RegionChanged(object sender, EventArgs e)
        {
            volumeBar.Value = currVolume;
        }
    }
    public class UploadFileSample
    {
        public void UploadFile(string bucketName = "your-unique-bucket-name", string localPath = "my-local-path/my-file-name", string objectName = "my-file-name")
        {
            var storage = StorageClient.Create();
            var fileStream = File.OpenRead(localPath);
            storage.UploadObject(bucketName, objectName, null, fileStream);
            Console.WriteLine($"Uploaded {objectName}.");
        }
    }

}

//timerLabel.Hide();
//copyText.Show();
/*foreach (var result in response.Results)
{
    foreach (var alternative in result.Alternatives)
    {
        string temp = "";
        char x = '"';
        string check = alternative.ToString().Remove(0, 17);
        string[] getWord;
        //x.ToString() + ','.ToString() + x.ToString() + 
        if (check.Contains(x.ToString() + ", " + x.ToString() + "confidence" + x))
        {
            getWord = check.Split(new string[] { x.ToString() + ", " + x.ToString() + "confidence" + x }, StringSplitOptions.None);
            text.Add(getWord[0]);
        }
    }
}

print = "Appointment Date: " + DateTime.Now.ToString() + "\n\n";
foreach (string x in text)
{
    print += x;
}*/