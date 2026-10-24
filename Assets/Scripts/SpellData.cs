
using UnityEngine;

public enum SpellElement
{
    Fire,
    Ice
}

[CreateAssetMenu(
    fileName = "NewSpell",
    menuName = "Spellcraft/Spell Data"
)]
public class SpellData : ScriptableObject
{
    public SpellElement element;
    public int damage = 1;
    public float projectileSpeed = 10f;
    public float statusDuration = 3f;
    public Color projectileColor = Color.white;
}
