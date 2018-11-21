using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

using Yezz.Utilities;
using Yezz.HabboHotel.Rooms;
using Yezz.HabboHotel.GameClients;

using Yezz.HabboHotel.Rooms.Chat.Commands.User;
using Yezz.HabboHotel.Rooms.Chat.Commands.User.Fun;
using Yezz.HabboHotel.Rooms.Chat.Commands.Moderator;
using Yezz.HabboHotel.Rooms.Chat.Commands.Moderator.Fun;
using Yezz.HabboHotel.Rooms.Chat.Commands.Administrator;

using Yezz.Communication.Packets.Outgoing.Rooms.Chat;
using Yezz.Communication.Packets.Outgoing.Notifications;
using Yezz.Database.Interfaces;
using Yezz.HabboHotel.Rooms.Chat.Commands.Events;
using Yezz.HabboHotel.Items.Wired;
using Yezz.Communication.Packets.Outgoing.Rooms.Notifications;
using Yezz.HabboHotel.Rooms.Chat.Commands.User.Fan;

namespace Yezz.HabboHotel.Rooms.Chat.Commands
{
    public class CommandManager
    {
        /// <summary>
        /// Command Prefix only applies to custom commands.
        /// </summary>
        private string _prefix = ":";

        /// <summary>
        /// Commands registered for use.
        /// </summary>
        private readonly Dictionary<string, IChatCommand> _commands;
        public  Dictionary<string, string> _commands2;

        /// <summary>
        /// The default initializer for the CommandManager
        /// </summary>
        public CommandManager(string Prefix)
        {
            this._prefix = Prefix;
            this._commands = new Dictionary<string, IChatCommand>();
            

            this.RegisterVIP();
            this.RegisterUser();
            this.RegisterEvents();
            this.RegisterModerator();
            this.RegisterAdministrator();
            //this.UpDateCommands2();
          
        }

        /// <summary>
        /// Request the text to parse and check for commands that need to be executed.
        /// </summary>
        /// <param name="Session">Session calling this method.</param>
        /// <param name="Message">The message to parse.</param>
        /// <returns>True if parsed or false if not.</returns>
        public bool Parse(GameClient Session, string Message)
        {
            if (Session == null || Session.GetHabbo() == null || Session.GetHabbo().CurrentRoom == null || YezzStaticGameSettings.IsGoingToBeClose)
                return false;

            if (!Message.StartsWith(_prefix))
                return false;

            Room room = Session.GetHabbo().CurrentRoom;

            if (room.GetFilter().CheckCommandFilter(Message))
                return false;

            if (Message == _prefix + "comandos" || Message == _prefix + "commands")
            {
                StringBuilder List = new StringBuilder();
                List<string> Comandos = new List<string>();
                List.Append("--------------------------------------\n");
                List.Append("I Comandos disponibles para ti ID[1] I\n");
                List.Append("--------------------------------------\n");

                Comandos = YezzEnvironment.GetGame().GetPermissionManager().GetCommandsForID(1);
                foreach (string Comando in Comandos)
                {
                    foreach (var CmdList in _commands.ToList())
                    {

                        if (CmdList.Value.PermissionRequired == Comando) { List.Append("\n:" + CmdList.Key + " " + CmdList.Value.Parameters + " - " + CmdList.Value.Description + "\n"); }else { continue; }



                    }
                }
                
                if (Session.GetHabbo().Rank > 2)
                {

                    for (int i = 2; i <= Session.GetHabbo().Rank; i++) {

                        List.Append("\n---------------------------------------------------------------");
                        List.Append("\nComandos disponibles para [" + GetRankName(i) + "] ID [" + i + "]");
                        List.Append("\n---------------------------------------------------------------");
                        List.Append("\n");

                        Comandos = YezzEnvironment.GetGame().GetPermissionManager().GetCommandsForID(i);
                        foreach (string Comando in Comandos)
                        {
                            foreach (var CmdList in _commands.ToList())
                            {

                                if (CmdList.Value.PermissionRequired == Comando) { List.Append("\n:" + CmdList.Key + " " + CmdList.Value.Parameters + " - " + CmdList.Value.Description + "\n"); } else { continue; }



                            }
                        }


                    }

                }

                List.Append("\n Todos los comandos son registrados en la base de datos para evitar abuso de los mismos |s");



                Session.SendMessage(new MOTDNotificationComposer(List.ToString()));



                return true;
            }

            Message = Message.Substring(1);
            string[] Split = Message.Split(' ');

            if (Split.Length == 0)
                return false;

            IChatCommand Cmd = null;
            if (_commands.TryGetValue(Split[0].ToLower(), out Cmd))
            {
                if (Session.GetHabbo().GetPermissions().HasRight("mod_tool"))
                    this.LogCommand(Session.GetHabbo().Id, Message, Session.GetHabbo().MachineId, Session.GetHabbo().Username, Session.GetHabbo().Look);

                if (!string.IsNullOrEmpty(Cmd.PermissionRequired))
                {
                    if (!Session.GetHabbo().GetPermissions().HasCommand(Cmd.PermissionRequired))
                        return false;
                }


                Session.GetHabbo().IChatCommand = Cmd;
                Session.GetHabbo().CurrentRoom.GetWired().TriggerEvent(WiredBoxType.TriggerUserSaysCommand, Session.GetHabbo(), this);

                Cmd.Execute(Session, Session.GetHabbo().CurrentRoom, Split);
                return true;
            }
            return false;
        }

