using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsAgroed : MonoBehaviour
{
    //--public GameObject player;
    private EnemyShip enemy;

    private void Awake()
    {
        //--player = GameObject.FindGameObjectWithTag("Player");
        enemy = GetComponentInParent<EnemyShip>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //can only be agroed by player or amo
        if (collision.CompareTag("Player")) {
            enemy.IsAgroed = true;
            enemy.Target = collision.gameObject;
        }
        if (collision.CompareTag("Amo")&&!collision.GetComponent<Amo>().source.CompareTag("Enemy")) {
            enemy.IsAgroed = true;
            enemy.Target = collision.GetComponent<Amo>().source;
        }
        
    }

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.gameObject == player) enemy.IsAgroed = false;
    //}
}
