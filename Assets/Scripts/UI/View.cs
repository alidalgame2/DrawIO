using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class View<T> : SingletonMB<T> where T : MonoBehaviour
{
	public float			m_FadeInDuration = 0.3f;
	public float 			m_FadeOutDuration = 0.3f;
	public CanvasGroup      m_Group;

	protected bool 			m_Visible;

	// Cache
	protected GameManager	m_GameManager;

	// Buffers
	private float 			m_StartTime;

	protected virtual void Awake()
	{
		// Cache
		m_GameManager = GameManager.Instance;

		// Init
		m_Visible = false;
		m_Group.alpha = 0.0f;
		m_Group.interactable = false;
		m_Group.blocksRaycasts = false;

		m_GameManager.onGamePhaseChanged += OnGamePhaseChanged;
	}

	protected virtual void OnGamePhaseChanged (GamePhase _GamePhase) {}

	public void Transition(bool _Appear)
	{
		m_Visible = _Appear;
		if(_Appear)
        {
			appearDisappearCoroutine = Appear();
			return;
		}
		appearDisappearCoroutine = Disappear();
	}

	private Coroutine appearDisappearCoroutine;

	public virtual void Initialize()
	{
		m_Group = GetComponent<CanvasGroup>();
	}
	public virtual Coroutine Appear()
	{
		StopAppearDisappearCoroutine();
		return StartCoroutine(AppearRoutine());
	}

	public IEnumerator AppearRoutine()
	{
		while (m_Group.alpha < 1)
		{
			m_Group.alpha += Time.deltaTime / m_FadeInDuration;
			yield return new WaitForEndOfFrame();
		}
		m_Group.blocksRaycasts = true;
		m_Group.interactable = true;
	}

	public virtual Coroutine Disappear()
	{
		StopAppearDisappearCoroutine();
		return StartCoroutine(DisappearRoutine());
	}

	private void StopAppearDisappearCoroutine()
	{
		if (appearDisappearCoroutine != null)
		{
			StopCoroutine(appearDisappearCoroutine);
		}
	}

	public IEnumerator DisappearRoutine()
	{
		m_Group.blocksRaycasts = false;
		m_Group.interactable = false;
		while (m_Group.alpha > 0)
		{
			m_Group.alpha -= Time.deltaTime / m_FadeInDuration;
			yield return new WaitForEndOfFrame();
		}
		
	}
}
