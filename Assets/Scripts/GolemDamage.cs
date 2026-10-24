
using UnityEngine;

public class GolemDamage : MonoBehaviour
{
    private GolemStatus status;
    public PlayerHealth playerHealth;
    public int damage = 1;
    public float attackDistance = 1.8f;
    public float attackCooldown = 1.5f;

    private float nextAttackTime;

    void Start()
    {
        status = GetComponent<GolemStatus>();

        if (playerHealth == null)
        {
            playerHealth =
                FindFirstObjectByType<PlayerHealth>();
        }
    }
    void Update()
    {
        if (status != null && status.currentState == GolemState.Frozen)
        {
            return;
        }

        if (playerHealth == null)
        {
            return;
        }

        Vector3 direction =
            playerHealth.transform.position
            - transform.position;

        direction.y = 0;

        float distance = direction.magnitude;

        if (distance <= attackDistance &&
            Time.time >= nextAttackTime)
        {
            playerHealth.TakeDamage(damage);

            nextAttackTime =
                Time.time + attackCooldown;
        }
    }
}
