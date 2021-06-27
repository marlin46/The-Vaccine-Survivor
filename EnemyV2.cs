using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyV2 : MonoBehaviour
{
    public bool AbleToBeDestroyed;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            if(AbleToBeDestroyed == true){
                EnemySpawner.enemies.Remove(this);
                ScoreSystem.Instance.UpdateScore(1);
                Destroy(gameObject);
            }
            
        }
    }
}
