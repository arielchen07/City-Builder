const mongoose = require('mongoose');
const Schema = mongoose.Schema;

// Item Schema and Model
const itemSchema = new Schema({
    userID: {
        type: String
    },
    quantity: {
        type: Number
    },
    category:{
        type: String
    },
    name:{
        type: String
    },
}, { timestamps: true });

// Creating Item model based on the mapSchema
const Item = mongoose.model('Item', itemSchema);
module.exports = Item;
