using System.Collections.Generic;
using System.Linq;
using Yezz.Communication.Packets.Outgoing.Messenger;
using Yezz.Communication.Packets.Outgoing.Rooms.Notifications;
using Yezz.Database.Interfaces;
using Yezz.HabboHotel.GameClients;

namespace Yezz.HabboHotel.Rooms.Chat.Commands.User
{
    class GroupChatCommand : IChatCommand
    {
        public string PermissionRequired => "user_normal";
        public string Parameters => "";
        public string Description => "Borra tu Chat de Grupo.";

        public void Execute(GameClient Session, Room Room, string[] Params)
        {
                if (Params.Length < 2)
                {
                    Session.SendWhisper("Ha ocurrido un error, especifica borrar", 34);
                    return;
                }

                if (!Room.CheckRights(Session, true))
                {
                    Session.SendWhisper("No tienes permisos.", 34);
                    return;
                }

                if (Room.Group == null)
                {
                    Session.SendWhisper("Esta sala no tiene grupo, si lo acabas de crear haz :unload", 34);
                    return;
                }

                var mode = Params[1].ToLower();
                var group = Room.Group;

                if (mode == "borrar")
                {
                    if (group.HasChat == false)
                    {
                        Session.SendWhisper("Este grupo no tiene chat aun.", 34);
                        return;
                    }

                    using (var adap = YezzEnvironment.GetDatabaseManager().GetQueryReactor())
                    {
                        adap.SetQuery("UPDATE groups SET has_chat = '0' WHERE id = @id");
                        adap.AddParameter("id", group.Id);
                        adap.RunQuery();
                    }
                    group.HasChat = false;
                    List<GameClient> GroupMembers = (from Client in YezzEnvironment.GetGame().GetClientManager().GetClients.ToList() where Client != null && Client.GetHabbo() != null select Client).ToList();
                    foreach (GameClient Client in GroupMembers)
                    {
                        if (Client != null)
                            continue;
                        Client.SendMessage(new FriendListUpdateComposer(group, -1));
                    }
                }
                else
                {
                    Session.SendNotification("Ha ocurrido un error");
                }


            }
        }
    }