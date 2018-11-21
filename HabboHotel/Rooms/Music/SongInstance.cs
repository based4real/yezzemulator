using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

using Yezz.HabboHotel.GameClients;
using Yezz.HabboHotel.Rooms;
using Yezz.HabboHotel.Users;
using Yezz.Communication.Packets.Incoming;
using System.Collections.Concurrent;

using Yezz.Database.Interfaces;
using log4net;
using Yezz.HabboHotel.Items;

namespace Yezz.HabboHotel.Rooms.Music
{
    public class SongInstance
    {
        private readonly SongItem mDiskItem;
        private readonly SongData mSongData;

        public SongInstance(SongItem Item, SongData SongData)
        {
            mDiskItem = Item;
            mSongData = SongData;
        }

        public SongData SongData
        {
            get { return mSongData; }
        }

        public SongItem DiskItem
        {
            get { return mDiskItem; }
        }
    }
}