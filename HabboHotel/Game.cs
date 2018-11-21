
using log4net;

using Yezz.Communication.Packets;
using Yezz.HabboHotel.GameClients;
using Yezz.HabboHotel.Moderation;
using Yezz.HabboHotel.Support;
using Yezz.HabboHotel.Catalog;
using Yezz.HabboHotel.Items;
using Yezz.HabboHotel.Items.Televisions;
using Yezz.HabboHotel.Navigator;
using Yezz.HabboHotel.Rooms;
using Yezz.HabboHotel.Groups;
using Yezz.HabboHotel.Quests;
using Yezz.HabboHotel.Achievements;
using Yezz.HabboHotel.LandingView;
using Yezz.HabboHotel.Global;

using Yezz.HabboHotel.Games;

using Yezz.HabboHotel.Rooms.Chat;
using Yezz.HabboHotel.Talents;
using Yezz.HabboHotel.Bots;
using Yezz.HabboHotel.Cache;
using Yezz.HabboHotel.Rewards;
using Yezz.HabboHotel.Badges;
using Yezz.HabboHotel.Permissions;
using Yezz.HabboHotel.Subscriptions;
using System.Threading;
using System.Threading.Tasks;
using Yezz.HabboHotel.Camera;
using Yezz.HabboHotel.Catalog.FurniMatic;
using Yezz.Communication.Packets.Incoming.LandingView;
using Yezz.HabboHotel.Helpers;
using Yezz.HabboHotel.Groups.Forums;
using System;
using Yezz.HabboHotel.Rooms.Music;
using Yezz.HabboHotel.Items.Crafting;
using Yezz.HabboHotel.Rooms.Polls;
using Yezz.HabboHotel.Calendar;
using Yezz.HabboHotel.Items.RentableSpaces;
using Yezz.Communication.Packets.Outgoing.Nux;
using Yezz.HabboHotel.LandingView.CommunityGoal;
//using Yezz.WebSockets;
//using Yezz.HabboHotel.Alphas;

namespace Yezz.HabboHotel
{
    public class Game
    {
        private static readonly ILog log = LogManager.GetLogger("Yezz.HabboHotel.Game");

        public GroupForumManager GetGroupForumManager()
        {
            return forummanager;
        }

        private GroupForumManager forummanager;
        private readonly PacketManager _packetManager;
        private readonly GameClientManager _clientManager;
        private readonly ModerationManager _modManager;
        private readonly FrontpageManager _frontpageManager;
        private readonly ItemDataManager _itemDataManager;
        private readonly CatalogManager _catalogManager;
        private readonly TelevisionManager _televisionManager;//TODO: Initialize from the item manager.
        private readonly NavigatorManager _navigatorManager;
        private readonly RoomManager _roomManager;
        private readonly ChatManager _chatManager;
        private readonly GroupManager _groupManager;
        private readonly QuestManager _questManager;
        private readonly AchievementManager _achievementManager;
        private readonly TalentTrackManager _talentTrackManager;
        private readonly LandingViewManager _landingViewManager;//TODO: Rename class
        private readonly GameDataManager _gameDataManager;
        private readonly CraftingManager _craftingManager;
        private readonly ServerStatusUpdater _globalUpdater;
        private readonly LanguageLocale _languageLocale;
        private readonly AntiMutant _antiMutant;
        private readonly BotManager _botManager;
        private readonly CacheManager _cacheManager;
        private readonly RewardManager _rewardManager;
        private readonly BadgeManager _badgeManager;
        private readonly SongManager _musicManager;
        private readonly PermissionManager _permissionManager;
        private readonly SubscriptionManager _subscriptionManager;
        private readonly TargetedOffersManager _targetedoffersManager;
        private readonly TalentManager _talentManager;
        //private readonly CameraPhotoManager _cameraManager;
        private readonly CrackableManager _crackableManager;
        private readonly FurniMaticRewardsManager _furniMaticRewardsManager;
        private readonly PollManager _pollManager;
        private readonly CommunityGoalVS _communityGoalVS;
        private readonly CalendarManager _calendarManager;
        private RentableSpaceManager _rentableSpaceManager;
        private readonly LeaderBoardDataManager _leaderBoardDataManager;
        public static int SessionUserRecord;

        private bool _cycleEnded;
        private bool _cycleActive;
        private Task _gameCycle;
        private int _cycleSleepTime = 25;

