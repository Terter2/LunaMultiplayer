﻿using LunaCommon.Message.Data.Chat;
using LunaCommon.Message.Interface;
using LunaCommon.Message.Server;
using Server.Client;
using Server.Log;
using Server.Message.Reader.Base;
using Server.Server;

namespace Server.Message.Reader
{
    public class ChatMsgReader : ReaderBase
    {
        public override void HandleMessage(ClientStructure client, IClientMessageBase message)
        {
            var messageData = (ChatMsgData)message.Data;
            if (messageData.From != client.PlayerName) return;

            if (messageData.Relay)
            {
                MessageQueuer.SendToAllClients<ChatSrvMsg>(messageData);
                LunaLog.ChatMessage($"{messageData.From}: {messageData.Text}");
            }
            else //Is a PM to server msg
            {
                LunaLog.Warning($"{messageData.From}: {messageData.Text}");
            }
        }
    }
}