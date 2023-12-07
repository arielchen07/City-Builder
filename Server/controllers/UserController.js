const express = require('express');
const User = require('../models/UserModel');
const Item = require('../models/InventoryModel')
const bcrypt = require('bcryptjs');
const jwt = require('jsonwebtoken');





// Registers a new user along with default items
const register = async(req, res) => {
    
       
    try {
        // Extract user details from the request body
        const {name, email, password} = req.body;
    
        // Check if a user with the provided email already exists
        const existingUser = await User.findOne({ email });
        if (existingUser) {
            return res.status(400).json({ error: "User with this email already exists" });
          }
        //If a new email, create user
        const user = await User.create({name, email, password});

        // Create default items for the user across various categories
        const item1 = await Item.create({userID: user._id, quantity:2, category: "housing", name: "singleHouse"});
        const item2 = await Item.create({userID: user._id, quantity:2, category: "energy", name: "coalPlant"});
        const item3 = await Item.create({userID: user._id, quantity:2, category: "energy", name: "windTurbineGenerator"});
        const item4 = await Item.create({userID: user._id, quantity:2, category: "energy", name: "solarEnergyPlant"});
        const item5 = await Item.create({userID: user._id, quantity:2, category: "water", name: "waterTower"});
        const item6 = await Item.create({userID: user._id, quantity:2, category: "sewage", name: "sewageTreatment"});
	const item7 = await Item.create({userID: user._id, quantity:2, category: "internet", name: "internetTower"});
        const item8 = await Item.create({userID: user._id, quantity:0, category: "resource", name: "wood"});
        const item9 = await Item.create({userID: user._id, quantity:0, category: "resource", name: "stone"});
        const item10 = await Item.create({userID: user._id, quantity:0, category: "resource", name: "metal"});
        const item11 = await Item.create({userID: user._id, quantity:0, category: "resource", name: "coins"});
        const item12 = await Item.create({userID: user._id, quantity:2, category: "harvester", name: "lumberYard"});
        const item13 = await Item.create({userID: user._id, quantity:2, category: "harvester", name: "stoneQuarry"});
        const item14 = await Item.create({userID: user._id, quantity:1, category: "harvester", name: "townHall"});
        const item15 = await Item.create({userID: user._id, quantity:2, category: "gas", name: "gasDistributor"});

        // Update user's item list with the default items
        user.items.push(item1._id);
        user.items.push(item2._id);
        user.items.push(item3._id);
        user.items.push(item4._id);
        user.items.push(item5._id);
        user.items.push(item6._id);
	user.items.push(item7._id);
        user.items.push(item8._id);
        user.items.push(item9._id);
        user.items.push(item10._id);
        user.items.push(item11._id);
        user.items.push(item12._id);
        user.items.push(item13._id);
        user.items.push(item14._id);
	user.items.push(item15._id);

        //save the changes
        await user.save();
        
        //Respond with the newly created user's ID
        res.status(201).json({ userID: user._id });
      } catch (e) {
        let msg;
        
        //Handle errors if any occur during the process
        res.status(400).json(msg)
    }
        
    };


// User login functionality
const login = async (req, res) => {
  try {
    // Extract email and password from the request body
    const { email, password } = req.body;
    
    // Find the user by email
    const user = await User.findOne({ email });

    // Check if the user exists and if the password matches
    if (!user) {
      return res.status(401).json({ error: 'User not found' });
    }
    const isPasswordValid = await bcrypt.compare(password, user.password);

    // If not, return authentication error
    if (!isPasswordValid) {
      return res.status(401).json({ error: 'Password not match' });
    }

    // If yes, set user status as online and respond with user ID and map ID
    user.status = 'online';
    await user.save();
    res.status(200).json({ userID: user._id, mapID: user.maps[0]});
  } catch (e) {

    // Handle errors if any occur during the process
    res.status(500).json({ error: 'Internal server error' });
  }
};

// Logs out the user by setting the user status as offline
const logout = async (req, res) => {
  try {
    // Extract user ID from the request parameters
    const userID = req.params.userID;

    // Find the user by ID
    const user = await User.findById(userID);

    // If user doesn't exist, return user not found error
    if (!user) {
      return res.status(404).json({ message: 'User not found' });
    }

    // If user exists, set status to offline and save the changes
    user.status = 'offline';
    await user.save();

    res.json({ message: 'User logged out successfully' });
  } catch (error) {
    console.error(error);
    res.status(500).json({ message: 'Internal Server Error' });
  }
};


// Retrieves user profile by user ID
  const userProfile = async (req, res) => {

    const userID = req.params.userID;
    
    // Find the user by ID
    User.findById(userID)
      .then(user => {
        if (!user) {
          // If user is not found, return a 404 error
          return res.status(404).json({ message: 'User not found' });
        }

        // Return the user's profile
        res.json(user);
      })
      .catch(error => {

        // Handle any errors that occur during the process
        res.status(500).json({ message: 'Internal Server Error' });
      });
  };

  
  
  //This module contains 4 user functions: register, login, userProfile, logout
module.exports = {register, login, userProfile, logout};

