using UnityEngine;
using System.Collections;

public class RendererBallisticPath : MonoBehaviour
{

	private LineRenderer _lineRenderer;

	public LineRenderer lineRenderer {
		get {
			return _lineRenderer ?? (_lineRenderer = GetComponent<LineRenderer> ());	
		}
	}

	// velocity for points rendered in line
	[SerializeField]
	public float initialVelocity;
	private const float maxTime = 10f;

	private const float fixedTimeStep = 0.02f;
	// maxnumber of points to render
	private const int maxIndexCount = 50;

	public LayerMask layerMask = -1;

	void Awake ()
	{
		_lineRenderer = GetComponent<LineRenderer> ();
	}

	void Update ()
	{
		// Setting up all the trajectory points and render it to trajectory lines
		Vector3 vectorVelocity = transform.forward * initialVelocity;
		lineRenderer.SetVertexCount ((int)(maxTime / fixedTimeStep));
		Vector3 currentPosition = transform.position;

		int index = 0;

		for (float t = 0.0f; t < maxTime; t += fixedTimeStep) {

			lineRenderer.SetPosition (index, currentPosition);
			RaycastHit hit;

			if (Physics.Raycast (currentPosition, vectorVelocity, out hit, vectorVelocity.magnitude * fixedTimeStep, layerMask)) {
				lineRenderer.SetVertexCount (index + 2);

				lineRenderer.SetPosition (index + 1, hit.point);
				break;
			}

			// Break trajectory line for maximum number of vertices 
			if (index > maxIndexCount) {
				lineRenderer.SetVertexCount (index);
				break;
			}

			currentPosition += vectorVelocity * fixedTimeStep;
			vectorVelocity += Physics.gravity * fixedTimeStep;
			index++;
		}
	}
}
