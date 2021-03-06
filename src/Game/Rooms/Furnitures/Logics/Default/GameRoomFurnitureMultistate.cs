using System;
using System.Linq;
using System.Collections.Generic;

using Newtonsoft.Json.Linq;

using MySql.Data.MySqlClient;

using Server.Game.Furnitures;

using Server.Game.Users;
using Server.Game.Users.Furnitures;

using Server.Game.Rooms.Actions;
using Server.Game.Rooms.Users;

using Server.Game.Rooms.Furnitures.Actions;

using Server.Events;
using Server.Socket.Messages;

namespace Server.Game.Rooms.Furnitures.Logics {
    class GameRoomFurnitureMultistateLogic : IGameRoomFurnitureLogic {
        public GameRoomFurniture Furniture { get; set; }

        public void OnUserUse(GameRoomUser user, JToken data) {
            if(!user.HasRights())
                return;
                
            int animation = data["animation"].ToObject<int>();

            Furniture.Animation = animation;
            
            user.User.Client.Send(new SocketMessage("OnRoomFurnitureUse", Furniture.Id).Compose());

            user.User.Room.Actions.AddEntity(Furniture.Id, 500, new GameRoomFurnitureAnimation(Furniture, animation));

            Furniture.SetAnimation(animation);
        }

        public void OnUserEnter(GameRoomUser user) {
            
        }

        public void OnUserLeave(GameRoomUser user) {
            
        }
    }
}
