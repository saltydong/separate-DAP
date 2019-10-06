using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToAction : Action {

	public Controllor controllor;

	public void setObj () {
		controllor = (FirstControllor)Director.getInstance ().currentSceneController;
	}
	
	public void moveToSide() {
		if (controllor.boatState == 2) {
			controllor.boat.transform.position = Vector3.MoveTowards (controllor.boat.transform.position, controllor.boatEnd, controllor.speed * Time.fixedDeltaTime);
			if (controllor.boat.transform.position == controllor.boatEnd) {
				controllor.boatState = 1;
				controllor.result ();
			}

		} else if (controllor.boatState == 3) {
			controllor.boat.transform.position = Vector3.MoveTowards (controllor.boat.transform.position, controllor.boatStart, controllor.speed * Time.fixedDeltaTime);
			if (controllor.boat.transform.position == controllor.boatStart) {
				controllor.boatState = 0;
				controllor.result ();
			}
		} 
	}
}
