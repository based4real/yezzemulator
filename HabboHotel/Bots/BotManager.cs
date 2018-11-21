﻿using System;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections.Generic;

using log4net;
using Yezz.Database.Interfaces;
using Yezz.HabboHotel.Rooms.AI.Responses;
using Yezz.HabboHotel.Rooms.AI;

namespace Yezz.HabboHotel.Bots
{
    public class BotManager
    {
        private static readonly ILog log = LogManager.GetLogger("Yezz.HabboHotel.Rooms.AI.BotManager");

        private List<BotResponse> _responses;

        public BotManager()
        {
            this._responses = new List<BotResponse>();

            this.Init();
        }

        public void Init()
        {
            if (this._responses.Count > 0)
                this._responses.Clear();

            using (IQueryAdapter dbClient = YezzEnvironment.GetDatabaseManager().GetQueryReactor())
            {
                dbClient.SetQuery("SELECT `bot_ai`,`chat_keywords`,`response_text`,`response_mode`,`response_beverage` FROM `bots_responses`");
                DataTable BotResponses = dbClient.getTable();

                if (BotResponses != null)
                {
                    foreach (DataRow Response in BotResponses.Rows)
                    {
                        this._responses.Add(new BotResponse(Convert.ToString(Response["bot_ai"]), Convert.ToString(Response["chat_keywords"]), Convert.ToString(Response["response_text"]), Response["response_mode"].ToString(), Convert.ToString(Response["response_beverage"])));
                    }
                }
            }
        }

        public BotResponse GetResponse(BotAIType AiType, string Message)
        {
            foreach (BotResponse Response in this._responses.Where(X => X.AiType == AiType).ToList())
            {
                if (Response.KeywordMatched(Message))
                {
                    return Response;
                }
            }

            return null;
        }
    }
}
