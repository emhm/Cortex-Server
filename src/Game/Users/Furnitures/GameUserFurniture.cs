using System;
using System.Collections.Generic;

using Newtonsoft.Json;

using MySql.Data.MySqlClient;

using Server.Game.Users;
using Server.Game.Rooms.Actions;

using Server.Game.Furnitures;

namespace Server.Game.Users.Furnitures {
    class GameUserFurniture {
        [JsonIgnore]
        public int Id;

        [JsonIgnore]
        public int User;

        [JsonIgnore]
        public int Room;

        [JsonIgnore]
        public GameFurniture Furniture;

        public GameUserFurniture(MySqlDataReader furniture) {
            Id = furniture.GetInt32("id");

            User = furniture.GetInt32("user");
            Room = furniture.GetInt32("room");

            Furniture = GameFurnitureManager.GetGameFurniture(furniture.GetString("furniture"));
        }
    }
}
