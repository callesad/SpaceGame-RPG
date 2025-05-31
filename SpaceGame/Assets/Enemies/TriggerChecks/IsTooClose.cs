using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsTooClose : MonoBehaviour
{
    public GameObject player;
    private Enemy enemy;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemy = GetComponentInParent<Enemy>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player) enemy.IsTooClose = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == player) enemy.IsTooClose = false;
    }
}
