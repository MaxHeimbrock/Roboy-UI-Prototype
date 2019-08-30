using RosSharp.RosBridgeClient;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperPublisher : Singleton<SuperPublisher>
{
    // We can not differenciate the notifications on the subscriber site, so we split the topics
    public string topicError = "/roboy/oui/OperatorLog/Error";
    public string topicWarning = "/roboy/oui/OperatorLog/Warning";
    public string topicInfo = "/roboy/oui/OperatorLog/Info";

    ErrorPublisher errorPublisher;
    WarningPublisher warningPublisher;
    InfoPublisher infoPublisher;

    // Start is called before the first frame update
    void Start()
    {        
        Debug.Log("Super Publisher started");

        errorPublisher = this.gameObject.AddComponent<ErrorPublisher>();
        errorPublisher.Topic = topicError;

        warningPublisher = this.gameObject.AddComponent<WarningPublisher>();
        warningPublisher.Topic = topicWarning;

        infoPublisher = this.gameObject.AddComponent<InfoPublisher>();
        infoPublisher.Topic = topicInfo;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            errorPublisher.publishMessage("mockToastr", 0);
        }
    }

    public void PublishMessage(string message, int code, LogText.LogLevel logLevel)
    {
        switch (logLevel)
        {
            case LogText.LogLevel.info:
                break;
            case LogText.LogLevel.warning:
                break;
            case LogText.LogLevel.error:
                errorPublisher.publishMessage(message, code);
                break;
        }
    }

    private class ErrorPublisher : Publisher<RosSharp.RosBridgeClient.Messages.Roboy.ErrorNotification>
    {
        /// <summary>
        /// Start method of TestPublisher.
        /// Starts a coroutine to initialize the publisher after 1 second to prevent race conditions.
        /// </summary>
        protected override void Start()
        {
            StartCoroutine(startPublisher(1.0f));
        }

        /// <summary>
        /// Starts the publisher and sends two test messages for demonstration purposes.
        /// </summary>
        /// <param name="waitTime"></param>
        /// <returns></returns>
        private IEnumerator startPublisher(float waitTime)
        {
            while (true)
            {
                yield return new WaitForSeconds(waitTime);
                base.Start();
                break;
            }
        }

        /// <summary>
        /// This method publishes a simple string messages to the topic of the object.
        /// </summary>
        /// <param name="message">is the message, which shall be published.</param>
        public void publishMessage(string message, int code)
        {
            // Debug.Log("Publish message " + message + " with code " + code);

            // Fill with message and dummy values - code 0 for operator log - code 1 for roboy log
            RosSharp.RosBridgeClient.Messages.Roboy.ErrorNotification errorMessage = new RosSharp.RosBridgeClient.Messages.Roboy.ErrorNotification(code, "", message, "", 0);

            Publish(errorMessage);
        }
    }

    private class WarningPublisher : Publisher<RosSharp.RosBridgeClient.Messages.Roboy.WarningNotification>
    {
        /// <summary>
        /// Start method of TestPublisher.
        /// Starts a coroutine to initialize the publisher after 1 second to prevent race conditions.
        /// </summary>
        protected override void Start()
        {
            StartCoroutine(startPublisher(1.0f));
        }

        /// <summary>
        /// Starts the publisher and sends two test messages for demonstration purposes.
        /// </summary>
        /// <param name="waitTime"></param>
        /// <returns></returns>
        private IEnumerator startPublisher(float waitTime)
        {
            while (true)
            {
                yield return new WaitForSeconds(waitTime);
                base.Start();
                break;
            }
        }

        /// <summary>
        /// This method publishes a simple string messages to the topic of the object.
        /// </summary>
        /// <param name="message">is the message, which shall be published.</param>
        public void publishMessage(string message, int code)
        {
            // Debug.Log("Publish message " + message + " with code " + code);

            // Fill with message and dummy values - code 0 for operator log - code 1 for roboy log
            RosSharp.RosBridgeClient.Messages.Roboy.WarningNotification warningMessage = new RosSharp.RosBridgeClient.Messages.Roboy.WarningNotification(code, "", message, "", 0);

            Publish(warningMessage);
        }
    }

    private class InfoPublisher : Publisher<RosSharp.RosBridgeClient.Messages.Roboy.InfoNotification>
    {
        /// <summary>
        /// Start method of TestPublisher.
        /// Starts a coroutine to initialize the publisher after 1 second to prevent race conditions.
        /// </summary>
        protected override void Start()
        {
            StartCoroutine(startPublisher(1.0f));
        }

        /// <summary>
        /// Starts the publisher and sends two test messages for demonstration purposes.
        /// </summary>
        /// <param name="waitTime"></param>
        /// <returns></returns>
        private IEnumerator startPublisher(float waitTime)
        {
            while (true)
            {
                yield return new WaitForSeconds(waitTime);
                base.Start();
                break;
            }
        }

        /// <summary>
        /// This method publishes a simple string messages to the topic of the object.
        /// </summary>
        /// <param name="message">is the message, which shall be published.</param>
        public void publishMessage(string message, int code)
        {
            // Debug.Log("Publish message " + message + " with code " + code);

            // Fill with message and dummy values - code 0 for operator log - code 1 for roboy log
            RosSharp.RosBridgeClient.Messages.Roboy.InfoNotification infoMessage = new RosSharp.RosBridgeClient.Messages.Roboy.InfoNotification(code, "", message, "", 0);

            Publish(infoMessage);
        }
    }
}
