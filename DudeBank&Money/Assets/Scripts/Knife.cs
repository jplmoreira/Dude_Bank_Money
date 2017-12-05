using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

public class Knife : MonoBehaviour {

    int damage = 1000;
    public PlatformerCharacter2D pc2dscript;
    public PlayerScript playerScript;

    public float nextKnifada = 0;
    public float cooldown = 1.0f;
    public bool attack = false;



    // Use this for initialization
    void Start () {
        playerScript = GameObject.Find("Player").GetComponent<PlayerScript>();
        pc2dscript = playerScript.pc2dscript;
        
    }

    public void activateKnife()
    {
        if (pc2dscript.timeStop && pc2dscript.timeStopActions > 0)
        {
            pc2dscript.timeStopActions--;
            nextKnifada = Time.time + cooldown;
            attack = true;
        }
        else if (!pc2dscript.timeStop && Time.time > nextKnifada)
        {
            nextKnifada = Time.time + cooldown;
            attack = true;
        }
    }

    private void Update()
    {
        if (attack) attack = false;
    }


    private void OnTriggerStay2D(Collider2D other)
    {
        if (attack)
        {
            /*Debug.LogError("facada em: " + other.tag);
            Destroy(other.gameObject);*/
            if (other.tag == "Enemy")
            {
                //Kill enemy
                Debug.LogError("facada em: " + other.tag);
                Destroy(other.gameObject);
                attack = false;
            }
        }
        else
        {
            Debug.LogError("nao facada em: " + other.tag);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (attack) attack = !attack;
    }


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
