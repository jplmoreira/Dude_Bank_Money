using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Seeker))]
public class EnemyAI : MonoBehaviour {

    //Caching
    private Seeker seeker;
    private Rigidbody2D rb;
    //The waypoint the enemy is moving towards
    private int currentWaypoint = 0;
    private bool searchingForPlayer = false;

    public Transform target;
    public float updateRate = 2f;
    //The calculated path
    public Path path;
    [HideInInspector]
    public bool pathHasEnded = false;
    //The AI's speed per second
    public float speed = 300f;
    public ForceMode2D fMode;
    //The max distance from AI to a waypoint to continue to the next
    public float nextWaypointDistance = 3;

    private void Start() {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        if (target == null) {
            if (!searchingForPlayer) {
                searchingForPlayer = true;
                StartCoroutine(SearchForPlayer());
            }
            return;
        }

        seeker.StartPath(transform.position, target.position, OnPathComplete);

        StartCoroutine(UpdatePath());
    }

    private IEnumerator SearchForPlayer() {
        GameObject result = GameObject.FindWithTag("Player");
        if (result == null) {
            yield return new WaitForSeconds(1f / updateRate);
            StartCoroutine(SearchForPlayer());
        } else {
            target = result.transform;
            searchingForPlayer = false;
            StartCoroutine(UpdatePath());
            yield return null;
        }
    }

    private IEnumerator UpdatePath() {
        if (target == null) {
            if (!searchingForPlayer) {
                searchingForPlayer = true;
                StartCoroutine(SearchForPlayer());
            }
            yield return null;
        }

        seeker.StartPath(transform.position, target.position, OnPathComplete);

        yield return new WaitForSeconds(1f / updateRate);
        StartCoroutine(UpdatePath());
    }

    public void OnPathComplete(Path p) {
        Debug.Log("We got path " + p.error);
        if (!p.error) {
            path = p;
            currentWaypoint = 0;
        }
    }

    private void FixedUpdate() {
        if (target == null) {
            if (!searchingForPlayer) {
                searchingForPlayer = true;
                StartCoroutine(SearchForPlayer());
            }
            return;
        }

        //TODO: Always look to player

        if (path == null)
            return;

        if (currentWaypoint >= path.vectorPath.Count) {
            if (pathHasEnded)
                return;

            Debug.Log("Reached the end of path");
            pathHasEnded = true;
            return;
        }
        pathHasEnded = false;

        //Direction to the next waypoint
        Vector3 dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;
        dir *= speed * Time.fixedDeltaTime;

        //Move the AI
        rb.AddForce(dir, fMode);

        float dist = Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]);
        if (dist < nextWaypointDistance) {
            currentWaypoint++;
            return;
        }
    }
}
