using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

using Yezz.Communication.Packets.Outgoing.Moderation;
using Yezz.HabboHotel.Items;
using Yezz.Communication.Packets.Outgoing.Notifications;
using Yezz.Database.Interfaces;
using System.Data;

namespace Yezz.HabboHotel.Rooms.Chat.Commands.Moderator
{
    class UpdateFurniture : IChatCommand
    {
        public string PermissionRequired
        {
            get { return "user_16"; }
        }


        public string Parameters
        {
            get { return "(tipo) (cantidad)"; }
        }

        public string Description
        {
            get { return "Envia una alerta a todo el hotel."; }
        }
        public void Execute(GameClients.GameClient Session, Rooms.Room Room, string[] Params)
        {
            RoomUser RUser = Room.GetRoomUserManager().GetRoomUserByHabbo(Session.GetHabbo().Id);
            List<Item> Items = Room.GetGameMap().GetRoomItemForSquare(RUser.X, RUser.Y);
            if (Params.Length == 1 || Params[1] == "utilidad")
            {
                StringBuilder Lista = new StringBuilder();
                Lista.Append("Lista de usos: (Nota: Edita TODOS los items abajo de ti): \r\r");
                Lista.Append("1) :item width numero (Edita la anchura del Item) \r");
                Lista.Append("2) :item length numero (Edita la longitud del Item) \r");
                Lista.Append("3) :item height numero (Edita la altura del Item) \r");
                Lista.Append("4) :item cansit si/no (Permite / No Permite sentarse sobre el Item) \r");
                Lista.Append("5) :item canwalk si/no (Permite / No Permite caminar sobre el Item) \r");
                Lista.Append("6) :item canstack si/no (Permite / No Permite apilar sobre el Item) \r");
                Lista.Append("7) :item mercadillo si/no (Permite / No Permite la venta del Item en mercadillo) \r");
                Lista.Append("8) :item interaction nombre (Asigna una interacción al Item) \r");
                Lista.Append("9) :item interactioncount numero (Asigna la cantidad de interacciones del Item) \r\r");
                Lista.Append("Nota: Para que se actualice el Item debes recogerlo y ponerlo en la sala nuevamente o refrescar la sala.");
                Session.SendMessage(new MOTDNotificationComposer(Lista.ToString()));
                return;
            }
            String Type = Params[1].ToLower();
            int numeroint = 0, FurnitureID = 0;
            double numerodouble = 0;
            DataRow Item = null;
            String opcion = "";
            switch (Type)
            {
                case "width":
                    {
                        try
                        {
                            numeroint = Convert.ToInt32(Params[2]);
                            foreach (Item IItem in Items.ToList())
                            {
                                if (IItem == null)
                                    continue;
                                using (IQueryAdapter dbClient = YezzEnvironment.GetDatabaseManager().GetQueryReactor())
                                {
                                    dbClient.SetQuery("SELECT base_item FROM items WHERE id = '" + IItem.Id + "' LIMIT 1");
                                    Item = dbClient.getRow();
                                    if (Item == null)
                                        continue;
                                    FurnitureID = Convert.ToInt32(Item[0]);
                                    dbClient.RunQuery("UPDATE `furniture` SET `width` = '" + numeroint + "' WHERE `id` = '" + FurnitureID + "' LIMIT 1");
                                }
                                Session.SendWhisper("Anchura del Item: " + FurnitureID + " editada con éxito (Valor de anchura ingresado: " + numeroint.ToString() + ")");
                            }
                            YezzEnvironment.GetGame().GetItemManager().Init();
                        }
                        catch (Exception)
                        {
                            Session.SendNotification("Ha ocurrido un error (Ingrese números válidos)");
                        }
                    }
                    break;
                case "length":
                    {
                        try
                        {
                            numeroint = Convert.ToInt32(Params[2]);
                            foreach (Item IItem in Items.ToList())
                            {
                                if (IItem == null)
                                    continue;
                                using (IQueryAdapter dbClient = YezzEnvironment.GetDatabaseManager().GetQueryReactor())
                                {
                                    dbClient.SetQuery("SELECT base_item FROM items WHERE id = '" + IItem.Id + "' LIMIT 1");
                                    Item = dbClient.getRow();
                                    if (Item == null)
                                        continue;
                                    FurnitureID = Convert.ToInt32(Item[0]);
                                    dbClient.RunQuery("UPDATE `furniture` SET `length` = '" + numeroint + "' WHERE `id` = '" + FurnitureID + "' LIMIT 1");
                                }
                                Session.SendWhisper("Longitud del Item: " + FurnitureID + " editada con éxito (Valor de longitud ingresado: " + numeroint.ToString() + ")");
                            }
                            YezzEnvironment.GetGame().GetItemManager().Init();
                        }
                        catch (Exception)
                        {
                            Session.SendNotification("Ha ocurrido un error (Ingrese números válidos)");
                        }
                    }
                    break;
                case "height":
                    {
                        try
                        {
                            numerodouble = Convert.ToDouble(Params[2]);
                            foreach (Item IItem in Items.ToList())
                            {
                                if (IItem == null)
                                    continue;
                                using (IQueryAdapter dbClient = YezzEnvironment.GetDatabaseManager().GetQueryReactor())
                                {
                                    dbClient.SetQuery("SELECT base_item FROM items WHERE id = '" + IItem.Id + "' LIMIT 1");
                                    Item = dbClient.getRow();
                                    if (Item == null)
                                        continue;
                                    FurnitureID = Convert.ToInt32(Item[0]);
                                    dbClient.RunQuery("UPDATE `furniture` SET `stack_height` = '" + numerodouble + "' WHERE `id` = '" + FurnitureID + "' LIMIT 1");
                                }
                                Session.SendWhisper("Altura del Item: " + FurnitureID + " editada con éxito (Valor de altura ingresado: " + numerodouble.ToString() + ")");
                            }
                            YezzEnvironment.GetGame().GetItemManager().Init();
                        }
                        catch (Exception)
                        {
                            Session.SendNotification("Ha ocurrido un error (Ingrese números válidos)");
                        }
                    }
                    break;
                case "interactioncount":
                    {
                        try
                        {
                            numeroint = Convert.ToInt32(Params[2]);
                            foreach (Item IItem in Items.ToList())
                            {
                                if (IItem == null)
                                    continue;
                                using (IQueryAdapter dbClient = YezzEnvironment.GetDatabaseManager().GetQueryReactor())
                                {
                                    dbClient.SetQuery("SELECT base_item FROM items WHERE id = '" + IItem.Id + "' LIMIT 1");
                                    Item = dbClient.getRow();
                                    if (Item == null)
                                        continue;
                                    FurnitureID = Convert.ToInt32(Item[0]);
                                    dbClient.RunQuery("UPDATE `furniture` SET `interaction_modes_count` = '" + numeroint + "' WHERE `id` = '" + FurnitureID + "' LIMIT 1");
                                }
                                Session.SendWhisper("Numero de interacciones del Item: " + FurnitureID + " editado con éxito (Valor ingresado: " + numeroint.ToString() + ")");
                            }
                            YezzEnvironment.GetGame().GetItemManager().Init();
                        }
                        catch (Exception)
                        {
                            Session.SendNotification("Ha ocurrido un error (Ingrese números válidos)");
                        }
                    }
                    break;
                case "cansit":
                    {
                        try
                        {
                            opcion = Params[2].ToLower();
                            if (!opcion.Equals("si") && !opcion.Equals("no"))
                            {
                                Session.SendWhisper("Ingresa una opción valida (si/no)");
                                return;
                            }
                            if (opcion.Equals("si"))
                                opcion = "1";
                            else if (opcion.Equals("no"))
                                opcion = "0";
                            foreach (Item IItem in Items.ToList())
                            {
                                if (IItem == null)
                                    continue;
                                using (IQueryAdapter dbClient = YezzEnvironment.GetDatabaseManager().GetQueryReactor())
                                {
                                    dbClient.SetQuery("SELECT base_item FROM items WHERE id = '" + IItem.Id + "' LIMIT 1");
                                    Item = dbClient.getRow();
                                    if (Item == null)
                                        continue;
                                    FurnitureID = Convert.ToInt32(Item[0]);
                                    dbClient.RunQuery("UPDATE `furniture` SET `can_sit` = '" + opcion + "' WHERE `id` = '" + FurnitureID + "' LIMIT 1");
                                }
                                Session.SendWhisper("can_sit del Item: " + FurnitureID + " editado con éxito");
                            }
                            YezzEnvironment.GetGame().GetItemManager().Init();
                        }
                        catch (Exception)
                        {
                            Session.SendNotification("Ha ocurrido un error.");
                        }
                    }
                    break;
                case "canstack":
                    {
                        try
                        {
                            opcion = Params[2].ToLower();
                            if (!opcion.Equals("si") && !opcion.Equals("no"))
                            {
                                Session.SendWhisper("Ingresa una opción valida (si/no)");
                                return;
                            }
                            if (opcion.Equals("si"))
                                opcion = "1";
                            else if (opcion.Equals("no"))
                                opcion = "0";
                            foreach (Item IItem in Items.ToList())
                            {
                                if (IItem == null)
                                    continue;
                                using (IQueryAdapter dbClient = YezzEnvironment.GetDatabaseManager().GetQueryReactor())
                                {
                                    dbClient.SetQuery("SELECT base_item FROM items WHERE id = '" + IItem.Id + "' LIMIT 1");
                                    Item = dbClient.getRow();
                                    if (Item == null)
                                        continue;
                                    FurnitureID = Convert.ToInt32(Item[0]);
                                    dbClient.RunQuery("UPDATE `furniture` SET `can_stack` = '" + opcion + "' WHERE `id` = '" + FurnitureID + "' LIMIT 1");
                                }
                                Session.SendWhisper("can_stack del Item: " + FurnitureID + " editado con éxito");
                            }
                            YezzEnvironment.GetGame().GetItemManager().Init();
                        }
                        catch (Exception)
                        {
                            Session.SendNotification("Ha ocurrido un error.");
                        }
                    }
                    break;
                case "canwalk":
                    {
                        try
                        {
                            opcion = Params[2].ToLower();
                            if (!opcion.Equals("si") && !opcion.Equals("no"))
                            {
                                Session.SendWhisper("Ingresa una opción valida (si/no)");
                                return;
                            }
                            if (opcion.Equals("si"))
                                opcion = "1";
                            else if (opcion.Equals("no"))
                                opcion = "0";
                            foreach (Item IItem in Items.ToList())
                            {
                                if (IItem == null)
                                    continue;
                                using (IQueryAdapter dbClient = YezzEnvironment.GetDatabaseManager().GetQueryReactor())
                                {
                                    dbClient.SetQuery("SELECT base_item FROM items WHERE id = '" + IItem.Id + "' LIMIT 1");
                                    Item = dbClient.getRow();
                                    if (Item == null)
                                        continue;
                                    FurnitureID = Convert.ToInt32(Item[0]);
                                    dbClient.RunQuery("UPDATE `furniture` SET `is_walkable` = '" + opcion + "' WHERE `id` = '" + FurnitureID + "' LIMIT 1");
                                }
                                Session.SendWhisper("can_walk del Item: " + FurnitureID + " editado con éxito");
                            }
                            YezzEnvironment.GetGame().GetItemManager().Init();
                        }
                        catch (Exception)
                        {
                            Session.SendNotification("Ha ocurrido un error.");
                        }
                    }
                    break;
                case "mercadillo":
                    {
                        try
                        {
                            opcion = Params[2].ToLower();
                            if (!opcion.Equals("si") && !opcion.Equals("no"))
                            {
                                Session.SendWhisper("Ingresa una opción valida (si/no)");
                                return;
                            }
                            if (opcion.Equals("si"))
                                opcion = "1";
                            else if (opcion.Equals("no"))
                                opcion = "0";
                            foreach (Item IItem in Items.ToList())
                            {
                                if (IItem == null)
                                    continue;
                                using (IQueryAdapter dbClient = YezzEnvironment.GetDatabaseManager().GetQueryReactor())
                                {
                                    dbClient.SetQuery("SELECT base_item FROM items WHERE id = '" + IItem.Id + "' LIMIT 1");
                                    Item = dbClient.getRow();
                                    if (Item == null)
                                        continue;
                                    FurnitureID = Convert.ToInt32(Item[0]);
                                    dbClient.RunQuery("UPDATE `furniture` SET `is_rare` = '" + opcion + "' WHERE `id` = '" + FurnitureID + "' LIMIT 1");
                                }
                                Session.SendWhisper("Opción de venta en el mercadillo del Item: " + FurnitureID + " editado con éxito");
                            }
                            YezzEnvironment.GetGame().GetItemManager().Init();
                        }
                        catch (Exception)
                        {
                            Session.SendNotification("Ha ocurrido un error.");
                        }
                    }
                    break;
                case "interaction":
                    {
                        try
                        {
                            opcion = Params[2].ToLower();
                            foreach (Item IItem in Items.ToList())
                            {
                                if (IItem == null)
                                    continue;
                                using (IQueryAdapter dbClient = YezzEnvironment.GetDatabaseManager().GetQueryReactor())
                                {
                                    dbClient.SetQuery("SELECT base_item FROM items WHERE id = '" + IItem.Id + "' LIMIT 1");
                                    Item = dbClient.getRow();
                                    if (Item == null)
                                        continue;
                                    FurnitureID = Convert.ToInt32(Item[0]);
                                    dbClient.RunQuery("UPDATE `furniture` SET `interaction_type` = '" + opcion + "' WHERE `id` = '" + FurnitureID + "' LIMIT 1");
                                }
                                Session.SendWhisper("Interacción del Item: " + FurnitureID + " editada con éxito. (Valor ingresado: " + opcion + ")");
                            }
                            YezzEnvironment.GetGame().GetItemManager().Init();
                        }
                        catch (Exception)
                        {
                            Session.SendNotification("Ha ocurrido un error.");
                        }
                    }
                    break;
                default:
                    {
                        Session.SendNotification("La opción ingrsada no existe, para saber las opciones decir :item utilidad");
                        return;
                    }
                    break;
            }


        }
    }
}