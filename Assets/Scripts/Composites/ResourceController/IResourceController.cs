using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IResourceController
{
    public ResourceController ResourceControlFunctions { get; set; }
    public List<ResourceInOut> Inputs { get; set; }
    public List<ResourceInOut> Outputs { get; set; }
}
