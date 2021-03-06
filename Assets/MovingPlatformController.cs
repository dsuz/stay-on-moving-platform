﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// オブジェクトを左右に動かし、プレイヤーが接触した時にはそのオブジェクトを子オブジェクトに設定する。
/// プレイヤーが上に乗った時にプレイヤーも一緒に動いて、オブジェクトから落ちないようにする機能を提供する。
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class MovingPlatformController : MonoBehaviour
{
    /// <summary>振り幅（横）</summary>
    public float m_amplitude_x = 1f;
    /// <summary>振り幅（縦）</summary>
    public float m_amplitude_y = 0f;
    /// <summary>動く速さ</summary>
    public float m_speed = 2.0f;
    private float m_timer;
    private Vector3 m_initialPosition;

    private void Start()
    {
        m_initialPosition = transform.position;
    }

    void Update()
    {
        // オブジェクトを回す
        m_timer += Time.deltaTime * m_speed;
        float posX = Mathf.Sin(m_timer) * m_amplitude_x;
        float posY = Mathf.Cos(m_timer) * m_amplitude_y;

        Vector3 pos = m_initialPosition;
        pos = pos + new Vector3(posX, posY, 0);
        transform.position = pos;
    }

    // プレイヤーが上に乗った時、それを子オブジェクトとすることによりプレイヤーはオブジェクトと一緒に動く
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.tag == "Player")
            collision.collider.gameObject.transform.SetParent(transform);
    }

    // プレイヤーがオブジェクトから離れた時は、親子関係を解除する
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.gameObject.tag == "Player")
            collision.collider.gameObject.transform.SetParent(null);
    }
}
