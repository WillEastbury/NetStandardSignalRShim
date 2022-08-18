using System.Collections;
using System.Collections.Generic;
using NetStandardSignalRShim;
using UnityEngine;

public class SignalRListenerAndComms : MonoBehaviour
{
    public SignalRShim SignalRShim {get; set;} = new SignalRShim();

    // Start is called before the first frame update
    void Start()
    {
        SignalRShim.StartConnectionAsync("http://localhost:7071/api");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
