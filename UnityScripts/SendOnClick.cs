using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class SendOnClick : MonoBehaviour {

	private SignalRListenerAndComms sr;

    void Start () {

		Text UIMsg = GameObject.Find("UIMsg").GetComponent<Text>();
		sr = FindObjectOfType<SignalRListenerAndComms>();
		
		sr.SignalRShim.SendMessageAsync("SendMessageFromClient", "Bish");

		sr.SignalRShim.BindIncomingListener<string>("ReceiveMessageString",
			(message) =>
			{
				UIMsg.text = "Message Received from SignalR: " + message;
			}
		);
	}	

}