        private string GetRankName( int i)
        {
            string RankName = "Undefined";
            #region RankNames (Switch)
            switch (i)
            {
                case 2:
                    RankName = "VIP";
                    break;
                case 3:
                    RankName = "PUB";
                    break;
                case 4:
                    RankName = "EDP";
                    break;
                case 5:
                    RankName = "LNC";
                    break;
                case 6:
                    RankName = "HELP";
                    break;
                case 7:
                    RankName = "EMB";
                    break;
                case 8:
                    RankName = "DJ";
                    break;
                case 9:
                    RankName = "BAW";
                    break;
                case 10:
                    RankName = "MD";
                    break;
                case 11:
                    RankName = "EDS";
                    break;
                case 12:
                    RankName = "EB";
                    break;
                case 13:
                    RankName = "CADM";
                    break;
                case 14:
                    RankName = "EDC";
                    break;
                case 15:
                    RankName = "ADM";
                    break;
                case 16:
                    RankName = "GRN";
                    break;
                case 17:
                    RankName = "FUN";
                    break;
                case 18:
                    RankName = "HIDE";
                    break;

            }
            #endregion
            return RankName;
        }
        /// <summary>
        /// Registers the VIP set of commands.
        /// </summary>
        private void RegisterVIP()
        {
            //USERS VIP
            this.Register("spull", new SuperPullCommand());
            this.Register("superpull", new SuperPullCommand());
            this.Register("supertiron", new SuperPullCommand());

            this.Register("spush", new SuperPushCommand());
            this.Register("superpush", new SuperPushCommand());
            this.Register("superempujada", new SuperPushCommand());

            this.Register("sincara", new FacelessCommand());
            this.Register("faceless", new FacelessCommand());

            this.Register("moonwalk", new MoonwalkCommand());

            this.Register("bubblebot", new BubbleBotCommand());

            //Custom
            this.Register("sizenombre", new ChatHTMLSizeCommand());
            this.Register("emoji", new EmojiCommand());
            this.Register("tag", new PrefixCommand2());
            this.Register("namecolor", new DeleteColorName());

            this.Register("welcome", new WelcomeCommand());
            this.Register("chatalerta", new ChatAlertCommand());

            this.Register("disparar", new CutCommand());
            this.Register("pedo", new FartFaceCommand());
            this.Register("patearculo", new SlapassCommand());
            this.Register("rko", new RkoCommand());
            this.Register("cortar", new CortarCabezaCommand());
            this.Register("quemar", new BurnCommand());

            this.Register("fastwalk", new FastwalkCommand());
            this.Register("caminarrapido", new FastwalkCommand());

            this.Register("verusuarioson", new OnDutyCommand());
            this.Register("usuarioson", new OnDutyCommand());

            this.Register("goto", new GOTOCommand());
            this.Register("taxi", new GOTOCommand());
            this.Register("ira", new GOTOCommand());

            this.Register("selfie", new SelfieCommand());

            this.Register("bubble", new BubbleCommand());
        }

