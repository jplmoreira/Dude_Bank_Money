using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour {

    public float viewRadius;
    [Range(0,360)]
    public float viewAngle;

    public LayerMask targetMask;
    public LayerMask obstacleMask;
    public Transform bulletTrail;
    public float alarmTime;

    [HideInInspector]
    public List<Transform> visibleTargets = new List<Transform>();
    [HideInInspector]
    public int reactionTime;

    private Transform barrel;
    private bool alreadyShot = false;
    private Countdown cd;
    private PlayerScript player;
    private EnemyScript enemy;

    private void Start() {
        reactionTime = 1;
        enemy = transform.parent.gameObject.GetComponent<EnemyScript>();
        barrel = transform.parent.Find("GunBarrel");
        cd = GameObject.Find("Player").GetComponent<Countdown>();
        player = GameObject.Find("Player").GetComponent<PlayerScript>();
        StartCoroutine("FindTargetsWithDelay", 0.2f);
    }

    private IEnumerator FindTargetsWithDelay(float delay) {
        while(true) {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }

    private IEnumerator SoundAlarm(float delay) {
        yield return new WaitForSeconds(delay);
        cd.SoundAlarm();
    }

    private IEnumerator AimAt(Transform target) {
        float slowedReaction = 1f;
        if (player.slowFactor == 0.1f) slowedReaction = 0.5f;
        StartCoroutine("SoundAlarm", alarmTime / slowedReaction);
        yield return new WaitForSeconds(0.5f / player.slowFactor / reactionTime);
        Vector3 dir = (target.position - barrel.position);
        Transform clone = (Transform)Instantiate(bulletTrail, barrel.position, Quaternion.FromToRotation(Vector3.right, dir));
        clone.GetComponent<MoveTrail>().Shoot(dir.normalized);      
        yield return new WaitForSeconds(2f / slowedReaction / reactionTime);
        alreadyShot = false;
    }

    private void FindVisibleTargets() {
        visibleTargets.Clear();
        Collider2D[] targetsInViewRadius = Physics2D.OverlapCircleAll(transform.parent.position, viewRadius, targetMask);

        for (int i = 0; i < targetsInViewRadius.Length; i++) {
            Transform target = targetsInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.parent.position).normalized;
            float angle = Vector3.Angle(transform.right, dirToTarget);
            if (angle < viewAngle / 2) {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics2D.Raycast(transform.position, dirToTarget, distanceToTarget, obstacleMask)) {
                    visibleTargets.Add(target);
                }
                if (visibleTargets.Count > 0 && !alreadyShot && player.slowFactor > 0) {
                    if (angle >= 90) enemy.Flip();
                    alreadyShot = true;
                    StartCoroutine("AimAt", visibleTargets[0]);
                }
            }
        }
    }

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal) {
        if (!angleIsGlobal) {
            angleInDegrees += transform.eulerAngles.z;
        }
        return new Vector3(Mathf.Cos(angleInDegrees * Mathf.Deg2Rad), Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0);
    }
}
