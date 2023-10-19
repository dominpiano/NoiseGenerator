using FftSharp;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;

namespace NoiseGenerator {
    public partial class MainWindow : Form {

        private WaveLib.WaveOutPlayer m_Player;
        private WaveLib.WaveFormat m_Format;
        byte[] samplesBuffer;
        private const int SAMPLE_RATE = 48000;
        private const int BUFFER_LENGTH = 1024;
        private const int BYTE_BUFFER_LENGTH = BUFFER_LENGTH * 2;

        public MainWindow() {
            InitializeComponent();

            m_Format = new WaveLib.WaveFormat(SAMPLE_RATE, 16, 1);
            samplesBuffer = new byte[BYTE_BUFFER_LENGTH];
        }


        private void Stop() {
            if (m_Player != null) {
                try {
                    m_Player.Dispose();
                }
                finally {
                    m_Player = null;
                }
            }
        }

        private byte[] _leftBuffer;
        private void Play() {
            //Stop();
            //GenerateFunction();
            //m_Player = new WaveLib.WaveOutPlayer(-1, m_Format, BYTE_BUFFER_LENGTH - 64, 3, (data, size) => {
            //    byte[] b = samplesBuffer;
            //    System.Runtime.InteropServices.Marshal.Copy(b, 0, data, size);
            //});
            openFileDialog1.ShowDialog();
            using (var reader = new Mp3FileReader(openFileDialog1.FileName)) {
                var pcmLength = (int) reader.Length;
                _leftBuffer = new byte[pcmLength / 2];
                var buffer = new byte[pcmLength];
                var bytesRead = reader.Read(buffer, 0, pcmLength);

                int index = 0;
                for (int i = 0; i < bytesRead; i += 4) {
                    _leftBuffer[index] = buffer[i];
                    index++;
                    _leftBuffer[index] = buffer[i + 1];
                    index++;
                }
                var player = new WaveLib.WaveOutPlayer(-1, new WaveLib.WaveFormat(44100, 16, 1), _leftBuffer.Length, 1, (data, size) => {
                    byte[] b = _leftBuffer;
                    System.Runtime.InteropServices.Marshal.Copy(b, 0, data, size);
                });
            }
        }

        private void playButton_Click(object sender, EventArgs e) {
            Play();
        }

        private void stopButton_Click(object sender, EventArgs e) {
            Stop();
        }

        private void GenerateFunction() {
            //Generate function with samples
            var raw = new byte[BYTE_BUFFER_LENGTH];
            /*
            //var sampleNoise = createPinkNoise(SAMPLE_RATE);
            double[] sampleNoise = new double[BUFFER_LENGTH];
            double[] filterCoefficients = [0.10091023048542094, 0.1513653457281314, 0.1870978567577278, 0.2, 0.1870978567577278, 0.1513653457281314, 0.10091023048542094];

            for (int i = 0; i < BUFFER_LENGTH; i++)
            {
                sampleNoise[i] = new Random().NextDouble();
            }

            var hopefullyPinkNoise = ApplyFIRFilter(sampleNoise, filterCoefficients);
            */
            for (int n = 0; n < BUFFER_LENGTH; n++) {

                var sineSample = Math.Sin(Math.PI * 2 * (n * 1f / SAMPLE_RATE) * 440);
                //AudioValues[n] = sineSample;
                var sample = (short) (sineSample * Int16.MaxValue);
                var bytes = BitConverter.GetBytes(sample);
                raw[n * 2] = bytes[0];
                raw[n * 2 + 1] = bytes[1];

            }

            samplesBuffer = raw;
        }

        private void MainWindow_FormClosed(object sender, FormClosedEventArgs e) {
            Environment.Exit(Environment.ExitCode);
        }
    }
}