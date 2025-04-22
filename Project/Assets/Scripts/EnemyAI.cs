using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public enum enemyState { chase, afraid, wander, dead}
    public enemyState currentState;

    public List<PathNode> path;
    public float speed = 15f;
    public float chaseSpeed = 15f;
    public float runawaySpeed = 5f;
    public int pathTollerance = 2;
    public float chanceToWander = 0f;
    public float wanderTime = 3f;
    public float reachThreshold = 0.1f;

    public Color baseColor = Color.white;
    public Color afraidColor = Color.white;
    private SpriteRenderer spriteRenderer;

    public Testing testing;
    private int currentWaypointIndex = 0;

    private float timer = 0f;
    private float timer2 = 0f;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        currentState = enemyState.chase;
        StartCoroutine(DelayedStart());
    }
    private IEnumerator DelayedStart()
    {
        yield return new WaitForSeconds(Random.Range(1.0f,2.5f));
        testing.Chase(this,baseColor);
    }

    void Update()
    {
        switch (currentState)
        {
            case enemyState.chase:
                ChasePlayer();
                break;
            case enemyState.afraid:
                RunAway();
                break;
            case enemyState.wander:
                WanderAround();
                break;
            case enemyState.dead:
                GoToSpawnPoint();
                break;
        }
        ManageState();
    }
    private void ManageState()
    {
        if (currentState == enemyState.chase)
        {
            timer += Time.deltaTime;
            timer2 = 0f;
            if (timer > 3f)
            {
                int value = Random.Range(1, 101);
                if (value < chanceToWander)
                {
                    currentState = enemyState.wander;
                    testing.ChooseCorner(this, afraidColor);
                }
                timer = 0f;
            }
        }
        else if (currentState == enemyState.wander) {
            timer2 += Time.deltaTime;
            timer = 0f;
            if (timer2 > wanderTime) { 
                currentState = enemyState.chase;
                testing.Chase(this, baseColor);
            }
        }
    }
    private void GoToSpawnPoint()
    {
        speed = chaseSpeed * 2;
        spriteRenderer.color = Color.black;
        if (path != null && path.Count > 0)
        {
            RunToCenter();
        }
    }
    private void WanderAround()
    {
        speed = chaseSpeed;
        spriteRenderer.color = baseColor;
        if (path != null && path.Count > 0)
        {
            RunToACorner();
        }

    }
    private void ChasePlayer()
    {
        speed = chaseSpeed;
        spriteRenderer.color = baseColor;
        if (path != null && path.Count > 0)
        {
            MoveAlongPath();
        }
    }
    private void RunAway()
    {
        speed = runawaySpeed;
        spriteRenderer.color = afraidColor;
        if (path != null && path.Count > 0)
        {
            RunToACorner();
        }
    }
    private void RunToCenter()
    {
        if (currentWaypointIndex < path.Count)
        {
            Vector3 targetPosition = path[currentWaypointIndex].ReturnNodeWorldPosition();
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

            if (Vector3.Distance(transform.position, targetPosition) <= reachThreshold)
            {
                currentWaypointIndex++;
            }
        }
        else
        {
            currentState = enemyState.chase;
        }
    }
    private void RunToACorner()
    {
        if (currentWaypointIndex < path.Count)
        {
            Vector3 targetPosition = path[currentWaypointIndex].ReturnNodeWorldPosition();
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

            if (Vector3.Distance(transform.position, targetPosition) <= reachThreshold)
            {
                currentWaypointIndex++;
            }
        }
        else
        {
            SetPath(new List<PathNode>());
            testing.ChooseCorner(this, baseColor);
        }
    }
    private void MoveAlongPath()
    {
        if (currentWaypointIndex < path.Count)
        {
            Vector3 targetPosition = path[currentWaypointIndex].ReturnNodeWorldPosition();
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

            if (Vector3.Distance(transform.position, targetPosition) <= reachThreshold)
            {
                currentWaypointIndex++;
                if (currentWaypointIndex >= pathTollerance) testing.Chase(this, baseColor);
            }
        }
        else
        {
            SetPath(new List<PathNode>());
            testing.Chase(this, baseColor);
        }
    }
    public void SetPath(List<PathNode> newPath)
    {
        path = newPath;
        currentWaypointIndex = 0; // Start from the beginning of the new path
    }
}
