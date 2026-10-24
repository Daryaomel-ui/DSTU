
using UnityEngine;

public class MagicProjectile : MonoBehaviour
{
    private Transform target;
    private SpellData spell;

    public void Initialize(Transform newTarget, SpellData newSpell)
    {
        target = newTarget;
        spell = newSpell;

        Renderer renderer = GetComponent<Renderer>();

        if (renderer != null)
        {
            MaterialPropertyBlock block = new MaterialPropertyBlock();
            block.SetColor("_BaseColor", spell.projectileColor);
            renderer.SetPropertyBlock(block);
        }

        Destroy(gameObject, 5f);
    }

    void Update()
    {
        if (target == null || spell == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 targetPosition = target.position + Vector3.up * 0.5f;

        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPosition,
            spell.projectileSpeed * Time.deltaTime
        );


        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {

            GolemStatus status = target.GetComponent<GolemStatus>();

            if (status != null)
            {
                status.ApplySpell(spell);
            }

            Destroy(gameObject);
        }

    }
}
