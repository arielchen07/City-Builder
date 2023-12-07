const express = require('express');
const Item = require('../models/InventoryModel');
const User = require('../models/UserModel');
const jwt = require('jsonwebtoken');

const createItem = async (req, res) => {
    try {
        const userID = req.params.userID;
        const quantity = req.body.quantity || 2;
        const category = req.body.category;
        const name = req.body.name;
        const item = await Item.create({ userID, quantity, category, name });
        const user = await User.findById(userID);
        
        user.items.push(item._id);
        await user.save();
        res.status(201).json(item);
    } catch (e) {
        res.status(400).json({ message: 'Error creating item' });
    }
};

const showAllMatch = (req, res) => {
    const userID = req.params.userID;
    Item.find({ userID: userID })
    .then(items => {
        res.json({ items });
    })
    .catch(error => {
        console.error(error);
        res.status(500).json({ message: 'showAllMatch Error' });
    });
};

const dec = (req, res) => {
    const itemID = req.params.itemID;
    const quantity = req.body.quantity || -1;
    Item.findById(itemID)
    .then(item => {
        if (!item) {
            res.status(404).json({ message: 'Item not found' });
            throw new Error('Item not found');
        }

        if (item.quantity <= 0) {
            res.status(400).json({ message: 'Item quantity is already 0 or less' });
            throw new Error('Item quantity is already 0 or less');
        }

        item.quantity += quantity;
        return item.save();
    })
    .then(updatedItem => {
        res.json(updatedItem);
    })
    .catch(error => {
        if (['Item not found', 'Item quantity is already 0 or less'].includes(error.message)) return;
        res.status(500).json({ message: 'decrease error' });
    });
};

const add = (req, res) => {
    const itemID = req.params.itemID;
    const quantity = req.body.quantity || 1;
    Item.findById(itemID)
    .then(item => {
        if (!item) {
            res.status(404).json({ message: 'Item not found' });
            throw new Error('Item not found');
        }
        item.quantity += quantity;
        return item.save();
    })
    .then(updatedItem => {
        res.json(updatedItem);
    })
    .catch(error => {
        if (error.message === 'Item not found') return;
        res.status(500).json({ message: 'increase error' });
    });
};

const deleteItem = (req, res) => {
    const itemID = req.params.itemID;
    Item.findByIdAndDelete(itemID)
    .then(deletedItem => {
        if (!deletedItem) {
            return res.status(404).json({ message: 'Item not found' });
        }
        res.json({ message: 'Item successfully deleted', item: deletedItem });
    })
    .catch(error => {
        res.status(500).json({ message: 'Error deleting item' });
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
 
const getItemIDMiddleware = async (req, res, next) => {
    const itemID = req.params.itemID;
    const item = await Item.findById(itemID);
    if (item) {
        req.itemID = item._id;  
        next();
    } else {
        res.status(400).send('Item not found');
    }
};

const getOneUID = async (req, res) => {
    // This funciton is only for testing
    try {
        const count = await User.countDocuments();
        const random = Math.floor(Math.random() * count); 
        const user = await User.findOne().skip(random); 
        if (!user) {
            return res.status(404).json({ message: 'No users found' });
        }
        res.json({ userID: user._id });
    } catch (e) {
        res.status(500).json({ message: 'Error fetching random user ID' });
    }
};

module.exports = {
    createItem, showAllMatch, dec, add, deleteItem, getUserIDMiddleware, getItemIDMiddleware, getOneUID 
};
