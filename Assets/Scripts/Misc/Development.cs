using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Development : MonoBehaviour
{
    //In this script there is a configuration for faster development and testing
    [SerializeField] private GameObject OVRSceneManager;
    [SerializeField] private OVRManager ovrManager;
    [SerializeField] private GameObject table;
    [SerializeField] private GameObject passthroughBuildingBlock;
    [SerializeField] private GameObject debugUI;
    
    // Start is called before the first frame update
    void Awake()
    {
        DisableSceneManager();
        //DisablePassthroughCamera();
        SpawnTableToScene();
        //ShowDebugUI();
    }
    
    private void DisableSceneManager()
    {
        OVRSceneManager.SetActive(false);
    }
    
    private void DisablePassthroughCamera()
    {
        passthroughBuildingBlock.SetActive(false);
        if (ovrManager.isInsightPassthroughEnabled)
        {
            ovrManager.isInsightPassthroughEnabled = false;
        }
    }
    
    private void SpawnTableToScene()
    {
        //Spawn the table
        table.SetActive(true);
        table.transform.position = new Vector3(0, 0.75f, 0.5f);
        Debug.Log("Table spawned to scene, local position: "+table.transform.localPosition);
    }
    
    private void ShowDebugUI()
    {
        if(!debugUI.activeSelf) debugUI.SetActive(true);
    }
}
