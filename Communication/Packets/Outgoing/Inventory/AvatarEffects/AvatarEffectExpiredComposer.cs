using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

using Yezz.HabboHotel.Users.Effects;

namespace Yezz.Communication.Packets.Outgoing.Inventory.AvatarEffects
{
    class AvatarEffectExpiredComposer : ServerPacket
    {
        public AvatarEffectExpiredComposer(AvatarEffect Effect)
            : base(ServerPacketHeader.AvatarEffectExpiredMessageComposer)
        {
            base.WriteInteger(Effect.SpriteId);
        }
    }
}
