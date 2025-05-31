using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarSizeVariation : MonoBehaviour
{

    private float size;
    private float min = 0.03f;
    private float max = 0.08f;

    // Start is called before the first frame update
    void Start()
    {
        size = Random.Range(min, max);
        transform.localScale = new Vector2(size, size);
    }
}
