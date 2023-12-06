const express = require('express');
const Map = require('../models/mapModel');
const User = require('../models/UserModel');

//Default empty map in json string format
let baseMapData = `{\"structureObjData\":[],\"tileData\":[{\"name\":\"grass3(Clone)\",\"position\":{\"x\":-4.0,\"y\":-0.10000000149011612,\"z\":-4.0},\"rotation\":{\"x\":0.0,\"y\":0.0,\"z\":0.0},\"isOccupied\":false},{\"name\":\"grass3(Clone)\",\"position\":{\"x\":1.0,\"y\":-0.10000000149011612,\"z\":-3.0},\"rotation\":{\"x\":0.0,\"y\":0.0,\"z\":0.0},\"isOccupied\":false},{\"name\":\"grass3(Clone)\",\"position\":{\"x\":-2.0,\"y\":-0.10000000149011612,\"z\":4.0},\"rotation\":{\"x\":0.0,\"y\":0.0,\"z\":0.0},\"isOccupied\":false},{\"name\":\"grass3(Clone)\",\"position\":{\"x\":0.0,\"y\":-0.10000000149011612,\"z\":-5.0},\"rotation\":{\"x\":0.0,\"y\":0.0,\"z\":0.0},\"isOccupied\":false},{\"name\":\"grass3(Clone)\",\"position\":{\"x\":-5.0,\"y\":-0.10000000149011612,\"z\":0.0},\"rotation\":{\"x\":0.0,\"y\":0.0,\"z\":0.0},\"isOccupied\":false},{\"name\":\"grass3(Clone)\",\"position\":{\"x\":-1.0,\"y\":-0.10000000149011612,\"z\":2.0},\"rotation\":{\"x\":0.0,\"y\":0.0,\"z\":0.0},\"isOccupied\":false},{\"name\":\"grass3(Clone)\",\"position\":{\"x\":-1.0,\"y\":-0.10000000149011612,\"z\":0.0},\"rotation\":{\"x\":0.0,\"y\":0.0,\"z\":0.0},\"isOccupied\":false},{\"name\":\"grass3(Clone)\",\"position\":{\"x\":-5.0,\"y\":-0.10000000149011612,\"z\":3.0},\"rotation\":{\"x\":0.0,\"y\":0.0,\"z\":0.0},\"isOccupied\":false},{\"name\":\"grass3(Clone)\",\"position\":{\"x\":-3.0,\"y\":-0.10000000149011612,\"z\":-5.0},\"rotation\":{\"x\":0.0,\"y\":0.0,\"z\":0.0},\"isOccupied\":false},{\"name\":\"grass3(Clone)\",\"position\":{\"x\":0.0,\"y\":-0.10000000149011612,\"z\":-1.0},\"rotation\":{\"x\":0.0,\"y\":0.0,\"z\":0.0},\"isOccupied\":false},{\"name\":\"grass3(Clone)\",\"position\":{\"x\":1.0,\"y\":-0.10000000149011612,\"z\":3.0},\"rotation\":{\"x\":0.0,\"y\":0.0,\"z\":0.0},\"isOccupied\":false},{\"name\":\"grass3(Clone)\",\"position\":{\"x\":-2.0,\"y\":-0.10000000149011612,\"z\":5.0},\"rotation\":{\"x\":0.0,\"y\":0.0,\"z\":0.0},\"isOccupied\":false},{\"name\":\"grass3(Clone)\",\"position\":{\"x\":-3.0,\"y\":-0.10000000149011612,\"z\":-4.0},\"rotation\":{\"x\":0.0,\"y\":0.0,\"z\":0.0},\"isOccupied\":false},{\"name\":\"grass3(Clone)\",\"position\":{\"x\":4.0,\"y\":-0.10000000149011612,\"z\":4.0},\"rotation\":{\"x\":0.0,\"y\":0.0,\"z\":0.0},\"isOccupied\":false},{\"name\":\"grass3(Clone)\",\"position\":{\"x\":-4.0,\"y\":-0.10000000149011612,\"z\":-3.0},\"rotation\":{\"x\":0.0,\"y\":0.0,\"z\":0.0},\"isOccupied\":false},{\"name\":\"grass3(Clone)\",\"position\":{\"x\":2.0,\"y\":-0.10000000149011612,\"z\":-4.0},\"rotation\":{\"x\":0.0,\"y\":0.0,\"z\":0.0},\"isOccupied\":false},{\"name\":\"grass3(Clone)\",\"position\":{\"x\":-5.0,\"y\":-0.10000000149011612,\"z\":5.0},\"rotation\":{\"x\":0.0,\"y\":0.0,\"z\":0.0},\"isOccupied\":false},{\"name\":\"grass3(Clone)\",\"position\":{\"x\":-2.0,\"y\":-0.10000000149011612,\"z\":2.0},\"rotation\":{\"x\":0.0,\"y\":0.0,\"z\":0.0},\"isOccupied\":false},{\"name\":\"grass3(Clone)\",\"position\":{\"x\":3.0,\"y\":-0.10000000149011612,\"z\":-5.0},\"rotation\":{\"x\":0.0,\"y\":0.0,\"z\":0.0},\"isOccupied\":false},{\"name\":\"grass3(Clone)\",\"position\":{\"x\":1.0,\"y\":-0.10000000149011612,\"z\":1.0},\"rotation\":{\"x\":0.0,\"y\":0.0,\"z\":0.0},\"isOccupied\":false},{\"name\":\"grass3(Clone)\",\"position\":{\"x\":2.0,\"y\":-0.10000000149011612,\"z\":5.0},\"rotation\":{\"x\":0.0,\"y\":0.0,\"z\":0.0},\"isOccupied\":false},{\"name\":\"grass3(Clone)\",\"position\":{\"x\":1.0,\"y\":-0.10000000149011612,\"z\":-5.0},\"rotation\":{\"x\":0.0,\"y\":0.0,\"z\":0.0},\"isOccupied\":false},{\"name\":\"grass3(Clone)\",\"position\":{\"x\":-2.0,\"y\":-0.10000000149011612,\"z\":-5.0},\"rotation\":{\"x\":0.0,\"y\":0.0,\"z\":0.0},\"isOccupied\":false},{\"name\":\"grass3(Clone)\",\"position\":{\"x\":3.0,\"y\":-0.10000000149011612,\"z\":4.0},\"rotation\":{\"x\":0.0,\"y\":0.0,\"z\":0.0},\"isOccupied\":false},{\"name\":\"grass3(Clone)\",\"position\":{\"x\":-2.0,\"y\":-0.10000000149011612,\"z\":0.0},\"rotation\":{\"x\":0.0,\"y\":0.0,\"z\":0.0},\"isOccupied\":false},{\"name\":\"grass2(Clone)\",\"position\":{\"x\":4.0,\"y\":-0.10000000149011612,\"z\":3.0},\"rotation\":{\"x\":0.0,\"y\":0.0,\"z\":0.0},\"isOccupied\":false},{\"name\":\"grass2(Clone)\",\"position\":{\"x\":5.0,\"y\":-0.10000000149011612,\"z\":3.0},\"rotation\":{\"x\":0.0,\"y\":0.0,\"z\":0.0},\"isOccupied\":false},{\"name\":\"grass2(Clone)\",\"position\":{\"x\":0.0,\"y\":-0.10000000149011612,\"z\":4.0},\"rotation\":{\"x\":0.0,\"y\":0.0,\"z\":0.0},\"isOccupied\":false},{\"name\":\"grass2(Clone)\",\"position\":{\"x\":2.0,\"y\":-0.10000000149011612,\"z\":2.0},\"rotation\":{\"x\":0.0,\"y\":0.0,\"z\":0.0},\"isOccupied\":false},{\"name\":\"grass2(Clone)\",\"position\":{\"x\":2.0,\"y\":-0.10000000149011612,\"z\":-1.0},\"rotation\":{\"x\":0.0,\"y\":0.0,\"z\":0.0},\"isOccupied\":false},{\"name\":\"grass2(Clone)\",\"position\":{\"x\":4.0,\"y\":-0.10000000149011612,\"z\":-2.0},\"rotation\":{\"x\":0.0,\"y\":0.0,\"z\":0.0},\"isOccupied\":false},{\"name\":\"grass2(Clone)\",\"position\":{\"x\":3.0,\"y\":-0.10000000149011612,\"z\":1.0},\"rotation\":{\"x\":0.0,\"y\":0.0,\"z\":0.0},\"isOccupied\":false},{\"name\":\"grass2(Clone)\",\"position\":{\"x\":4.0,\"y\":-0.10000000149011612,\"z\":-4.0},\"rotation\":{\"x\":0.0,\"y\":0.0,\"z\":0.0},\"isOccupied\":false},{\"name\":\"grass2(Clone)\",\"position\":{\"x\":5.0,\"y\":-0.10000000149011612,\"z\":-3.0},\"rotation\":{\"x\":0.0,\"y\":0.0,\"z\":0.0},\"isOccupied\":false},{\"name\":\"grass2(Clone)\",\"position\":{\"x\":4.0,\"y\":-0.10000000149011612,\"z\":0.0},\"rotation\":{\"x\":0.0,\"y\":0.0,\"z\":0.0},\"isOccupied\":false},{\"name\":\"grass2(Clone)\",\"position\":{\"x\":3.0,\"y\":-0.10000000149011612,\"z\":-3.0},\"rotation\":{\"x\":0.0,\"y\":0.0,\"z\":0.0},\"isOccupied\":false},{\"name\":\"grass2(Clone)\",\"position\":{\"x\":-1.0,\"y\":-0.10000000149011612,\"z\":-5.0},\"rotation\":{\"x\":0.0,\"y\":0.0,\"z\":0.0},\"isOccupied\":false},{\"name\":\"grass2(Clone)\",\"position\":{\"x\":2.0,\"y\":-0.10000000149011612,\"z\":-5.0},\"rotation\":{\"x\":0.0,\"y\":0.0,\"z\":0.0},\"isOccupied\":false},{\"name\":\"grass2(Clone)\",\"position\":{\"x\":5.0,\"y\":-0.10000000149011612,\"z\":-2.0},\"rotation\":{\"x\":0.0,\"y\":0.0,\"z\":0.0},\"isOccupied\":false},{\"name\":\"grass2(Clone)\",\"position\":{\"x\":0.0,\"y\":-0.10000000149011612,\"z\":5.0},\"rotation\":{\"x\":0.0,\"y\":0.0,\"z\":0.0},\"isOccupied\":false},{\"name\":\"grass2(Clone)\",\"position\":{\"x\":-5.0,\"y\":-0.10000000149011612,\"z\":2.0},\"rotation\":{\"x\":0.0,\"y\":0.0,\"z\":0.0},\"isOccupied\":false},{\"name\":\"grass2(Clone)\",\"position\":{\"x\":4.0,\"y\":-0.10000000149011612,\"z\":5.0},\"rotation\":{\"x\":0.0,\"y\":0.0,\"z\":0.0},\"isOccupied\":false},{\"name\":\"grass2(Clone)\",\"position\":{\"x\":-5.0,\"y\":-0.10000000149011612,\"z\":-2.0},\"rotation\":{\"x\":0.0,\"y\":0.0,\"z\":0.0},\"isOccupied\":false},{\"name\":\"grass2(Clone)\",\"position\":{\"x\":1.0,\"y\":-0.10000000149011612,\"z\":0.0},\"rotation\":{\"x\":0.0,\"y\":0.0,\"z\":0.0},\"isOccupied\":false},{\"name\":\"grass2(Clone)\",\"position\":{\"x\":5.0,\"y\":-0.10000000149011612,\"z\":-5.0},\"rotation\":{\"x\":0.0,\"y\":0.0,\"z\":0.0},\"isOccupied\":false},{\"name\":\"grass2(Clone)\",\"position\":{\"x\":5.0,\"y\":-0.10000000149011612,\"z\":4.0},\"rotation\":{\"x\":0.0,\"y\":0.0,\"z\":0.0},\"isOccupied\":false},{\"name\":\"grass2(Clone)\",\"position\":{\"x\":3.0,\"y\":-0.10000000149011612,\"z\":-1.0},\"rotation\":{\"x\":0.0,\"y\":0.0,\"z\":0.0},\"isOccupied\":false},{\"name\":\"grass2(Clone)\",\"position\":{\"x\":-5.0,\"y\":-0.10000000149011612,\"z\":1.0},\"rotation\":{\"x\":0.0,\"y\":0.0,\"z\":0.0},\"isOccupied\":false},{\"name\":\"grass2(Clone)\",\"position\":{\"x\":3.0,\"y\":-0.10000000149011612,\"z\":3.0},\"rotation\":{\"x\":0.0,\"y\":0.0,\"z\":0.0},\"isOccupied\":false},{\"name\":\"grass2(Clone)\",\"position\":{\"x\":-1.0,\"y\":-0.10000000149011612,\"z\":-1.0},\"rotation\":{\"x\":0.0,\"y\":0.0,\"z\":0.0},\"isOccupied\":false},{\"name\":\"grass2(Clone)\",\"position\":{\"x\":-5.0,\"y\":-0.10000000149011612,\"z\":4.0},\"rotation\":{\"x\":0.0,\"y\":0.0,\"z\":0.0},\"isOccupied\":false},{\"name\":\"grass2(Clone)\",\"position\":{\"x\":-1.0,\"y\":-0.10000000149011612,\"z\":3.0},\"rotation\":{\"x\":0.0,\"y\":0.0,\"z\":0.0},\"isOccupied\":false},{\"name\":\"grass2(Clone)\",\"position\":{\"x\":0.0,\"y\":-0.10000000149011612,\"z\":-2.0},\"rotation\":{\"x\":0.0,\"y\":0.0,\"z\":0.0},\"isOccupied\":false},{\"name\":\"grass2(Clone)\",\"position\":{\"x\":5.0,\"y\":-0.10000000149011612,\"z\":0.0},\"rotation\":{\"x\":0.0,\"y\":0.0,\"z\":0.0},\"isOccupied\":false},{\"name\":\"grass2(Clone)\",\"position\":{\"x\":4.0,\"y\":-0.10000000149011612,\"z\":-1.0},\"rotation\":{\"x\":0.0,\"y\":0.0,\"z\":0.0},\"isOccupied\":false},{\"name\":\"grass2(Clone)\",\"position\":{\"x\":1.0,\"y\":-0.10000000149011612,\"z\":-2.0},\"rotation\":{\"x\":0.0,\"y\":0.0,\"z\":0.0},\"isOccupied\":false},{\"name\":\"grass2(Clone)\",\"position\":{\"x\":-1.0,\"y\":-0.10000000149011612,\"z\":1.0},\"rotation\":{\"x\":0.0,\"y\":0.0,\"z\":0.0},\"isOccupied\":false},{\"name\":\"grass2(Clone)\",\"position\":{\"x\":2.0,\"y\":-0.10000000149011612,\"z\":0.0},\"rotation\":{\"x\":0.0,\"y\":0.0,\"z\":0.0},\"isOccupied\":false},{\"name\":\"grass2(Clone)\",\"position\":{\"x\":-4.0,\"y\":-0.10000000149011612,\"z\":-5.0},\"rotation\":{\"x\":0.0,\"y\":0.0,\"z\":0.0},\"isOccupied\":false},{\"name\":\"grass2(Clone)\",\"position\":{\"x\":1.0,\"y\":-0.10000000149011612,\"z\":2.0},\"rotation\":{\"x\":0.0,\"y\":0.0,\"z\":0.0},\"isOccupied\":false},{\"name\":\"grass2(Clone)\",\"position\":{\"x\":4.0,\"y\":-0.10000000149011612,\"z\":5.0},\"rotation\":{\"x\":0.0,\"y\":0.0,\"z\":0.0},\"isOccupied\":false},{\"name\":\"grass2(Clone)\",\"position\":{\"x\":3.0,\"y\":-0.10000000149011612,\"z\":-4.0},\"rotation\":{\"x\":0.0,\"y\":0.0,\"z\":0.0},\"isOccupied\":false},{\"name\":\"grass2(Clone)\",\"position\":{\"x\":4.0,\"y\":-0.10000000149011612,\"z\":1.0},\"rotation\":{\"x\":0.0,\"y\":0.0,\"z\":0.0},\"isOccupied\":false},{\"name\":\"grass2(Clone)\",\"position\":{\"x\":-3.0,\"y\":-0.10000000149011612,\"z\":5.0},\"rotation\":{\"x\":0.0,\"y\":0.0,\"z\":0.0},\"isOccupied\":false},{\"name\":\"grass2(Clone)\",\"position\":{\"x\":5.0,\"y\":-0.10000000149011612,\"z\":1.0},\"rotation\":{\"x\":0.0,\"y\":0.0,\"z\":0.0},\"isOccupied\":false},{\"name\":\"grass2(Clone)\",\"position\":{\"x\":-5.0,\"y\":-0.10000000149011612,\"z\":-1.0},\"rotation\":{\"x\":0.0,\"y\":0.0,\"z\":0.0},\"isOccupied\":false},{\"name\":\"grass2(Clone)\",\"position\":{\"x\":3.0,\"y\":-0.10000000149011612,\"z\":0.0},\"rotation\":{\"x\":0.0,\"y\":0.0,\"z\":0.0},\"isOccupied\":false},{\"name\":\"grass1(Clone)\",\"position\":{\"x\":0.0,\"y\":-0.10000000149011612,\"z\":2.0},\"rotation\":{\"x\":0.0,\"y\":0.0,\"z\":0.0},\"isOccupied\":false},{\"name\":\"grass1(Clone)\",\"position\":{\"x\":-4.0,\"y\":-0.10000000149011612,\"z\":-2.0},\"rotation\":{\"x\":0.0,\"y\":0.0,\"z\":0.0},\"isOccupied\":false},{\"name\":\"grass1(Clone)\",\"position\":{\"x\":-5.0,\"y\":-0.10000000149011612,\"z\":-4.0},\"rotation\":{\"x\":0.0,\"y\":0.0,\"z\":0.0},\"isOccupied\":false},{\"name\":\"grass1(Clone)\",\"position\":{\"x\":-1.0,\"y\":-0.10000000149011612,\"z\":4.0},\"rotation\":{\"x\":0.0,\"y\":0.0,\"z\":0.0},\"isOccupied\":false},{\"name\":\"grass1(Clone)\",\"position\":{\"x\":5.0,\"y\":-0.10000000149011612,\"z\":5.0},\"rotation\":{\"x\":0.0,\"y\":0.0,\"z\":0.0},\"isOccupied\":false},{\"name\":\"grass1(Clone)\",\"position\":{\"x\":0.0,\"y\":-0.10000000149011612,\"z\":1.0},\"rotation\":{\"x\":0.0,\"y\":0.0,\"z\":0.0},\"isOccupied\":false},{\"name\":\"grass1(Clone)\",\"position\":{\"x\":1.0,\"y\":-0.10000000149011612,\"z\":5.0},\"rotation\":{\"x\":0.0,\"y\":0.0,\"z\":0.0},\"isOccupied\":false},{\"name\":\"grass1(Clone)\",\"position\":{\"x\":4.0,\"y\":-0.10000000149011612,\"z\":-3.0},\"rotation\":{\"x\":0.0,\"y\":0.0,\"z\":0.0},\"isOccupied\":false},{\"name\":\"grass1(Clone)\",\"position\":{\"x\":-1.0,\"y\":-0.10000000149011612,\"z\":5.0},\"rotation\":{\"x\":0.0,\"y\":0.0,\"z\":0.0},\"isOccupied\":false},{\"name\":\"grass1(Clone)\",\"position\":{\"x\":2.0,\"y\":-0.10000000149011612,\"z\":1.0},\"rotation\":{\"x\":0.0,\"y\":0.0,\"z\":0.0},\"isOccupied\":false},{\"name\":\"grass1(Clone)\",\"position\":{\"x\":5.0,\"y\":-0.10000000149011612,\"z\":2.0},\"rotation\":{\"x\":0.0,\"y\":0.0,\"z\":0.0},\"isOccupied\":false},{\"name\":\"grass1(Clone)\",\"position\":{\"x\":2.0,\"y\":-0.10000000149011612,\"z\":3.0},\"rotation\":{\"x\":0.0,\"y\":0.0,\"z\":0.0},\"isOccupied\":false},{\"name\":\"grass1(Clone)\",\"position\":{\"x\":4.0,\"y\":-0.10000000149011612,\"z\":-5.0},\"rotation\":{\"x\":0.0,\"y\":0.0,\"z\":0.0},\"isOccupied\":false},{\"name\":\"grass1(Clone)\",\"position\":{\"x\":0.0,\"y\":-0.10000000149011612,\"z\":-3.0},\"rotation\":{\"x\":0.0,\"y\":0.0,\"z\":0.0},\"isOccupied\":false},{\"name\":\"grass1(Clone)\",\"position\":{\"x\":0.0,\"y\":-0.10000000149011612,\"z\":0.0},\"rotation\":{\"x\":0.0,\"y\":0.0,\"z\":0.0},\"isOccupied\":false},{\"name\":\"grass1(Clone)\",\"position\":{\"x\":-5.0,\"y\":-0.10000000149011612,\"z\":-5.0},\"rotation\":{\"x\":0.0,\"y\":0.0,\"z\":0.0},\"isOccupied\":false},{\"name\":\"grass1(Clone)\",\"position\":{\"x\":2.0,\"y\":-0.10000000149011612,\"z\":-2.0},\"rotation\":{\"x\":0.0,\"y\":0.0,\"z\":0.0},\"isOccupied\":false},{\"name\":\"grass1(Clone)\",\"position\":{\"x\":1.0,\"y\":-0.10000000149011612,\"z\":-4.0},\"rotation\":{\"x\":0.0,\"y\":0.0,\"z\":0.0},\"isOccupied\":false},{\"name\":\"grass1(Clone)\",\"position\":{\"x\":-2.0,\"y\":-0.10000000149011612,\"z\":1.0},\"rotation\":{\"x\":0.0,\"y\":0.0,\"z\":0.0},\"isOccupied\":false},{\"name\":\"grass1(Clone)\",\"position\":{\"x\":4.0,\"y\":-0.10000000149011612,\"z\":2.0},\"rotation\":{\"x\":0.0,\"y\":0.0,\"z\":0.0},\"isOccupied\":false},{\"name\":\"grass1(Clone)\",\"position\":{\"x\":2.0,\"y\":-0.10000000149011612,\"z\":-3.0},\"rotation\":{\"x\":0.0,\"y\":0.0,\"z\":0.0},\"isOccupied\":false},{\"name\":\"grass1(Clone)\",\"position\":{\"x\":1.0,\"y\":-0.10000000149011612,\"z\":-1.0},\"rotation\":{\"x\":0.0,\"y\":0.0,\"z\":0.0},\"isOccupied\":false},{\"name\":\"grass1(Clone)\",\"position\":{\"x\":5.0,\"y\":-0.10000000149011612,\"z\":5.0},\"rotation\":{\"x\":0.0,\"y\":0.0,\"z\":0.0},\"isOccupied\":false},{\"name\":\"grass1(Clone)\",\"position\":{\"x\":-2.0,\"y\":-0.10000000149011612,\"z\":3.0},\"rotation\":{\"x\":0.0,\"y\":0.0,\"z\":0.0},\"isOccupied\":false},{\"name\":\"grass1(Clone)\",\"position\":{\"x\":0.0,\"y\":-0.10000000149011612,\"z\":3.0},\"rotation\":{\"x\":0.0,\"y\":0.0,\"z\":0.0},\"isOccupied\":false},{\"name\":\"grass1(Clone)\",\"position\":{\"x\":-5.0,\"y\":-0.10000000149011612,\"z\":-3.0},\"rotation\":{\"x\":0.0,\"y\":0.0,\"z\":0.0},\"isOccupied\":false},{\"name\":\"grass1(Clone)\",\"position\":{\"x\":2.0,\"y\":-0.10000000149011612,\"z\":4.0},\"rotation\":{\"x\":0.0,\"y\":0.0,\"z\":0.0},\"isOccupied\":false},{\"name\":\"grass1(Clone)\",\"position\":{\"x\":1.0,\"y\":-0.10000000149011612,\"z\":4.0},\"rotation\":{\"x\":0.0,\"y\":0.0,\"z\":0.0},\"isOccupied\":false},{\"name\":\"grass1(Clone)\",\"position\":{\"x\":5.0,\"y\":-0.10000000149011612,\"z\":-4.0},\"rotation\":{\"x\":0.0,\"y\":0.0,\"z\":0.0},\"isOccupied\":false},{\"name\":\"grass1(Clone)\",\"position\":{\"x\":3.0,\"y\":-0.10000000149011612,\"z\":2.0},\"rotation\":{\"x\":0.0,\"y\":0.0,\"z\":0.0},\"isOccupied\":false},{\"name\":\"grass1(Clone)\",\"position\":{\"x\":3.0,\"y\":-0.10000000149011612,\"z\":-2.0},\"rotation\":{\"x\":0.0,\"y\":0.0,\"z\":0.0},\"isOccupied\":false},{\"name\":\"grass1(Clone)\",\"position\":{\"x\":5.0,\"y\":-0.10000000149011612,\"z\":-1.0},\"rotation\":{\"x\":0.0,\"y\":0.0,\"z\":0.0},\"isOccupied\":false},{\"name\":\"grass1(Clone)\",\"position\":{\"x\":-4.0,\"y\":-0.10000000149011612,\"z\":5.0},\"rotation\":{\"x\":0.0,\"y\":0.0,\"z\":0.0},\"isOccupied\":false},{\"name\":\"grass1(Clone)\",\"position\":{\"x\":3.0,\"y\":-0.10000000149011612,\"z\":5.0},\"rotation\":{\"x\":0.0,\"y\":0.0,\"z\":0.0},\"isOccupied\":false}]}`;

