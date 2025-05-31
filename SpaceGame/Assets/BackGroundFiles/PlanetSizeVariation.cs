using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetSizeVariation : MonoBehaviour
{

    private float size;
    private float min = 0.01f;
    private float max = 0.06f;

    // Start is called before the first frame update
    void Start()
    {
        size = Random.Range(min, max);
        transform.localScale = new Vector2(size, size);
    }
}
