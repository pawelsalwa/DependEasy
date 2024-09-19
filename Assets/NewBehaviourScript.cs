using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private float amplitude = 1f;
    [SerializeField] private float timeScaleMultipler = 1f;

    // Start is called before the first frame update
    void Start()
    {
        UnityEngine.MonoBehaviour.FindObjectOfType<Object>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, Mathf.Sin(Time.time * timeScaleMultipler) * amplitude, transform.position.z);
    }
}
