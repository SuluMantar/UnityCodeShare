using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityBase : ScriptableObject
{
    public string abilityName;
    public string description;
    public float cooldown;
    public float duration;

    public abstract void ActivateAbility();
}
