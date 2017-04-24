using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class TCPNetwork : MonoBehaviour {

    //public int tester;
    public int myReliableChannelId = 0;
    public int hostId = 0;
    public int connectionId = 0;
    GameObject connectionSphere;
    ConditionManager condition;
    //GameObject sphere;
    public byte[] sendbuffer = { 0, 0, 0, 0, 0, 0 };
    int sendbufferLength = 6;
    //GameObject debugText;
    //public int save = 0;

    // Use this for initialization
    void Start () {
        connectionSphere = GameObject.Find("Connection");
        condition = GameObject.Find("ConditionManager").GetComponent<ConditionManager>();
        //sphere = GameObject.Find("s2");
        connectionSphere.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        //sphere.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        //debugText = GameObject.Find("DebugText");
        //tester = 0;

        NetworkTransport.Init();

        ConnectionConfig config = new ConnectionConfig();
        myReliableChannelId = config.AddChannel(QosType.Reliable);
        int myUnreliableChannelId = config.AddChannel(QosType.Unreliable);
        Debug.Log("Reliable channel ID : " + myReliableChannelId + " " + myUnreliableChannelId);

        HostTopology topology = new HostTopology(config, 10);

        hostId = NetworkTransport.AddHost(topology, 8888);
        Debug.Log("PC Host ID : " + hostId);
        //dbg(myReliableChannelId.ToString() + " " + hostId.ToString());

#if UNITY_EDITOR
        byte error;
        connectionId = NetworkTransport.Connect(hostId, "143.215.111.174", 8888, 0, out error);
        Debug.Log("PC ConnetionID : " + connectionId + " Error : " + error);
        //NetworkTransport.Disconnect(hostId, connectionId, out error);
#else
        //int connectionId = NetworkTransport.Connect(hostId, "127.0.0.1", 8888, 0, out error);
        //Debug.Log("PC ConnetionID : " + connectionId);
        //NetworkTransport.Disconnect(hostId, connectionId, out error);
#endif


    }

    void dbg(string s)
    {
        //debugText.GetComponent<Text>().text = s;
    }

    // Update is called once per frame
    void Update () {
#if UNITY_EDITOR
        sendbuffer[0] = (byte)condition.event_state;
        sendbuffer[1] = (byte)condition.location_state;
        sendbuffer[2] = (byte)condition.timeofday_state;
        sendbuffer[3] = (byte)condition.useractivity_state;
        sendbuffer[4] = (byte)condition.cognitiveload_state;
        sendbuffer[5] = (byte)condition.emotion_state;

        int recHostId;
        int connectionId;
        int channelId;
        byte[] recBuffer = new byte[1024];
        int bufferSize = 1024;
        int dataSize;
        byte error;
        NetworkEventType recData = NetworkTransport.Receive(out recHostId, out connectionId, out channelId, recBuffer, bufferSize, out dataSize, out error);
        if (sendbuffer[0] > 0 )
        {
            //connectionSphere.GetComponent<Renderer>().material.SetColor("_Color", new Color(sendbuffer[1], sendbuffer[2], sendbuffer[3], 1.0f));
            NetworkTransport.Send(0, 1, 1, sendbuffer, sendbufferLength, out error);
            //Debug.Log("Sending.." + recHostId + " " + connectionId + " " + channelId + " " + error);
            Debug.Log("Sending.." + error);
            //sendbuffer[0] = 0;
            condition.event_state = 0;
        }
        if (sendbuffer[0] == 44)
        {
            NetworkTransport.Disconnect(0, 1, out error);
            Debug.Log("Disconnecting.." + error);
            sendbuffer[0] = 0;
        }
#else
        int recHostId;
        int connectionId;
        int channelId;
        byte[] recBuffer = new byte[1024];
        int bufferSize = 1024;
        int dataSize;
        byte error;
        NetworkEventType recData = NetworkTransport.Receive(out recHostId, out connectionId, out channelId, recBuffer, bufferSize, out dataSize, out error);
        switch (recData)
        {
            case NetworkEventType.Nothing:         //1 3
                //Debug.Log("Nothing");
                break;
            case NetworkEventType.ConnectEvent:    //2 1
                //Debug.Log("Connected");
                connectionSphere.GetComponent<Renderer>().material.SetColor("_Color", new Color(0.0f, 1.0f, 0.0f, 1.0f));
                break;
            case NetworkEventType.DataEvent:       //3 0
                connectionSphere.GetComponent<Renderer>().material.SetColor("_Color", new Color(0.0f, 0.0f, 1.0f, 1.0f));
                //sphere.GetComponent<Renderer>().material.SetColor("_Color", new Color(recBuffer[1], recBuffer[2], recBuffer[3], 1.0f));
                //Debug.Log(recBuffer[0]);
                /*if (recBuffer[0] == 20) save = 1;
                if (recBuffer[0] == 22) save = 2;
                if (recBuffer[0] == 24) save = 3;
                if (recBuffer[0] == 26) save = 4;
                if (recBuffer[0] == 28) save = 5;
                if (recBuffer[0] == 30) save = 6;*/
                condition.event_state = (ConditionManager._event)recBuffer[0];
                condition.location_state = (ConditionManager._location)recBuffer[1];
                condition.timeofday_state = (ConditionManager._timeofday)recBuffer[2];
                condition.useractivity_state = (ConditionManager._useractivity)recBuffer[3];
                condition.cognitiveload_state = (ConditionManager._cognitiveload)recBuffer[4];
                condition.emotion_state = (ConditionManager._emotion)recBuffer[5];
                break;
            case NetworkEventType.DisconnectEvent: //4 2
                //Debug.Log("Disconnected");
                connectionSphere.GetComponent<Renderer>().material.SetColor("_Color", new Color(1.0f, 0.0f, 0.0f, 1.0f));
                break;
        }
        //v[(int)recData]++;
        //dbg(v[0].ToString()+" "+ v[1].ToString() + " " + v[2].ToString() + " " + v[3].ToString() + " " + v[4].ToString() + " ");
#endif
    }
    //int []v = { 0, 0, 0, 0, 0 };
}
