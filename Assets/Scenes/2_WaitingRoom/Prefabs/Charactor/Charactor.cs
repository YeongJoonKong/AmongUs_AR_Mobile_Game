using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charactor : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Module")
        {
            print(1);
            MemberManager.instance.Head--;
        }
    }
}
