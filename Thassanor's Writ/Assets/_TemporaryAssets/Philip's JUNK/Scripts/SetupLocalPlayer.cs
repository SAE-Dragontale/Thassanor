using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SetupLocalPlayer : NetworkBehaviour {

    [SyncVar]
    public string pname = "player";

    [SyncVar]
    public Color playerColor = Color.white;

    void OnGUI()
    {
        if (isLocalPlayer)
        {
            pname = GUI.TextField(new Rect(25, Screen.height - 40, 100, 30), pname);
            if (GUI.Button(new Rect(130, Screen.height - 40, 80, 30), "Change"))
            {
                CmdChangeName(pname);
            }
        }

    }

    [Command]
    public void CmdChangeName(string newName)
    {
        pname = newName;
    }

    // Use this for initialization
    void Start () {
        if (isLocalPlayer)
        {
            GetComponent<CarController>().enabled = true;
            SmoothCameraFollow.target = this.transform;
        }

        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        foreach (Renderer r in renderers)
            r.material.color = playerColor;

        this.transform.position = new Vector3(Random.Range(-20, 20), 0, Random.Range(-20, 20));
	}

    private void Update()
    {
            this.GetComponentInChildren<TextMesh>().text = pname;
    }
}
