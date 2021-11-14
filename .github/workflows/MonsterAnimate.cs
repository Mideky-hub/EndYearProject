/* Copyright by Mideky-hub on GitHub on the 2021.
 * 
 * No claim can be made out of this code by the fact that its for private usage, the only goal of the code is to complete my End Year Project.
 * Any modification provided in this code has to be verified by me and is going into my intellectual protection.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.AI;

public class MonsterAnimate : MonoBehaviour
{
    public GameObject player;

    Animator animator; // IMPORT_LIB_UNITY /!\
    NavMeshAgent navMeshAgent; // **

    // ------------ Declaring the string to state ------------

    // Setting up the avaiable state of our monsters.
    const string STAND_STATE = "Stand";
    const string TAKE_DAMAGE_STATE = "Damage";
    public const string DEFEATED_STATE = "Defeated";
    public const string WALK_STATE = "Walk";

    // Help up to set up the current action later in the code.
    public string currentAction;

    // ------------ Setting them all as boolean ------------

    // Permit us to reset the animation once we need to switch on another one.
    private void ResetAnimation()
    {
        animator.SetBool(STAND_STATE, false); // Awake, Stand.
        animator.SetBool(TAKE_DAMAGE_STATE, false); // TakeDamage, TakingDamage.
        animator.SetBool(DEFEATED_STATE, false); //Defeated.
        animator.SetBool(WALK_STATE, false); //Walk.
    }

    // When the monsters appears.
    private void Awake()
    {
        // Setting the current action to stand.
        currentAction = STAND_STATE;

        // Setting the reference to the corresponds the component.
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = FindObjectOfType<PlayerBehaviour>().gameObject;
    }
    
    // Idle animation in the beginning 
    private void Stand()
    {
        ResetAnimation();
        currentAction = STAND_STATE;
        animator.SetBool(STAND_STATE, true);
    }

    private void Walk()
    {
        ResetAnimation();
        currentAction = WALK_STATE;
        animator.SetBool(WALK_STATE, true);
    }

    // When we shoot at the monster.
    public void TakeDamage()
    {
        ResetAnimation();
        currentAction = TAKE_DAMAGE_STATE;
        animator.SetBool(TAKE_DAMAGE_STATE, true);
    }

    // Once the monster doesn't have enough HP to live, is dead, logic.
    public void Defeated()
    {
        ResetAnimation();
        currentAction = DEFEATED_STATE;
        animator.SetBool(DEFEATED_STATE, true);
    }

    // We switch from the Stand to TakeDamage and vice-versa.
    private void TakingDamage()
    {
        if (this.animator.GetCurrentAnimatorStateInfo(0).IsName(TAKE_DAMAGE_STATE))
        {
            float normalizedTime = this.animator.GetCurrentAnimatorStateInfo(0).normalizedTime;

            if(normalizedTime > 1)
            {
                Stand();
            }
        }
    }

    // ------------ UPDATE ------------
    private void Update()
    {
        if (currentAction == DEFEATED_STATE)
        {
            navMeshAgent.ResetPath();
            return; // The AI die.
        }

        if (currentAction == TAKE_DAMAGE_STATE)
        {
            navMeshAgent.ResetPath();
            TakingDamage();
            return;
        }
        if (player != null)
        {
            if (MovingToTarget())
            {
                // We return the function. So the AI walk.
                return;
            }
        }
    }

    // ------------ AI PART ------------

    // We make the AI Moving forward to us by using NMA and RotateToTarget(). 
    private bool MovingToTarget()
    {
        // Set the the ennemis to destination of the player location.
        navMeshAgent.SetDestination(player.transform.position);

        // We set on walk if the AI is not too close from us.
        if (navMeshAgent.remainingDistance > navMeshAgent.stoppingDistance)
        {
            // We wont the AI to go by attacking us at r = 200f; so we set it on walk to be sure.
            if (currentAction != WALK_STATE)
            {
                // Calling Walk();
                Walk();
            }
        }
        else
        {
            RotateToTarget(player.transform);
            return false;
        }

        return true;
    }

    // Permit the AI to turn around him to us, in order to set the MovingToTarget.
    private void RotateToTarget(Transform target)
    {
        // We set the direction of the AI to us, but not a destination but as a direction.
        Vector3 direction = (target.position - transform.position).normalized;
        // Setting up the movement from Vector3. 
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        // Calling and transforming the rotation to bring the AI in our direction.
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 3f);
    }
}
