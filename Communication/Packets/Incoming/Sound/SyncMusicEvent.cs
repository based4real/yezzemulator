﻿using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using Yezz.Communication.Packets.Outgoing.Rooms.Music;
using Yezz.HabboHotel.Rooms;

namespace Yezz.Communication.Packets.Incoming.Rooms.Music
{
    class SyncMusicEvent : IPacketEvent
    {
        public void Parse(HabboHotel.GameClients.GameClient Session, ClientPacket Packet)
        {
            Room Instance = Session.GetHabbo().CurrentRoom;

            if (Instance == null)
            {
                return;
            }

            if (Instance.GetRoomMusicManager().IsPlaying)
            {
                Session.SendMessage(new SyncMusicComposer(Instance.GetRoomMusicManager().CurrentSong.SongData.Id, Instance.GetRoomMusicManager().SongQueuePosition, Instance.GetRoomMusicManager().SongSyncTimestamp));
            }
        }
    }
}
