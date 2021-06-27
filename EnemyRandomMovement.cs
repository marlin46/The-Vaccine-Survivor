using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRandomMovement : MonoBehaviour
{
    private float latestDirectionChangeTime;
 private readonly float directionChangeTime = 3f;
 public float characterVelocity = 2f;
 private Vector2 movementDirection;
 private Vector2 movementPerSecond;
 
 
 void Start(){
     latestDirectionChangeTime = 0f;
     calcuateNewMovementVector();
 }
 
 void calcuateNewMovementVector(){
     movementDirection = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized;
     movementPerSecond = movementDirection * characterVelocity;
 }
 
 void Update(){
     if (Time.time - latestDirectionChangeTime > directionChangeTime){
         latestDirectionChangeTime = Time.time;
         calcuateNewMovementVector();
     }
     
     transform.position = new Vector2(transform.position.x + (movementPerSecond.x * Time.deltaTime), 
     transform.position.y + (movementPerSecond.y * Time.deltaTime));
 
 }
}
