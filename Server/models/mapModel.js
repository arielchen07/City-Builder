const mongoose = require('mongoose');
const Schema = mongoose.Schema;


// Item Schema and Model
const mapSchema = new Schema({
    userID: {
        type: String
    },
    mapData: {
        type: String,  
    },
    mapName:{
        type: String,
        default:'Map1'
    }
}, { timestamps: true });

// Creating Map model based on the mapSchema
const Map = mongoose.model('Map', mapSchema);
module.exports = Map;