using System.Collections.Generic;
using UnityEngine;

public class BeatDetector
{
    public static float[] DetectBeats(AudioClip clip, float threshold = 0.1f)
    {
        float[] samples = new float[clip.samples];
        clip.GetData(samples, 0);

        List<float> beats = new List<float>();

        int sampleRate = clip.frequency;

        for (int i = 1; i < samples.Length; i++)
        {
            if (samples[i] > threshold && samples[i - 1] <= threshold)
            {
                float time = (float)i / sampleRate;
                beats.Add(time);
            }
        }

        return beats.ToArray();
    }
}