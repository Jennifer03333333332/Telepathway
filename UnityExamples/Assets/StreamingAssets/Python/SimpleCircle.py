from pysensationcore import *
 
simpleCircleBlock = defineBlock("SimpleCircle")
defineInputs(simpleCircleBlock, "t")
defineOutputs(simpleCircleBlock, "out")
 
circlePathInstance = createInstance("CirclePath", "circlePathInstance")
renderPathInstance = createInstance("RenderPath", "renderPathInstance")
 
connect(simpleCircleBlock.t, renderPathInstance.t)
connect(Constant((0.02, 0, 0)), circlePathInstance.radius)
connect(Constant((125, 0, 0)), renderPathInstance.drawFrequency)
 
connect(circlePathInstance.out, renderPathInstance.path)
connect(renderPathInstance.out, simpleCircleBlock.out)
