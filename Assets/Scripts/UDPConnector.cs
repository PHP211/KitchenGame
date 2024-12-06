using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class UDPConnector : MonoBehaviour {

    public static UDPConnector Instance { get; private set; }

    public int direction = 0;
    public int isMovement = 0;
    public int interaction = 0;

    public string receivedString = "";

    private UdpClient udpClient;
    public int port = 25001;
    private IPEndPoint remoteEndPoint;

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        udpClient = new UdpClient(port);
        remoteEndPoint = new IPEndPoint(IPAddress.Any, port);

        // Bắt đầu nhận dữ liệu UDP
        udpClient.BeginReceive(new AsyncCallback(ReceiveData), null);
    }

    // Hàm xử lý dữ liệu UDP
    private void ReceiveData(IAsyncResult result) {
        try {
            // Nhận dữ liệu từ UDP
            byte[] receivedBytes = udpClient.EndReceive(result, ref remoteEndPoint);
            string receivedString = Encoding.ASCII.GetString(receivedBytes);

            Debug.Log("Received data: " + receivedString);

            string[] receiveData = receivedString.Split(',');

            if (receiveData.Length == 3) {

                if (int.TryParse(receiveData[0].Trim(), out int parsedIsMovement)) {
                    isMovement = parsedIsMovement;
                } else {
                    Debug.LogError("Failed to parse isMovement.");
                }

                if (int.TryParse(receiveData[1].Trim(), out int parsedDirection)) {
                    direction = parsedDirection;
                } else {
                    Debug.LogError("Failed to parse direction.");
                }

                if (int.TryParse(receiveData[2].Trim(), out int parsedInteraction)) {
                    interaction = parsedInteraction;
                } else {
                    Debug.LogError("Failed to parse interaction.");
                }

            } else {
                Debug.LogError("Invalid data format: expected 3 values.");
            }

            udpClient.BeginReceive(new AsyncCallback(ReceiveData), null);
        } catch (Exception e) {
            Debug.LogError("Error receiving UDP data: " + e.Message);
        }
    }

    private void OnApplicationQuit() {
        if (udpClient != null)
            udpClient.Close();
    }
}
