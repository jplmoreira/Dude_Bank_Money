using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TNTWeapon : MonoBehaviour
{
    public GameObject tntThrowable;
    public float throwSpeed;
    Transform barrel;
    Rigidbody2D tntRigid;
    public Material explodeMaterial;
    private bool exploded;
    private bool throwable;

    //private GameObject instantiatedTNT;

    // Use this for initialization
    void Start ()
    {
        barrel = transform.Find("GunBarrel");
        exploded = false;
        throwable = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 mousePosition = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
                                                   Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
        Vector3 dir = mousePosition - barrel.position;
        UpdateTrajectory(barrel.position, dir.normalized * throwSpeed * Time.deltaTime, new Vector3(0.0f, -9.8f, 0.0f));
        if (Input.GetButtonDown("Fire1") && throwable)
        {
            throwable = false;
            float rotation = transform.parent.rotation.eulerAngles.z;
            if (transform.parent.localScale.x < 0)
            {
                rotation += 180;
            }
            GameObject clone = (GameObject)Instantiate(tntThrowable, barrel.position, Quaternion.Euler(0f, 0f, rotation));
            //Vector3 mousePosition = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
            //                                    Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
            //Vector3 dir = mousePosition - barrel.position;
            tntRigid = clone.GetComponent<Rigidbody2D>();
            tntRigid.AddForce(Time.deltaTime * throwSpeed * dir.normalized, ForceMode2D.Impulse);
        }
        if (!exploded && tntRigid && tntRigid.velocity == Vector2.zero)
        {
            exploded = true;
            StartCoroutine(Explode(tntRigid.transform.position));
        }
    }

    private void OnDrawGizmos()
    {
        if (tntRigid && tntRigid.velocity == Vector2.zero)
        {
            Gizmos.DrawWireSphere(tntRigid.transform.position, 5.0f);
        }
    }


    void UpdateTrajectory(Vector3 initialPosition, Vector3 initialVelocity, Vector3 gravity)
    {
        int numSteps = 50; // for example
        float timeDelta = 1.0f / initialVelocity.magnitude; // for example

        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = numSteps;

        Vector3 position = initialPosition;
        Vector3 lastPos = position;
        Vector3 velocity = initialVelocity;

        int i = 0;
        Vector2 contactPoint = Vector2.zero;
        RaycastHit2D hit;
        bool getOut = false;
        while (i < numSteps && !getOut)
        {
            hit = Physics2D.Linecast(new Vector2(lastPos.x, lastPos.y), new Vector2(position.x, position.y));

            if (hit.point == Vector2.zero || hit.collider.tag == "trajectory")
            {
                lineRenderer.SetPosition(i, position);
                lastPos = position;
                position += velocity * timeDelta + 0.5f * gravity * timeDelta * timeDelta;
                velocity += gravity * timeDelta;
                i++;
            }
            else
            {
                contactPoint = hit.point;
                getOut = true;
            }
        }
        if (i != numSteps)
        {
            lineRenderer.SetPosition(i, contactPoint);
            i++;
            for (int j = i; j < numSteps; j++)
            {
                lineRenderer.SetPosition(j, lastPos);
            }
        }
    }

    public IEnumerator Explode(Vector2 stopPos)
    {
        yield return new WaitForSeconds(1.0f);
        tntRigid.gameObject.GetComponent<SpriteRenderer>().material.color = Color.yellow;
        yield return new WaitForSeconds(1.0f);
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

        Destroy(tntRigid.gameObject);

        throwable = true;
        exploded = false;
    }
}
