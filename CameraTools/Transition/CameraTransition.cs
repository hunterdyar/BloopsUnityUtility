using System;
using System.Collections;
using UnityEngine;
public class CameraTransition : MonoBehaviour
{
	[SerializeField] private Material _transitionMaterial;
	 public Color TransitionColor;
	public static Action TransitionInDoneEvent;
	public static Action TransitionOutDoneEvent;
	public float timeForTransition;
	// public UnityEvent OnTransitionComplete;
	// public UityEvent OnTransitionStart;
	public bool OpenOnStart;
	private Blit _blit;
	private int _lerpID;
	private int _colorID;
	private Action afterTransitionAction;
	private void Awake()
	{
		_lerpID = Shader.PropertyToID("_Lerp");
		_colorID = Shader.PropertyToID("_Color");
		_blit = gameObject.AddComponent<Blit>();
		_blit._transitionMaterial = _transitionMaterial;
		_blit.enabled = false;
		
		
	}

	private void Start()
	{
		if (OpenOnStart)
		{
			_blit.enabled = true;
			TransitionOpenCurtain();
		}
	}

	IEnumerator Transition(float start, float end, float totalTime)
	{
		_blit.enabled = true;
		_transitionMaterial.SetFloat(_lerpID,start);
		_transitionMaterial.SetColor(_colorID,TransitionColor);
		float t = 0;
		while (t < 1)
		{
			_transitionMaterial.SetFloat(_lerpID,Mathf.Lerp(start,end,t));
			t += Time.deltaTime / totalTime;
			yield return null;
		}
		_transitionMaterial.SetFloat(_lerpID,end);
		yield return null;
		if (end == 0)
		{
			_blit.enabled = false;
			TransitionOutDoneEvent?.Invoke();
		}else if (end == 1)
		{
			TransitionInDoneEvent?.Invoke();
		}
		afterTransitionAction?.Invoke();
	}

	[ContextMenu("Transition In")]
	public Coroutine TransitionCloseCurtain()
	{
		afterTransitionAction = null;
		return StartCoroutine(Transition(0, 1, timeForTransition));
	}
	[ContextMenu("Transition Out")]

	public Coroutine TransitionOpenCurtain()
	{
		afterTransitionAction = null;
		return StartCoroutine(Transition(1, 0, timeForTransition));
	}

	public void TransitionOpenCurtain(Action afterAction)
	{
		afterTransitionAction = afterAction;
		StartCoroutine(Transition(1, 0, timeForTransition));
	}
	public Coroutine TransitionCloseCurtain(Action afterAction)
	{
		afterTransitionAction = afterAction;
		return StartCoroutine(Transition(0, 1, timeForTransition));
	}
}
