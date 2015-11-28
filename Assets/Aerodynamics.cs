using UnityEngine;
using System.Collections;

public class Aerodynamics : MonoBehaviour {

	Rigidbody rigid;
    [Range(0.0f, 2.0f)]
    public float factor = 1.0f;
    [Range(0.0f, 20.0f)]
    public float turnspeed = 10.0f;
    [Range(0.0f, 200.0f)]
    public float startspeed = 100.0f;

    // Use this for initialization
    void Start () {
		rigid = GetComponent<Rigidbody> ();
        rigid.centerOfMass = Vector3.forward * 2f;
        rigid.velocity = Vector3.forward * startspeed;
    }
	
	// Update is called once per frame
	void FixedUpdate () {

        rigid.velocity = Vector3.Lerp(rigid.velocity, rigid.velocity.magnitude * rigid.transform.forward * 0.5f+0.5f* rigid.transform.forward * Vector3.Project(rigid.velocity, rigid.transform.forward).magnitude,0.8f*Time.fixedDeltaTime);

		//make it float
		rigid.AddForceAtPosition (Vector3.up * 9.81f * factor, transform.position);        

        //add wasd
        rigid.AddRelativeTorque(Input.GetAxis("Bank") * -Vector3.forward * turnspeed);
        rigid.AddRelativeTorque(Input.GetAxis("Horizontal") * Vector3.up * turnspeed);
        rigid.AddRelativeTorque(Input.GetAxis("Vertical") * Vector3.right * turnspeed);
        rigid.AddRelativeForce(Input.GetButton("Fire1")? Vector3.forward * 100f:Vector3.zero);

        //tend to point at velocity
        rigid.AddRelativeTorque (.4f * Vector3.Cross(rigid.transform.forward, rigid.velocity));

        //debug lines
        Debug.DrawLine(rigid.worldCenterOfMass, rigid.worldCenterOfMass + rigid.velocity, Color.black);
        Debug.DrawLine(rigid.worldCenterOfMass, rigid.worldCenterOfMass + rigid.transform.forward*10f, Color.blue);
        Debug.DrawLine(rigid.worldCenterOfMass, rigid.worldCenterOfMass + rigid.transform.right * 10f, Color.green);
        Debug.DrawLine(rigid.worldCenterOfMass, rigid.worldCenterOfMass + rigid.transform.right * -10f, Color.red);
    }
}