        public Game()
        {
            Console.WriteLine();
            log.Info(">> Iniciando Yezz Emulator para " + YezzEnvironment.HotelName + "...");
            Console.WriteLine();

            SessionUserRecord = 0;
            GetHallOfFame.getInstance().Load();
            this._packetManager = new PacketManager();
            this._rentableSpaceManager = new RentableSpaceManager();
            this._clientManager = new GameClientManager();
            this._modManager = new ModerationManager();

            this._itemDataManager = new ItemDataManager();
            this._itemDataManager.Init();
            //this._cameraManager = new CameraPhotoManager();
            //this._cameraManager.Init(this._itemDataManager);
            this._catalogManager = new CatalogManager();
            this._catalogManager.Init(this._itemDataManager);
            this._frontpageManager = new FrontpageManager();

            this._televisionManager = new TelevisionManager();
            this._crackableManager = new CrackableManager();
            this._crackableManager.Initialize(YezzEnvironment.GetDatabaseManager().GetQueryReactor());
            this._furniMaticRewardsManager = new FurniMaticRewardsManager();
            this._furniMaticRewardsManager.Initialize(YezzEnvironment.GetDatabaseManager().GetQueryReactor());

            this._craftingManager = new CraftingManager();
            this._craftingManager.Init();

            this._navigatorManager = new NavigatorManager();
            this._roomManager = new RoomManager();
            this._chatManager = new ChatManager();
            this._groupManager = new GroupManager();
            this._questManager = new QuestManager();
            this._achievementManager = new AchievementManager();
            _talentManager = new TalentManager();
            _talentManager.Initialize();
            this._talentTrackManager = new TalentTrackManager();

            this._landingViewManager = new LandingViewManager();
            this._gameDataManager = new GameDataManager();

            this._globalUpdater = new ServerStatusUpdater();
            this._globalUpdater.Init();

            this._languageLocale = new LanguageLocale();
            this._antiMutant = new AntiMutant();
            this._botManager = new BotManager();

            this._cacheManager = new CacheManager();
            this._rewardManager = new RewardManager();
            this._musicManager = new SongManager();

            this._badgeManager = new BadgeManager();
            this._badgeManager.Init();

            this.forummanager = new GroupForumManager();

            this._communityGoalVS = new CommunityGoalVS();
            this._communityGoalVS.LoadCommunityGoalVS();

            this._permissionManager = new PermissionManager();
            this._permissionManager.Init();

            this._subscriptionManager = new SubscriptionManager();
            this._subscriptionManager.Init();

            HelperToolsManager.Init();

            this._calendarManager = new CalendarManager();
            this._calendarManager.Init();

            this._leaderBoardDataManager = new LeaderBoardDataManager();

            this._targetedoffersManager = new TargetedOffersManager();
            this._targetedoffersManager.Initialize(YezzEnvironment.GetDatabaseManager().GetQueryReactor());

            this._pollManager = new PollManager();
            this._pollManager.Init();
            //WebSocketManager.StartListener();


        }

        public void StartGameLoop()
        {
            this._gameCycle = new Task(GameCycle);
            this._gameCycle.Start();

            this._cycleActive = true;
        }

        private void GameCycle()
        {
            while (this._cycleActive)
            {
                this._cycleEnded = false;

                YezzEnvironment.GetGame().GetRoomManager().OnCycle();
                YezzEnvironment.GetGame().GetClientManager().OnCycle();
                //AlphaManager.getInstance().onCycle();
                this._cycleEnded = true;
                Thread.Sleep(this._cycleSleepTime);
            }
        }

        public void StopGameLoop()
        {
            this._cycleActive = false;

            while (!this._cycleEnded)
            {
                Thread.Sleep(this._cycleSleepTime);
            }
        }

        public PacketManager GetPacketManager()
        {
            return _packetManager;
        }

        public GameClientManager GetClientManager()
        {
            return _clientManager;
        }

        public SongManager GetMusicManager()
        {
            return _musicManager;
        }

        public CatalogManager GetCatalog()
        {
            return _catalogManager;
        }

        public NavigatorManager GetNavigator()
        {
            return _navigatorManager;
        }

        public CalendarManager GetCalendarManager()
        {
            return _calendarManager;
        }

        public FrontpageManager GetCatalogFrontPageManager()
        {
            return this._frontpageManager;
        }

        public ItemDataManager GetItemManager()
        {
            return _itemDataManager;
        }

        public RoomManager GetRoomManager()
        {
            return _roomManager;
        }

        internal TargetedOffersManager GetTargetedOffersManager()
        {
            return _targetedoffersManager;
        }

        public AchievementManager GetAchievementManager()
        {
            return _achievementManager;
        }

        public TalentTrackManager GetTalentTrackManager()
        {
            return _talentTrackManager;
        }

        public TalentManager GetTalentManager()
        {
            return _talentManager;

        }

        public ModerationManager GetModerationManager()
        {
            return this._modManager;
        }

        public PermissionManager GetPermissionManager()
        {
            return this._permissionManager;
        }

        public SubscriptionManager GetSubscriptionManager()
        {
            return this._subscriptionManager;
        }

        public QuestManager GetQuestManager()
        {
            return this._questManager;
        }

        public RentableSpaceManager GetRentableSpaceManager()
        {
            return _rentableSpaceManager;
        }

        public GroupManager GetGroupManager()
        {
            return _groupManager;
        }

        public LandingViewManager GetLandingManager()
        {
            return _landingViewManager;
        }
        public TelevisionManager GetTelevisionManager()
        {
            return _televisionManager;
        }

        public ChatManager GetChatManager()
        {
            return this._chatManager;
        }

        //public CameraPhotoManager GetCameraManager()
        //{
        //    return this._cameraManager;
        //}

        internal CrackableManager GetPinataManager()
        {
            return this._crackableManager;
        }

        public CraftingManager GetCraftingManager()
        {
            return this._craftingManager;
        }

        public FurniMaticRewardsManager GetFurniMaticRewardsMnager()
        {
            return this._furniMaticRewardsManager;
        }

        public GameDataManager GetGameDataManager()
        {
            return this._gameDataManager;
        }

        public LanguageLocale GetLanguageLocale()
        {
            return this._languageLocale;
        }

        public AntiMutant GetAntiMutant()
        {
            return this._antiMutant;
        }

        public BotManager GetBotManager()
        {
            return this._botManager;
        }

        public CacheManager GetCacheManager()
        {
            return this._cacheManager;
        }

        public RewardManager GetRewardManager()
        {
            return this._rewardManager;
        }


        internal LeaderBoardDataManager GetLeaderBoardDataManager()
        {
            return this._leaderBoardDataManager;
        }


        public BadgeManager GetBadgeManager()
        {
            return this._badgeManager;
        }


        internal object GetFilterComponent()
        {
            throw new NotImplementedException();
        }

        public PollManager GetPollManager()
        {
            return this._pollManager;
        }

        public CommunityGoalVS GetCommunityGoalVS()
        {
            return this._communityGoalVS;
        }
    }
}