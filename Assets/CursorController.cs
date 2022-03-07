using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    private float PosValue = 0.5f;
    [SerializeField]private AnimationCurve curve;
    [SerializeField]private float moveSpeed;
    [SerializeField]private float HorizontalScale;
    [SerializeField] private float VerticalScale;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        PosValue += Input.GetAxis("Horizontal") * Time.deltaTime*moveSpeed;
        PosValue = Mathf.Clamp(PosValue, 0, 1);
        
        transform.localPosition = new Vector3((PosValue-0.5f)*2*HorizontalScale,curve.Evaluate(PosValue)*VerticalScale,transform.localPosition.z);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Hit"))
        {
            Debug.Log("Hit");
        }
        if (collision.gameObject.CompareTag("Dodge"))
        {
            Debug.Log("Dodge");
        }
        if (collision.gameObject.CompareTag("LongHit"))
        {
            Debug.Log("LongHit");
        }
    }
}
