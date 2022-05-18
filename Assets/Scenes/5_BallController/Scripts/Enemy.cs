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
        //���� Ÿ���� ���� �����Ÿ� �ȿ� �ִٸ�
        //Hit�� �ϰ�ʹ�.
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

        // ���� Ÿ���� ���� �����Ÿ� �ۿ��ִٸ�
        float dist = Vector3.Distance(transform.position, playerKnight.transform.position);
        if (dist >= attackDistance)
        {
            // �̵����·� �����ϰ�ʹ�.
            //anim.CrossFade("Walk Forward WO Root Fixed", 0.1f, 0);
            anim.SetTrigger("Move");
            state = State.Move;

            //�ִϸ��̼��� ����� �� �ް��� �ٲ�� ����� ������ ����� �� ����.
            //CrossFade ���� �Լ��� ���� �ε巴�� �ִϸ��̼��� ��ȭ�� �̷����� �ְ� ����.
            //anim.CrossFade("Move", 0.1f, 0);
        }
    }

    internal void OnDamageFinished()
    {
        print("OnDamageFinished()");

        // ���� state�� Death���
        if (state == State.Death)
        {
            // state�� Death�� �ٲ������ �ִϸ��̼��� ���������̴�.
            // Death �ִϸ��̼��� ����ϰ�ʹ�.
            anim.SetTrigger("Death");
            // �Լ��� �ٷ� �����ϰ�ʹ�.
            return;
        }

        // ���� Ÿ�ٰ� ���� �Ÿ��� ���� ���� �Ÿ���� 
        float dist = Vector3.Distance(transform.position, playerKnight.transform.position);
        // ���ݻ��·� �����ϰ�
        if (dist < attackDistance)
        {
            state = State.Attack;

            agent.isStopped = false;

            anim.SetTrigger("Attack");

        }
        // �׷��� �ʴٸ� �̵����·� �����ϰ�ʹ�.
        else
        {
            state = State.Move;
            anim.SetTrigger("Move");
            //anim.CrossFade("Move", 0.1f, 0);

            //agent�� �������� ��� �ϰ� �ʹ�.
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
        // ���� state�� Death���
        if (state == State.Death)
        {
            // �Լ��� �ٷ� �����ϰ�ʹ�.
            return;
        }

        if (eshp == null)
        {
            eshp = gameObject.GetComponent<EnemyHp>();
        }

        if (eshp != null)
        {
            //agent�� ���� ��� �ϰ� �ʹ�.
            agent.isStopped = true;
            agent.velocity = Vector3.zero;

            // ü���� dmgAmount��ŭ ���ҽ�Ű��ʹ�.
            eshp.HP -= dmgAmount;
            // ü���� 0���ϰ��Ǹ� Enemy�� �ı��ϰ�ʹ�.
            if (eshp.HP <= 0)
            {
                // �������·� �����ϰ�ʹ�.
                state = State.Death;
                anim.SetTrigger("Death");
                gameObject.layer = LayerMask.NameToLayer("EnemyDeath");
            }
            else
            {
                // ���������·� �����ϰ�ʹ�.
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
        // ���� ���� ã�Ҵٸ�
        if (playerKnight != null)
        {
            // �̵����·� �����ϰ�ʹ�.
            state = State.Move;
            anim.SetTrigger("Move");
        }
    }


    private void UpdateMove()
    {

        agent.destination = playerKnight.transform.position;
        // ���� Ÿ�ٰ��� �Ÿ��� ���ݰ��� �Ÿ����� �۴ٸ�
        float dist = Vector3.Distance(transform.position, playerKnight.transform.position);
        if (dist < attackDistance)
        {
            // ���ݻ��·� �����ϰ�ʹ�.
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
