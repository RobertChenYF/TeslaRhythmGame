using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.iOS;
public class CursorController : MonoBehaviour
{
    private float PosValue = 0.5f;
    [SerializeField]private AnimationCurve curve;
    [SerializeField]private float moveSpeed;
    [SerializeField]private float HorizontalScale;
    [SerializeField]private float VerticalScale;
    private Quaternion currentRotation;
    private bool hitObj;
    [SerializeField]private ParticleSystem particle;
    [SerializeField] private ParticleSystem particleRed;
    [SerializeField] private ParticleSystem particleGreen;
    // Start is called before the first frame update
    void Start()
    {
        currentRotation = transform.rotation;
        //var emission = particle.emission;
        //emission.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount >= 1 || Input.GetMouseButton(0))
        {

        Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //Debug.DrawRay(Camera.main.transform.position,ray.direction*20.0f,Color.cyan,5.0f);
        RaycastHit hit;
            if (Physics.Raycast(ray, out hit,99.0f)) {
                //Debug.Log("hit object");
                if (hit.collider.gameObject.CompareTag("Cursor"))
                {
                    hitObj = true;
                    GetComponent<MeshRenderer>().material.SetColor("_Color", Color.green);
                    Debug.Log(hit.point);
                    //Debug.Log();
                    
                }

            } 
        }
        else
        {
            hitObj = false;
            GetComponent<MeshRenderer>().material.SetColor("_Color", Color.blue);
        }

        if (hitObj)
        {
           //Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            //Vector3 newPoint = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,5.8f));
            Vector3 newPoint = Camera.main.ScreenToWorldPoint(new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, 5.8f));
            //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //this.transform.position = new Vector3(newPoint.x,newPoint.y,-4.2f);

            PosValue += Mathf.Sign(newPoint.x - transform.position.x) * Mathf.Min(moveSpeed, Mathf.Abs(transform.position.x - newPoint.x) * Time.deltaTime);
        }
        
        PosValue += Input.GetAxis("Horizontal") * Time.deltaTime*moveSpeed;
        PosValue = Mathf.Clamp(PosValue, 0, 1);
        
        transform.localPosition = new Vector3((PosValue-0.5f)*2*HorizontalScale,curve.Evaluate(PosValue)*VerticalScale,transform.localPosition.z);
        currentRotation.eulerAngles = new Vector3(currentRotation.eulerAngles.x,currentRotation.eulerAngles.y, curve.Evaluate(PosValue)*60*Mathf.Sign(PosValue-0.5f));
        transform.rotation = currentRotation;
        
        //var emission = particle.emission;
        //emission.enabled = false;
    }

    private void LateUpdate()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Hit"))
        {

            particle.Emit(20);
            Debug.Log("Hit");
        }
        if (collision.gameObject.CompareTag("Dodge"))
        {
            particleRed.Emit(20);
            Debug.Log("Dodge");
        }
        if (collision.gameObject.CompareTag("LongHit"))
        {
            particleGreen.Emit(20);
            Debug.Log("LongHit");
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Hit"))
        {
            particle.Emit(Mathf.CeilToInt(60 * Time.deltaTime));
            Debug.Log("Hit");
        }
        if (collision.gameObject.CompareTag("Dodge"))
        {
            particleRed.Emit(Mathf.CeilToInt(60 * Time.deltaTime));
            Debug.Log("Dodge");
        }
        if (collision.gameObject.CompareTag("LongHit"))
        {
            particleGreen.Emit(Mathf.CeilToInt(60 * Time.deltaTime));
            Debug.Log("LongHit");
        }
    }
}
