using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using Yezz.HabboHotel.Rooms.Music;
using Yezz.Communication.Packets.Outgoing.Rooms.Music;

namespace Yezz.Communication.Packets.Incoming.Rooms.Music
{
    class GetMusicDataEvent : IPacketEvent
    {
        public void Parse(HabboHotel.GameClients.GameClient Session, ClientPacket Packet)
        {
            int Songs = Packet.PopInt();

            List<SongData> SongData = new List<SongData>();

            for (int i = 0; i < Songs; i++)
            {
                int Pint = Packet.PopInt();
                SongData item = YezzEnvironment.GetGame().GetMusicManager().GetSong(Pint);

                if (item != null)
                    SongData.Add(item);
            }

            Session.SendMessage(new GetMusicDataComposer(SongData));
        }
    }
}
