const mongoose = require('mongoose');
const Schema = mongoose.Schema;

// Item Schema and Model
const itemSchema = new Schema({
    userID: {
        type: String
    },
    quantity: {
        type: Number
    }
}, { timestamps: true });

const Item = mongoose.model('Item', itemSchema);
module.exports = Item;



// const GameInventorySchema = new Schema({
//     // Define your GameInventory schema fields here
// });

// GameInventorySchema.statics.getGameInventory = async function(id) {
//     try {
//         const inv = await this.find({userId: id});
//         return inv;
//     } catch (err) {
//         throw new Error(err);
//     }
// }

// GameInventorySchema.statics.createGameInventory = async function(userId) {
//     try {
//         const inv = new this({userId: userId, items: []}); // Assuming you have an items array in the schema
//         await inv.save();
//     } catch (err) {
//         throw new Error(err);
//     }
// }

// GameInventorySchema.methods.addGame = async function(gameId) {
//     try {
//         this.games.push(gameId); // Assuming you have a games array in the schema
//         await this.save();
//     } catch (err) {
//         throw new Error(err);
//     }
// }

// GameInventorySchema.methods.removeGame = async function(gameId) {
//     try {
//         this.games.splice(this.games.indexOf(gameId), 1);
//         await this.save();
//     } catch (err) {
//         throw new Error(err);
//     }
// }

// const GameInventory = mongoose.model('GameInventory', GameInventorySchema);
// module.exports = GameInventory;
