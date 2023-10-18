using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System;
using System.Text;

namespace NoiseGenerator
{
    public partial class MainWindow : Form
    {

        private const int SAMPLE_RATE = 48000;
        private const int BUFFER_LENGTH = 1024; //Must be power of 2

        private SignalGenerator sg;
        private WaveFormat waveFormat;
        private WaveOutEvent waveOut;
        private BufferedWaveProvider bufferedWaveProvider;

        public MainWindow()
        {
            InitializeComponent();

            waveFormat = new WaveFormat(48000, 16, 1);
            sg = new SignalGenerator();
            sg.Type = SignalGeneratorType.Pink;
        }
        private void playButton_Click(object sender, EventArgs e)
        {
            bufferedWaveProvider = new BufferedWaveProvider(sg.WaveFormat);
            timer1.Enabled = true;

            waveOut = new WaveOutEvent();
            waveOut.Init(bufferedWaveProvider);
            waveOut.Play();
            
        }
        private void stopButton_Click(object sender, EventArgs e)
        {
            waveOut.Stop();
            timer1.Enabled = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int frameSize = BUFFER_LENGTH;
            var frames = new byte[frameSize];
            var wp = new SampleToWaveProvider(sg);
            wp.Read(frames, 0, frameSize);

            bufferedWaveProvider.AddSamples(frames, 0, frameSize);
        }
    }
}