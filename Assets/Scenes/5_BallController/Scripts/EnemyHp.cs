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
            // curHP에 value를 대입하되 범위 안의 값으로 하고싶다.
            curHP = Mathf.Clamp(value, 0, maxHP);

            sliderHP.value = curHP;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        // 태어날 때 체력을 최대체력으로하고싶다.
        sliderHP.maxValue = maxHP;
        HP = maxHP;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
