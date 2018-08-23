using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BackgroundStarsScroller : MonoBehaviour
{
    public float scrollSpeed;
    public float tileSizeZ;

    private Vector3 startPosition;

    //void Start()
    //{
    //    startPosition = transform.position;
    //}

    //void Update()
    //{
    //    float newPosition = Mathf.Repeat(Time.time * scrollSpeed, tileSizeZ);
    //    transform.position = startPosition + Vector3.right * newPosition;
    //}

    void Start()
    {
        startPosition = GetComponent<RectTransform>().localPosition;
    }

    void Update()
    {
        float newPosition = Mathf.Repeat(Time.time * scrollSpeed, tileSizeZ);
        GetComponent<RectTransform>().localPosition = startPosition + Vector3.right * newPosition;
    }
}