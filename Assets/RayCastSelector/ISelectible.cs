using UnityEngine;
using System.Collections;

public interface ISelectible  {

	void TouchHold(Vector3 hitPos);
	void TouchUp();
	void TouchDown();
	void OutOfFocus();
	void Unselected();
	void Selected();
}
 