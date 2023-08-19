using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollow : MonoBehaviour
{
    public List<GameObject> follows;
    public float speed = 2f;
    public bool isLoop = true;
    public int index = 0;

    private void Update()
    { 
        Vector3 destination = follows[index].transform.position;
        Vector3 rotDestination = follows[index].transform.localEulerAngles;
        transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
        transform.localEulerAngles = Vector3.Slerp(transform.localEulerAngles, rotDestination, 100f*Time.deltaTime);
        float distance = Vector3.Distance(transform.position, destination);
        if(distance <=0.05)
        {
            if(index < follows.Count-1)
            {
                index++;
            }
            else
            {
                if(isLoop)
                {

                    index = 0;
                }
            }
        }
    }
}
