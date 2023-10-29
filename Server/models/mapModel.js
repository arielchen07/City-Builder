const mongoose = require('mongoose');
const Schema = mongoose.Schema;

// Item Schema and Model
const mapSchema = new Schema({
    userID: {
        type: String
    },
    mapData: {
        type: String
    }
}, { timestamps: true });

const Map = mongoose.model('Map', mapSchema);
module.exports = Map;