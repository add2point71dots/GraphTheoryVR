# Virtual Reality Graph Maker

##### Capstone Project for Ada Developer's Academy

### What is it?

A virtual reality tool for making and manipulating [graph theory](https://en.wikipedia.org/wiki/Graph_theory) type graphs (nodes and connections between them). You can create nodes, connect them, move them, destroy them, and color them.

### Why?

Graphs can sometimes be difficult to conceptualize in two dimensions. For example, the [complete graph](https://en.wikipedia.org/wiki/Complete_graph) [K<sub>5</sub>](https://commons.wikimedia.org/wiki/File:Complete_graph_K5.svg) cannot be drawn on paper without overlapping edges, but in virtual reality, it can. Also, it's fun to play with shapes in VR!

### How can I use this?

You'll need a few things:
- A PC powerful enough to run a VR setup
- [Vive headset and controllers](https://www.vive.com/us/)
- [Steam](http://store.steampowered.com/) (and a Steam account)
- [SteamVR](https://steamcommunity.com/steamvr)

If you just want to run the program:
- From the [releases tab](https://github.com/add2point71dots/GraphTheoryVR/releases), download the `GraphMaker.zip` folder and unzip it.
- Make sure you have Steam and SteamVR running.
- Open the `GraphMaker.exe` file.
- Start graphing!

If you want to work with the code, in addition you'll need:
- [Unity](https://store.unity.com/) (free version works great!)

You can then run Steam and SteamVR and open up the project in Unity. Hit play to start making graphs, or explore the project in the Unity editor.

### How do I graph things?

- Instructions are displayed by default. You can toggle them off and on by pressing the menu button on your right controller.
- The menu button on the left controller toggles a mode menu.
- Switch modes by selecting one with your right laser pointer and hitting the trigger button.

Graph Mode:
- Right controller trigger creates new nodes.
- Hold the right controller pad button and drag between nodes to make edges.
- Move a node by holding the right controller grab button on the node.

Destroy Mode:
- Right controller trigger destroys whichever node or edge you're pointing at.

Color Mode:
- Select colors (displayed on left controller) via the right controller trigger.
- Color a node by pointing at it and hitting trigger on the right controller.

---
### Ada Capstone Requirements
- [Original Product Plan](https://gist.github.com/add2point71dots/8041750422cee18966585b1cf2d8b787)
- [Trello Board](https://trello.com/b/m33N9Qq7/graph-theory-vr)
