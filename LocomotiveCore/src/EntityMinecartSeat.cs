using System.Collections.Generic;
using Vintagestory.API.Common;
using Vintagestory.API.Common.Entities;
using Vintagestory.GameContent;

namespace LocomotiveCore;

public class EntityMinecartSeat : EntityRideableSeat
{
    Dictionary<string, string> animations => Entity.Properties.Attributes["mountAnimations"].AsObject<Dictionary<string, string>>();
    public string actionAnim;
        
    public override AnimationMetaData SuggestedAnimation
    {
        get
        {
            if (actionAnim == null) return null;

            if (Passenger?.Properties?.Client.AnimationsByMetaCode?.TryGetValue(actionAnim, out var ameta) == true)
            {
                return ameta;
            }

            return null;
        }
    }
    
    public EntityMinecartSeat(IMountable mountablesupplier, string seatId, SeatConfig config) : base(mountablesupplier, seatId, config)
    {
        RideableClassName = "minecart";
    }
    
    public override void DidMount(EntityAgent entityAgent)
    {
        base.DidMount(entityAgent);

        entityAgent.AnimManager.StartAnimation(config.Animation ?? animations["idle"]);
    }

    public override void DidUnmount(EntityAgent entityAgent)
    {
        if (Passenger != null)
        {
            Passenger.AnimManager?.StopAnimation(animations["ready"]);
            Passenger.AnimManager?.StopAnimation(animations["forwards"]);
            Passenger.AnimManager?.StopAnimation(animations["backwards"]);
            Passenger.AnimManager?.StopAnimation(animations["idle"]);
            Passenger.AnimManager?.StopAnimation(config.Animation);
            Passenger.Pos.Roll = 0;
        }

        base.DidUnmount(entityAgent);
    }
}