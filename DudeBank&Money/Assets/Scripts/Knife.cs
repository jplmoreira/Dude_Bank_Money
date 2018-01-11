using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

public class Knife : MonoBehaviour
{

    int damage = 1000;
    public PlatformerCharacter2D pc2dscript;
    public PlayerScript playerScript;
    public GameObject player;

    public int rotationOffset = 0;
    public Vector3 startPosition = new Vector3(0.0f, -0.15f, 0.0f);
    public float nextKnifada = 0;
    public float cooldown = 1.0f;
    public float knifeSpeed = 10.0f;
    public bool inUse = false;
    //public bool attack = false;



    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("Player");
        playerScript = GameObject.Find("Player").GetComponent<PlayerScript>();
        pc2dscript = playerScript.pc2dscript;

    }

    public void ActivateKnife()
    {
        if (pc2dscript.timeStop && pc2dscript.timeStopActions > 0)
        {
            inUse = true;
            pc2dscript.timeStopActions--;
            nextKnifada = Time.time + cooldown;

            Vector3 mousePosition = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
                                                Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
            Vector3 dir = mousePosition - transform.position;
            Vector3 dashDir = dir.normalized * knifeSpeed;
            //Debug.LogError("dash direction: " + dashDir.ToString());
            GetComponent<Rigidbody2D>().AddForce(dashDir, ForceMode2D.Impulse);

            Invoke("GoBackKnife", 0.5f);
            //attack = true;
        }
        else if (!pc2dscript.timeStop && Time.time > nextKnifada)
        {
            inUse = true;
            nextKnifada = Time.time + cooldown;

            Vector3 mousePosition = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
                                                Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
            Vector3 dir = mousePosition - transform.position;
            Vector3 dashDir = dir.normalized * knifeSpeed;
            //Debug.LogError("dash direction: " + dashDir.ToString());
            GetComponent<Rigidbody2D>().AddForce(dashDir, ForceMode2D.Impulse);

            Invoke("GoBackKnife", 0.1f);

            //attack = true;
        }
    }

    public void GoBackKnife()
    {
        Vector3 comeBackVel = GetComponent<Rigidbody2D>().velocity;
        GetComponent<Rigidbody2D>().velocity = new Vector3(-comeBackVel.x, -comeBackVel.y, 0);
        Invoke("ResetKnife", 0.1f);
    }

    public void ResetKnife()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
        GetComponent<Rigidbody2D>().position = player.GetComponent<Rigidbody2D>().position;
        inUse = false;
        //transform.position = new Vector3(0.1f, 0.1f, 0);
    }


    private void Update()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position; // Get position difference between the mouse cursor and the player
        difference.Normalize();                                                                        // Normalize the vector

        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;                          // Find the angle

        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + rotationOffset);
        
        
    }

    private void FixedUpdate()
    {
        if (!inUse)
        {
            //GetComponent<Rigidbody2D>().velocity = player.GetComponent<Rigidbody2D>().velocity;  ---> doesn't work well idk :/
            GetComponent<Rigidbody2D>().position = player.GetComponent<Rigidbody2D>().position;     // bruteforce fix
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(collision.gameObject.gameObject);
        }
    }


    //private void OnTriggerStay2D(Collider2D other)
    //{
    /*Debug.LogError("facada em: " + other.tag);
    Destroy(other.gameObject);*/
    /*if (other.tag == "Enemy")
    {
        //Kill enemy
        Debug.LogError("facada em: " + other.tag);
        Destroy(other.gameObject);
        //attack = false;
    }  
}*/



    //old way, using a raycast, wasn't working
    /*
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(1))
        {
            if (pc2dscript.timeStop && pc2dscript.timeStopActions > 0)
            {
                //pc2dscript.timeStopActions--;
                nextKnifada = Time.time + cooldown;
                attack();
            }
            else if (!pc2dscript.timeStop && Time.time > nextKnifada)
            {
                nextKnifada = Time.time + cooldown;
                attack();
            }
        }
	}

    

    void attack() { 
        Vector3 dir = knife.position - transform.position;
        Vector3 pos = transform.position + new Vector3(1, 0, 0);

        RaycastHit hit;
        if(Physics.Raycast(knife.position, dir.normalized, out hit, 5.0f))
        {
            pc2dscript.timeStopActions--;
            if (hit.distance < maxDistance)
            {
                hit.transform.SendMessage("Die");
            }
        }
        else
        {
            Debug.LogError("ERROR: knife position = " + knife.position.ToString() + "/n dir = " + dir.normalized.ToString());
            Debug.DrawRay(knife.position, dir.normalized, Color.red, 10.0f, false);
        }
    }*/

}
