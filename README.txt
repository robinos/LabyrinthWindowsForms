The LabyrinthProgram runs a WindowsForms application where the user can enter
row and column and a random maze is created that the program can then be
instructed to solve for the shortest path.

This is a C# adaptation of a school project written in Java. The Java
versions of MazeLogic and ExtendedGraph were written by Robin Osborne and
Emma Dirnberger. The original DisjointSets, Graph, GridPoint and Pair were
part of example Java code provided by Mark Allen Weiss and Uno Holmer.

IPriorityQueue and PriorityQueue are untested implementations of C# example
code by Leon van Bokhorst at
http://www.remondo.net/generic-priority-queue-example-csharp/ which was chosen
for similarity to the previous linked list implementation.

All C# adaptations were done by Robin Osborne. The LineDrawnEventArgs,
PathSquareFilledEventArgs and LabyrinthGameGridForm are unique to the C#
version and written by Robin Osborne.

class LabyrinthProgram
- contains the Main method that initialises observer connections and starts
the WindowsForms application

class MazeLogic
- the maze logic that utilizes disjoint sets for building the maze and an
unweighted graph algorith for determining the shortest path through the maze

partial class LabyrinthGameGridForm
- observes MazeLogic events and changes the graphical view accordingly for
maze creation and solving

class LineDrawnEventArgs
- extension of EventArgs for the SquareWallRemoved event in MazeLogic

class PathSquareFilledEventArgs
- extension of EventArgs for the SquareWallRemoved event in MazeLogic

class Pair
- a generic class for a pair of objects

class DisjointSets
- a DisjointSets implementation

class Graph, class Vertex, class Edge, class SearchPath
- creates a Graph for finding the shortest path using a single-source
unweighted shortest-path algorithm (used in this project) or using
Dijkstra's algorithm (not used currently in this project)

class ExtendedGraph
- An extension of Graph for returning the path to another class for use
(MazeLogic uses this to determine the shortest path and send data onwards
to LabyrinthGameGridForm for display)

class GridPoint
- A point represented by column and row that can determine a point in the
directions UP, LEFT, RIGHT, or DOWN from it and return information for a
move to that other point. The Java version was simply called Point but that
conflicts with a C# graphical class.

interface IPriorityQueue
- an interface for a priority queue

class PriorityQueue
- an implementation of a priority queue using linked lists
