using System.Collections.Generic;
using Bloops.StateMachine;
using UnityEngine;

namespace Bloops.Utilities
{
	public class CameraUtility
	{
		public static Rect RectFromTransforms(IEnumerable<Transform> elementsToFit)
		{
			float minX = Mathf.Infinity;
			float maxX = Mathf.NegativeInfinity;
			float minY = Mathf.Infinity;
			float maxY = Mathf.NegativeInfinity;

			foreach (var t in elementsToFit)
			{
				var position = t.position;
				minX = Mathf.Min(minX, position.x);
				maxX = Mathf.Max(maxX, position.x);
				minY = Mathf.Min(minY, position.y);
				maxY = Mathf.Max(maxY, position.y);
			}

			float width = maxX - minX;
			float height = maxY - minY;
			
			Rect r = new Rect(minX,minY,width,height);
			return r;
		}

		public static void SetCameraToRect(Rect r)
		{
			var cam = Camera.main;
			cam.transform.position = new Vector3(r.center.x, r.center.y, -10);
			cam.orthographicSize = Mathf.Max(r.width/2, r.height/2);
		}
	}
}