// Endpoint to create a map for a user
const createMap = async (req, res) => {
    try {
        // Retrieve user ID from the request parameters
        const userID = req.params.userID;
       
        // Fetch map data from the request body or use default base map data
        const mapData = req.body.mapData || baseMapData;

        // Fetch map name from the request body or set a default name
        const mapName = req.body.mapName || 'Map1'; 

        // Create a new map document in the database
        const map = await Map.create({ userID, mapData, mapName});

        // Find the user associated with the map
        const user = await User.findById(userID);
        
        // If user is not found, return a 404 status
        if (!user) {
            return res.status(404).json({ message: 'User not found' });
        }

        // Format map details and update the user's maps list
        const mapDetails = `${map._id}:${mapName}`;
        user.maps.push(mapDetails);
        await user.save();

        // Respond with the map ID
        res.status(201).json({mapID: map._id});
    } catch (e) {
        // Handle errors during map creation
        res.status(400).json({ message: 'Error creating map' });
    }
};

// Endpoint to update map data
const saveMap = async (req, res) => {
    try{
        // Retrieve map ID from request parameters
        const mapID = req.params.mapID;

        // Find the map by ID
        const map = await Map.findById(mapID);
        map.mapData = req.body.mapData;
        await map.save();

        // Respond with the updated map
        res.status(201).json(map);
    } catch (e) {
        // Handle errors during map update
        res.status(400).json({ message: 'Error Updating Map' });
    }

}

