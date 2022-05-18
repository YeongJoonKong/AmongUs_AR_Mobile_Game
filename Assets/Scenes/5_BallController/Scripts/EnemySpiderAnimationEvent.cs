using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpiderAnimationEvent : MonoBehaviour
{

    public Enemy enemySpider;


    public void OnAttackHit()
    {
        enemySpider.OnAttackHit();
    }

    public void OnAttackFinished()
    {
        enemySpider.OnAttackFinished();
    }

    public void OnDamageFinished()
    {
        enemySpider.OnDamageFinished();
    }

    internal void OnDeathFinished()
    {
        enemySpider.OnDeathFinished();
    }

    internal void SoundAttack()
    {
        enemySpider.SoundAttack();
        
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

}
