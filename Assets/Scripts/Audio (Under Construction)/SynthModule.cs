using System.Collections.Generic;
using UnityEngine;

public class SynthModule : MonoBehaviour
{
    public double frequency = 440.0;
    private double _increment;
    private double _phase;
    private const double SampleRate = 48000.0;

    public enum WaveShape
    {
        Square,
        Sine
    }

    public WaveShape selectWaveShape;

    [Range(0.0f, 0.99f)] public float volume;
    [Range(0.0f, 0.99f)] private float _gain;


    public Dictionary<string, int> SoundFreqs = new Dictionary<string, int>()
    {
        {"Jump", 840}
};

public float[] frequencies;
    public int thisFreq;

    // private void Start()
    // {
    //     // frequencies = new float[8];
    //     // f
    // }


    #region Wave Shape

    // private float NoiseShape()
    // {
    //     var randFreq = Random.Range(40, 2000);
    //     
    // }
    
    
    private float SquareWave()
    {
        if (_gain * Mathf.Sin((float) _phase) >= 0)
        {
            return _gain * 0.6f;
        }
        else
        {
            return - _gain * 0.6f;
        }
    }

    private float SineWave()
    {
        return _gain * Mathf.Sin( (float)_phase);
    }
    
    

    #endregion


    

    private void OnAudioFilterRead(float[] data, int channels)
    {
        _increment = frequency * 2.0 * Mathf.PI / SampleRate;

        for (var i = 0; i < data.Length; i += channels)
        {
            _phase += _increment;
            
            if (selectWaveShape == WaveShape.Square)
            {
                data[i] = SquareWave();
            }

            if (selectWaveShape == WaveShape.Sine)
            {
                data[i] = SineWave();
            }

  
            if (channels == 2)
            {
                data[i + 1] = data[i];
            }

            if (_phase > (Mathf.PI * 2))
            {
                _phase = 0.0;
            }
        }
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            _gain = volume;
        }
        if (Input.GetKeyUp(KeyCode.X))
        {
            _gain = 0;
        }
    }
}
