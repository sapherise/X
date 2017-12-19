using System;
using UnityEngine;

[Serializable]
public class TankManager {
    public Color m_PlayerColor;            
    public Transform m_SpawnPoint;
    [HideInInspector] public int m_PlayerNumber;
    [HideInInspector] public GameObject m_Instance;
	public float m_Speed = 6f;
 
    private TankMovement m_Movement;
    private GameObject m_CanvasGameObject;

    public void Setup() {
        m_Movement = m_Instance.GetComponent<TankMovement>();
        m_CanvasGameObject = m_Instance.GetComponentInChildren<Canvas>().gameObject;
        m_Instance.name = "Tank" + m_PlayerNumber;
        m_Movement.m_PlayerNumber = m_PlayerNumber;
    }

    public void DisableControl() {
        m_Movement.enabled = false;
        m_CanvasGameObject.SetActive(false);
    }

    public void EnableControl() {
        m_Movement.enabled = true;

        m_CanvasGameObject.SetActive(true);
    }

    public void Move(float x, float y) {
        m_Movement.x = x;
        m_Movement.y = y;
    }
}
