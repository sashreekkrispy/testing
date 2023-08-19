using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class car_hood_inter : MonoBehaviour
{
    private Animator animator;
    Camera cam;
    RaycastHit hit;
    Ray ray;
    Collider Collidercol;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        Collidercol = GameObject.Find("hood.001").GetComponent<Collider>();
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
    }
    private void Update()
    {
        ray = cam.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out hit))
        {
            if (hit.collider == Collidercol)
            {
                animator.SetBool("up", true);
            }
        }
    }
}
