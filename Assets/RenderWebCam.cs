using UnityEngine;
using System.Collections;

public class RenderWebCam : MonoBehaviour {

	public string deviceName;
	private WebCamTexture webcamTexture;
	private Quaternion baseRotation;

	// Use this for initialization
	void Start()
	{
		
		// Ensure orientation and scaling of the image.		
		var scale = 2.2f;
		Quaternion rotation = Quaternion.Euler(0, 0, 0);
		Matrix4x4 rotationMatrix = Matrix4x4.TRS(Vector3.zero, rotation, new Vector3(scale , scale , scale));
		gameObject.GetComponent<Renderer>().material.SetMatrix("_Rotation", rotationMatrix);

		int t = 0;
		bool front_facing = false;

		WebCamDevice[] devices = WebCamTexture.devices;

		// Gets the back facing camera if there is one on the device
		while(front_facing && t < devices.Length){
			deviceName = devices[t].name;
			front_facing = devices[t].isFrontFacing;
			t++;
		}

		webcamTexture = new WebCamTexture(deviceName);

		// performs the vertical axis flip
		if (!front_facing) {
			transform.Rotate(new Vector3(0,0,180));
		}

		// performs the horizontal axis flip that is the issue regardless
		transform.localScale = new Vector3(-1 * transform.localScale.x, 
		                                   transform.localScale.y,
		                                   transform.localScale.z);

		GetComponent<Renderer>().material.mainTexture = webcamTexture;
		webcamTexture.Play();
	}
	
	// Update is called once per frame
	void Update () {
	}
}
