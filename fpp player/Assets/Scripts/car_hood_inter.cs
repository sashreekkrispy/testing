using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class car_hood_inter : MonoBehaviour
{
    private Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();

    }
    private void Update()
    {
        animator.SetBool("up", true);
    }
}
