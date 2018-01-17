using UnityEngine;
using System.Collections;

public class ReceiveMaterialChangeByHit : MonoBehaviour 
{
	public float changeDuration = 2f;

	public Material[] firstMaterials = null;

	public Material[] toAssignMaterials = null;



	public void DoChange ()
	{
		MeshRenderer mr = GetComponent<MeshRenderer> ();

		if (mr == null)
			return;

		mr.materials = toAssignMaterials;

		Invoke ("ResetChange", changeDuration);
	}

	public void ResetChange ()
	{
		MeshRenderer mr = GetComponent<MeshRenderer> ();

		if (mr == null)
			return;

		mr.materials = firstMaterials;


		FreeThis ();
	}

	void FreeThis ()
	{
		Destroy (this);
	}
}