        /// <summary>
        /// Registers the Events set of commands.
        /// </summary>
        private void RegisterEvents()
        {
            this.Register("eha", new EventAlertCommand());
            this.Register("eventha", new EventAlertCommand());
            this.Register("evento", new EventAlertCommand());

            this.Register("pha", new PublicityAlertCommand());
            this.Register("oleada", new PublicityAlertCommand());

            this.Register("eventoda2", new Da2AlertCommand());

            this.Register("poll", new PollCommand());
            this.Register("encuesta", new PollCommand());
            this.Register("endpoll", new EndPollCommand());

            this.Register("quizz", new IdolQuizCommand());

            this.Register("radioalert", new DJAlert());
            this.Register("radio", new DJAlert());
            this.Register("dj", new DJAlert());
            this.Register("djalert", new DJAlert());

            this.Register("notievento", new EventoExpress());
            this.Register("eventoexpress", new EventoExpress());
            this.Register("eventexpress", new EventoExpress());

            this.Register("masspoll", new MassPollCommand());

            this.Register("notifica", new NotificaCommand());

            this.Register("megaoferta", new MegaOferta());

            this.Register("special", new SpecialEvent());
            this.Register("ee", new SpecialEvent());
            this.Register("es", new SpecialEvent());

            this.Register("fbconcurso", new FacebookCommand());
        }

