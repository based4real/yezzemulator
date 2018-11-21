﻿using System;
using System.Data;

namespace Yezz.HabboHotel.Users.Authenticator
{
    public static class HabboFactory
    {
        public static Habbo GenerateHabbo(DataRow Row, DataRow UserInfo)
        {
            return new Habbo(Convert.ToInt32(Row["id"]), Convert.ToString(Row["username"]), Convert.ToInt32(Row["rank"]), Convert.ToString(Row["motto"]), Convert.ToString(Row["look"]),
                Convert.ToString(Row["gender"]), Convert.ToInt32(Row["credits"]), Convert.ToInt32(Row["activity_points"]),
                Convert.ToInt32(Row["home_room"]), YezzEnvironment.EnumToBool(Row["block_newfriends"].ToString()), Convert.ToInt32(Row["last_online"]),
                YezzEnvironment.EnumToBool(Row["hide_online"].ToString()), YezzEnvironment.EnumToBool(Row["hide_inroom"].ToString()),
                Convert.ToDouble(Row["account_created"]), Convert.ToInt32(Row["vip_points"]), Convert.ToString(Row["machine_id"]), Convert.ToString(Row["volume"]),
                YezzEnvironment.EnumToBool(Row["chat_preference"].ToString()), YezzEnvironment.EnumToBool(Row["focus_preference"].ToString()), YezzEnvironment.EnumToBool(Row["pets_muted"].ToString()), YezzEnvironment.EnumToBool(Row["bots_muted"].ToString()),
                YezzEnvironment.EnumToBool(Row["advertising_report_blocked"].ToString()), Convert.ToDouble(Row["last_change"].ToString()), Convert.ToInt32(Row["gotw_points"]), Convert.ToInt32(Row["puntos_eventos"]),
                YezzEnvironment.EnumToBool(Convert.ToString(Row["ignore_invites"])), Convert.ToDouble(Row["time_muted"]), Convert.ToDouble(UserInfo["trading_locked"]),
                YezzEnvironment.EnumToBool(Row["allow_gifts"].ToString()), Convert.ToInt32(Row["friend_bar_state"]), YezzEnvironment.EnumToBool(Row["disable_forced_effects"].ToString()),
                YezzEnvironment.EnumToBool(Row["allow_mimic"].ToString()), Convert.ToInt32(Row["rank_vip"]), Convert.ToByte(Row["guia"].ToString()), Convert.ToByte(Row["publi"].ToString()),
                Convert.ToByte(Row["builder"].ToString()), Convert.ToByte(Row["croupier"].ToString()), Row["talent_status"].ToString(), (Row["nux_user"].ToString() == "true"), (Row["nux_room"].ToString() == "true"), Convert.ToByte(Row["targeted_buy"]),
                Convert.ToString(Row["namecolor"]), Convert.ToString(Row["bubble_color"]), Convert.ToString(Row["tag"]), Convert.ToString(Row["tagcolor"]), Convert.ToInt32(Row["bubble_id"]), Convert.ToByte(Row["changename"]), Convert.ToString(Row["pin_client"]), Convert.ToInt32(Row["respetos"]));
        }
    }
}