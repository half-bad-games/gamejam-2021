using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Layered_Rendering : MonoBehaviour
{
    // Start is called before the first frame update
    public int z_offset;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Set render layer to z.axis
        gameObject.GetComponent<SpriteRenderer>().sortingOrder = -(int)transform.position.z + z_offset;
    }
}
