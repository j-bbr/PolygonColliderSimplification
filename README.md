# Polygon Collider Simplification
A couple of scripts for creating efficient non primitive 2d colliders in Unity(Edge Collider 2D and Polygon Collider 2D). 
You can adjust and see the results of the optimization in the Editor without entering playmode.

## Polygon Collider

![Polygon Collider Optimization](https://img.itch.zone/aW1hZ2UvNjgxOTcvMzEwMTcyLmdpZg==/original/iZAynj.gif)


## Edge Collider

![Edge Collider Optimization](https://img.itch.zone/aW1hZ2UvNjgxOTcvMzEwMTcxLmdpZg==/original/VjvR9G.gif)

## How it works

Both scripts remove points from the collision shape with a given reduction tolerance using the Ramer-Douglas-Peucker Algorithm. The Edge Collider Optimizer works in conjunction with a polygon collider by casting down a lot of rays from the upper edge of the bounds of the polygon and creating an edge out of the intersection points. 
That edge is then optimized with the given tolerance. After your satisfied with the result you can remove the polygon collider (and the optimization scripts as well, they are only thought for the Editor). 
Because the rays are cast down this doesn’t deal with overhanging sprite geometry, but still gives you a better starting point to manually adjust the edge than the default line with 2 points.

Link to the original c# implementation of the Ramer-Douglas-Peucker Algorithm (modified slightly for Unity):
http://www.codeproject.com/Articles/18936/A-Csharp-Implementation-of-Douglas-Peucker-Line-Ap

## License
MIT
