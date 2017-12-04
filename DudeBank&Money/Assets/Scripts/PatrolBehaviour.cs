using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolBehaviour : MonoBehaviour
{
    public Vector3 pos1;
    public Vector3 currentEndPos;
    public Vector3 pos2;
    public float speedX;

    private void Start()
    {
        pos1 = transform.position;
        currentEndPos = pos2;
        speedX = 1.0f;
    }

    private void Update()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(speedX, GetComponent<Rigidbody2D>().velocity.y);
        if (currentEndPos == pos2 && Vector3.Distance(transform.position, pos2) < 0.1f)
        {
            currentEndPos = pos1;
            transform.localScale = new Vector3(-1, 1, 1);
            speedX *= -1;
        }
        if (currentEndPos == pos1 && Vector3.Distance(transform.position, pos1) < 0.1f)
        {
           currentEndPos = pos2;
           transform.localScale = new Vector3(1, 1, 1);
           speedX *= -1;
        }
        //transform.position = Vector3.Lerp(transform.position, currentEndPos, Time.deltaTime * 1.0f);
    }
}
