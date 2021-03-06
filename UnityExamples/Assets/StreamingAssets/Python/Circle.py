from pysensationcore import *

import sensation_helpers as sh

pathInstance = createInstance("CirclePath", "CirclePathInstance")

circle = sh.createSensationFromPath("Circle",
                                    {
                                        ("radius", pathInstance.radius) : (0.02, 0, 0),
                                    },
                                    output = pathInstance.out,
                                    drawFrequency = 100,
                                    renderMode=sh.RenderMode.Loop
                                    )

setMetaData(circle, "Allow-Transform", True)
attachDocumentation(circle,
                    """
                    Outputs a circular path, with given radius, at the given offset
                    
                    A palm-tracked alternative to `CircleSensation` exists called `PalmTrackedCircle` which additionally
                    requires the `LeapDataSource` to supply further auto-mapped values.
                    """)

setMetaData(circle.radius, "Type", "Scalar")
attachDocumentation(circle.radius,
                    """
                    The radius of the circle
                    """)
