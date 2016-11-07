using System;
using System.Collections;
using UnityEngine;

public interface ICoRunner {

	Coroutine Run (IEnumerator enumerator);

	Coroutine Delay (Action action, float delay_sec, bool unscaled);

	Coroutine WaitFor (Func<bool> predicate);

	Coroutine WaitForUnscaledSeconds (float seconds);

	void Stop (Coroutine coroutine);

	void StopAll ();
}

public static class Co {
	private static ICoRunner sceneInstance_ = null;
	private static ICoRunner permanentInstance_ = null;

	public static ICoRunner WithScene {
		get {
			if (sceneInstance_ == null) {
				var go = new GameObject("[CoRunner.Scene]");
				sceneInstance_ = go.AddComponent<CoRunner>();
			}
			return sceneInstance_;
		}
	}

	public static ICoRunner WithPermanent {
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
		return WithScene.Run(enumerator);
	}

	public static Coroutine Delay (Action action, float delay_sec, bool unscaled = false) {
		return WithScene.Delay(action, delay_sec, unscaled);
	}

	public static Coroutine WaitFor (Func<bool> predicate) {
		return WithScene.WaitFor(predicate);
	}

	public static Coroutine WaitForUnscaledSeconds (float seconds) {
		return WithScene.WaitForUnscaledSeconds(seconds);
	}

	public static void Stop (Coroutine coroutine) {
		WithScene.Stop(coroutine);
	}

	public static void StopAll () {
		WithScene.StopAll();
	}

	#endregion Shortcuts
}

public sealed class CoRunner : MonoBehaviour, ICoRunner {

	public Coroutine Run (IEnumerator enumerator) {
		return StartCoroutine(enumerator);
	}

	public Coroutine Delay (Action action, float delay_sec, bool unscaled) {
		return Run(CoDelay(action, delay_sec, unscaled));
	}

	private IEnumerator CoDelay (Action action, float delay_sec, bool unscaled) {
		if (unscaled) {
			var end = Time.unscaledTime + delay_sec;
			while (end > Time.unscaledTime)
				yield return null;
		} else {
			yield return new WaitForSeconds(delay_sec);
		}
		if (action != null)
			action();
	}

	public Coroutine WaitFor (Func<bool> predicate) {
		return Run(CoWaitFor(predicate));
	}

	private IEnumerator CoWaitFor (Func<bool> predicate) {
		while (predicate())
			yield return null;
	}

	public Coroutine WaitForUnscaledSeconds (float seconds) {
		return Run(CoWaitForUnscaledSeconds(seconds));
	}

	private IEnumerator CoWaitForUnscaledSeconds (float seconds) {
		var end = Time.unscaledTime + seconds;
		while (end > Time.unscaledTime)
			yield return null;
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