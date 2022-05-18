using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHp : MonoBehaviour
{
    public int maxHP = 3;
    int curHP;
    public Slider sliderHP;


    public int HP
    {
        get { return curHP; }
        set
        {
            // curHP�� value�� �����ϵ� ���� ���� ������ �ϰ�ʹ�.
            curHP = Mathf.Clamp(value, 0, maxHP);

            sliderHP.value = curHP;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        // �¾ �� ü���� �ִ�ü�������ϰ�ʹ�.
        sliderHP.maxValue = maxHP;
        HP = maxHP;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
