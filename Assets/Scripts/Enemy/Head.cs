using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : BodyPart, IDamageable
{
    public new void Damage(int amount)
    {
        owner.TakeDamage(amount * 2);
    }
}
