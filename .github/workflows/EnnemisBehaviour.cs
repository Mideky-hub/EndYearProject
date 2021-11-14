/* Copyright by Mideky-hub on GitHub on the 2021.
 * 
 * No claim can be made out of this code by the fact that its for private usage, the only goal of the code is to complete my End Year Project.
 * Any modification provided in this code has to be verified by me and is going into my intellectual protection.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemisBehaviour : MonoBehaviour
{
    // Setiing stats.
    public int maxHitPoint = 5;
    public int hitPoint = 0;

    // Setting the last shot that the ennemis has been hit with.
    public float timeSinceLasHIt = 0.0f;

    void Start()
    {
        // Define at the start that we are full health.
        hitPoint = maxHitPoint;
    }

    void Update()
    {
        // No need update.
    }

    public void GetDamage(int damage)
    {
        // When we get hit, we lost hp.
        hitPoint -= damage;

        // While we have HP, we can get damaged.
        if (hitPoint > 0)
        {
            gameObject.SendMessage("TakeDamage", SendMessageOptions.DontRequireReceiver);
        }
        else
        {
            // The mob has been defeated so, GG !
            gameObject.SendMessage("Defeated", SendMessageOptions.DontRequireReceiver);
        }
    }
}