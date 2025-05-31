using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsAgroed : MonoBehaviour
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
        if (collision.CompareTag("Player")) {
            enemy.IsAgroed = true;
            enemy.Target = collision.gameObject;
        }
        if (collision.CompareTag("Amo")) {
            enemy.IsAgroed = true;
            enemy.Target = collision.GetComponent<LazerBehaviour>().source;
        }
        
    }

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.gameObject == player) enemy.IsAgroed = false;
    //}
}
