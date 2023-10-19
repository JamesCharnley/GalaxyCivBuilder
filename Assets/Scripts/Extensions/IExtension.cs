using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IExtension
{
    public void Setup(IBuildable _owner);
    public void Destroy(IBuildable _owner);
}
