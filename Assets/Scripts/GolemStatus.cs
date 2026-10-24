
using UnityEngine;

public enum GolemState
{
    Normal,
    Burning,
    Frozen
}

public class GolemStatus : MonoBehaviour
{
    public GolemState currentState = GolemState.Normal;

    public ParticleSystem steamExplosionPrefab;
    public float explosionRadius = 3f;
    public int explosionDamage = 2;


    private GolemHealth health;
    private Renderer golemRenderer;
    private MaterialPropertyBlock colorBlock;

    private float statusEndTime;
    private float nextBurnTime;

    void Awake()
    {
        health = GetComponent<GolemHealth>();
        golemRenderer = GetComponent<Renderer>();
        colorBlock = new MaterialPropertyBlock();
    }

    void Start()
    {
        SetState(GolemState.Normal, 0f);
    }

    void Update()
    {
        if (currentState == GolemState.Normal)
        {
            return;
        }

        if (Time.time >= statusEndTime)
        {
            SetState(GolemState.Normal, 0f);
            return;
        }

        if (currentState == GolemState.Burning &&
            Time.time >= nextBurnTime)
        {
            health.TakeDamage(1);
            nextBurnTime = Time.time + 1f;
        }
    }

    public void ApplySpell(SpellData spell)
    {
        if (spell == null || health == null)
        {
            return;
        }


        if (spell.element == SpellElement.Fire &&
            currentState == GolemState.Frozen)
        {
            Explode();

            health.TakeDamage(spell.damage + 2);

            SetState(GolemState.Normal, 0f);

            return;
        }

        health.TakeDamage(spell.damage);

        if (spell.element == SpellElement.Fire)
        {
            SetState(GolemState.Burning, spell.statusDuration);
        }
        else if (spell.element == SpellElement.Ice)
        {
            SetState(GolemState.Frozen, spell.statusDuration);
        }
    }


    void Explode()
    {
        Vector3 explosionPosition =
            transform.position + Vector3.up * 0.5f;

        if (steamExplosionPrefab != null)
        {
            ParticleSystem effect = Instantiate(
                steamExplosionPrefab,
                explosionPosition,
                Quaternion.identity
            );

            Destroy(effect.gameObject, 3f);
        }

        GolemHealth[] golems =
            FindObjectsByType<GolemHealth>(
                FindObjectsSortMode.None
            );

        foreach (GolemHealth golem in golems)
        {
            if (golem == health)
            {
                continue;
            }

            Vector3 direction =
                golem.transform.position - transform.position;

            direction.y = 0;

            if (direction.magnitude <= explosionRadius)
            {
                golem.TakeDamage(explosionDamage);
            }
        }

        Debug.Log("Паровой взрыв!");
    }

    void SetState(GolemState newState, float duration)
    {
        currentState = newState;
        statusEndTime = Time.time + duration;
        nextBurnTime = Time.time + 1f;

        Color color = Color.gray;

        if (newState == GolemState.Burning)
        {
            color = Color.red;
        }
        else if (newState == GolemState.Frozen)
        {
            color = Color.cyan;
        }

        if (golemRenderer != null)
        {
            golemRenderer.GetPropertyBlock(colorBlock);
            colorBlock.SetColor("_BaseColor", color);
            golemRenderer.SetPropertyBlock(colorBlock);
        }

        Debug.Log("Состояние голема: " + currentState);
    }
}
