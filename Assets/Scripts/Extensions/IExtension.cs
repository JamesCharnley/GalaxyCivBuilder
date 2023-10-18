using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IExtension
{
    public void Setup(GameObject _owner);
    public void Destroy(GameObject _owner);
}