// Endpoint to load all maps associated with a user
const loadAllMaps = (req, res) => {
    const userID = req.params.userID;
    Map.find({ userID: userID })
    .then(maps => {
        // Respond with all maps belonging to the user
        res.json({ maps });
    })
    .catch(error => {
        // Handle errors during map retrieval
        console.error(error);
        res.status(500).json({ message: 'showAllMatch Error' });
    });
};

// Endpoint to load a specific map by its ID
const loadMap = (req, res) => {
    const mapID = req.params.mapID;
    
    Map.findById( mapID )
    .then(maps => {
        // Respond with the map data
        res.json({ mapData: maps.mapData });
    })
    .catch(error => {
        // Handle errors during map retrieval
        console.error(error);
        res.status(500).json({ message: 'showAllMatch Error' });
    });
};

// Function to delete a map by its ID
const deleteMap = (req, res) => {
    // Extract the map ID from request parameters
    const mapID = req.params.mapID;

    // Find the map by ID and delete it
    Map.findByIdAndDelete(mapID)
    .then(deletedMap => {
        // If map doesn't exist, return a 404 error
        if (!deletedMap) {
            return res.status(404).json({ message: 'Map not found' });
        }

        // If deletion is successful, respond with success message and the deleted map
        res.json({ message: 'Item successfully deleted', map: deletedMap });
    })
    .catch(error => {

        // If an error occurs during deletion, return a 500 error
        res.status(500).json({ message: 'Error deleting map' });
    });
};

// Middleware function to get user ID from request parameters
const getUserIDMiddleware = async (req, res, next) => {
    // Extract the user ID from request parameters
    const userID = req.params.userID;

    // Find the user by ID in the database
    const user = await User.findById(userID);
    if (user) {
        // If user exists, proceed to the next middleware or route handler
        next();
    } else {
        // If user is not found, return a 400 error
        res.status(400).send('User not found');
    }
};

// Middleware function to get map ID from request parameters
const getMapIDMiddleware = async (req, res, next) => {
     // Extract the map ID from request parameters
    const mapID = req.params.mapID;

    // Find the map by ID in the database
    const map = await Map.findById(mapID);
    if (map) {

        // If map exists, attach the map's ID to the request object and proceed to next middleware or route handler
        req.mapID = map._id;  
        next();
    } else {

        // If map is not found, return a 400 error
        res.status(400).send('Map not found');
    }
};


// This file contains the below 6 functions
module.exports = {
    createMap, deleteMap, getUserIDMiddleware, getMapIDMiddleware, saveMap, loadAllMaps, loadMap
};
