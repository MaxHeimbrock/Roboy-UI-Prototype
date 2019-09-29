 using RosSharp.RosBridgeClient;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class publishes to the log message topics.
/// Also, for debugging messages can be send on key press.
/// For every log level, there is a subclass to handle that topic.
/// </summary>
public class LogPublisher : Singleton<LogPublisher>
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
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            infoPublisher.publishMessage("Look!! Potatoes");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            errorPublisher.publishMessage("Roboy battery is low");
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            errorPublisher.publishMessage("Oops, I broke. Best Regards\nRoboy");
        }
    }
    
    /// <summary>
    /// This publishes a message with the according publisher subclass taken for the log level
    /// </summary>
    /// <param name="message">message text as string</param>
    /// <param name="logLevel">can be info, warning or error; changes representation of message in log menu</param>
    public void PublishMessage(string message, LogText.LogLevel logLevel)
    {
        switch (logLevel)
        {
            case LogText.LogLevel.info:
                infoPublisher.publishMessage(message);
                break;
            case LogText.LogLevel.warning:
                warningPublisher.publishMessage(message);
                break;
            case LogText.LogLevel.error:
                errorPublisher.publishMessage(message);
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
        public void publishMessage(string message)
        {
            // Fill with message and dummy values
            RosSharp.RosBridgeClient.Messages.Roboy.ErrorNotification errorMessage = new RosSharp.RosBridgeClient.Messages.Roboy.ErrorNotification(0, "", message, "", 0);

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
        public void publishMessage(string message)
        {
            // Fill with message and dummy values
            RosSharp.RosBridgeClient.Messages.Roboy.WarningNotification warningMessage = new RosSharp.RosBridgeClient.Messages.Roboy.WarningNotification(0, "", message, "", 0);

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
        public void publishMessage(string message)
        {
            // Fill with message and dummy values - code 0 for operator log - code 1 for roboy log
            RosSharp.RosBridgeClient.Messages.Roboy.InfoNotification infoMessage = new RosSharp.RosBridgeClient.Messages.Roboy.InfoNotification(0, "", message, "", 0);

            Publish(infoMessage);
        }
    }
}
