using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperPublisher : Singleton<SuperPublisher>
{
    // We can not differenciate the notifications on the subscriber site, so we split the topics
    public string topicError;
    public string topicWarning;
    public string topicInfo;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
