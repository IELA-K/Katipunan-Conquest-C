using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turretScript : MonoBehaviour
{
    public float Range;
    public Transform Target;
    public GameObject AlarmLight;
    bool Detected = false;
    Vector2 Direction;
    public GameObject Gun;
    public GameObject bullet;
    public float FireRate;
    float nextTimeToFire = 0;
    public Transform Shootpoint;
    public float Force;

    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        Vector2 targetPos = Target.position;
        Direction = targetPos - (Vector2)transform.position;
        RaycastHit2D rayInfo = Physics2D.Raycast(transform.position, Direction, Range);

        if (rayInfo)
        {
            if (rayInfo.collider.gameObject.tag == "Player")
            {
                if (Detected == false)
                {
                    Detected = true;
                    AlarmLight.GetComponent<SpriteRenderer>().color = Color.red;
                }
            }
            else
            {
                if (Detected == true)
                {
                    Detected = false;
                    AlarmLight.GetComponent<SpriteRenderer>().color = Color.green;
                }
            }
        }
        if (Detected)
        {
            Gun.transform.up = Direction;
            if (Time.time > nextTimeToFire)
            {
                nextTimeToFire = Time.time + 1 / FireRate;
                shoot();
            }
        }
        else
        {
            Gun.transform.up = Vector2.zero;
        }
    }
    void shoot()
    {
        GameObject BulletIns = Instantiate(bullet, Shootpoint.position, Quaternion.identity);
        BulletIns.GetComponent<Rigidbody2D>().AddForce(Direction * Force);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, Range);
    }

    // Expectimax algorithm
    float expectimax(int depth, int player)
    {
        if (depth == 0)
        {
            // Base case: return the value of the current state
            return evaluate(player);
        }

        // Choose the best action
        float bestValue = float.MinValue;
        foreach (var action in possibleActions())
        {
            // Make a recursive call to the expectimax function,
            // assuming that the opponent will take the best action
            float value = -expectimax(depth - 1, 1 - player);

            // Update the best value
            bestValue = Mathf.Max(bestValue, value);
        }

        // Return the expected value of the current state
        return bestValue;
    }

    // Evaluate the current state
    float evaluate(int player)
    {
        // If the player has won, return 1
        if (player == 1)
        {
            return 1;
        }

        // If the player has lost, return -1
        if (player == 0)
        {
            return -1;
        }

        // Otherwise, return 0
        return 0;
    }

    // Get the possible actions
    IEnumerable<Action> possibleActions()
    {
        // If the turret is not detected, it can only rotate
        if (!Detected)
        {
            yield return new Action(() => Gun.transform.up = Direction);
        }
        else
        {
            // If the turret is detected, it can shoot or rotate
            yield return new Action(() => shoot());
            yield return new Action(() => Gun.transform.up = Direction);
        }
    }
}
