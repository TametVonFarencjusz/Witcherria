using System.IO;
using Terraria;
using Terraria.ID;
using Witcherria.Players;

namespace Witcherria
{
    // This is a partial class, meaning some of its parts were split into other files. See ExampleMod.*.cs for other portions.
    // The class is partial to organize similar code together to clarify what is related.
    // This class extends from the Mod class as seen in ExampleMod.cs. Make sure to extend from the mod class, ": Mod", in your own code if using this file as a template for you mods Mod class.
    public partial class Witcherria
    {
        internal enum MessageType : byte
        {
            SignSyncPlayer,
        }

        public override void HandlePacket(BinaryReader reader, int whoAmI)
        {
            MessageType msgType = (MessageType)reader.ReadByte();
            byte playerNumber;

            switch (msgType)
            {
                // This message syncs ExampleStatIncreasePlayer.exampleLifeFruits and ExampleStatIncreasePlayer.exampleManaCrystals
                case MessageType.SignSyncPlayer:
                    playerNumber = reader.ReadByte();
                    SignPlayer modPlayer = Main.player[playerNumber].GetModPlayer<SignPlayer>();
                    modPlayer.ReceivePlayerSync(reader);

                    if (Main.netMode == NetmodeID.Server)
                    {
                        // Forward the changes to the other clients
                        modPlayer.SyncPlayer(-1, whoAmI, false);
                    }
                    break;
            }
        }

    }
}