using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.Common.Entities;
using Vintagestory.GameContent;

namespace LocomotiveCore;

public class MinecartEntity : Entity, ISeatInstSupplier
{
    public IMountableSeat CreateSeat(IMountable mountable, string seatId, SeatConfig config)
    {
        return new EntityMinecartSeat(mountable, seatId, config);
    }
    
    public override bool ApplyGravity
    {
        get { return true; }
    }
    
}