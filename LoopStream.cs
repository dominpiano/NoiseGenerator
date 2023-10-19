using Microsoft.VisualBasic;
using NAudio.Wave;
using ScottPlot.Drawing.Colormaps;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoiseGenerator
{
    public class LoopStream : ISampleProvider
    {
        private readonly ISampleProvider source;

        public MySampleProvider(ISampleProvider source)
        {
            this.source = source;
        }

        public int Read(float[] buffer, int offset, int count)
        {
            int samplesRead = source.Read(buffer, offset, count);
            // TODO: examine and optionally change the contents of buffer
            return samplesRead;
        }

        public WaveFormat WaveFormat
        {
            get { return source.WaveFormat; }
        }

        /*
        private void GenerateFunction()
        {
            //Generate function with samples
            var raw = new byte[BUFFER_LENGTH * 2];

            phase += 2 * Math.PI * freq / SAMPLE_RATE;
            if (phase >= 2 * Math.PI)
                phase -= 2 * Math.PI;
            for (int n = 0; n < BUFFER_LENGTH; n++)
            {

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
        */
    }
}
