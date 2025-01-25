using System;
using UnityEngine;

[Serializable]
public class MicrophoneInput {
    [SerializeField]
    private int sampleWindow = 64;

    AudioClip microphoneClip;

    bool reading = false;

    public void StartReading() {
        Debug.Log(Microphone.devices.Length);
        microphoneClip = Microphone.Start(Microphone.devices[0], true, 20, AudioSettings.outputSampleRate);
        reading = true;
    }

    public void EndReading() {
        Microphone.End(Microphone.devices[0]);
        reading = false;
    }

    float GetLoudnessFromAudioClip(int clipPosition, AudioClip clip) {
        Debug.Log("EE");
        int startPosition = clipPosition - sampleWindow;
        float[] waveData = new float[sampleWindow];
        clip.GetData(waveData, startPosition);

        float totalLoudness = 0f;

        for (int i = 0; i < sampleWindow; i++) {
            totalLoudness += Mathf.Abs(waveData[i]);
        }
        return totalLoudness / sampleWindow;
    }

    public float GetLoudness() {
        if (reading) return GetLoudnessFromAudioClip(Microphone.GetPosition(Microphone.devices[0]), microphoneClip);
        return 0f;
    }
}
