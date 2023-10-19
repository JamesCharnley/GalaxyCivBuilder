using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DomesticCargoHub : MonoBehaviour, IExtension
{

    public int ShipCount = 4;
    public void Setup(IBuildable _owner)
    {
        ITransportController tc = _owner as ITransportController;
        if(tc != null)
        {
            tc.TransportControl.AddDomesticCargoShips(ShipCount);
        }
    }

    public void Destroy(IBuildable _owner)
    {
        ITransportController tc = _owner as ITransportController;
        if (tc != null)
        {
            tc.TransportControl.RemoveDomesticCargoShips(ShipCount);
        }
       
    }

}
