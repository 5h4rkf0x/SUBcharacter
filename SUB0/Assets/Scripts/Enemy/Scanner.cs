using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    public float scanRange;
    public LayerMask targetLayer;
    public RaycastHit2D target;
    public Transform nearestTarget;

    void FixedUpdate()
    {
        target = Physics2D.CircleCast(transform.position, scanRange, Vector2.zero, 0, targetLayer);
        if(target)
        {
            nearestTarget = GetNearest();
        }
        
    }

    Transform GetNearest()
    {
        Transform result = null;
        float diff = 100;

        
        Vector3 myPos = transform.position;
        Vector2 targetPos = target.transform.position;
        float curDiff = Vector3.Distance(myPos, targetPos);

        if (curDiff < diff)
        {
            diff = curDiff;
            result = target.transform;
        }

 
        return result;
    }
}

