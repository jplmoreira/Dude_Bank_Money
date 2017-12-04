using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public Vector3 pos1;
    public Vector3 currentEndPos;
    public Vector3 pos2;

    [System.Serializable]
    public class EnemyStats
    {
        public int health = 1;
    }

    public EnemyStats enemyStats = new EnemyStats();
    public bool facingRight = true;

    private FieldOfView fov;

    private void Awake() {
        fov = transform.Find("Eyes").gameObject.GetComponent<FieldOfView>();
    }
    
    private void Start()
    {
        pos1 = transform.position;
        currentEndPos = pos2;
    }

    //private void Update()
    //{
    //    if (currentEndPos == pos2 && Vector3.Distance(transform.position, pos2) < 0.1f)
    //    {
    //        currentEndPos = pos1;
    //        transform.localScale = new Vector3(-1, 1, 1);
    //   }
    //    if (currentEndPos == pos1 && Vector3.Distance(transform.position, pos1) < 0.1f)
    //   {
    //       currentEndPos = pos2;
    //       transform.localScale = new Vector3(1, 1, 1);
    //    }
    //    transform.position = Vector3.Lerp(transform.position, currentEndPos, Time.deltaTime * 1.0f);
    //}

    public void Flip() {
        facingRight = !facingRight;
        Transform fov = transform.Find("Eyes");
        fov.Rotate(new Vector3(0, 0, 180));

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    public void Alarmed() {
        fov.viewAngle = 360;
        fov.reactionTime = 2;
    }
}
