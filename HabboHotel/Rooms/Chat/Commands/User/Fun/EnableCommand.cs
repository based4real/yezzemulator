using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

using Yezz.HabboHotel.Rooms;
using Yezz.HabboHotel.Rooms.Games;
using Yezz.HabboHotel.Rooms.Games.Teams;

namespace Yezz.HabboHotel.Rooms.Chat.Commands.User.Fun
{
    class EnableCommand : IChatCommand
    {
        public string PermissionRequired => "user_normal";
        public string Parameters => "[EFECTO ID]";
        public string Description => "Habilitar un efecto en tu personaje.";

        public void Execute(GameClients.GameClient Session, Rooms.Room Room, string[] Params)
        {
            if (Params.Length == 1)
            {
                Session.SendWhisper("Usted debe escribir un ID Efecto", 34);
                return;
            }

            RoomUser ThisUser = Session.GetHabbo().CurrentRoom.GetRoomUserManager().GetRoomUserByHabbo(Session.GetHabbo().Username);
            if (ThisUser == null)
                return;

            if (ThisUser.RidingHorse)
            {
                Session.SendWhisper("No se puede activar un efecto mientras montas un caballo", 34);
                return;
            }
            else if (ThisUser.Team != TEAM.NONE)
                return;
            else if (ThisUser.isLying)
                return;

            int EffectId = 0;
            if (!int.TryParse(Params[1], out EffectId))
                return;

            if (EffectId > int.MaxValue || EffectId < int.MinValue)
                return;

            // VIP Effect - VIP 2
            if (EffectId == 44 && Session.GetHabbo().Rank < 2 || EffectId == 593 && Session.GetHabbo().Rank < 2 || EffectId == 604 && Session.GetHabbo().Rank < 2 || EffectId == 605 && Session.GetHabbo().Rank < 2 || EffectId == 606 && Session.GetHabbo().Rank < 2 || EffectId == 607 && Session.GetHabbo().Rank < 2 || EffectId == 608 && Session.GetHabbo().Rank < 2)
            { Session.SendWhisper("Lo sentimos, no eres VIP, por ello no puedes usar dicho efecto.", 34); return; }

            //PUBLI - Publicistas 5
            if (EffectId == 601 && Session.GetHabbo().Rank < 5)
            {
                Session.SendWhisper("Lo sentimos, no eres Publicista, por ello no puedes usar dicho efecto.", 34);
                return;
            }

            //SUPER STAR 6
            if (EffectId == 598 && Session.GetHabbo().Rank < 6)
            {
                Session.SendWhisper("Lo sentimos, no eres una Super Estrella, por ello no puedes usar dicho efecto.", 34);
                return;
            }

            // Guia/ALfa 
            if (EffectId == 178 && Session.GetHabbo().Rank < 8)
            {
                Session.SendWhisper("Lo sentimos, no eres Guía, por ello no puedes usar dicho efecto.", 34);
                return;
            }
            // Guide Effects
            if (EffectId == 592 && Session.GetHabbo()._guidelevel < 3 && Session.GetHabbo().Rank < 8 || EffectId == 595 && Session.GetHabbo()._guidelevel < 2 && Session.GetHabbo().Rank < 8 || EffectId == 597 && Session.GetHabbo()._guidelevel < 1 && Session.GetHabbo().Rank < 8)
            { Session.SendWhisper("Lo sentimos, no perteneces al equipo guía, es por ello que no puedes usar este efecto.", 34); return; }

            // BOT
            if (EffectId == 187 && Session.GetHabbo().Rank < 8)
            {
                Session.SendWhisper("Lo sentimos, no eres Bot, por ello no puedes usar dicho efecto.", 34);
                return;
            }

            //BAW
            if (EffectId == 599 && Session.GetHabbo().Rank < 9)
            {
                Session.SendWhisper("Lo sentimos, no eres Constructor Oficial del Hotel, por ello no puedes usar dicho efecto.", 34);
                return;
            }

            // Staff Effects
            if (EffectId == 102 && Session.GetHabbo().Rank < 10)
            {
                Session.SendWhisper("Lo sentimos, no eres Staff, por ello no puedes usar dicho efecto.", 34);
                return;
            }

            //Croupier
            if (EffectId == 594 && Session.GetHabbo().Rank < 10 || EffectId == 777 && Session.GetHabbo().Rank < 10)
            {
                Session.SendWhisper("Lo sentimos, no eres Croupier, por ello no puedes usar dicho efecto.", 34);
                return;
            }

            //MODS
            if (EffectId == 596 && Session.GetHabbo().Rank < 11)
            {
                Session.SendWhisper("Lo sentimos, no eres Moderador, por ello no puedes usar dicho efecto.", 34);
                return;
            }

            //Game Master
            if (EffectId == 602 && Session.GetHabbo().Rank < 12)
            {
                Session.SendWhisper("Lo sentimos, no eres Game Master, por ello no puedes usar dicho efecto.", 34);
                return;
            }

            //EDC 13
            if (EffectId == 603 && Session.GetHabbo().Rank < 13)
            {
                Session.SendWhisper("Lo sentimos, no eres Encargado de Concursos, por ello no puedes usar dicho efecto.", 34);
                return;
            }

            //Forbi
            if (EffectId == 609 && Session.GetHabbo().Username == "Forbi")
            {
                Session.SendWhisper("Tu no eres Forbi.", 34);
                return;
            }

            Session.GetHabbo().LastEffect = EffectId;
            Session.GetHabbo().Effects().ApplyEffect(EffectId);
        }
    }
}
