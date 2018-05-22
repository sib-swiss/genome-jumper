Thanks for downloading Team Robison's Automated Scene Builder.  We hope you love it!

Usage:

1. Create a class that implements the ISceneObject interface. ( See the included SphereExampleSceneObject as an example ).
2. Create the object / prefab that is referenced by the resourcePath in your ISceneObject.
3. Create a file with a .json extension.  

  The format inside of this file should be:

	{
  		"sceneObjects":[
    		{"machineName":"SphereExample","x":4,"y":0,"z":0},
			{"machineName":"SphereExample","x":1,"y":1,"z":0,"isAbsolute":1}
  		]
	}

	* machineName matches up to the ISceneObjects's machineName property.
	* x, y, and z are the x,y,z to position the game object at. 
  	  These can either be an units relative to the GetPosition method of your concrete ISceneObject 
  	  or they can be an absolute world positions.
	* isAbsolute is an optional value that specifies if the x,y,z are to be absolute or processed by GetPosition 
  	  in your concrete ISceneObject classes.
  	  
  	*** It is worth noting that while z is required, it is ignored for 2D games.  For 2D games just enter 0 or any valid z value ***
  	*** and it will be ignored ***
  	
Now that your custom class and your json file exists, you're ready to import.

4. Click on the Tools menu item in Unity and then the Automated Scene Builder menu item.  

  You should see the following options:

	* Read Template File: This will read in the template file.
	* Build: Builds from the loaded template file. ( This will also try to load the template file if it hasn't been loaded yet. )
	* Reset: Removes all of the registered objects by their tags specified in ISceneObject implementations.
	* Reset and Build: Calls reset, and then calls build.
	* Set Origin: This will allow you to select a gameobject from the current scene that you would like to be the origin
	  for relative positioning.  This is ignored for absolute positioning.
	  
5. Finally, click on Reset and Build, select your json file, and watch the magic happen!


If you run into any problems, don't hesitate to check out the tutorials 
and blog or contact us on http://www.teamrobison.com.

Good luck and happy automating. 


