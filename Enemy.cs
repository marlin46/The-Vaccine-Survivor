using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public int health = 10;
    public Transform player;
    private float distToPlayer;
    public float range;


    // Start is called before the first frame update
    void Start()
    {
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log(gameObject.name + " Health: " + health);

        if (health <= 0)
        {
            Debug.Log(gameObject.name + " health is gone");
            Destroy(gameObject);
        }
    }
    void Update()
    {
        distToPlayer = Vector2.Distance(transform.position, player.position);

        /*if (distToPlayer <= range)
        {
            GetComponent<BulletSpawner>().SpawnBullets();
        }*/
    }

    void OnCollisionEnter2D(Collision2D collisionInfo)
    {

        Debug.Log("Enemy OnCollisionEnter2D: " + collisionInfo.collider.name + " with other " + collisionInfo.otherCollider.name);
        if (distToPlayer <= 2)
        {
            Debug.Log("CLOSE");


        }

    }
    // Update is called once per frame
    void Die()
    {
        Debug.Log("about to die: " + gameObject.name);
        //Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
        Debug.Log("Enemy gameObject should be destroyed");
    }
}