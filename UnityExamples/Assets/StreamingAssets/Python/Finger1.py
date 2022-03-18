from pysensationcore import *
import sensation_helpers as sh

prefix = "point"
polylinePath = createInstance("PolylinePath", "PolylinePathInstance")
points = sh.createList(6)
connect(points["output"], polylinePath.points)

fingerPatch = sh.createSensationFromPath("Finger1",
                        {
                            ("thumb_distal_position", points["inputs"][0]) : (0,0,0),
                            ("thumb_intermediate_position", points["inputs"][1]) : (0,0,0),
                            ("thumb_metacarpal_position", points["inputs"][2]) : (0,0,0),
                            ("thumb_proximal_position", points["inputs"][3]) : (0,0,0),
                            ("thumb_distal_position", points["inputs"][4]) : (0,0,0),
                            ("thumb_distal_position", points["inputs"][5]) : (0,0,0)
                        },
                        output = polylinePath.out,
                        drawFrequency = 70,
                        definedInVirtualSpace = True,
                        renderMode = sh.RenderMode.Bounce
                        )
                        
setMetaData(fingerPatch.thumb_distal_position, "Input-Visibility", False)                        
setMetaData(fingerPatch.thumb_intermediate_position, "Input-Visibility", False)
setMetaData(fingerPatch.thumb_metacarpal_position, "Input-Visibility", False)
setMetaData(fingerPatch.thumb_proximal_position, "Input-Visibility", False)
setMetaData(fingerPatch.thumb_distal_position, "Input-Visibility", False)
setMetaData(fingerPatch.thumb_distal_position, "Input-Visibility", False)
                        