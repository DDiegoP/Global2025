using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using System;

[Serializable]
public class RecordMic : MonoBehaviour {
    [Header("Choose a Microphone")]
    private int RecordingDeviceIndex = 0;
    [TextArea] public string RecordingDeviceName = null;

    [Header("Volume")]
    public float volume;

    public float volumeLimit = 0.2f;
    private bool _recording;
    private List<string> _devicesName;
    private List<int> _devicesIndex;

    // FMOD Objects
    private FMOD.Sound sound;
    private FMOD.CREATESOUNDEXINFO exinfo;
    private FMOD.Channel channel;
    private FMOD.Channel reverbChannel;
    private FMOD.ChannelGroup channelGroup;

    // Number of recording devices
    private int numOfDriversConnected = 0;
    private int numOfDrivers = 0;

    // Device information
    private System.Guid MicGUID;
    private int SampleRate = 0;
    private FMOD.SPEAKERMODE fmodSpeakerMode;
    private int NumOfChannels = 0;
    private FMOD.DRIVER_STATE driverState;
    private FMOD.DSP playerDSP;

    // Guarda los nombres de todos los dispositivos
    private void GetAllDevicesInfo()
    {
        _devicesName = new List<string>();
        string aux;
        for (int i = 0; i < numOfDrivers; i++)
        {
            // Step 2: Get information about the recording device
            RuntimeManager.CoreSystem.getRecordDriverInfo(
                i,
                out aux,
                50,
                out MicGUID,
                out SampleRate,
                out fmodSpeakerMode,
                out NumOfChannels,
                out driverState);
            _devicesName.Add(aux);
        }
    }

    // Descarta los dispositivos que no sean microfonos
    private void DiscardDrivers()
    {
        _devicesIndex = new List<int>();
        for (int i = 0; i < _devicesName.Count; i++)
        {
            if (_devicesName[i].Contains("[loopback]")) numOfDrivers--;
            else _devicesIndex.Add(i);
        }
    }


    // Devuelve si hay algun micro conectado o no
    private bool AnyMicrophoneAvailable()
    {
        FMOD.RESULT result;

        // Step 1: Check if any recording devices are available
        result = RuntimeManager.CoreSystem.getRecordNumDrivers(out numOfDrivers, out numOfDriversConnected);
        GetAllDevicesInfo();
        DiscardDrivers();

        return result == FMOD.RESULT.OK && numOfDrivers > 0;
    }


    // Guardamos la informacion del microfono seleccionado
    private bool GetMicrophoneInfo()
    {
        // Step 2: Get information about the recording device
        FMOD.RESULT result = RuntimeManager.CoreSystem.getRecordDriverInfo(
            _devicesIndex[RecordingDeviceIndex],
            out RecordingDeviceName,
            50,
            out MicGUID,
            out SampleRate,
            out fmodSpeakerMode,
            out NumOfChannels,
            out driverState);

        if (result != FMOD.RESULT.OK)
        {
            Debug.LogError("Failed to get recording device info: " + result);
            return false;
        }

        Debug.Log("Se ha seleccionado el micro " + RecordingDeviceName);
        return true;
    }

    // Crea el sonido de FMOD (que es el input del microfono seleccionado)
    private bool CreateFMODSound()
    {
        // Step 3: Store relevant information into FMOD.CREATESOUNDEXINFO
        exinfo.cbsize = System.Runtime.InteropServices.Marshal.SizeOf(typeof(FMOD.CREATESOUNDEXINFO));
        exinfo.numchannels = NumOfChannels;
        exinfo.format = FMOD.SOUND_FORMAT.PCM16;
        exinfo.defaultfrequency = SampleRate;
        exinfo.length = (uint)SampleRate * sizeof(short) * (uint)NumOfChannels;

        // Step 4: Create an FMOD Sound object to hold the recording
        FMOD.RESULT result = RuntimeManager.CoreSystem.createSound(
            exinfo.userdata,
            FMOD.MODE.LOOP_NORMAL | FMOD.MODE.OPENUSER,
            ref exinfo,
            out sound);

        if (result != FMOD.RESULT.OK)
        {
            Debug.LogError("Failed to create sound: " + result);
            return false;
        }

        return true;
    }

    // Empieza a grabarlo y reproducirlo en el juego
    private void StartRecording()
    {
        // Step 5: Start recording into the Sound object
        FMOD.RESULT result = RuntimeManager.CoreSystem.recordStart(RecordingDeviceIndex, sound, true);
        if (result != FMOD.RESULT.OK)
        {
            Debug.LogError("Failed to start recording: " + result);
            return;
        }

        // Step 6: Start a coroutine to play the sound after a delay
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        // Create channel group
        FMOD.RESULT result = RuntimeManager.CoreSystem.createChannelGroup("Recording", out channelGroup);
        if (result != FMOD.RESULT.OK)
        {
            Debug.LogError("Failed to create channel group: " + result);
            yield break;
        }


        // Play the sound
        result = RuntimeManager.CoreSystem.playSound(sound, channelGroup, true, out channel);
        if (result != FMOD.RESULT.OK)
        {
            Debug.LogError("Failed to play sound: " + result);
            yield break;
        }

        Debug.Log("Ready to play");
        channel.setPaused(false);
        channel.setVolume(2f);
        channel.set3DMinMaxDistance(0, 0);
    }
    private void OnDestroy()
    {
        stop();
    }

    public void stop()
    {
        RuntimeManager.CoreSystem.recordStop(RecordingDeviceIndex);
        channel.stop();
    }
    public bool init()
    {
        if (AnyMicrophoneAvailable())
        {
            if (GetMicrophoneInfo() && CreateFMODSound())
            {
                StartRecording();
                Debug.Log("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
                _recording = true;
                return true;
            }
        }
        _recording = false;
        return false;
    }

    private bool CheckMicrophonesConnected()
    {
        if (AnyMicrophoneAvailable())
        {
            while (RecordingDeviceIndex >= numOfDrivers) RecordingDeviceIndex--;
            if (GetMicrophoneInfo() && CreateFMODSound())
            {
                StartRecording();
                _recording = true;
                return true;
            }

        } 
        return false;
    }

    public float GetLoudness()
    {
        if (!_recording)
            return 0;

        float rms = 0f;
        FMOD.RESULT result = channel.getDSP(RecordingDeviceIndex, out playerDSP);
        playerDSP.setMeteringEnabled(true, true);
        FMOD.DSP_METERING_INFO playerLevel;
        FMOD.DSP_METERING_INFO playerInput;
        result = playerDSP.getMeteringInfo(out playerInput, out playerLevel);
      
        float playerOutput = playerLevel.peaklevel[0] + playerLevel.peaklevel[1];
        rms = playerOutput * 3;

        return rms;
    }
}