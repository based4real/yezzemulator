using System;
using System.Data;
using Yezz.HabboHotel.Items;
using Yezz.Communication.Packets.Outgoing.Inventory.Purse;
using Yezz.Database.Interfaces;
using Yezz.Communication.Packets.Outgoing.Rooms.Notifications;

namespace Yezz.HabboHotel.Rooms.Chat.Commands.User
{
    class ConvertDiamondsCommand : IChatCommand
    {
        public string PermissionRequired
        {
            get { return "user_normal"; }
        }

        public string Parameters
        {
            get { return ""; }
        }

        public string Description
        {
            get { return "Lleva tus diamantes de inventario a el Monedero."; }
        }

        public void Execute(GameClients.GameClient Session, Rooms.Room Room, string[] Params)
        {
                int TotalValue = 0;

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
                        Session.SendWhisper("Actualmente no tiene artículos en su inventario!", 34);
                        return;
                    }

                    foreach (DataRow Row in Table.Rows)
                    {
                        Item Item = Session.GetHabbo().GetInventoryComponent().GetItem(Convert.ToInt32(Row[0]));
                        if (Item == null)
                            continue;

                        if (!Item.GetBaseItem().ItemName.StartsWith("DIAMND_") && !Item.GetBaseItem().ItemName.StartsWith("DF_") && !Item.GetBaseItem().ItemName.StartsWith("DIA_") && !Item.GetBaseItem().ItemName.StartsWith("DI_"))
                            continue;

                        if (Item.RoomId > 0)
                            continue;

                        string[] Split = Item.GetBaseItem().ItemName.Split('_');
                        int Value = int.Parse(Split[1]);

                        using (IQueryAdapter dbClient = YezzEnvironment.GetDatabaseManager().GetQueryReactor())
                        {
                            dbClient.RunQuery("DELETE FROM `items` WHERE `id` = '" + Item.Id + "' LIMIT 1");
                        }

                        Session.GetHabbo().GetInventoryComponent().RemoveItem(Item.Id);

                        TotalValue += Value;

                        if (Value > 0)
                        {
                            Session.GetHabbo().Diamonds += Value;
                            Session.SendMessage(new HabboActivityPointNotificationComposer(Session.GetHabbo().Diamonds, Value, 5));
                        }
                    }

                    if (TotalValue > 0)
                        Session.SendNotification("¡Todos los diamantes se han convertido con éxito!\r\r(Valor total: " + TotalValue + " diamantes!");
                    else
                        Session.SendNotification("¡Parece que no tienes ningún artículo intercambiable!");
                }
                catch
                {
                    Session.SendNotification("Vaya, se produjo un error al convertir tus diamantes.");
                }
            }
        }
    }