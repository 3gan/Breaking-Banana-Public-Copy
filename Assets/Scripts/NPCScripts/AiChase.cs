using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System;

public class AiChase : MonoBehaviour
{
    public GameObject player;
    public float speed;

    private float distance;
    private Animator anim;

    public GameObject gameOverUI;
    public CursorController cursorController;
    public GameObject hotbar;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;

        transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);

        PlayDirectionAnimation(angle);
    }
    void PlayDirectionAnimation(float angle)
    {
        if (angle >= -45 && angle <= 45)
        {
            anim.Play("WalkUp");
        }
        else if (angle >= 135 || angle <= -135)
        {
            anim.Play("WalkDown");
        }
        else if (angle < -45 && angle > -135)
        {
            anim.Play("WalkLeft");
        }
        else if (angle > 45 && angle < 135)
        {
            anim.Play("WalkRight");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player.SetActive(false);
            gameOverUI.SetActive(true);
        }
    }
}
