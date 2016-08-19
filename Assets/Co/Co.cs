using System;
using System.Collections;
using UnityEngine;

public enum CoRunnerType {

	/// <summary>
	/// It will be unloaded after scene has changed
	/// </summary>
	INSTANT,

	/// <summary>
	/// It will be alive forever even if scene has changed
	/// </summary>
	PERMANENT
}

public interface ICoRunner {

	Coroutine Run (IEnumerator enumerator);

	Coroutine Delay (float delay_sec, Action action);

	void Stop (Coroutine coroutine);

	void StopAll ();
}

public static class Co {
	private static ICoRunner instantInstance_ = null;
	private static ICoRunner permanentInstance_ = null;

	public static ICoRunner InstantRunner {
		get {
			if (instantInstance_ == null) {
				var go = new GameObject("[CoRunner.Instant]");
				instantInstance_ = go.AddComponent<CoRunner>();
			}
			return instantInstance_;
		}
	}

	public static ICoRunner PermanentRunner {
		get {
			if (permanentInstance_ == null) {
				var go = new GameObject("[CoRunner.Permanent]");
				GameObject.DontDestroyOnLoad(go);
				permanentInstance_ = go.AddComponent<CoRunner>();
			}
			return permanentInstance_;
		}
	}

	#region Shortcuts

	public static Coroutine Run (IEnumerator enumerator) {
		return InstantRunner.Run(enumerator);
	}

	public static Coroutine Delay (float delay_sec, Action action) {
		return InstantRunner.Delay(delay_sec, action);
	}

	public static void Stop (Coroutine coroutine) {
		InstantRunner.Stop(coroutine);
	}

	public static void StopAll () {
		InstantRunner.StopAll();
	}

	#endregion Shortcuts
}

public sealed class CoRunner : MonoBehaviour, ICoRunner {

	public Coroutine Run (IEnumerator enumerator) {
		return StartCoroutine(enumerator);
	}

	public Coroutine Delay (float delay_sec, Action action) {
		return Run(CoDelay(delay_sec, action));
	}

	private IEnumerator CoDelay (float delay_sec, Action action) {
		yield return new WaitForSeconds(delay_sec);
		if (action != null)
			action();
	}

	public void Stop (Coroutine coroutine) {
		if (coroutine == null)
			return;

		StopCoroutine(coroutine);
	}

	public void StopAll () {
		StopAllCoroutines();
	}
}
