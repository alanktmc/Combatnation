using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSpectrum : MonoBehaviour
{
    public GameObject[] targets;
    public float testvalue;

    public GameObject music;
    public float dalay_music_time;

    private void Update()
    {
        // get the data
        StartCoroutine(delaytostart());
    }

    private void Start()
    {
        /// initialize buffer
        m_audioSpectrum = new float[128];
        StartCoroutine(delaytostart());
        music.GetComponent<AudioSource>().Play();

        //count how many secs to start

    }

    // This value served to AudioSyncer for beat extraction
    public static float spectrumValue { get; private set; }

    // Unity fills this up for us
    private float[] m_audioSpectrum;

    IEnumerator delaytostart()
    {
        yield return new WaitForSeconds(dalay_music_time);
        //After we have waited 5 seconds print the time again.
        gameObject.GetComponent<AudioSource>().GetSpectrumData(m_audioSpectrum, 0, FFTWindow.BlackmanHarris);
        // assign spectrum value
        // this "engine" focuses on the simplicity of other classes only..
        // ..needing to retrieve one value (spectrumValue)
        if (m_audioSpectrum != null && m_audioSpectrum.Length > 0)
        {
            spectrumValue = m_audioSpectrum[0] * 100 * 1000;
        }
        if (spectrumValue > testvalue) ;
    }
}
//Debug.Log(spectrumValue);
