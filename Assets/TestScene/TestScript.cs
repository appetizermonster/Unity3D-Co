using System.Collections;
using UnityEngine;

public class PlainClass {

	public void TestCoroutine () {
		Co.Run(CountSeconds());
	}

	private IEnumerator CountSeconds () {
		for (var i = 1; i <= 10; ++i) {
			yield return new WaitForSeconds(1f);
			Debug.Log(i + " seconds elasped");
		}
	}
}

public class TestScript : MonoBehaviour {

	private void Start () {
		Co.Delay(1f, () => Debug.Log("after 1 second"));
		Co.Delay(2f, () => Debug.Log("after 2 seconds"));

		var plainClass = new PlainClass();
		plainClass.TestCoroutine();
	}
}
