using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteRotation : MonoBehaviour
{
    public float speed = 10f;

    private void FixedUpdate()
    {
        transform.Rotate(transform.rotation.x, speed * Time.deltaTime, transform.rotation.z);
    }
}
