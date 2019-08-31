using RosSharp.RosBridgeClient;
using RosSharp.RosBridgeClient.Messages.Roboy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperSubscriber : Singleton<SuperSubscriber>
{
    // We can not differenciate the notifications on the subscriber site, so we split the topics
    public string topicError = "/roboy/oui/OperatorLog/Error";
    public string topicWarning = "/roboy/oui/OperatorLog/Warning";
    public string topicInfo = "/roboy/oui/OperatorLog/Info";

    // This messageQueue is filled with incoming messages and pulled by LogText in every frame
    private Queue<RosSharp.RosBridgeClient.Message> operatorMessageQueue;

    public void EnqueueOperatorMessage(RosSharp.RosBridgeClient.Message msg)
    {
        operatorMessageQueue.Enqueue(msg);
    }

    public Message DequeueOperatorMessage()
    {
        return operatorMessageQueue.Dequeue();
    }

    public int MessageQueueCount()
    {
        return operatorMessageQueue.Count;
    }

    // Start is called before the first frame update
    void Start()
    {
        operatorMessageQueue = new Queue<Message>();
        Debug.Log("Super Subscriber started");

        ErrorSubscriber errorSubscriber = this.gameObject.AddComponent<ErrorSubscriber>();
        errorSubscriber.Topic = topicError;

        WarningSubscriber warningSubscriber = this.gameObject.AddComponent<WarningSubscriber>();
        warningSubscriber.Topic = topicWarning;

        InfoSubscriber infoSubscriber = this.gameObject.AddComponent<InfoSubscriber>();
        infoSubscriber.Topic = topicInfo;
    }

    private class ErrorSubscriber : Subscriber<RosSharp.RosBridgeClient.Messages.Roboy.ErrorNotification>
    {
        /// <summary>
        /// Holds the currently received data for other objects to read
        /// </summary>
        private string messageData;

        /// <summary>
        /// Start method of TestSubscriber.
        /// Starts a coroutine to initialize the subscriber after 1 second to prevent race conditions.
        /// </summary>
        protected override void Start()
        {
            StartCoroutine(startSubscriber(1.0f));
        }

        /// <summary>
        /// Initializes the subscriber.
        /// </summary>
        /// <param name="waitTime"> defines the time, after that subscriber is initialized.</param>
        /// <returns></returns>
        public IEnumerator startSubscriber(float waitTime)
        {
            while (true)
            {
                yield return new WaitForSeconds(waitTime);
                base.Start();
                //Debug.Log("Started Operator Log Error Subscriber");
                break;
            }
        }

        /// <summary>
        /// This handler is called, whenever a message on the subscribed topic is received.
        /// </summary>
        /// <param name="message"> is the received message.</param>
        protected override void ReceiveMessage(ErrorNotification message)
        {
            // Debug.Log("Received Operator Log Error Message: " + message.msg);

            // Split operator and roboy log with code 0 for operator log, 1 for roboy log
            if (message.code == 0)
                SuperSubscriber.Instance.EnqueueOperatorMessage(message);
        }
    }

    private class WarningSubscriber : Subscriber<RosSharp.RosBridgeClient.Messages.Roboy.WarningNotification>
    {
        /// <summary>
        /// Holds the currently received data for other objects to read
        /// </summary>
        private string messageData;

        /// <summary>
        /// Start method of TestSubscriber.
        /// Starts a coroutine to initialize the subscriber after 1 second to prevent race conditions.
        /// </summary>
        protected override void Start()
        {
            StartCoroutine(startSubscriber(1.0f));
        }

        /// <summary>
        /// Initializes the subscriber.
        /// </summary>
        /// <param name="waitTime"> defines the time, after that subscriber is initialized.</param>
        /// <returns></returns>
        public IEnumerator startSubscriber(float waitTime)
        {
            while (true)
            {
                yield return new WaitForSeconds(waitTime);
                base.Start();
                //Debug.Log("Started Operator Log Warning Subscriber");
                break;
            }
        }

        protected override void ReceiveMessage(WarningNotification message)
        {
            // Debug.Log("Received Operator Log Warning Message: " + message.msg);

            // Split operator and roboy log with code 0 for operator log, 1 for roboy log
            if (message.code == 0)
                SuperSubscriber.Instance.EnqueueOperatorMessage(message);
        }
    }

    private class InfoSubscriber : Subscriber<RosSharp.RosBridgeClient.Messages.Roboy.InfoNotification>
    {
        /// <summary>
        /// Holds the currently received data for other objects to read
        /// </summary>
        private string messageData;

        /// <summary>
        /// Start method of TestSubscriber.
        /// Starts a coroutine to initialize the subscriber after 1 second to prevent race conditions.
        /// </summary>
        protected override void Start()
        {
            StartCoroutine(startSubscriber(1.0f));
        }

        /// <summary>
        /// Initializes the subscriber.
        /// </summary>
        /// <param name="waitTime"> defines the time, after that subscriber is initialized.</param>
        /// <returns></returns>
        public IEnumerator startSubscriber(float waitTime)
        {
            while (true)
            {
                yield return new WaitForSeconds(waitTime);
                base.Start();
                //Debug.Log("Started Operator Log Info Subscriber");
                break;
            }
        }

        protected override void ReceiveMessage(InfoNotification message)
        {
            // Debug.Log("Received Operator Log Info Message: " + message.msg);

            // Split operator and roboy log with code 0 for operator log, 1 for roboy log
            if (message.code == 0)
                SuperSubscriber.Instance.EnqueueOperatorMessage(message);
        }
    }
}
