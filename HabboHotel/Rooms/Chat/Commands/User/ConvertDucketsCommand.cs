using System;
using System.Data;
using Yezz.HabboHotel.Items;
using Yezz.Communication.Packets.Outgoing.Inventory.Purse;
using Yezz.Database.Interfaces;
using Yezz.Communication.Packets.Outgoing.Rooms.Notifications;

namespace Yezz.HabboHotel.Rooms.Chat.Commands.User
{
    class ConvertDucketsCommand : IChatCommand
    {
        public string PermissionRequired => "user_normal";
        public string Parameters => "";
        public string Description => "Convertir sus muebles canjeables por duckets reales.";

        public void Execute(GameClients.GameClient Session, Rooms.Room Room, string[] Params)
        {
                int TotalDuckets = 0;

                try
                {
                    DataTable Table = null;
                    using (IQueryAdapter dbClient = YezzEnvironment.GetDatabaseManager().GetQueryReactor())
                    {
                        dbClient.SetQuery("SELECT `id` FROM `items` WHERE `user_id` = '" + Session.GetHabbo().Id + "' AND (`room_id`=  '0' OR `room_id` = '')");
                        Table = dbClient.getTable();
                    }

                    if (Table == null)
                    {
                        Session.SendWhisper("¡No posees ninguna moneda en tu inventario!", 34);
                        return;
                    }

                    foreach (DataRow Row in Table.Rows)
                    {
                        Item Item = Session.GetHabbo().GetInventoryComponent().GetItem(Convert.ToInt32(Row[0]));
                        if (Item == null)
                            continue;

                        if (!Item.GetBaseItem().ItemName.StartsWith("DU_") && !Item.GetBaseItem().ItemName.StartsWith("DUC_"))
                            continue;

                        if (Item.RoomId > 0)
                            continue;

                        string[] Split = Item.GetBaseItem().ItemName.Split('_');
                        int Value = int.Parse(Split[1]);

                        using (IQueryAdapter dbClient = YezzEnvironment.GetDatabaseManager().GetQueryReactor())
                        {
                            dbClient.runFastQuery("DELETE FROM `items` WHERE `id` = '" + Item.Id + "' LIMIT 1");
                        }

                        Session.GetHabbo().GetInventoryComponent().RemoveItem(Item.Id);

                        TotalDuckets += Value;

                        if (Value > 0)
                        {
                            Session.GetHabbo().Duckets += Value;
                            Session.SendMessage(new ActivityPointsComposer(Session.GetHabbo().Duckets, Session.GetHabbo().Diamonds, Session.GetHabbo().GOTWPoints));
                        }
                    }

                    if (TotalDuckets > 0)
                        Session.SendWhisper("¡Has canjeado correctamente " + TotalDuckets + " duckets de tu inventario!", 34);
                    else
                        Session.SendWhisper("¡Lo sentimos, ah ocurrido un error!", 34);
                }
                catch
                {
                    Session.SendNotification("¡Lo sentimos, ha ocurrido un error!");
                }
            }
        }
    }