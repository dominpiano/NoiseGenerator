using FftSharp;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System;
using System.Data;
using System.Diagnostics;
using System.Reflection.Emit;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Windows.Forms;

namespace NoiseGenerator {
    public partial class MainWindow : Form {

        private const int SAMPLE_RATE = 48000;
        private const int BUFFER_LENGTH = 48000;

        private WaveFormat waveFormat;
        private WaveOut waveOut;
        int freq = 200; //a

        private RawSourceWaveStream rawSourceStream;

        private BufferedWaveProvider bufferedWaveProvider;

        public MainWindow() {
            InitializeComponent();

            timer1.Interval = BUFFER_LENGTH*1000/SAMPLE_RATE;
            waveFormat = new WaveFormat(SAMPLE_RATE, 16, 1);
            
        }

        private void playButton_Click(object sender, EventArgs e) {
            if (waveOut != null)
                return;

            //timer1.Enabled = true;
            //bufferedWaveProvider = new BufferedWaveProvider(waveFormat);
            //bufferedWaveProvider.BufferLength = SAMPLE_RATE * 4;
            //sampleManager = new AudioSampleManager(bufferedWaveProvider.ToSampleProvider(), SAMPLE_RATE);

            GenerateFunction();
            var audioLoop = new AudioLooper(rawSourceStream);
            waveOut = new WaveOut();
            //waveOut.DesiredLatency = 200;
            waveOut.Init(audioLoop);
            waveOut.Play();
        }
        private void stopButton_Click(object sender, EventArgs e) {
            timer1.Enabled = false;
            if (waveOut != null) {
                waveOut.Stop();
                waveOut.Dispose();
                waveOut = null;
            }
        }

        private void GenerateFunction() {
            //Generate function with samples
            timer1.Enabled = false;
            var raw = new byte[BUFFER_LENGTH * 2];
            

            /*
            double[] sampleNoise = new double[BUFFER_LENGTH];
            double[] filterCoefficients = [0.10091023048542094, 0.1513653457281314, 0.1870978567577278, 0.2, 0.1870978567577278, 0.1513653457281314, 0.10091023048542094];

            for (int i = 0; i < BUFFER_LENGTH; i++)
            {
                sampleNoise[i] = new Random().NextDouble();
            }

            var hopefullyPinkNoise = ApplyFIRFilter(sampleNoise, filterCoefficients);
            */


            for (int n = 0; n < BUFFER_LENGTH; n++) {
                //var audioSample = Math.Sin(Math.PI * 2 * (n * 1f / SAMPLE_RATE) * freq);
                var audioSample = (float)new Random().NextDouble();
                //var audioSample = sampleNoise[n];
                var sample = (short) (audioSample * Int16.MaxValue);
                var bytes = BitConverter.GetBytes(sample);
                raw[n * 2] = bytes[0];
                raw[n * 2 + 1] = bytes[1];
            }

            var ms = new MemoryStream(raw);
            rawSourceStream = new RawSourceWaveStream(ms, waveFormat);
            /*
            if (bufferedWaveProvider.BufferedBytes > 0)
                Debug.WriteLine("here");
            bufferedWaveProvider.AddSamples(raw, 0, raw.Length);
            timer1.Enabled = true;
            */
        }

        public static double[] ApplyFIRFilter(double[] signal, double[] filterCoefficients) {
            int signalLength = signal.Length;
            int filterLength = filterCoefficients.Length;
            int outputLength = signalLength + filterLength - 1;
            double[] output = new double[outputLength];

            for (int n = 0; n < outputLength; n++) {
                output[n] = 0.0;
                for (int k = 0; k < filterLength; k++) {
                    if (n - k >= 0 && n - k < signalLength) {
                        output[n] += signal[n - k] * filterCoefficients[k];
                    }
                }
            }

            return output;
        }

        //Calkiem dobra funkcja tworzaca rozowy szum
        //Ale zajebiœcie wolna
        /*
        public double[] createPinkNoise(long sampleCount, int quality = 10000, double lowestFrequency = 20, double highestFrequency = 20000, double volumeAdjust = 5.0)
        {
            long samples = sampleCount;
            double[] d = new double[samples];
            double[] offsets = new double[samples];
            double lowestWavelength = highestFrequency / lowestFrequency;
            Random r = new Random();
            for (int j = 0; j < quality; j++)
            {
                double wavelength = Math.Pow(lowestWavelength, (j * 1.0) / quality) * SAMPLE_RATE / highestFrequency;
                double offset = r.NextDouble() * Math.PI * 2;     // Important offset is needed, as otherwise all the waves will be almost in phase, and this will ruin the effect!
                for (long i = 0; i < samples; i++)
                {
                    d[i] += Math.Cos(i * Math.PI * 2 / wavelength + offset) / quality * volumeAdjust;
                }
            }
            return d;
        }
        */

        private void hScrollBar1_ValueChanged(object sender, EventArgs e) {
            freq = hScrollBar1.Value + 200;
        }

        private void MainWindow_FormClosed(object sender, FormClosedEventArgs e) {
            Environment.Exit(Environment.ExitCode);
        }

        private void timer1_Tick(object sender, EventArgs e) {
            GenerateFunction();
        }
    }
}