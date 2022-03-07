using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[System.Serializable]
public class LevelManager : MonoBehaviour
{
    public AudioClip song;
    public float speed;
    
    public List<Transform> lanePos;

    public List<GameObject> Blocks;

    private void Start()
    {
        
    }

    private void Update()
    {
        transform.position = new Vector3(transform.position.x,transform.position.y,transform.position.z - Time.deltaTime*speed);
    }
}
