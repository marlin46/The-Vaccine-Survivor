using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public LayerMask layerMask;
    public Vector2 velocity;
    public float speed;
    public float rotation;
    public float lifeTime;
    public string type;
    public float timer { get; set; }
    void Start()
    {
        timer = lifeTime;
        //transform.rotation = Quaternion.Euler(0, 0, rotation);
    }
    void Update()
    {
        transform.Translate(velocity * speed * Time.deltaTime);
        // LIFETIME OVER
        timer -= Time.deltaTime;
        if (timer <= 0) Destroy(gameObject); /*gameObject.SetActive(false);*/
        // HIT WALL
        var hit = Physics2D.Raycast(transform.position, velocity.normalized, velocity.magnitude * speed * Time.deltaTime, layerMask);
        if (hit.collider != null && hit.collider.tag == "Wall")
            gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag) 
        {
            case "Bounds":
                Destroy(gameObject);
                break;
        }
    }
}
