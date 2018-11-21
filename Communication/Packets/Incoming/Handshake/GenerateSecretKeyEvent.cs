using System;

using Yezz.Communication.Packets.Incoming;
using Yezz.Utilities;
using Yezz.HabboHotel.GameClients;

using Yezz.Communication.Encryption;
using Yezz.Communication.Encryption.Crypto.Prng;
using Yezz.Communication.Packets.Outgoing.Handshake;

namespace Yezz.Communication.Packets.Incoming.Handshake
{
    public class GenerateSecretKeyEvent : IPacketEvent
    {
        public void Parse(HabboHotel.GameClients.GameClient Session, ClientPacket Packet)
        {
            string CipherPublickey = Packet.PopString();
           
            BigInteger SharedKey = HabboEncryptionV2.CalculateDiffieHellmanSharedKey(CipherPublickey);
            if (SharedKey != 0)
            {
                Session.RC4Client = new ARC4(SharedKey.getBytes());
                Session.SendMessage(new SecretKeyComposer(HabboEncryptionV2.GetRsaDiffieHellmanPublicKey()));
            }
            else 
            {
                Session.SendNotification("Se ha producido un error, por favor inicie sesion nuevamente!");
                return;
            }
        }
    }
}