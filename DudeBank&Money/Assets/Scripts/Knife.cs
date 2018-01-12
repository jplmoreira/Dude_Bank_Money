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
    public GameObject knifePrefab;

    public int rotationOffset = 0;
    public Vector3 startPosition = new Vector3(0.0f, -0.15f, 0.0f);
    public float nextKnifada = 0;
    public float cooldown = 1.0f;
    public float knifeSpeed;    //initialized on Start()
    public bool inUse = false;
    //public bool attack = false;

    public bool spawned = false;

    private Rigidbody2D rigidbodyToMove;
    private GameObject knifeInstantiated;

   // Use this for initialization
   void Awake()
    {
        player = GameObject.Find("Player");
        playerScript = GameObject.Find("Player").GetComponent<PlayerScript>();
        pc2dscript = playerScript.pc2dscript;
        knifeSpeed = 20.0f;
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
            rigidbodyToMove.AddForce(dashDir, ForceMode2D.Impulse);

            Invoke("GoBackKnife", 0.1f);
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
            rigidbodyToMove.AddForce(dashDir, ForceMode2D.Impulse);

            Invoke("GoBackKnife", 0.1f);

            //attack = true;
        }
    }

    public void GoBackKnife()
    {
        Vector3 comeBackVel = rigidbodyToMove.velocity;
        rigidbodyToMove.velocity = new Vector3(-comeBackVel.x, -comeBackVel.y, 0);
        Invoke("ResetKnife", 0.1f);
    }

    public void ResetKnife()
    {
        rigidbodyToMove.velocity = new Vector3(0, 0, 0);
        rigidbodyToMove.transform.localPosition = startPosition;
        //rigidbodyToMove.position = player.GetComponent<Rigidbody2D>().position;
        inUse = false;
        //transform.position = new Vector3(0.1f, 0.1f, 0);
        Invoke("DestroyKnife", 0.1f);
    }

    public void DestroyKnife()
    {
        Destroy(knifeInstantiated);
        spawned = false;
        playerScript.RemovesKnife();
    }

    public void SpawnKnife()
    {
        if (!spawned)
        {
            spawned = true;
            knifeInstantiated = (GameObject)Instantiate(knifePrefab, startPosition, Quaternion.identity);
            knifeInstantiated.transform.parent = this.transform;
            knifeInstantiated.transform.localPosition = startPosition;
            rigidbodyToMove = knifeInstantiated.GetComponent<Rigidbody2D>();
            ActivateKnife();
        }
    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (!inUse && spawned)
        {
            //GetComponent<Rigidbody2D>().velocity = player.GetComponent<Rigidbody2D>().velocity;  ---> doesn't work well idk :/
            rigidbodyToMove.transform.localPosition = startPosition;
            //rigidbodyToMove.position = player.GetComponent<Rigidbody2D>().position;     // bruteforce fix
        }
        //if (Input.GetButtonDown("Fire1"))
        //{
        //    ActivateKnife();
        //}
    }



}
