using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransportController
{
    int TotalBasicTransportShips = 5;
    int TotalDomesticCargoShips = 0;

    public void AddDomesticCargoShips(int _number)
    {
        TotalDomesticCargoShips += _number;
    }
    public void RemoveDomesticCargoShips(int _number)
    {
        TotalDomesticCargoShips -= _number;
    }

}
