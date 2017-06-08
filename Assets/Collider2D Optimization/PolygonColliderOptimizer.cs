using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Collider2DOptimization{
	/// <summary>
	/// Polygon collider optimizer. Removes points from the collider polygon with 
	/// the given reduction Tolerance
	/// </summary>
	[AddComponentMenu("2D Collider Optimization/ Polygon Collider Optimizer")]
	[RequireComponent(typeof(PolygonCollider2D))]
	public class PolygonColliderOptimizer : MonoBehaviour {
		public double tolerance = 0;
		private PolygonCollider2D coll;
		private List<List<Vector2>> originalPaths = new List<List<Vector2>>();

		void OnValidate()
		{
			if(coll == null)
			{
				//When first getting a reference to the collider save the paths
				//so that the optimization is redoable (by performing it on the original path
				//every time)
				coll = GetComponent<PolygonCollider2D>();
				for(int i = 0; i < coll.pathCount; i++)
				{
					List<Vector2> path = new List<Vector2>(coll.GetPath(i));
					originalPaths.Add(path);
				}
			}
			//Reset the original paths
			if(tolerance <= 0)
			{
				for(int i = 0; i < originalPaths.Count; i++)
				{
					List<Vector2> path = originalPaths[i];
					coll.SetPath(i, path.ToArray());
				}
				return;
			}
			for(int i = 0; i < originalPaths.Count; i++)
			{
				List<Vector2> path = originalPaths[i];
				path = ShapeOptimizationHelper.DouglasPeuckerReduction(path, tolerance);
				coll.SetPath(i, path.ToArray());

			}
		}
	}
}
