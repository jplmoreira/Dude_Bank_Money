using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

public class PlayerScript : MonoBehaviour {

    [System.Serializable]
    public class PlayerStats {
        public int health = 1;
    }

    public PlayerStats playerStats = new PlayerStats();
    public int fallBoundary = -10;

    private bool reset = false;
    private float resourceVal = 100f;
    public float Resource {
        get { return resourceVal; }
    }

    private float nextDash = 1;
    public float dashCooldown = 2;
    public float dashSpeed = 200;   
    public float dashTime = 1;      
    private bool dashing;

    public PlatformerCharacter2D pc2dscript;
    public bool right;

    public Weapon wscript;

    private void Start()
    {
        wscript = GetComponentInChildren<Weapon>();
    }

    private void Update() {
        if (transform.position.y <= fallBoundary || transform.position.x >= 39)
            DamagePlayer();
        if (transform.GetComponent<Countdown>().timeRate > 1)
            resourceVal -= Time.deltaTime * 10;
        if (Input.GetKeyDown(KeyCode.R))
            DamagePlayer();
        if (Input.GetKeyDown(KeyCode.E)) {
            if (resourceVal > 50 || transform.GetComponent<Countdown>().timeStop) {
                transform.GetComponent<Countdown>().timeRate = 1f;
                transform.GetComponent<Countdown>().timeStop = !transform.GetComponent<Countdown>().timeStop;
                if (transform.GetComponent<Countdown>().timeStop)
                    resourceVal -= 50;
                GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
                for (int i = 0; i < enemies.Length; i++) {
                    EnemyAI enemy = enemies[i].GetComponent<EnemyAI>();
                    if (enemy.speed != 0)
                        enemy.speed = 0;
                    else
                        enemy.speed = 600;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Q)) {
            if (resourceVal > 0 || transform.GetComponent<Countdown>().timeRate > 1) {
                transform.GetComponent<Countdown>().timeStop = false;
                float timeRate = transform.GetComponent<Countdown>().timeRate;
                if (timeRate == 1)
                    transform.GetComponent<Countdown>().timeRate = 10f;
                else
                    transform.GetComponent<Countdown>().timeRate = 1f;
                GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
                for (int i = 0; i < enemies.Length; i++) {
                    EnemyAI enemy = enemies[i].GetComponent<EnemyAI>();
                    if (enemy.speed != 300)
                        enemy.speed = 300;
                    else
                        enemy.speed = 600;
                }
            }
        }
        if (!reset && resourceVal <= 0) {
            transform.GetComponent<Countdown>().timeStop = false;
            transform.GetComponent<Countdown>().timeRate = 1f;
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            for (int i = 0; i < enemies.Length; i++) {
                EnemyAI enemy = enemies[i].GetComponent<EnemyAI>();
                enemy.speed = 600;
            }
            reset = true;
        }
    }

    public void DamagePlayer() {
        GameMaster.KillPlayer(this);
    }

    private void DashReset()
    {
        dashing = false;
    }

    private void FixedUpdate()
    {
        right = pc2dscript.m_FacingRight;

        if (Input.GetKeyDown(KeyCode.LeftShift) && Time.time > nextDash)
        {
            dashing = true;
            nextDash = Time.time + dashCooldown;
            if (dashing)
            {
                Dash();
                wscript.ResetShot();
                Invoke("DashReset", dashTime);
            }
        }
    }

    private void Dash()
    {
       
        if (right)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(dashSpeed, 0), ForceMode2D.Impulse);
        }
        else
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(-dashSpeed, 0), ForceMode2D.Impulse);
        }
    }

}
