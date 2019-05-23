using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IVideoSubscriber 
{
    void ReceivePushNotification(int code);

    void SubscribeToVideoCapture();
}
