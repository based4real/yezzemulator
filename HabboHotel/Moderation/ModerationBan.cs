﻿using System;

using Yezz.HabboHotel.Moderation;

namespace Yezz.HabboHotel.Moderation
{

    public class ModerationBan
    {
        public string Value;
        public double Expire;
        public string Reason;
        public ModerationBanType Type;

        public ModerationBan(ModerationBanType Type, string Value, string Reason, Double Expire)
        {
            this.Type = Type;
            this.Value = Value;
            this.Reason = Reason;
            this.Expire = Expire;
        }

        public bool Expired
        {
            get
            {
                if (YezzEnvironment.GetUnixTimestamp() >= Expire)
                    return true;
                return false;
            }
        }
    }
}