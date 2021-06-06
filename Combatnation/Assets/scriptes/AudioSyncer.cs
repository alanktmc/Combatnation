using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

/// <summary>
/// Parent class responsible for extracting beats from..
/// ..spectrum value given by AudioSpectrum.cs
/// </summary>
public class AudioSyncer : MonoBehaviour
{
	public float bias;
	public float timeStep;
	public float timeToBeat;
	public float restSmoothTime;

	private float m_previousAudioValue;
	private float m_audioValue;
	private float m_timer;

	public bool m_isBeat;
	
	public AudioSpectrum main;
	bool time_to_activate = true;

	private Volume volume;
	private ChromaticAberration ca;

	public virtual void OnBeat()
	{


		//Debug.Log("beat"+gameObject.name);
		m_timer = 0;
		m_isBeat = true;
		//ca.intensity.value = 0.0f;

	}

	public virtual void OnUpdate()
	{
		int random_target = Random.Range(0,5);
		if (random_target == 5)
			random_target -= 1;
		// update audio value
		m_previousAudioValue = m_audioValue;
		m_audioValue = AudioSpectrum.spectrumValue;

		// if audio value went below the bias during this frame
		if (m_previousAudioValue > gameObject.GetComponent<AudioSyncer>().bias &&
			m_audioValue <= gameObject.GetComponent<AudioSyncer>().bias)
		{
			// if minimum beat interval is reached
			//if (m_timer > timeStep && time_to_activate)
			if (m_timer > timeStep)
			{
				main.targets[random_target].SetActive(true);
				OnBeat();
			}
		}
		
		// if audio value went above the bias during this frame
		if (m_previousAudioValue <= gameObject.GetComponent<AudioSyncer>().bias &&
			m_audioValue > gameObject.GetComponent<AudioSyncer>().bias)
		{
			// if minimum beat interval is reached
			//if (m_timer > timeStep && time_to_activate)
			if (m_timer > timeStep)
			{
				main.targets[random_target].SetActive(true);
				OnBeat();
			}
		}

		m_timer += Time.deltaTime;
	}

	private void Update()
	{
		int temp_count = 0;
		for(int i =0; i < 5; i++)
        {
			if (main.targets[i].activeSelf)
				temp_count++;
        }

		if (temp_count > 2)
        {
			StartCoroutine(delay());
			temp_count = 0;
		}

		OnUpdate();
	}

	IEnumerator delay()
	{
		time_to_activate = false;
		yield return new WaitForSeconds(1.5f);
		time_to_activate = true;

	}

}
