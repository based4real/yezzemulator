﻿using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace Yezz.Communication.Packets.Outgoing.Rooms.Settings
{
    class HideUserOnPlaying : ServerPacket
    {
        public HideUserOnPlaying(bool state)
            : base(ServerPacketHeader.HideUserOnPlayingComposer)
        {
            base.WriteBoolean(state);
        }
    }
}
