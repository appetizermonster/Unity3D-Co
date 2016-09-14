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
		Co.Delay(() => Debug.Log("after 1 second"), 1f);
		Co.Delay(() => Debug.Log("after 2 seconds"), 2f);

		var plainClass = new PlainClass();
		plainClass.TestCoroutine();
	}
}
