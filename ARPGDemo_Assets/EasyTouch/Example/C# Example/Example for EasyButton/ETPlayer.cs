using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ETPlayer : MonoBehaviour {
	
	public GameObject bullet;
	
	private Transform model;
	private Transform gun;
	
	void OnEnable(){
		EasyJoystick.On_JoystickMove += On_JoystickMove;	
		EasyJoystick.On_JoystickMoveEnd += On_JoystickMoveEnd;
		//EasyButton.On_ButtonPress += On_ButtonPress;
		EasyButton.On_ButtonUp += On_ButtonUp;	
		//EasyButton.On_ButtonDown += On_ButtonDown;
	}

	void Fire(){
		//if (buttonName=="Fire"){
			Instantiate( bullet, gun.transform.position, gun.rotation);
		//}		
	}


	void OnDisable(){
		EasyJoystick.On_JoystickMove -= On_JoystickMove;	
		EasyJoystick.On_JoystickMoveEnd -= On_JoystickMoveEnd;
//		EasyButton.On_ButtonPress -= On_ButtonPress;
		EasyButton.On_ButtonUp -= On_ButtonUp;	
	}
		
	void OnDestroy(){
		EasyJoystick.On_JoystickMove -= On_JoystickMove;	
		EasyJoystick.On_JoystickMoveEnd -= On_JoystickMoveEnd;
//		EasyButton.On_ButtonPress -= On_ButtonPress;
		EasyButton.On_ButtonUp -= On_ButtonUp;	
	}
	
	void Start(){
		model = transform.Find("Model").transform;	
		gun = transform.Find("Gun").transform;	
	}
	
	void On_JoystickMove( MovingJoystick move){
	
		float angle = move.Axis2Angle(true);
		transform.rotation  = Quaternion.Euler( new Vector3(0,angle,0));
		transform.Translate( Vector3.forward * move.joystickValue.magnitude * Time.deltaTime);	
		
		model.GetComponent<Animation>().CrossFade("Run");

	}
	
	void On_JoystickMoveEnd (MovingJoystick move)
	{
		model.GetComponent<Animation>().CrossFade("idle");
	}
	
	/*
	void On_ButtonPress (string buttonName)
	{
		if (buttonName=="Fire"){
			Instantiate( bullet, gun.transform.position, gun.rotation);
		}
	}*/
	
	void On_ButtonUp (string buttonName)
	{
		if (buttonName=="Exit"){
            SceneManager.LoadScene("StartMenu");	
		}
	}	
}
