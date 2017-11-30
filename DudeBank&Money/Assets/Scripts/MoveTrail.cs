using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTrail : MonoBehaviour {

    public int bulletSpeed = 100;
    public Transform hitParticle;

    private Rigidbody2D rigidBody;

    private void Awake() {
        rigidBody = GetComponent<Rigidbody2D>();
        Vector2 mousePosition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
                                            Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        GameObject barrel = GameObject.FindWithTag("GunBarrel");
        if (barrel == null)
            Debug.LogError("GunBarrel not found");
        Vector2 bulletPosition = new Vector2(barrel.transform.position.x, barrel.transform.position.y);
        rigidBody.AddForce((mousePosition - bulletPosition).normalized * bulletSpeed, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        rigidBody.velocity = new Vector2(0, 0);
        ContactPoint2D contact = collision.contacts[0];
        GameObject colliderObject = collision.collider.gameObject;
        if (colliderObject.tag == "Enemy") {
            EnemyScript enemy = colliderObject.GetComponent<EnemyScript>();
            enemy.DamageEnemy(1);
        }
        Destroy(gameObject);
        Transform particle = Instantiate(hitParticle, contact.point, Quaternion.FromToRotation(Vector3.right, contact.normal));
        Destroy(particle.gameObject, 1f);
    }

    private void OnCollisionStay2D(Collision2D collision) {
        rigidBody.velocity = new Vector2(0, 0);
        ContactPoint2D contact = collision.contacts[0];
        Destroy(gameObject);
        Transform particle = Instantiate(hitParticle, contact.point, Quaternion.FromToRotation(Vector3.right, contact.normal));
        Destroy(particle.gameObject, 1f);
    }
}
