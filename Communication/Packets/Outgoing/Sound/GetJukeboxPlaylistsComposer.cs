﻿using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using Yezz.HabboHotel.Rooms.Music;
using Yezz.Communication.Packets.Outgoing;

namespace Yezz.Communication.Packets.Outgoing.Rooms.Music
{
    class GetJukeboxPlaylistsComposer : ServerPacket
    {
        public GetJukeboxPlaylistsComposer(int PlaylistCapacity, List<SongInstance> Playlist)
            : base(ServerPacketHeader.GetJukeboxPlaylistsMessageComposer)
        {
            base.WriteInteger(PlaylistCapacity);
            base.WriteInteger(Playlist.Count);

            foreach (SongInstance Song in Playlist)
            {
                base.WriteInteger(Song.DiskItem.itemID);
                base.WriteInteger(Song.SongData.Id);
            }
        }
    }
}
