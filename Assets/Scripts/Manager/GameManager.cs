using System;
using UnityEngine;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

[Serializable]
public class GameManager : MonoBehaviour {
    public GameObject m_TankPrefab;         
    public TankManager[] m_Tanks;
	public int current = 1;
	public float m_Speed = 6f;
    public UDPManagement udp;
    UdpClient client;

    private void Start() {
        SpawnAllTanks();
        initNetwork();
    }

    private void initNetwork() {
        udp = new UDPManagement();
        client = udp.Start();
        Thread t = new Thread(SAReceiveMessage);
        t.Start();

        udp.SASendMessage("ENTER");
    }

    // 接受服务器的消息，并使某个坦克移动到某个位置
    public void SAReceiveMessage() {
        IPEndPoint point = new IPEndPoint(IPAddress.Any, 0);
        while (true) {
            Byte[] receiveBytes = client.Receive(ref point);
            string content = Encoding.UTF8.GetString(receiveBytes);
            print("接收：" + content);
            // var json = JsonUtility.FromJson<JsonMovement>(content);
            // var playerNumber = json.playerNumber;
            // print("接收：" + json.x + ", " + json.y);
            // print("==");
            // m_Tanks[playerNumber].Move(json.x, json.y);
        }
    }

	// 创建所有坦克
    private void SpawnAllTanks() {
        for (int i = 0; i < m_Tanks.Length; i++) {
			var p = m_Tanks[i].m_SpawnPoint.position;
			var r = m_Tanks[i].m_SpawnPoint.rotation;
            m_Tanks[i].m_Instance = Instantiate(m_TankPrefab, p, r) as GameObject;
            m_Tanks[i].m_PlayerNumber = i;
            m_Tanks[i].Setup();
        }
    }

    private void EnableTankControl() {
        for (int i = 0; i < m_Tanks.Length; i++) {
            m_Tanks[i].EnableControl();
        }
    }

    private void DisableTankControl() {
        for (int i = 0; i < m_Tanks.Length; i++) {
            m_Tanks[i].DisableControl();
        }
    }

	private void OnEnable() {
		EasyJoystick.On_JoystickMove += OnJoystickMove;
		EasyJoystick.On_JoystickMoveEnd += OnJoystickMoveEnd;
	}

	private void OnDisable() {
	}


	// 虚拟摇杆移动，将移动的位置发到服务器
	void OnJoystickMove(MovingJoystick move) {
		if (move.joystickName != "Joystick") {
			return;
		}
		float x = move.joystickAxis.x;
		float y = move.joystickAxis.y;

        // 当移动摇杆时，创建角色操作行为的 json
        var jsonMovement = new JsonMovement();
        jsonMovement.x = x;
        jsonMovement.y = y;
        jsonMovement.playerNumber = current;
        string json = JsonUtility.ToJson(jsonMovement);

        print("发送：" + x + ", " + y);
        print("==");
        // 将 json 发送到服务器
        udp.SASendMessage(json);
	}

	// 虚拟摇杆结束移动，将停止的信息发到服务器
	void OnJoystickMoveEnd(MovingJoystick move) {
		// if (move.joystickName == "Joystick") {
        // // 当移动摇杆时，创建角色操作行为的 json
        // var jsonMovement = new JsonMovement();
        // jsonMovement.x = 0;
        // jsonMovement.y = 0;
        // jsonMovement.playerNumber = current;
        // string json = JsonUtility.ToJson(jsonMovement);

        // // 将 json 发送到服务器
        // udp.SASendMessage(json);
		// }
	}
}