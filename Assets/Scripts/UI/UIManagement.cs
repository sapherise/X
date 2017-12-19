using UnityEngine;
using UnityEngine.UI;

public class UIManagement : MonoBehaviour {
	public GameManager gm;

	void Start() {
	}

	void OnGUI() {
		if (GUI.Button(new Rect(50, 50, 50, 50), "0")) {
			print("已切换到 0 玩家");
			gm.current = 0;
		}

		if (GUI.Button(new Rect(110, 50, 50, 50), "1")) {
			print("已切换到 1 玩家");
			gm.current = 1;
		}
	}
}
