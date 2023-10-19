using FftSharp;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;

namespace NoiseGenerator {
    public partial class MainWindow : Form {

        private const int SAMPLE_RATE = 48000;
        private const int BUFFER_LENGTH = 24000;

        private WaveFormat waveFormat;
        private WaveOut waveOut;

        private RawSourceWaveStream rs;
        LoopStream loop;
        int freq = 203; //a

        readonly double[] AudioValues;
        readonly double[] FftValues;

        Thread updateBufferThread;
        bool runThread = false;

        private BufferedWaveProvider bufferedWaveProvider;

        public MainWindow() {
            InitializeComponent();

            /*
            AudioValues = new double[BUFFER_LENGTH];
            double[] paddedAudio = Pad.ZeroPad(AudioValues);
            double[] fftMag = Transform.FFTmagnitude(paddedAudio);
            FftValues = new double[fftMag.Length];
            double fftPeriod = FFT.FrequencyResolution(fftMag.Length, SAMPLE_RATE);
            formsPlot1.Plot.AddSignal(FftValues, 1.0 / fftPeriod);
            formsPlot1.Plot.YLabel("Spectral Power");
            formsPlot1.Plot.XLabel("Frequency (kHz)");
            formsPlot1.Refresh();
            */

            //updateBufferThread = new Thread(UpdateBuffer);
            //updateBufferThread.Start();

            waveFormat = new WaveFormat(SAMPLE_RATE, 16, 1);
        }


        private void playButton_Click(object sender, EventArgs e) {
            if (waveOut != null)
                return;
            //bufferedWaveProvider = new BufferedWaveProvider(waveFormat);
            GenerateFunction();
            loop = new LoopStream(rs, BUFFER_LENGTH);
            waveOut = new WaveOut();
            waveOut.Init(loop);
            waveOut.Play();
            //runThread = true;
        }
        private void stopButton_Click(object sender, EventArgs e) {
            //runThread = false;
            if (waveOut != null) {
                waveOut.Stop();
                waveOut.Dispose();
                waveOut = null;
            }
        }
        double phase;

        private void GenerateFunction() {
            //Generate function with samples
            var raw = new byte[BUFFER_LENGTH * 2];
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
            phase += 2 * Math.PI * freq / SAMPLE_RATE;
            if (phase >= 2 * Math.PI)
                phase -= 2 * Math.PI;


            for (int n = 0; n < BUFFER_LENGTH; n++) {

                var sineSample = Math.Sin(Math.PI * 2 * (n * 1f / SAMPLE_RATE) * freq + phase);
                //AudioValues[n] = sineSample;
                var sample = (short) (sineSample * Int16.MaxValue);
                var bytes = BitConverter.GetBytes(sample);
                raw[n * 2] = bytes[0];
                raw[n * 2 + 1] = bytes[1];
            }
            //for (int n = 0; n < 2; n++)
            //    bufferedWaveProvider.AddSamples(raw, 0, raw.Length);
            //GenerateFunction();
            var ms = new MemoryStream(raw);
            rs = new RawSourceWaveStream(ms, waveFormat);
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
            loop.frequency = hScrollBar1.Value + 200;
        }

        //Method to update buffer if it's available
        private void UpdateBuffer() {
            while (true) {
                if (runThread) {
                    //while(bufferedWaveProvider)
                    //GenerateFunction();
                }
            }
        }

        private void MainWindow_FormClosed(object sender, FormClosedEventArgs e) {
            Environment.Exit(Environment.ExitCode);
        }
    }
}