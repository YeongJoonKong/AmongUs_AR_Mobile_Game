using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Enemy : MonoBehaviour
{
    public enum State
    {
        Search,
        Move,
        Attack,
        Damaged,
        Death,
    }
    public State state;

    GameObject playerKnight;
    public float speed = 5;
    public NavMeshAgent agent;
    public Animator anim;


    public float attackDistance = 0.1f;

    AudioSource audioSource;
    public AudioClip soundMonsterAttack;

    Rigidbody rb;


    private void Awake()
    {
        audioSource = this.gameObject.GetComponent<AudioSource>();
    }


    internal void OnAttackHit()
    {
        print("OnAttackHit()");
        //만약 타겟이 공격 사정거리 안에 있다면
        //Hit를 하고싶다.
        float dist = Vector3.Distance(transform.position, playerKnight.transform.position);
        if (dist < attackDistance)
        {
            anim.SetTrigger("Attack");
        }
    }

    internal void SoundAttack()
    {
        audioSource.clip = soundMonsterAttack;
        audioSource.Play();
    }

    internal void OnAttackFinished()
    {
        print("OnAttackFinished()");

        // 만약 타겟이 공격 사정거리 밖에있다면
        float dist = Vector3.Distance(transform.position, playerKnight.transform.position);
        if (dist >= attackDistance)
        {
            // 이동상태로 전이하고싶다.
            //anim.CrossFade("Walk Forward WO Root Fixed", 0.1f, 0);
            anim.SetTrigger("Move");
            state = State.Move;

            //애니메이션이 변경될 때 급격히 바뀌면 어색한 동작이 연출될 수 있음.
            //CrossFade 블렌딩 함수를 통해 부드럽게 애니메이션의 변화가 이뤄질수 있게 해줌.
            //anim.CrossFade("Move", 0.1f, 0);
        }
    }

    internal void OnDamageFinished()
    {
        print("OnDamageFinished()");

        // 만약 state가 Death라면
        if (state == State.Death)
        {
            // state는 Death로 바뀌었지만 애니메이션이 씹힌상태이다.
            // Death 애니메이션을 재생하고싶다.
            anim.SetTrigger("Death");
            // 함수를 바로 종료하고싶다.
            return;
        }

        // 만약 타겟과 나의 거리가 공격 가능 거리라면 
        float dist = Vector3.Distance(transform.position, playerKnight.transform.position);
        // 공격상태로 전이하고
        if (dist < attackDistance)
        {
            state = State.Attack;

            agent.isStopped = false;

            anim.SetTrigger("Attack");

        }
        // 그렇지 않다면 이동상태로 전이하고싶다.
        else
        {
            state = State.Move;
            anim.SetTrigger("Move");
            //anim.CrossFade("Move", 0.1f, 0);

            //agent야 멈추지마 라고 하고 싶다.
            agent.isStopped = false;
        }
    }

    internal void OnDeathFinished()
    {
        print("OnDeathFinished()");
        Destroy(gameObject);
    }


    EnemyHp eshp;

    internal void TakeDamage(int dmgAmount)
    {
        // 만약 state가 Death라면
        if (state == State.Death)
        {
            // 함수를 바로 종료하고싶다.
            return;
        }

        if (eshp == null)
        {
            eshp = gameObject.GetComponent<EnemyHp>();
        }

        if (eshp != null)
        {
            //agent야 멈춰 라고 하고 싶다.
            agent.isStopped = true;
            agent.velocity = Vector3.zero;

            // 체력을 dmgAmount만큼 감소시키고싶다.
            eshp.HP -= dmgAmount;
            // 체력이 0이하가되면 Enemy를 파괴하고싶다.
            if (eshp.HP <= 0)
            {
                // 죽음상태로 전이하고싶다.
                state = State.Death;
                anim.SetTrigger("Death");
                gameObject.layer = LayerMask.NameToLayer("EnemyDeath");
            }
            else
            {
                // 데미지상태로 전이하고싶다.
                state = State.Damaged;
                anim.SetTrigger("Damaged");
            }
        }
        //agent.isStopped = false;
    }






    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        state = State.Search;
        rb = GetComponent<Rigidbody>();
    }

    private void UpdateSearch()
    {
        playerKnight = GameObject.Find("PlayerKnight");
        // 만약 적을 찾았다면
        if (playerKnight != null)
        {
            // 이동상태로 전이하고싶다.
            state = State.Move;
            anim.SetTrigger("Move");
        }
    }


    private void UpdateMove()
    {

        agent.destination = playerKnight.transform.position;
        // 만약 타겟과의 거리가 공격가능 거리보다 작다면
        float dist = Vector3.Distance(transform.position, playerKnight.transform.position);
        if (dist < attackDistance)
        {
            // 공격상태로 전이하고싶다.
            state = State.Attack;

            anim.SetTrigger("Attack");

        }
    }

    private void UpdateAttack()
    {
        //transform.LookAt(PlayerKnight.transform);

        Vector3 dir = playerKnight.transform.position - transform.position;
        dir.Normalize();
        Quaternion targetRotation = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 5);
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.Search:
                UpdateSearch();
                break;

            case State.Move:
                UpdateMove();
                break;

            case State.Attack:
                UpdateAttack();
                break;

            default: break;
        }
    }




}