        /// <summary>
        /// Registers the default set of commands.
        /// </summary>
        private void RegisterUser()
        {
            //Nuevos - RANK: 1
            this.Register("groupchat", new GroupChatCommand());
            this.Register("chatdegrupo", new GroupChatCommand());
            //this.Register("chatdegrupo", new GroepChatCommand());

            this.Register("convertcredits", new ConvertCreditsCommand());
            this.Register("convertircredits", new ConvertCreditsCommand());
            this.Register("convertircreditos", new ConvertCreditsCommand());

            this.Register("convertdiamonds", new ConvertDiamondsCommand());
            this.Register("convertirdiamantes", new ConvertDiamondsCommand());

            this.Register("convertduckets", new ConvertDucketsCommand());
            this.Register("convertirduckets", new ConvertDucketsCommand());

            this.Register("hidewired", new HideWiredCommand());
            this.Register("ocultarwired", new HideWiredCommand());
            this.Register("wiredoff", new HideWiredCommand());

            this.Register("eventtype", new EventSwapCommand());
            this.Register("tipoevento", new EventSwapCommand());

            this.Register("changelog", new ChangelogCommand());
            this.Register("cambios", new ChangelogCommand());

            this.Register("chess", new SetChessGameCommand());
            this.Register("ajedrez", new SetChessGameCommand());

            this.Register("random", new RandomizeCommand());

            //this.Register("vipstatus", new ViewVIPStatusCommand());

            this.Register("build", new BuildCommand());

            this.Register("color", new ColourCommand());

            //this.Register("prefix", new PrefixCommand());



            //Normales
            this.Register("handitem", new CarryCommand());
            this.Register("carry", new CarryCommand());
            this.Register("itemenmano", new CarryCommand());

            this.Register("about", new InfoCommand());
            this.Register("info", new InfoCommand());

            this.Register("online", new OnlineCommand());

            this.Register("disablewhispers", new DisableWhispersCommand());
            this.Register("desactivarsusurros", new DisableWhispersCommand());

            this.Register("mimic", new MimicCommand());
            this.Register("copy", new MimicCommand());

            this.Register("disablemimic", new DisableMimicCommand());
            this.Register("disablecopy", new DisableMimicCommand());
            this.Register("desactivarcopy", new DisableMimicCommand());

            this.Register("pet", new PetCommand());
            this.Register("mascota", new PetCommand());

            this.Register("mutepets", new MutePetsCommand());
            this.Register("silenciarmascotas", new MutePetsCommand());

            this.Register("mutebots", new MuteBotsCommand());
            this.Register("silenciarbots", new MuteBotsCommand());

            this.Register("dance", new DanceCommand());
            this.Register("bailar", new DanceCommand());

            this.Register("push", new PushCommand());
            this.Register("empujar", new PushCommand());
            this.Register("empujon", new PushCommand());

            this.Register("pull", new PullCommand());
            this.Register("halar", new PullCommand());
            this.Register("jalar", new PullCommand());

            this.Register("enable", new EnableCommand());
            this.Register("efecto", new EnableCommand());

            this.Register("follow", new FollowCommand());
            this.Register("seguir", new FollowCommand());

            this.Register("empty", new EmptyItems());
            this.Register("emptyitems", new EmptyItems());
            this.Register("limpiarinventario", new EmptyItems());

            this.Register("disablefriends", new DisableFriends());
            this.Register("desactivarsolicitudes", new DisableFriends());

            this.Register("enablefriends", new EnableFriends());
            this.Register("activarsolicitudes", new EnableFriends());

            this.Register("disablegifts", new DisableGiftsCommand());
            this.Register("desactivarregalos", new DisableGiftsCommand());

            this.Register("lay", new LayCommand());
            this.Register("acostarse", new LayCommand());

            this.Register("sit", new SitCommand());
            this.Register("sentarse", new SitCommand());

            this.Register("stand", new StandCommand());
            this.Register("pararse", new StandCommand());

            this.Register("stats", new StatsCommand());
            this.Register("estadisticas", new StatsCommand());

            this.Register("dnd", new DNDCommand());
            this.Register("desactivarmensajes", new DNDCommand());


            //Como dueño de sala
            this.Register("pickall", new PickAllCommand());
            this.Register("recoger", new PickAllCommand());
            this.Register("recogertodo", new PickAllCommand());

            this.Register("ejectall", new EjectAllCommand());

            this.Register("builder", new Builder());
            this.Register("constructor", new Builder());

            this.Register("unload", new UnloadCommand());

            this.Register("reload", new Reloadcommand());
            this.Register("recargar", new Reloadcommand());

            this.Register("fixroom", new RegenMaps());
            this.Register("regenmaps", new RegenMaps());
            this.Register("regenerarmapa", new RegenMaps());

            this.Register("setmax", new SetMaxCommand());

            this.Register("setspeed", new SetSpeedCommand());

            this.Register("disablediagonal", new DisableDiagonalCommand());
            this.Register("desactivardiagonal", new DisableDiagonalCommand());

            this.Register("room", new RoomCommand());
            this.Register("sala", new RoomCommand());
            this.Register("desactivarcomandosensala", new RoomCommand());

            this.Register("kickpets", new KickPetsCommand());
            this.Register("echarmascotas", new KickPetsCommand());

            this.Register("kickbots", new KickBotsCommand());
            this.Register("echarbots", new KickBotsCommand());

            
            //Custom
            this.Register("ayuda", new HelpCommand());
            this.Register("help", new HelpCommand());

            this.Register("kissuser", new KissCommand());
            this.Register("besar", new KissCommand());
            this.Register("beso", new KissCommand());

            this.Register("golpear", new GolpeCommand());
            this.Register("golpe", new GolpeCommand());

            this.Register("curar", new CurarCommand());

            this.Register("buyroom", new BuyRoomCommand());
            this.Register("comprarroom", new BuyRoomCommand());
            this.Register("comprarsala", new BuyRoomCommand());

            this.Register("sellroom", new SellRoomCommand());
            this.Register("venderroom", new SellRoomCommand());
            this.Register("vendersala", new SellRoomCommand());

            this.Register("kill", new KillCommand());
            this.Register("matar", new KillCommand());

            this.Register("ausente", new AfkCommand());
            this.Register("afk", new AfkCommand());

            this.Register("despertar", new BackCommand());

            //this.Register("violar", new SexCommand());

            this.Register("sexo", new SexoCommand());

            this.Register("paja", new JerkOffCommand());
            this.Register("hacerpaja", new hacerpaja());

            this.Register("fumar", new WeedCommand());
            this.Register("smoke", new FumarCommand());

            this.Register("variables", new WiredVariable());
            this.Register("wired", new WiredVariable());

            this.Register("disablespam", new DisableSpamCommand());
            //this.Register("build", new BuildCommand());
            //this.Register("vipstatus", new ViewVIPStatusCommand());

            //this.Register("precios", new PriceList());
            //this.Register("custom", new CustomLegit());

            //Juegos Customs
            this.Register("apostar", new SetBetCommand());
            this.Register("apuesta", new SetBetCommand());

            this.Register("casino", new CasinoCommand());
            this.Register("jugarda2", new CasinoCommand());
            this.Register("contador", new CasinoCommand());

            this.Register("closedice", new CloseDiceCommand());
            this.Register("cerrarda2", new CloseDiceCommand());
            this.Register("cerrardice", new CloseDiceCommand());

            //this.Register("flagme", new FlagMeCommand());
        }

