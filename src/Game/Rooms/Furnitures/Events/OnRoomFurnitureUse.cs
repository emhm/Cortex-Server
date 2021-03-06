using System;
using System.Collections.Generic;

using Newtonsoft.Json.Linq;

using MySql.Data.MySqlClient;

using Server.Game.Users;
using Server.Game.Users.Furnitures;

using Server.Game.Rooms;
using Server.Game.Rooms.Users;
using Server.Game.Rooms.Actions;
using Server.Game.Rooms.Furnitures.Actions;

using Server.Game.Furnitures;

using Server.Socket.Clients;
using Server.Socket.Events;
using Server.Socket.Messages;

namespace Server.Game.Rooms.Furnitures.Events {
    class OnRoomFurnitureUse : ISocketEvent {
        public string Event => "OnRoomFurnitureUse";

        public int Execute(SocketClient client, JToken data) {
            if(client.User.Room == null)
                return 0;

            GameRoomUser roomUser = client.User.Room.GetUser(client.User.Id);

            if(!roomUser.HasRights())
                return 0;
            
            int id = data["id"].ToObject<int>();

            GameRoomFurniture roomFurniture = client.User.Room.Furnitures.Find(x => x.Id == id);

            if(roomFurniture == null)
                return 0;

            if(roomFurniture.Logic == null)
                return 1;

            roomFurniture.Logic.OnUserUse(roomUser, data);

            return 1;
        }
    }
}
