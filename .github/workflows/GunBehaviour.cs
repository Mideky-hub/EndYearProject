using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBehaviour : MonoBehaviour
{
    private Camera fpsCam;

    public int gunDamage = 1;
    public float weaponRange = 200f;

    public float hitForce = 100f;
    public float fireRate = 0.25f;
    private float nextFire;

    public LayerMask layerMask;

    void start ()
    {
        fpsCam = GetComponentInParent<Camera>();
    }

    void Update ()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;

            print(nextFire);

            Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));
            RaycastHit hit;

            if(Physics.Raycast(rayOrigin, fpsCam.transform.forward, out hit, weaponRange, layerMask))
            {
                print("Target");

                if (hit.rigidbody != null)
                {
                    hit.rigidbody.AddForce(-hit.normal * hitForce);
                    if(hit.collider.gameObject.GetComponent<TargetBehaviour>() != null)
                    {
                        hit.collider.gameObject.GetComponent<TargetBehaviour>().GetDamage(gunDamage);
                    }
                }
            }
        }
    }
}