        private void Register(string v)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Registers the moderator set of commands.
        /// </summary>
        private void RegisterModerator()
        {
            //RANK - 4
            this.Register("bp", new BanPubliCommand());
            this.Register("bpu", new BanPubliCommand());
            this.Register("banpubli", new BanPubliCommand());
            this.Register("banearpubli", new BanPubliCommand());

            this.Register("prefixname", new PrefixNameCommand());
            this.Register("tagname", new PrefixNameCommand());
            this.Register("prefijoname", new PrefixNameCommand());

            this.Register("pcolor", new ColourPrefixCommand());
            this.Register("prefijocolor", new ColourPrefixCommand());
            this.Register("tagcolor", new ColourPrefixCommand());
            this.Register("prefixcolor", new ColourPrefixCommand());

            this.Register("sa", new StaffAlertCommand());
            this.Register("staffalerta", new StaffAlertCommand());

            this.Register("coords", new CoordsCommand());
            this.Register("coordenadas", new CoordsCommand());

            this.Register("alleyesonme", new AllEyesOnMeCommand());
            this.Register("iloveforbi", new AllEyesOnMeCommand());

            this.Register("allaroundme", new AllAroundMeCommand());
            this.Register("forbifamoso", new AllAroundMeCommand());

            this.Register("ignorewhispers", new IgnoreWhispersCommand());
            this.Register("ignorarsusurro", new IgnoreWhispersCommand());

            this.Register("forced_effects", new DisableForcedFXCommand());
            this.Register("desactivarefectos", new DisableForcedFXCommand());


            //RANK - 6
            this.Register("troll", new TrollAlert());

            this.Register("msgusers", new TrollAlertUser());
            this.Register("trollusers", new TrollAlertUser());
            this.Register("notialert", new TrollAlertUser());

            this.Register("teleport", new TeleportCommand());
            this.Register("tele", new TeleportCommand());
            this.Register("crack", new TeleportCommand());

            this.Register("override", new OverrideCommand());
            this.Register("unconoemadre", new OverrideCommand());

            this.Register("superfastwalk", new SuperFastwalkCommand());
            this.Register("caminarsuperrapido", new SuperFastwalkCommand());

            this.Register("forcesit", new ForceSitCommand());
            this.Register("sientate", new ForceSitCommand());

            this.Register("forcelay", new ForceLay());
            this.Register("acuestate", new ForceLay());

            this.Register("forcestand", new ForceStand());
            this.Register("parate", new ForceStand());

            this.Register("userson", new ViewOnlineCommand());
            this.Register("genteon", new ViewOnlineCommand());

            this.Register("goboom", new GoBoomCommand());

            this.Register("spam", new spamCommand());

            this.Register("c", new TrollAlert());


            //RANK - 7
            this.Register("summon", new SummonCommand());
            this.Register("traer", new SummonCommand());


            //RANK - 10
            this.Register("ui", new UserInfoCommand());
            this.Register("userinfo", new UserInfoCommand());
            this.Register("informacionde", new UserInfoCommand());
            this.Register("userinformacion", new UserInfoCommand());

            this.Register("roomunmute", new RoomUnmuteCommand());
            this.Register("desmutearsala", new RoomUnmuteCommand());

            this.Register("roommute", new RoomMuteCommand());
            this.Register("mutearsala", new RoomMuteCommand());

            this.Register("roomalert", new RoomAlertCommand());
            this.Register("salaalerta", new RoomAlertCommand());

            this.Register("roomkick", new RoomKickCommand());
            this.Register("echaratodos", new RoomKickCommand());
            this.Register("echardelasala", new RoomKickCommand());

            this.Register("mute", new MuteCommand());
            this.Register("smute", new MuteCommand());
            this.Register("silenciar", new MuteCommand());
            this.Register("mutear", new MuteCommand());

            this.Register("unmute", new UnmuteCommand());
            this.Register("desmutear", new UnmuteCommand());

            this.Register("kick", new KickCommand());
            this.Register("skick", new KickCommand());

            this.Register("dc", new DisconnectCommand());
            this.Register("disconnect", new DisconnectCommand());
            this.Register("desconectar", new DisconnectCommand());

            this.Register("alert", new AlertCommand());

            this.Register("tradeban", new TradeBanCommand());

            this.Register("freeze", new FreezeCommand());
            this.Register("congelar", new FreezeCommand());

            this.Register("unfreeze", new UnFreezeCommand());
            this.Register("descongelar", new UnFreezeCommand());


            //RANK - 11
            this.Register("customalert", new customalertCommand());
            
            this.Register("ban", new BanCommand());
            this.Register("banear", new BanCommand());

            this.Register("mip", new MIPCommand());
            this.Register("ipban", new IPBanCommand());
            this.Register("banearip", new IPBanCommand());

            this.Register("ha", new HotelAlertCommand());
            this.Register("hotelalert", new HotelAlertCommand());

            this.Register("lastmsg", new LastMessagesCommand());
            this.Register("verhistorial", new LastMessagesCommand());

            this.Register("lastconsolemsg", new LastConsoleMessagesCommand());
            this.Register("verhistorialdeconsola", new LastConsoleMessagesCommand());

            this.Register("senduser", new SendUserCommand());
            this.Register("enviarusuario", new SendUserCommand());

            this.Register("makesay", new MakeSayCommand());
            this.Register("di", new MakeSayCommand());

            this.Register("flaguser", new FlagUserCommand());
            this.Register("cambiatedenombre", new FlagUserCommand());

            this.Register("makepublic", new MakePublicCommand());
            this.Register("salapublica", new MakePublicCommand());

            this.Register("makeprivate", new MakePrivateCommand());
            this.Register("salaprivada", new MakePrivateCommand());


            //RANK - 12
            this.Register("roombadge", new RoomBadgeCommand());
            this.Register("darplacaalasala", new RoomBadgeCommand());
            this.Register("salaplaca", new RoomBadgeCommand());

            this.Register("givebadge", new GiveBadgeCommand());
            this.Register("darplaca", new GiveBadgeCommand());

            this.Register("give", new GiveCommand());
            this.Register("dar", new GiveCommand());
            this.Register("darmoneda", new GiveCommand());

            this.Register("massenable", new MassEnableCommand());
            this.Register("efectoparatodos", new MassEnableCommand());

            this.Register("massdance", new MassDanceCommand());
            this.Register("baileparatodos", new MassDanceCommand());

            this.Register("premiar", new PremiarCommand());


            //RANK - 13
            this.Register("roomgive", new RoomGiveCommand());

            this.Register("massbadge", new MassBadgeCommand());
            this.Register("placaparatodos", new MassBadgeCommand());
            this.Register("hotelplaca", new MassBadgeCommand());

            this.Register("massgive", new MassGiveCommand());
            this.Register("hoteleconomia", new MassGiveCommand());
            this.Register("daratodosmonedas", new MassGiveCommand());

            this.Register("hal", new HALCommand());
            this.Register("alertaurl", new HALCommand());


            //RANK - 14
            this.Register("unban", new UnBanCommand());
            this.Register("desbanear", new UnBanCommand());
            this.Register("quitarban", new UnBanCommand());


            //RANK - 15
            this.Register("addblackword", new FilterCommand());
            this.Register("filtro", new FilterCommand());


            //RANK - 16
            this.Register("rank", new GiveRanksCommand());
        }

