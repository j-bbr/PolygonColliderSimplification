using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Collider2DOptimization{
	/// <summary>
	/// Edge collider optimizer. Creates an edge collider by casting rays from the upper edge of the bounding box
	/// into the edgeNormalOpposite direction (by default downwards). The resulting points are then reduced with 
	/// the given tolerance
	/// </summary>
	[RequireComponent(typeof(PolygonCollider2D), typeof(EdgeCollider2D))]
	public class EdgeColliderOptimizer : MonoBehaviour {
		public Vector2 edgeNormalOpposite = Vector2.down;
		public int rayBudget = 1000;
		public double tolerance = 0;
		private EdgeCollider2D coll;
		private PolygonCollider2D polygon;

		void OnValidate()
		{
			coll =  coll ?? GetComponent<EdgeCollider2D>();
			polygon =  polygon ?? GetComponent<PolygonCollider2D>();

			List<Vector2> path = new List<Vector2>();
			Vector2 upperRight = polygon.bounds.max;
			Vector2 upperLeft = polygon.bounds.min;
			upperLeft.y = upperRight.y;
			for(int i = 0; i < rayBudget; i++)
			{
				float t = (float)i/(float)rayBudget;
				//interpolate along the upper edge of the collider bounds
				Vector2 rayOrigin = Vector2.Lerp(upperLeft, upperRight, t);
				RaycastHit2D[] hits = Physics2D.RaycastAll(rayOrigin, edgeNormalOpposite, polygon.bounds.size.y);

				for(int j = 0; j < hits.Length; j++)
				{
					RaycastHit2D hit = hits[j];
					if(hit.collider == polygon)
					{
						Vector2 localHitPoint = transform.InverseTransformPoint(hit.point);
						path.Add(localHitPoint);
						break;
					}
				}
			}
			if(tolerance > 0) path = ShapeOptimizationHelper.DouglasPeuckerReduction(path, tolerance);
			coll.points = path.ToArray();
		}
	}
}
