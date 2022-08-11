using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector3 m_movement;
    private float m_horizontal;
    private float m_vertial;
    private Animator m_animator;
    private Rigidbody m_rigidbody;
    private float m_turnSpeed = 20.0f;
    private Quaternion m_rotation = Quaternion.identity;

    // Start is called before the first frame update
    void Start()
    {
        m_animator = GetComponent<Animator>();
        m_rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        m_horizontal = Input.GetAxis("Horizontal");
        m_vertial = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        m_movement.Set(m_horizontal, 0.0f, m_vertial);
        m_movement.Normalize();

        bool hasHorizontalMove = !Mathf.Approximately(m_horizontal, 0.0f);
        bool hasVerticalMove = !Mathf.Approximately(m_vertial, 0.0f);

        bool isWalking = hasHorizontalMove || hasVerticalMove;

        m_animator.SetBool("isWalking", isWalking);

        // 用三维矢量表示玩家的转动方向
        Vector3 desiredRotation = Vector3.RotateTowards(transform.forward, m_movement, Time.deltaTime * m_turnSpeed, 0.0f);
        m_rotation = Quaternion.LookRotation(desiredRotation);
    }

    private void OnAnimatorMove()
    {
        m_rigidbody.MovePosition(m_rigidbody.position + m_movement * m_animator.deltaPosition.magnitude);
        m_rigidbody.MoveRotation(m_rotation);
    }
}
