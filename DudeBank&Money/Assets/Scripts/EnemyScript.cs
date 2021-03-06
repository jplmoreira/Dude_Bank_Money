﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{

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

    public void Die()
    {
        Destroy(this.gameObject);
    }
}
