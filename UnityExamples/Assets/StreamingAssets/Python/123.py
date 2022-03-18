from pysensationcore import *
import sensation_helpers as sh

prefix = "point"
polylinePath = createInstance("PolylinePath", "PolylinePathInstance")
points = sh.createList(6)
connect(points["output"], polylinePath.points)

fingerPatch = sh.createSensationFromPath("123",
                        {
                            ("indexFinger_distal_position", points["inputs"][0]) : (0.01015643,0.1701314,0.0758999),
                            ("indexFinger_proximal_position", points["inputs"][1]) : (0.01361755,0.1882957,0.03470681),
                            ("palm_position", points["inputs"][2]) : (0.03522119,0.176564,-0.005705185),
                            ("ringFinger_proximal_position", points["inputs"][3]) : (0.05804475,0.1838068,0.02851498),
                            ("ringFinger_distal_position", points["inputs"][4]) : (0.06099714,0.1743391,0.07716589),
                            ("indexFinger_distal_position", points["inputs"][5]) : (0.01015643,0.1701314,0.0758999)
                        },
                        output = polylinePath.out,
                        drawFrequency = 70,
                        definedInVirtualSpace = True,
                        renderMode = sh.RenderMode.Bounce
                        )
                        
setMetaData(fingerPatch.indexFinger_distal_position, "Input-Visibility", False)                        
setMetaData(fingerPatch.indexFinger_proximal_position, "Input-Visibility", False)
setMetaData(fingerPatch.palm_position, "Input-Visibility", False)
setMetaData(fingerPatch.ringFinger_proximal_position, "Input-Visibility", False)
setMetaData(fingerPatch.ringFinger_distal_position, "Input-Visibility", False)
setMetaData(fingerPatch.indexFinger_distal_position, "Input-Visibility", False)
                        