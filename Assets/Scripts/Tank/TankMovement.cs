using UnityEngine;

public class TankMovement : MonoBehaviour {
	public int m_PlayerNumber = 1;
	public float m_TurnSpeed = 180f;
	public AudioSource m_MovementAudio;
	public AudioClip m_EngineIdling;
	public AudioClip m_EngineDriving;
	public float m_PitchRange = 0.2f;
	public float m_Speed = 6f;

	private string m_MoveVertical;
	private string m_MoveHorizontal;
	public float x;
	public float y;

	private void Start() {
	}

	private void Update() {
		Move(x, y);
	}

	// 坦克移动
	private void Move(float x, float z) {
		if (Mathf.Abs(x) < 0.1f && Mathf.Abs(z) < 0.1f) {
			return;
		}
		var xNew = transform.position.x + x;
		var zNew = transform.position.z + z;
		transform.LookAt(new Vector3(xNew, transform.position.y, zNew));
		transform.Translate(Vector3.forward * Time.deltaTime * m_Speed);
	}
}
