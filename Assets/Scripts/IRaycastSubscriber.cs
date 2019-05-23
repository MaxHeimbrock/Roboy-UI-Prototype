using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRaycastSubscriber 
{
    void ReceivePushNotification(RaycastHit hit, bool isHit);

    void SubscribeToRaycastManager();
}
