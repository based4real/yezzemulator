using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using Yezz.Communication.Packets.Outgoing.Nux;
using Yezz.Communication.Packets.Outgoing;
using Yezz.Communication.Packets.Outgoing.Rooms.Notifications;

namespace Yezz.Communication.Packets.Incoming.Nucs
{
    class RoomNucsAlerta : IPacketEvent
    {
        public void Parse(HabboHotel.GameClients.GameClient Session, ClientPacket Packet)
        {
            var habbo = Session.GetHabbo();

            if (habbo == null) return;

            if (!habbo.PassedNuxNavigator && !habbo.PassedNuxCatalog && !habbo.PassedNuxItems && !habbo.PassedNuxMMenu && !habbo.PassedNuxChat && !habbo.PassedNuxCredits && !habbo.PassedNuxDuckets)
            {
                Session.SendMessage(new NuxAlertComposer("helpBubble/add/BOTTOM_BAR_NAVIGATOR/Este es el navegador. ¡Úsalo para explorar las miles de salas que hay en " + YezzEnvironment.HotelName + "."));
                habbo.PassedNuxNavigator = true;
            }

            else if (habbo.PassedNuxNavigator && !habbo.PassedNuxCatalog && !habbo.PassedNuxItems && !habbo.PassedNuxMMenu && !habbo.PassedNuxChat && !habbo.PassedNuxCredits && !habbo.PassedNuxDuckets)
            {
                Session.SendMessage(new NuxAlertComposer("helpBubble/add/BOTTOM_BAR_CATALOGUE/Esta es la tienda. Aquí encontrarás elementos impresionantes y únicos que hacen que " + YezzEnvironment.HotelName + " sea aún más divertido. ¡Pruébalo!"));
                habbo.PassedNuxCatalog = true;
            }

            else if (habbo.PassedNuxNavigator && habbo.PassedNuxCatalog && !habbo.PassedNuxItems && !habbo.PassedNuxMMenu && !habbo.PassedNuxChat && !habbo.PassedNuxCredits && !habbo.PassedNuxDuckets)
            {
                Session.SendMessage(new NuxAlertComposer("helpBubble/add/BOTTOM_BAR_INVENTORY/Este es el inventario. Para colocar tus furnis, tan sólo tienes que arrastrarlos hasta el suelo."));
                habbo.PassedNuxItems = true;
            }

            else if (habbo.PassedNuxNavigator && habbo.PassedNuxCatalog && habbo.PassedNuxItems && !habbo.PassedNuxMMenu && !habbo.PassedNuxChat && !habbo.PassedNuxCredits && !habbo.PassedNuxDuckets)
            {
                Session.SendMessage(new NuxAlertComposer("helpBubble/add/MEMENU_CLOTHES/Aquí están los ajustes. Puedes cambiarte de ropa y modificar aspectos de tu personaje."));
                habbo.PassedNuxMMenu = true;
            }

            else if (habbo.PassedNuxNavigator && habbo.PassedNuxCatalog && habbo.PassedNuxItems && habbo.PassedNuxMMenu && !habbo.PassedNuxChat && !habbo.PassedNuxCredits && !habbo.PassedNuxDuckets)
            {
                Session.SendMessage(new NuxAlertComposer("helpBubble/add/CHAT_INPUT/Hable con otros " + YezzEnvironment.HotelName + "'s escribiendo aquí."));
                habbo.PassedNuxChat = true;
            }

            else if (habbo.PassedNuxNavigator && habbo.PassedNuxCatalog && habbo.PassedNuxItems && habbo.PassedNuxMMenu && habbo.PassedNuxChat && !habbo.PassedNuxCredits && !habbo.PassedNuxDuckets)
            {
                Session.SendMessage(new NuxAlertComposer("helpBubble/add/CREDITS_BUTTON/En este apartado podras ver la cantidad de créditos que tienes."));
                habbo.PassedNuxCredits = true;
            }

            else if (habbo.PassedNuxNavigator && habbo.PassedNuxCatalog && habbo.PassedNuxItems && habbo.PassedNuxMMenu && habbo.PassedNuxChat && habbo.PassedNuxCredits && !habbo.PassedNuxDuckets)
            {
                Session.SendMessage(new NuxAlertComposer("helpBubble/add/DIAMONDS_BUTTON/En este apartado podrás ver la moneda oficial de " + YezzEnvironment.HotelName + ", la cual tendrás que ganártela."));
                habbo.PassedNuxDuckets = true;
                string figure = Session.GetHabbo().Look;
                YezzEnvironment.GetGame().GetClientManager().StaffAlert(RoomNotificationComposer.SendBubble("fig/" + figure, "El usuario " + Session.GetHabbo().Username + " se ha registrado y ha pasado las Nux Alerts en " + YezzEnvironment.HotelName + ".", ""));

            }

            if (habbo.PassedNuxNavigator && habbo.PassedNuxCatalog && habbo.PassedNuxItems && habbo.PassedNuxMMenu && habbo.PassedNuxChat && habbo.PassedNuxCredits && habbo.PassedNuxDuckets)
            {
                habbo._NUX = false;
                using (var dbClient = YezzEnvironment.GetDatabaseManager().GetQueryReactor())
                    dbClient.runFastQuery("UPDATE users SET nux_user = 'false' WHERE id = " + Session.GetHabbo().Id + ";");
            }
        }
    }
}