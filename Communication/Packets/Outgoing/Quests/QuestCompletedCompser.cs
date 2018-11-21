using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yezz.Communication.Packets.Outgoing.Quests
{
    class QuestCompletedCompser : ServerPacket
    {
        public QuestCompletedCompser()
            : base(ServerPacketHeader.QuestCompletedMessageComposer)
        {

        }
    }
}
