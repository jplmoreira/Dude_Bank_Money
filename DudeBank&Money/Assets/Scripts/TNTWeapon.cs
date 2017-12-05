using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TNTWeapon : MonoBehaviour
{
    public GameObject tntThrowable;
    public float throwSpeed;
    Transform barrel;
    // Use this for initialization
    void Start ()
    {
        barrel = transform.Find("GunBarrel");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            float rotation = transform.parent.rotation.eulerAngles.z;
            if (transform.parent.localScale.x < 0)
            {
                rotation += 180;
            }
            GameObject clone = (GameObject)Instantiate(tntThrowable, barrel.position, Quaternion.Euler(0f, 0f, rotation));
            Vector3 mousePosition = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
                                                Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
            Vector3 dir = mousePosition - barrel.position;
            clone.GetComponent<Rigidbody2D>().AddForce(Time.deltaTime * throwSpeed * dir.normalized, ForceMode2D.Impulse);
        }
    }
}