        /// <summary>
        /// Registers the administrator set of commands.
        /// </summary>
        private void RegisterAdministrator()
        {
            //RANK - 10
            this.Register("verclones", new VerClonesCommand());

            this.Register("viewinventary", new ViewInventaryCommand());
            this.Register("verinventario", new ViewInventaryCommand());


            //RANK - 11
            this.Register("deletegroup", new DeleteGroupCommand());
            this.Register("borrargrupo", new DeleteGroupCommand());

            this.Register("addtag", new AddTagsToUserCommand());

            this.Register("ca", new CustomizedHotelAlert());


            //RANK - 13
            this.Register("summonall", new SummonAll());
            this.Register("atraeratodos", new SummonAll());

            this.Register("givespecial", new GiveSpecialReward());

            this.Register("massevent", new MassiveEventCommand());

            this.Register("removebadge", new RemoveBadgeCommand());

            this.Register("ia", new SendGraphicAlertCommand());

            this.Register("iau", new SendImageToUserCommand());

            this.Register("viewevents", new ViewStaffEventListCommand());
            this.Register("viewevent", new ViewStaffEventListCommand());
            this.Register("vereventos", new ViewStaffEventListCommand());


            //RANK - 14
            this.Register("staffson", new StaffInfo());
            this.Register("staffons", new StaffInfo());

            this.Register("emptyuser", new EmptyUser());
            this.Register("borrarinventariode", new EmptyUser());


            //RANK - 15
            this.Register("darvip", new ReloadUserrVIPRankCommand());

            this.Register("alerttype", new AlertSwapCommand());


            //RANK - 16
            this.Register("addpredesigned", new AddPredesignedCommand());
            this.Register("addpackdesala", new AddPredesignedCommand());
            this.Register("addpack", new AddPredesignedCommand());
            this.Register("addlote", new AddPredesignedCommand());

            this.Register("removepredesigned", new RemovePredesignedCommand());
            this.Register("quitarpackdesala", new RemovePredesignedCommand());
            this.Register("quitarpack", new RemovePredesignedCommand());
            this.Register("quitarlote", new RemovePredesignedCommand());

            this.Register("update", new UpdateCommand());
            this.Register("actualizar", new UpdateCommand());

            this.Register("item", new UpdateFurniture());

            this.Register("dcall", new DesconectarnAll());

            this.Register("shutdownforbi", new ShutdownCommand());
            this.Register("reiniciarforbi", new RestartCommand());

            this.Register("voucher", new VoucherCommand());


            //NUEVOS
            this.Register("forcebox", new ForceFurniMaticBoxCommand());

            this.Register("mw", new MultiwhisperModeCommand());

            this.Register("progress", new ProgressAchievementCommand());

            this.Register("control", new ControlCommand());

            this.Register("dice", new ForceDiceCommand());

            this.Register("link", new LinkStaffCommand());
        }

