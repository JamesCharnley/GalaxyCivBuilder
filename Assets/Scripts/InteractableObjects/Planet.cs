using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Buildable), typeof(ResourceController))]
public class Planet : HabitableObject, IResourceController, IBuildable
{

    // IBuildable interface implementation
    public Buildable BuildableFunctions { get; set; }
    public int MaxSlots { get; set; }
    public int CurrentSlots { get; set; }
    public List<EFacility> CompatibleFacilities { get; set; }
    [SerializeField] private List<EFacility> _CompatibleFacilities;

    //IResourceController interface implementation
    public ResourceController ResourceControlFunctions { get; set; }
    public List<ResourceInOut> Inputs { get; set; }
    [SerializeField] private List<ResourceInOut> _Inputs;
    public List<ResourceInOut> Outputs { get; set; }
    [SerializeField] private List<ResourceInOut> _Outputs;

    private void Start()
    {
        // IBuildable init
        BuildableFunctions = GetComponent<Buildable>();
        BuildableFunctions.OwnerInterface = this;

        // IResourceController init
        ResourceControlFunctions = GetComponent<ResourceController>();
        ResourceControlFunctions.OwnerInterface = this;
        Inputs = _Inputs;
        Outputs = _Outputs;
    }


}
