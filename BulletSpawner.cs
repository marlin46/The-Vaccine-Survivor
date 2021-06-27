using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public GameObject bulletResource;
    public string bulletType;
    public float minRotation;
    public float maxRotation;
    public float offsetRotation;
    public int numberOfBullets;
    public bool isRandom;
    public bool isParent = true;
    public float cooldown;
    float timer;
    public float bulletSpeed;
    public Vector2 bulletVelocity;
    public bool sfx;
    public bool updateDistributedRotations = false;

    float[] rotations;
    void Start()
    {
        timer = cooldown;
        rotations = new float[numberOfBullets];
        if (!isRandom)
        {
            /* 
             * This doesn't need to be in update because the rotations will be the same no matter what
             * Unless if we change min Rotation and max Rotation Variables leave this in Start.
             */
            DistributedRotations();
        }
    }

    // Update is called once per frame
    public void Update()
    {
        if (timer <= 0)
        {
            SpawnBullets();
            timer = cooldown;
        }
        timer -= Time.deltaTime;
    }

    // Select a random rotation from min to max for each bullet
    public float[] RandomRotations()
    {
        for (int i = 0; i < numberOfBullets; i++)
        {
            rotations[i] = Random.Range(minRotation, maxRotation) + offsetRotation;
        }
        return rotations;

    }
    
    // This will set random rotations evenly distributed between the min and max Rotation.
    public float[] DistributedRotations()
    {
        if (numberOfBullets != 1) {
            for (int i = 0; i < numberOfBullets; i++)
            {
                var fraction = (float)i / ((float)numberOfBullets - 1f);
                var difference = maxRotation - minRotation;
                var fractionOfDifference = fraction * difference;
                rotations[i] = fractionOfDifference + minRotation + offsetRotation; // We add minRotation to undo Difference
            }
        } else rotations[0] = 0;

        //foreach (var r in rotations) print(r);
        return rotations;
    }
    public GameObject[] SpawnBullets()
    {
        if (isRandom)
        {
            RandomRotations();
        } else
        {
            if (updateDistributedRotations)
                DistributedRotations();
        }
        // Spawn Bullets
        GameObject[] spawnedBullets = new GameObject[numberOfBullets];
        for (int i = 0; i < numberOfBullets; i++)
        {
            spawnedBullets[i] = BulletManager.GetBulletFromPool(bulletType);
            if (spawnedBullets[i] == null)
            {
                spawnedBullets[i] = Instantiate(bulletResource, transform);
                BulletManager.bullets.Add(spawnedBullets[i]);
            }
            else
            {
                spawnedBullets[i].transform.SetParent(transform);
                spawnedBullets[i].transform.localPosition = Vector2.zero;
            }
            var b = spawnedBullets[i].GetComponent<Bullet>();
            b.transform.rotation = Quaternion.Euler(0,0,rotations[i]);
            b.speed = bulletSpeed;
            b.velocity = bulletVelocity;
            if (!isParent)
            {
                spawnedBullets[i].transform.SetParent(null, true);
                //print(spawnedBullets[i]);
                b.rotation += transform.eulerAngles.z;
            }
        }
        if (sfx == true) AudioManager.current.PlaySound("SmallLaser");
        return spawnedBullets;
    }
}
