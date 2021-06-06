using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

//using UnityEngine.Rendering.Universal;

public class AudioSyncScale : AudioSyncer
{
	public Vector3 beatScale;
	public Vector3 restScale;
	public GameObject missed;
	public bool beatttt;

	public override void OnUpdate()
	{
		base.OnUpdate();
		beatttt = m_isBeat;
		if (m_isBeat) return;
		if (!gameObject.name.Equals("target_backup")) {
			StartCoroutine(Destroy());
		}
	}

	public override void OnBeat()
	{
		base.OnBeat();
		StopCoroutine("MoveToScale");
		StartCoroutine("MoveToScale", beatScale);

		if (!gameObject.name.Equals("target_backup"))
		{
			StartCoroutine(Destroy());
		}

	}

	private IEnumerator MoveToScale(Vector3 _target)
	{
		if (!gameObject.name.Equals("target_backup"))
		{
		Vector3 _curr = transform.localScale;
		Vector3 _initial = _curr;
		float _timer = 0;
		while (_curr != _target)
		{
			_curr = Vector3.Lerp(_initial, _target, _timer / timeToBeat);
			_timer += Time.deltaTime;
			transform.localScale = _curr;
			yield return null;
		}
			m_isBeat = false;

		}
	}

	IEnumerator Destroy()
	{
		transform.localScale = Vector3.Lerp(transform.localScale, restScale, restSmoothTime * Time.deltaTime);
		yield return new WaitForSeconds(2f);

		gameObject.SetActive(false);
		missed.SetActive(true);
		Debug.Log("time to destroy");
	}


}
