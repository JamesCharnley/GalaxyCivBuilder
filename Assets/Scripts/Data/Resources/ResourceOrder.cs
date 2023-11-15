using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceOrder
{
    IResourceController Owner;
    Resource Order;
    ResourceManager ResourceManager;
    public ResourceOrder(IResourceController _owner, Resource _order, ResourceManager _resourceManager)
    {
        Owner = _owner;
        Order = _order;
        ResourceManager = _resourceManager;
    }

    public Resource GetOrder()
    {
        return Order;
    }
    public IResourceController GetOwner()
    {
        return Owner;
    }

    public void UpdateOrder(int _amount)
    {
        if(Order.Amount + _amount <= 0)
        {
            TerminateOrder();
            return;
        }
        Order.Amount += _amount;
    }

    void TerminateOrder()
    {
        ResourceManager.AddResourceOrderTermination(this);
    }
}
