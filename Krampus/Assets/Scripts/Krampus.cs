using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Krampus : MonoBehaviour
{

    private Animator animator;
    private Rigidbody2D rigidbody;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
    }


    enum State
    {
        Idle,
        Walk,
        Attack
    }



    private State currentState;

    private void ExitState(State state){
        switch(state)
        {
            case State.Attack:
                if (attackTarget)
                    Destroy(attackTarget);
                break;
        }
    }

    private void EnterState(State state)
    {
        switch(state)
        {
            case State.Idle:
                animator.Play("Krampus_Idle");
                break;
            case State.Walk:
                animator.Play("Krampus_walk");
                break;
            case State.Attack:
                animator.Play("Attack");
                break;
        }
    }


    private void SetState(State nextState)
    {
        ExitState(currentState);
        currentState = nextState;
        EnterState(nextState);
    }


    private float idleTime = 3;
    private float idleTimer = 0;
    private void Idle()
    {
        idleTimer += Time.deltaTime;
        if (idleTimer > idleTime)
            SetState(State.Walk);
    }


    private float attackTimer = 0;
    private float attackTime = 0.5f;
    private void Attack()
    {

        attackTimer += Time.deltaTime;
        if (attackTimer > attackTime)
        {
            attackTimer = 0;
            if (animator.GetCurrentAnimatorStateInfo(0).length > animator.GetCurrentAnimatorStateInfo(0).normalizedTime * 2)
                SetState(State.Idle);
        }
        //todo



    }

    bool isLeft = false;

    void SwapDirections()
    {
        isLeft = !isLeft;

        GetComponent<SpriteRenderer>().flipX = !isLeft;
    }
    private int walkSpeed = 5;
    private void Walk()
    {
        rigidbody.velocity = new Vector2(walkSpeed * (isLeft ? 1 : -1), 0);
    }


    GameObject attackTarget = null;


    private void OnCollisionEnter2D(Collision2D collision)
    {

        Door door = collision.transform.GetComponent<Door>();
        if (door)
        {
            attackTarget = door.gameObject;
            SetState(State.Attack);
        }
        else
            SwapDirections();

    }


    private void Update()
    {
        switch(currentState)
        {
            case State.Attack:
                Attack();
                break;
            case State.Idle:
                Idle();
                break;
            case State.Walk:
                Walk();
                break;
        }
    }

}
