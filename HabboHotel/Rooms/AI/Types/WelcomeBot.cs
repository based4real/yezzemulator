using System;
using System.Drawing;

using Yezz.HabboHotel.GameClients;
using Yezz.HabboHotel.Rooms.AI.Speech;
using Yezz.Communication.Packets.Outgoing.Rooms.Chat;
using Yezz.HabboHotel.Rooms.AI.Responses;
using Yezz.Utilities;
using Yezz.HabboHotel.Rooms.AI;
using Yezz.HabboHotel.Rooms;
using Yezz.Communication.Packets.Outgoing.Inventory.Purse;
using Yezz.Communication.Packets.Outgoing.Inventory.Furni;
using Yezz.HabboHotel.Items;
using Yezz.Communication.Packets.Outgoing.Nux;
using Yezz.Communication.Packets.Outgoing;
using Yezz.Communication.Packets.Outgoing.Rooms.Nux;

namespace Yezz.HabboHotel.Rewards.Rooms.AI.Types
{
    class WelcomeBot : BotAI
    {
        private int VirtualId;
        private int ActionTimer = 0;

        public WelcomeBot(int VirtualId)
        {
            this.VirtualId = VirtualId;
            ActionTimer = 0;

        }


        public override void OnSelfEnterRoom()
        {

        }
        public override void OnSelfLeaveRoom(bool Kicked)
        {
        }

        public override void OnUserEnterRoom(RoomUser User)
        {
        }

        public override void OnUserLeaveRoom(GameClient Client)
        {
            //if ()
        }

        public override void OnUserSay(RoomUser User, string Message)
        {

        }

        public override void OnUserShout(RoomUser User, string Message)
        {

        }

