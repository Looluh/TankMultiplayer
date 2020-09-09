using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankPlayer : MonoBehaviour
{
    public HingeJoint[] RghWheels;
    public HingeJoint[] LftWheels;
    public float turn;
    public float power;

    public PhotonView pview;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().centerOfMass = new Vector3(0, -1, 0);

        if (pview.IsMine)
        {
            Camera.main.GetComponent<NetCamera>().SetPlayer(gameObject);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (pview.IsMine)
        {

            power = Input.GetAxis("Vertical");
            turn = Input.GetAxis("Horizontal");

            foreach (HingeJoint wheel in RghWheels)
            {
                JointMotor motor = new JointMotor();
                motor.targetVelocity = power * 600 + turn * -300;
                motor.force = 1000;
                wheel.motor = motor;
            }
            foreach (HingeJoint wheel in LftWheels)
            {
                JointMotor motor = new JointMotor();
                motor.targetVelocity = power * 600 + turn * 300;
                motor.force = 1000;
                wheel.motor = motor;
            }

        }

    }

}
