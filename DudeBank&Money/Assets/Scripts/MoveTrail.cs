using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTrail : MonoBehaviour {

    public int bulletSpeed = 10;
    public Transform hitParticle;

    private Rigidbody2D rigidBody;

    private void Awake() {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    public void Shoot(Vector3 dir) {
        rigidBody.AddForce(dir * bulletSpeed, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        rigidBody.velocity = new Vector2(0, 0);
        if (collision.contacts.Length > 0) {
            ContactPoint2D contact = collision.contacts[0];
            GameObject colliderObject = collision.collider.gameObject;
            if (colliderObject.tag == "Enemy" || colliderObject.tag == "Player") {
                CharacterScript character = colliderObject.GetComponent<CharacterScript>();
                character.DamageCharacter(1);
            }
            Transform particle = Instantiate(hitParticle, contact.point, Quaternion.FromToRotation(Vector3.right, contact.normal));
            Destroy(particle.gameObject, 1f);
        }
        Destroy(gameObject);
    }

    private void OnCollisionStay2D(Collision2D collision) {
        rigidBody.velocity = new Vector2(0, 0);
        ContactPoint2D contact = collision.contacts[0];
        Destroy(gameObject);
        Transform particle = Instantiate(hitParticle, contact.point, Quaternion.FromToRotation(Vector3.right, contact.normal));
        Destroy(particle.gameObject, 1f);
    }
}
