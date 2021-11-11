using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetBehaviour : MonoBehaviour
{
    public int maxHitPoint = 5;

    public int hitPoint = 0;

    private void Start ()
    {
        hitPoint = maxHitPoint;
    }

    public void GetDamage(int damage)
    {
        hitPoint -= damage;

        if(hitPoint < 1)
        {
            Destroy(gameObject);
        }
    }
}
