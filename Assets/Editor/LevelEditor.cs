using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
[CustomEditor(typeof(LevelManager))]
public class LevelEditor : Editor
{
    private int lane;
    public GameObject HitBlock;
    public GameObject DodgeBlock;
    public GameObject LongHitBlock;

    private Transform Level;
    private LevelManager levelManager;
    private List<Transform> LanePos;

    private void OnEnable()
    {
        Level = GameObject.Find("Level").transform;
        levelManager = GameObject.Find("Level").GetComponent<LevelManager>();
        LanePos = GameObject.Find("Level").GetComponent<LevelManager>().lanePos;
    }
    public override void OnInspectorGUI()
    {



        lane = (int)EditorGUILayout.Slider(lane, 1, 5);
        if (GUILayout.Button("Add Hit Block at lane " + lane))
        {

            Debug.Log("Add Hit Block at lane " + lane);
            AddBlock(lane,HitBlock);
        }
        if (GUILayout.Button("Add Dodge Block at lane " + lane))
        {
            AddBlock(lane,DodgeBlock);
            Debug.Log("Add Dodge Block at lane " + lane);
        }
        if (GUILayout.Button("Add Long Hit Block at lane " + lane))
        {
            AddBlock(lane,LongHitBlock);
            Debug.Log("Add Long Hit Block at lane " + lane);
        }
        if (GUILayout.Button("Delete the last Block"))
        {
            DeleteBlock(levelManager.Blocks.Count - 1);
            Debug.Log("delete last block");
        }

        DrawDefaultInspector();
    }

    public void DeleteBlock(int index)
    {
        if (index > 0)
        {
            GameObject a = levelManager.Blocks[index];
            levelManager.Blocks.RemoveAt(index);
            GameObject.DestroyImmediate(a);
        }

    }

    public void AddBlock(int lane,GameObject block)
    {

        GameObject a = Instantiate(block, LanePos[lane - 1].position, LanePos[lane - 1].rotation, Level);
        a.transform.position = new Vector3(a.transform.position.x, a.transform.position.y,
            levelManager.Blocks[levelManager.Blocks.Count - 1].transform.position.z + levelManager.Blocks[levelManager.Blocks.Count - 1].transform.localScale.z);
        levelManager.Blocks.Add(a);
    }
}
