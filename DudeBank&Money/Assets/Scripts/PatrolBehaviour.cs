using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolBehaviour : MonoBehaviour
{
    public Vector3 pos1;
    public Vector3 currentEndPos;
    public Vector3 pos2;
    public float speedX;
    public EnemyScript itsScript;

    private PlayerScript player;
    private float slowDown;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerScript>();
        pos1 = transform.position;
        currentEndPos = pos2;
        speedX = 1.0f;
    }

    private void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(speedX * player.slowFactor, GetComponent<Rigidbody2D>().velocity.y);
        if (currentEndPos == pos2 && Vector3.Distance(transform.position, pos2) < 0.1f)
        {
            currentEndPos = pos1;
            itsScript.Flip();
            speedX *= -1;
        }
        if (currentEndPos == pos1 && Vector3.Distance(transform.position, pos1) < 0.1f)
        {
           currentEndPos = pos2;
           itsScript.Flip();
           speedX *= -1;
        }
    }
}
