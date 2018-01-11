using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

public class PlayerScript : MonoBehaviour {

    private CharacterScript character;
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
    public int fallBoundary = -10;
    public float slowFactor = 1f;
    public bool robbed = false;

    public PlatformerCharacter2D pc2dscript;
    public bool right;

    public Knife faca;

    [HideInInspector]
    public Weapon wscript;
    public TNTWeapon tntScript;

    enum ActiveWeapon
    {
        GUN,
        TNT
    }

    ActiveWeapon activeWeapon;

    private void Start()
    {
        dashSpeed = 170;
        dashTime = 0.025f;
        wscript = GetComponentInChildren<Weapon>();
        tntScript = GetComponentInChildren<TNTWeapon>();
        faca = GetComponentInChildren<Knife>();
        character = GetComponent<CharacterScript>();
        activeWeapon = ActiveWeapon.GUN;
        if (character == null)
            Debug.LogError("Could not find stats");
    }

    private void Update() {
        if (transform.position.y <= fallBoundary)
            character.DamageCharacter(9999);
        if (slowFactor == 0.1f)
            resourceVal -= Time.deltaTime / slowFactor;

        if (Input.GetKeyDown(KeyCode.R))
            GameMaster.ReloadLevel();

        if (Input.GetKeyDown(KeyCode.E)) {
            if (resourceVal >= 50 || slowFactor == 0) {
                slowFactor = 1 - slowFactor;
                pc2dscript.timeStop = !pc2dscript.timeStop;
                if (slowFactor == 0){
                    resourceVal -= 50;
                    pc2dscript.timeStopActions = 5;
                } else {
                    pc2dscript.timeReset = true;
                }
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            faca.ActivateKnife();
        }

        if (Input.GetKeyDown(KeyCode.Q)) {
            if (resourceVal > 0 || slowFactor == 0.1f) {
                pc2dscript.timeStop = false;
                pc2dscript.timeStopActions = 0;
                pc2dscript.timeReset = true;
                if (slowFactor == 0) slowFactor = 1;
                if (pc2dscript.currSpeed == 15) {
                    pc2dscript.currSpeed = 5;
                } else {
                    pc2dscript.currSpeed = 15;
                }
                slowFactor = 0.1f / slowFactor;
            }
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            if (activeWeapon == ActiveWeapon.GUN)
            {
                activeWeapon = ActiveWeapon.TNT;
                wscript.enabled = false;
                tntScript.enabled = true;
            }
            else if (activeWeapon == ActiveWeapon.TNT)
            {
                activeWeapon = ActiveWeapon.GUN;
                wscript.enabled = true;
                tntScript.enabled = false;
            }
        }

        if (!reset && resourceVal <= 0 && slowFactor > 0) {
            pc2dscript.timeStop = false;
            pc2dscript.currSpeed = 15;
            slowFactor = 1f;
            reset = true;
        }
    }

    private void DashReset()
    {
        dashing = false;
        pc2dscript.isDashing = false;
        GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
    }

    private void FixedUpdate()
    {
        right = pc2dscript.m_FacingRight;

        if (Input.GetKeyDown(KeyCode.LeftShift) && !dashing)
        {
            if (pc2dscript.timeStop && pc2dscript.timeStopActions > 0)
            {
                pc2dscript.timeStopActions--;
                dashing = true;
                pc2dscript.isDashing = true;
                nextDash = Time.time + dashCooldown;
                if (dashing)
                {
                    Dash();
                    wscript.ResetShot();
                    Invoke("DashReset", dashTime);
                }
            }
            else if (!pc2dscript.timeStop && Time.time > nextDash)
            {
                dashing = true;
                pc2dscript.isDashing = true;
                nextDash = Time.time + dashCooldown;
                if (dashing)
                {
                    Dash();
                    wscript.ResetShot();
                    Invoke("DashReset", dashTime);
                }
            }
        }
    }

    

    private void Dash()
    {
        if (pc2dscript.timeStop){
            Vector3 mousePosition = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
                                                Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
            Vector3 dir = mousePosition - transform.position;
            Vector3 dashDir = dir.normalized * dashSpeed;
            //Debug.LogError("dash direction: " + dashDir.ToString());
            GetComponent<Rigidbody2D>().AddForce(dashDir, ForceMode2D.Impulse);
        }
        else if (right)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(dashSpeed, 0), ForceMode2D.Impulse);
        }
        else
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(-dashSpeed, 0), ForceMode2D.Impulse);
        }
    }

}
