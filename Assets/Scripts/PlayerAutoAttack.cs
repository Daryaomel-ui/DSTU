
using UnityEngine;

public class PlayerAutoAttack : MonoBehaviour
{
    public SpellData fireSpell;
    public SpellData iceSpell;
    public MagicProjectile projectilePrefab;

    public float attackRange = 10f;
    public float shotInterval = 1f;

    private SpellData currentSpell;
    private float nextShotTime;

    void Start()
    {
        currentSpell = fireSpell;
    }

    void Update()
    {
        if (Input.GetButtonDown("SelectFire"))
        {
            currentSpell = fireSpell;
        }

        if (Input.GetButtonDown("SelectIce"))
        {
            currentSpell = iceSpell;
        }

        if (!Input.GetButtonDown("Attack") ||
            Time.time < nextShotTime)
        {
            return;
        }


        if (projectilePrefab == null || currentSpell == null)
        {
            return;
        }

        Transform nearestGolem = FindNearestGolem();

        if (nearestGolem == null)
        {
            return;
        }

        MagicProjectile projectile = Instantiate(
            projectilePrefab,
            transform.position + Vector3.up * 0.8f,
            Quaternion.identity
        );

        projectile.Initialize(nearestGolem, currentSpell);

        nextShotTime = Time.time + shotInterval;
    }

    Transform FindNearestGolem()
    {
        GolemMovement[] golems =
            FindObjectsByType<GolemMovement>(
                FindObjectsSortMode.None
            );

        Transform nearest = null;
        float nearestDistance = attackRange;

        foreach (GolemMovement golem in golems)
        {
            if (!golem.isActiveAndEnabled)
            {
                continue;
            }

            Vector3 direction =
                golem.transform.position - transform.position;

            direction.y = 0;

            float distance = direction.magnitude;

            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearest = golem.transform;
            }
        }

        return nearest;
    }
}
