using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class move : MonoBehaviour
{
   public Transform cam;
    Rigidbody rb;
    public WheelCollider[] wheels = new WheelCollider[4];
    public Transform[] tires = new Transform[4]; 
    private float maxF = 50f; 
    private float power = 200f;
    private float rot = 10f;
    private float brake = 0;
    private float maxvel = 20f;
    public Text velo_text;
    


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        for (int i = 0; i < 4; i++)
        {
            wheels[i].steerAngle = 0;
            wheels[i].ConfigureVehicleSubsteps(5, 12, 13);
        }

        rb.centerOfMass = new Vector3(0, 0, 0);
        rb.centerOfMass = new Vector3(0, -1, 0);
    }
    void Update()
    {
        UpdateMeshesPostion();
        Wheel();
        Debug.Log((int)Vector3.Dot(rb.velocity, transform.forward));
    }

    private void Wheel() 
    {
        if (Input.GetKey(KeyCode.Space)) brake = 300;
        if (Input.GetKeyUp(KeyCode.Space)) brake = 0;
        float a = Input.GetAxis("Vertical");
        float vel = Mathf.Abs(Vector3.Dot(rb.velocity, transform.forward));
        velo_text.text = ((int)(vel*10)).ToString();
        if(vel>12f) velo_text.color = Color.red;
        else if(vel>8f) velo_text.color = Color.yellow;
        else velo_text.color = Color.green;
        if (vel < maxvel)
        {
            rb.AddForce(transform.rotation * new Vector3(0, 0, a * power));
        }
        else a = 0;
        for (int i = 0; i < 4; i++)
        {
            wheels[i].motorTorque = maxF * a;
            wheels[i].brakeTorque = brake;
        }       
        float steer = rot * Input.GetAxis("Horizontal");
        for (int i = 0; i < 2; i++) 

        {
            wheels[i].steerAngle = steer;
        }

    }
    void UpdateMeshesPostion()

    {

        for (int i = 0; i < 4; i++)

        {

            Quaternion quat;

            Vector3 pos;

            wheels[i].GetWorldPose(out pos, out quat);

            tires[i].position = pos;

            tires[i].rotation = quat;

        }

    }

}
