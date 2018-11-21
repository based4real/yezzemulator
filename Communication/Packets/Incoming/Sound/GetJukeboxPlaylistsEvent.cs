using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using Yezz.HabboHotel.Rooms;
using Yezz.HabboHotel.Rooms.Music;
using Yezz.Communication.Packets.Outgoing.Rooms.Music;

namespace Yezz.Communication.Packets.Incoming.Rooms.Music
{
    class GetJukeboxPlaylistsEvent : IPacketEvent
    {
        public void Parse(HabboHotel.GameClients.GameClient Session, ClientPacket Packet)
        {
            if (Session != null)
            {
                Room Instance = Session.GetHabbo().CurrentRoom;

                if (Instance == null || !Instance.CheckRights(Session, true))
                    return;

                Session.SendMessage(new GetJukeboxPlaylistsComposer(MusicManager.PlaylistCapacity, Instance.GetRoomMusicManager().Playlist.Values.ToList()));
            }
        }
    }
}
