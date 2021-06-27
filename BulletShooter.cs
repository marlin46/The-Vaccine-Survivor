using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShooter : MonoBehaviour
{
    public Camera cam;
    public GameObject bulletResource;
    public string bulletType;
    public GameObject transformReference;
    public float rotationRangeOfError;
    public float bulletSpeed;
    public float cooldown;
    public float timer { get; set; }
    public bool isParent = false;
    public bool isManual = false;
    public bool sfx;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timer <= 0 && !isManual)
        {
            SpawnBullet();
            timer = cooldown;
        }
        timer -= Time.deltaTime;
    }
    public GameObject SpawnBullet()
    {
        var g = Instantiate(bulletResource, transform);
        BulletManager.bullets.Add(g);

        /*GameObject g = BulletManager.GetBulletFromPool(bulletType);
        if (g == null)
        {
            g = Instantiate(bulletResource, transform);
            BulletManager.bullets.Add(g);
        }
        else
        {
            g.transform.SetParent(transform);
            g.transform.localPosition = Vector2.zero;
        }*/

        var b = g.GetComponent<Bullet>();
        b.rotation = transform.localEulerAngles.z;
        b.speed = bulletSpeed;
        if (!isParent)
        {
            g.transform.SetParent(null, true);
            //print(spawnedBullets[i]);
            //b.rotation += transform.eulerAngles.z;
        }
        if (sfx == true) AudioManager.current.PlaySound("SmallLaser");
        return g;
    }
}
