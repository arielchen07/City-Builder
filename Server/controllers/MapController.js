const express = require('express');
const Map = require('../models/mapModel');
const User = require('../models/UserModel');
const jwt = require('jsonwebtoken');

const createMap = async (req, res) => {
    try {
        const userID = req.params.userID;
        console.log(userID);
        const mapData = req.body.mapData;
        const map = await Map.create({ userID, mapData});
        const user = await User.findById(userID);

        // if (!user) {
        //     return res.status(404).json({ message: 'User not found' });
        // }

        user.maps.push(map._id);
        await user.save();
        res.status(201).json(map);
    } catch (e) {
        res.status(400).json({ message: 'Error creating map' });
    }
};

const saveMap = async (req, res) => {
    try{
        // const temp = "abcdefg"
        const mapID = req.params.mapID;
        const map = await Map.findById(mapID);
        map.mapData = req.body.mapData;
        await map.save();
        res.status(201).json(map);
    } catch (e) {
        res.status(400).json({ message: 'Error Updating Map' });
    }

}

const loadAllMaps = (req, res) => {
    const userID = req.params.userID;
    Map.find({ userID: userID })
    .then(maps => {
        res.json({ maps });
    })
    .catch(error => {
        console.error(error);
        res.status(500).json({ message: 'showAllMatch Error' });
    });
};

const loadMap = (req, res) => {
    const mapID = req.params.mapID;
    
    Map.findById( mapID )
    .then(maps => {
        res.json(maps.mapData);
    })
    .catch(error => {
        console.error(error);
        res.status(500).json({ message: 'showAllMatch Error' });
    });
};

// const decOne = (req, res) => {
//     const itemID = req.params.itemID;
//     Item.findById(itemID)
//     .then(item => {
//         if (!item) {
//             res.status(404).json({ message: 'Item not found' });
//             throw new Error('Item not found');
//         }

//         if (item.quantity <= 0) {
//             res.status(400).json({ message: 'Item quantity is already 0 or less' });
//             throw new Error('Item quantity is already 0 or less');
//         }

//         item.quantity -= 1;
//         return item.save();
//     })
//     .then(updatedItem => {
//         res.json({ item: updatedItem });
//     })
//     .catch(error => {
//         if (['Item not found', 'Item quantity is already 0 or less'].includes(error.message)) return;
//         res.status(500).json({ message: 'decrease error' });
//     });
// };

// const addOne = (req, res) => {
//     const itemID = req.params.itemID;
//     Item.findById(itemID)
//     .then(item => {
//         if (!item) {
//             res.status(404).json({ message: 'Item not found' });
//             throw new Error('Item not found');
//         }
//         item.quantity += 1;
//         return item.save();
//     })
//     .then(updatedItem => {
//         res.json({ item: updatedItem });
//     })
//     .catch(error => {
//         if (error.message === 'Item not found') return;
//         res.status(500).json({ message: 'increase error' });
//     });
// };




const deleteMap = (req, res) => {
    const mapID = req.params.mapID;
    Map.findByIdAndDelete(mapID)
    .then(deletedMap => {
        if (!deletedMap) {
            return res.status(404).json({ message: 'Map not found' });
        }
        res.json({ message: 'Item successfully deleted', map: deletedMap });
    })
    .catch(error => {
        res.status(500).json({ message: 'Error deleting map' });
    });
};

const getUserIDMiddleware = async (req, res, next) => {
    const userID = req.params.userID;
    const user = await User.findById(userID);
    if (user) {
        next();
    } else {
        res.status(400).send('User not found');
    }
};

const getMapIDMiddleware = async (req, res, next) => {
    const mapID = req.params.mapID;
    const map = await Map.findById(mapID);
    if (map) {
        req.mapID = map._id;  
        next();
    } else {
        res.status(400).send('Map not found');
    }
};

// const getOneUID = async (req, res) => {
//     // This funciton is only for testing
//     try {
//         const count = await User.countDocuments();
//         const random = Math.floor(Math.random() * count); 
//         const user = await User.findOne().skip(random); 
//         if (!user) {
//             return res.status(404).json({ message: 'No users found' });
//         }
//         res.json({ userID: user._id });
//     } catch (e) {
//         res.status(500).json({ message: 'Error fetching random user ID' });
//     }
// };

module.exports = {
    createMap, deleteMap, getUserIDMiddleware, getMapIDMiddleware, saveMap, loadAllMaps, loadMap
};
