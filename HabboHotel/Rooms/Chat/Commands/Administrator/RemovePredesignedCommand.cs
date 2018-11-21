using Yezz.HabboHotel.Catalog.PredesignedRooms;
using System.Text;
using System.Linq;
using System.Globalization;

namespace Yezz.HabboHotel.Rooms.Chat.Commands.Administrator
{
    class RemovePredesignedCommand : IChatCommand
    {
        public string PermissionRequired => "user_16";
        public string Parameters => "";
        public string Description => "Elimina la Sala de la lista de Salas pre-diseñadas";

        public void Execute(GameClients.GameClient Session, Rooms.Room Room, string[] Params)
        {
            if (Room == null) return;
            //if (!YezzEnvironment.GetGame().GetCatalog().GetPredesignedRooms().Exists((uint)Room.Id))
            //{
            //    Session.SendWhisper("La sala no existe en la lista.");
            //    return;
            //}

            var predesignedId = 0U;
            using (var dbClient = YezzEnvironment.GetDatabaseManager().GetQueryReactor())
            {
                dbClient.SetQuery("SELECT id FROM catalog_predesigned_rooms WHERE room_id = " + Room.Id + ";");
                predesignedId = (uint)dbClient.getInteger();

                dbClient.runFastQuery("DELETE FROM catalog_predesigned_rooms WHERE room_id = " + Room.Id + " AND id = " +
                    predesignedId + ";");
            }

            YezzEnvironment.GetGame().GetCatalog().GetPredesignedRooms().predesignedRoom.Remove(predesignedId);
            Session.SendWhisper("La Sala se eliminó correctamente de la lista.");
        }
    }
}