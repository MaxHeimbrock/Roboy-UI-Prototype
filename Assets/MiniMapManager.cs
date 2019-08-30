using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapManager : MonoBehaviour
{
    public enum MiniMapTechnique {YAH, PointCloud};

    public MiniMapTechnique currentMinimap = MiniMapTechnique.YAH;

    public GameObject YAH_Minimap;
    public GameObject Pointcloud_Minimap;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Switch MiniMap
        if (Input.GetKeyDown(KeyCode.M))
        {
            
            switch (currentMinimap)
            {
                case MiniMapTechnique.YAH:
                    SwitchToPointCloud();
                    break;
                case MiniMapTechnique.PointCloud:
                    SwitchToYAH();
                    break;
            }
        }
    }

    public void SwitchToYAH()
    {
        Debug.Log("Switch Minimap to YAH");
        currentMinimap = MiniMapTechnique.YAH;
        Pointcloud_Minimap.SetActive(false);
        YAH_Minimap.SetActive(true);
    }

    public void SwitchToPointCloud()
    {
        Debug.Log("Switch Minimap to PC");
        currentMinimap = MiniMapTechnique.PointCloud;
        YAH_Minimap.SetActive(false);
        Pointcloud_Minimap.SetActive(true);
    }
}