        /// <summary>
        /// Registers a Chat Command.
        /// </summary>
        /// <param name="CommandText">Text to type for this command.</param>
        /// <param name="Command">The command to execute.</param>
        public void Register(string CommandText, IChatCommand Command)
        {
            this._commands.Add(CommandText, Command);
        }

        public static string MergeParams(string[] Params, int Start)
        {
            var Merged = new StringBuilder();
            for (int i = Start; i < Params.Length; i++)
            {
                if (i > Start)
                    Merged.Append(" ");
                Merged.Append(Params[i]);
            }

            return Merged.ToString();
        }

        public void LogCommand(int UserId, string Data, string MachineId, string Username, string Look)
        {
            using (IQueryAdapter dbClient = YezzEnvironment.GetDatabaseManager().GetQueryReactor())
            {
                dbClient.SetQuery("INSERT INTO `logs_client_staff` (`user_id`,`data_string`,`machine_id`, `timestamp`) VALUES (@UserId,@Data,@MachineId,@Timestamp)");
                dbClient.AddParameter("UserId", UserId);
                dbClient.AddParameter("Data", Data);
                dbClient.AddParameter("MachineId", MachineId);
                dbClient.AddParameter("Timestamp", YezzEnvironment.GetUnixTimestamp());
                dbClient.RunQuery();
            }

            if (Data == "regenmaps" || Data.StartsWith("c") || Data == "sa" || Data == "ga" || Data == "info" || UserId == 3)
            { return; }

            else
            YezzEnvironment.GetGame().GetClientManager().ManagerAlert(RoomNotificationComposer.SendBubble("/fig/" + Look, "" + Username + "\n\nUsó el comando:\n:" + Data + ".", ""));
        }

        public bool TryGetCommand(string Command, out IChatCommand IChatCommand)
        {
            return this._commands.TryGetValue(Command, out IChatCommand);
        }
    }
}
