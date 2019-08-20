using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RosSharp.RosBridgeClient.Messages.Sensor;
using RosSharp.RosBridgeClient.Messages.Geometry;

public class PC_Conversion : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Get3DPoint(PointCloud2 pointCloud, int u, int v)
    {
        int width = pointCloud.width;
        int height = pointCloud.height;

        int arrayPos = v * pointCloud.row_step + u * pointCloud.point_step;

        int arrayPosX = arrayPos + pointCloud.fields[0].offset; // X has an offset of 0
        int arrayPosY = arrayPos + pointCloud.fields[1].offset; // Y has an offset of 4
        int arrayPosZ = arrayPos + pointCloud.fields[2].offset; // Z has an offset of 8

        float x = pointCloud.data[arrayPosX];
    }

    /*void PixelTo3DPoint(const PointCloud2 pCloud, const int u, const int v, geometry_msgs::Point &p)
    {
      // get width and height of 2D point cloud data
      int width = pCloud.width;
    int height = pCloud.height;

    // Convert from u (column / width), v (row/height) to position in array
    // where X,Y,Z data starts
    int arrayPosition = v * pCloud.row_step + u * pCloud.point_step;

    // compute position in array where x,y,z data start
    int arrayPosX = arrayPosition + pCloud.fields[0].offset; // X has an offset of 0
    int arrayPosY = arrayPosition + pCloud.fields[1].offset; // Y has an offset of 4
    int arrayPosZ = arrayPosition + pCloud.fields[2].offset; // Z has an offset of 8

    float X = 0.0;
    float Y = 0.0;
    float Z = 0.0;

    memcpy(&X, &pCloud.data[arrayPosX], sizeof(float));
    memcpy(&Y, &pCloud.data[arrayPosY], sizeof(float));
    memcpy(&Z, &pCloud.data[arrayPosZ], sizeof(float));

    // put data into the point p
    p.x = X;
      p.y = Y;
      p.z = Z;

    }*/
}
