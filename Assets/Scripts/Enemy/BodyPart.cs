using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPart : MonoBehaviour, IDamageable
{
    protected Enemy owner = null;

    public void Setup(Enemy newOwner)
    {
        owner = newOwner;
    }

    public void Damage(int amount)
    {
        owner.TakeDamage(amount);
    }
}
