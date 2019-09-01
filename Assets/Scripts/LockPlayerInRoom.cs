using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// This only works, if the simple camera controller is disabled
/// </summary>
public class LockPlayerInRoom : MonoBehaviour
{
    Vector3 lastPos;

    bool lockZ = false;
    bool lockX = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // Wand oben && links || wand oben && rechts || Wand unten
        //  4.6
        //  ----
        //  |  |_
        //  |    |
        //  ------
        //  -2.5
        if ((this.transform.position.z >= 4.6 && this.transform.position.x < 0.4f) || (this.transform.position.z >= 1.79f && this.transform.position.x > 0.4f) || this.transform.position.z <= -2.5f)
            lockZ = true;

        //  
        //      ----
        // 4.85 |  |_  -0.4
        //      |    |  1.39
        //      ------
        //  
        // Wand links || Wand rechts && unten || Wand rechts && oben
        if (this.transform.position.x <= -4.85f || (this.transform.position.x >= 1.39f && this.transform.position.z < 1.79f) || (this.transform.position.x >= -0.4f &&  this.transform.position.z > 1.79f))
            lockX = true;

        if (lockX == true && lockZ == false)
            this.transform.position = new Vector3(lastPos.x, this.transform.position.y, this.transform.position.z);
        else if (lockX == false && lockZ == true)
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, lastPos.z);
        else if (lockX == true && lockZ == true)
            this.transform.position = new Vector3(lastPos.x, this.transform.position.y, lastPos.z);

        lastPos = this.transform.position;
        lockX = false;
        lockZ = false;
    }
}
