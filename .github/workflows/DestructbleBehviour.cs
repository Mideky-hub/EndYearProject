/* Copyright by Mideky-hub on GitHub on the 2021.
 * 
 * No claim can be made out of this code by the fact that its for private usage, the only goal of the code is to complete my End Year Project.
 * Any modification provided in this code has to be verified by me and is going into my intellectual protection.
*/

using UnityEngine;

public class DestructbleBehviour : MonoBehaviour
{
    public void Defeated()
    {
        // Simply destroy the object.
        Destroy(gameObject);
    }
}
