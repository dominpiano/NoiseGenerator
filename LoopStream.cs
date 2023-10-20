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
        private int SAMPLE_RATE;
        public int frequency = 200;

        public LoopStream(ISampleProvider source, int sampleRate)
        {
            this.source = source;
            this.SAMPLE_RATE = sampleRate;
        }

        public int Read(float[] buffer, int offset, int count)
        {
            int samplesRead = source.Read(buffer, offset, count);

            GenerateFunction(buffer, count);

            return samplesRead;
        }

        public WaveFormat WaveFormat
        {
            get { return source.WaveFormat; }
        }

        
        private float[] GenerateFunction(float[] samplesToModify, int count)
        {

            for (int n = 0; n < count; n++)
            {
                samplesToModify[n] = (float)Math.Sin(Math.PI * 2 * (n * 1f / SAMPLE_RATE) * 100);
            }
            return samplesToModify;
        }
        
    }
}
