using System;
using UnityEngine;
using UnityEngine.UI;

public class SineWaveView : MonoBehaviour
{
    [SerializeField]
    private Slider slider_Amplitude;

    [SerializeField]
    private Slider slider_WaveLength;

    [SerializeField]
    private Slider slider_CommonLength;

    [SerializeField]
    private Slider slider_PointPosition;

    [SerializeField]
    private SineWaveDrawer waveDrawer;

    private void Awake()
    {
        slider_Amplitude.value = waveDrawer.Amplitude;
        slider_WaveLength.value = waveDrawer.Wavelength;
        slider_CommonLength.value = waveDrawer.Length;

        slider_Amplitude.onValueChanged.AddListener(OnAmplitudeValueChanged);
        slider_WaveLength.onValueChanged.AddListener(OnWaveLengthValueChanged);
        slider_CommonLength.onValueChanged.AddListener(OnCommonLengthValueChanged);
        slider_PointPosition.onValueChanged.AddListener(OnPointPositionValueChanged);
    }

    private void OnDestroy()
    {
        slider_Amplitude.onValueChanged.RemoveListener(OnAmplitudeValueChanged);
        slider_WaveLength.onValueChanged.RemoveListener(OnWaveLengthValueChanged);
        slider_CommonLength.onValueChanged.RemoveListener(OnCommonLengthValueChanged);
        slider_PointPosition.onValueChanged.RemoveListener(OnPointPositionValueChanged);
    }

    private void OnPointPositionValueChanged(float value)
    {
        waveDrawer.SetPointPosition(value);
    }

    private void OnCommonLengthValueChanged(float value)
    {
        waveDrawer.SetLength(value);
    }

    private void OnWaveLengthValueChanged(float value)
    {
        waveDrawer.SetWavelength(value);
    }

    private void OnAmplitudeValueChanged(float value)
    {
        waveDrawer.SetAmplitude(value);
    }
}