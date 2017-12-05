using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TNTWeapon : MonoBehaviour
{
    public GameObject tntThrowable;
    public float throwSpeed;
    Transform barrel;
    Rigidbody2D tntRigid;
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
            tntRigid = clone.GetComponent<Rigidbody2D>();
            tntRigid.AddForce(Time.deltaTime * throwSpeed * dir.normalized, ForceMode2D.Impulse);
        }
        if (tntRigid && tntRigid.velocity == Vector2.zero)
        {
            Debug.Log("STOPPED");
            StartCoroutine(Explode(tntRigid.transform.position));
        }
    }

    private void OnDrawGizmos()
    {
        if (tntRigid && tntRigid.velocity == Vector2.zero)
        {
            Debug.Log("STOPPED");
            Gizmos.DrawWireSphere(tntRigid.transform.position, 5.0f);
        }
    }

    public IEnumerator Explode(Vector2 stopPos)
    {
        yield return new WaitForSeconds(2.0f);
        Collider2D[] colliders;
        colliders = Physics2D.OverlapCircleAll(stopPos,5.0f);

        for (int i = 0; i < colliders.Length; i++)
        {
            if(colliders[i].tag == "Enemy" || colliders[i].tag == "Player")
            {
                CharacterScript character = colliders[i].GetComponent<CharacterScript>();
                character.DamageCharacter(1);
            }
        }
    }
}
