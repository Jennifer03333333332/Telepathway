from pysensationcore import *
import sensation_helpers as sh

prefix = "point"
polylinePath = createInstance("PolylinePath", "PolylinePathInstance")
points = sh.createList(6)
connect(points["output"], polylinePath.points)

fingerPatch = sh.createSensationFromPath("Finger2",
                        {
                            ("indexFinger_distal_position", points["inputs"][0]) : (0,0,0),
                            ("indexFinger_intermediate_position", points["inputs"][1]) : (0,0,0),
                            ("indexFinger_proximal_position", points["inputs"][2]) : (0,0,0),
                            ("indexFinger_proximal_position", points["inputs"][3]) : (0,0,0),
                            ("indexFinger_distal_position", points["inputs"][4]) : (0,0,0),
                            ("indexFinger_distal_position", points["inputs"][5]) : (0,0,0)
                        },
                        output = polylinePath.out,
                        drawFrequency = 70,
                        definedInVirtualSpace = True,
                        renderMode = sh.RenderMode.Bounce
                        )
                        
setMetaData(fingerPatch.indexFinger_distal_position, "Input-Visibility", False)                        
setMetaData(fingerPatch.indexFinger_intermediate_position, "Input-Visibility", False)
setMetaData(fingerPatch.indexFinger_proximal_position, "Input-Visibility", False)
setMetaData(fingerPatch.indexFinger_proximal_position, "Input-Visibility", False)
setMetaData(fingerPatch.indexFinger_distal_position, "Input-Visibility", False)
setMetaData(fingerPatch.indexFinger_distal_position, "Input-Visibility", False)
                        