        public override void OnTimerTick()
        {
            if (GetBotData() == null)
                return;

            GameClient Target = YezzEnvironment.GetGame().GetClientManager().GetClientByUsername(GetRoom().OwnerName);
            if (Target == null || Target.GetHabbo() == null || Target.GetHabbo().CurrentRoom != GetRoom())
            {
                GetRoom().GetGameMap().RemoveUserFromMap(GetRoomUser(), new Point(GetRoomUser().X, GetRoomUser().Y));
                GetRoom().GetRoomUserManager().RemoveBot(GetRoomUser().VirtualId, false);
                return;
            }
            var habbo = Target.GetHabbo();

            if (ActionTimer <= 0)
            {
                switch (Target.GetHabbo().GetStats().WelcomeLevel)
                {
                    case 0:
                    default:
                        Point nextCoord;
                        RoomUser Target2 = GetRoom().GetRoomUserManager().GetRoomUserByHabbo(GetBotData().ForcedUserTargetMovement);
                        if (GetBotData().ForcedMovement)
                        {
                            if (GetRoomUser().Coordinate == GetBotData().TargetCoordinate)
                            {
                                GetBotData().ForcedMovement = false;
                                GetBotData().TargetCoordinate = new Point();

                                GetRoomUser().MoveTo(GetBotData().TargetCoordinate.X, GetBotData().TargetCoordinate.Y);
                            }
                        }
                        else if (GetBotData().ForcedUserTargetMovement > 0)
                        {

                            if (Target2 == null)
                            {
                                GetBotData().ForcedUserTargetMovement = 0;
                                GetRoomUser().ClearMovement(true);
                            }
                            else
                            {
                                var Sq = new Point(Target2.X, Target2.Y);

                                if (Target2.RotBody == 0)
                                {
                                    Sq.Y--;
                                }
                                else if (Target2.RotBody == 2)
                                {
                                    Sq.X++;
                                }
                                else if (Target2.RotBody == 4)
                                {
                                    Sq.Y++;
                                }
                                else if (Target2.RotBody == 6)
                                {
                                    Sq.X--;
                                }


                                GetRoomUser().MoveTo(Sq);
                            }
                        }
                        else if (GetBotData().TargetUser == 0)
                        {
                            nextCoord = GetRoom().GetGameMap().getRandomWalkableSquare();
                            GetRoomUser().MoveTo(nextCoord.X, nextCoord.Y);
                        }
                        Target.GetHabbo().GetStats().WelcomeLevel++;
                        break;
                    case 1:
                        GetRoomUser().Chat("¡Bienvenid@ a " + YezzEnvironment.HotelName + ", " + GetRoom().OwnerName + "!\nSoy Frank el manager del Hotel.\n¿Preparado para encontrar muchas\nsorpresas?", false, 33);
                        Target.GetHabbo().GetStats().WelcomeLevel++;
                        break;

                    case 2:
                        GetRoomUser().Chat("Ahora presta atención, te\nexplicaremos como se juega en\n" + YezzEnvironment.HotelName + ".", false, 33);
                        Target.GetHabbo().GetStats().WelcomeLevel++;
                        break;

                    case 3:
                        if (Target.GetHabbo()._NUX)
                        {
                            var nuxStatus = new ServerPacket(ServerPacketHeader.NuxUserStatus);
                            nuxStatus.WriteInteger(2);
                            Target.SendMessage(nuxStatus);
                            Target.SendMessage(new NuxAlertComposer("nux/lobbyoffer/hide"));
                            Target.SendMessage(new NuxAlertComposer("helpBubble/add/HC_JOIN_BUTTON/Subscribete al Club VIP de " + YezzEnvironment.HotelName + ", para obtener muchas ventajas."));
                        }
                        Target.GetHabbo().GetStats().WelcomeLevel++;
                        break;

                    case 4:
                        if (habbo.PassedNuxNavigator && habbo.PassedNuxCatalog && habbo.PassedNuxItems && habbo.PassedNuxMMenu && habbo.PassedNuxChat && habbo.PassedNuxCredits && habbo.PassedNuxDuckets)
                        {
                        GetRoomUser().Chat("Disfruta tu estancia. ¡En\n" + YezzEnvironment.HotelName + " todos somos únicos!", false, 33);
                        Target.GetHabbo().GetStats().WelcomeLevel++;
                        }
                        break;

                    case 5:
                        GetRoomUser().Chat("...no he terminado aún,\n¡Elige tu regalo de bienvenida!", false, 33);
                        Target.GetHabbo().GetStats().WelcomeLevel++;
                        Target.SendMessage(new NuxItemListComposer());
                        break;

                    case 6:
                        Target.GetHabbo().GetStats().WelcomeLevel++;
                        break;

                    case 7:
                        GetRoomUser().Chat("Bien " + GetRoom().OwnerName + ", es hora de irme.\nNo olvides conectarte todos los\ndías en " + YezzEnvironment.HotelName + "!", false, 33);
                        Target.GetHabbo().GetStats().WelcomeLevel++;
                        break;

                    case 8:
                        if (habbo.PassedNuxNavigator && habbo.PassedNuxCatalog && habbo.PassedNuxItems && habbo.PassedNuxMMenu && habbo.PassedNuxChat && habbo.PassedNuxCredits && habbo.PassedNuxDuckets)
                        {
                            Target.SendMessage(new NuxAlertComposer("nux/lobbyoffer/show"));
                            var nuxStatus = new ServerPacket(ServerPacketHeader.NuxUserStatus);
                            nuxStatus.WriteInteger(0);
                            Target.SendMessage(nuxStatus);
                        }
                        GetRoom().GetGameMap().RemoveUserFromMap(GetRoomUser(), new Point(GetRoomUser().X, GetRoomUser().Y));
                        GetRoom().GetRoomUserManager().RemoveBot(GetRoomUser().VirtualId, false);
                        Target.GetHabbo().GetStats().WelcomeLevel++;
                        break;

                }
                ActionTimer = new Random(DateTime.Now.Millisecond + this.VirtualId ^ 2).Next(5, 15);
            }
            else
                ActionTimer--;
        }
    }
}


