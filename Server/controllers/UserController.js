const express = require('express');
const User = require('../models/UserModel');
const Item = require('../models/InventoryModel')
const bcrypt = require('bcryptjs');
const jwt = require('jsonwebtoken');





// Register function
const register = async(req, res) => {
    
       
    try {
        const {name, email, password} = req.body;
        console.log(req.body);
        // Check if a user with the provided email already exists
        const existingUser = await User.findOne({ email });
        if (existingUser) {
            return res.status(400).json({ error: "User with this email already exists" });
          }
        //If a new email, create user
        const user = await User.create({name, email, password});
        const item1 = await Item.create({userID: user._id, quantity:2, category: "housing", name: "singleHouse"});
        const item2 = await Item.create({userID: user._id, quantity:2, category: "energy", name: "coalPlant"});
        const item3 = await Item.create({userID: user._id, quantity:2, category: "energy", name: "windTurbineGenerator"});
        const item4 = await Item.create({userID: user._id, quantity:2, category: "energy", name: "solarEnergyPlant"});
        const item5 = await Item.create({userID: user._id, quantity:2, category: "water", name: "waterTower"});
        const item6 = await Item.create({userID: user._id, quantity:2, category: "sewage", name: "sewageTreatment"});

        const item7 = await Item.create({userID: user._id, quantity:0, category: "resource", name: "wood"});
        const item8 = await Item.create({userID: user._id, quantity:0, category: "resource", name: "stone"});
        const item9 = await Item.create({userID: user._id, quantity:0, category: "resource", name: "metal"});

        const item10 = await Item.create({userID: user._id, quantity:2, category: "harvester", name: "lumberYard"});
        const item11 = await Item.create({userID: user._id, quantity:2, category: "harvester", name: "stoneQuary"});
        const item12 = await Item.create({userID: user._id, quantity:0, category: "resource", name: "coins"});

        const item13 = await Item.create({userID: user._id, quantity:2, category: "havester", name: "townHall"});
        const item14 = await Item.create({userID: user._id, quantity:2, category: "gas", name: "gasDistributor"});

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

        user.status = "online";
        await user.save();
        
        // res.status(201).json({user, item1});
        res.status(201).json({ userID: user._id });
      } catch (e) {
        let msg;
        // if(e.code == 11000){
        //   msg = "User already exists"
        // } else {
        //   msg = e.message;
        // }
        console.log(e);
        res.status(400).json(msg)
    }
        
    };



// const login = (req, res, next) =>{
//     var email = req.body.email
//     var password = req.body.password

//     User.findOne({email})
//     .then(user => {
//         if(user){
//             if(password != user.password){
//                 // console.log('password:', password);
//                 // console.log('user.password', user.password);
//                 res.json({
//                     message:'Password does not matched!'
//                 })
//             }else{
//                 res.json({
//                     message:'Login Successfully!'
//                 })
//             }
//         }else{
//             res.json({
//                 message: 'No user found!'
//             })
//         }
//     })
// }

const login = async (req, res) => {
  try {
    const { email, password } = req.body;
    console.log(req.body)
    const user = await User.findOne({ email });

    if (!user) {
      return res.status(401).json({ error: 'User not found' });
    }
    const isPasswordValid = await bcrypt.compare(password, user.password);

    if (!isPasswordValid) {
      return res.status(401).json({ error: 'Password not match' });
    }
    user.status = 'online';
    await user.save();
    
    // const token = jwt.sign({ userId: user._id }, 'your-secret-key', { expiresIn: '1h' });

    res.status(200).json({ userID: user._id, mapID: user.maps[0]});
  } catch (e) {
    console.error(e);
    res.status(500).json({ error: 'Internal server error' });
  }
};

const logout = async (req, res) => {
  try {
    const userID = req.params.userID;
    const user = await User.findById(userID);

    if (!user) {
      return res.status(404).json({ message: 'User not found' });
    }

    user.status = 'offline';
    await user.save();

    res.json({ message: 'User logged out successfully' });
  } catch (error) {
    console.error(error);
    res.status(500).json({ message: 'Internal Server Error' });
  }
};



  const userProfile = async (req, res) => {
    const userID = req.params.userID;
    console.log(userID)
    User.findById(userID)
      .then(user => {
        if (!user) {
          return res.status(404).json({ message: 'User not found' });
        }
        res.json(user);
      })
      .catch(error => {
        console.error(error);
        res.status(500).json({ message: 'Internal Server Error' });
      });
  };

  
  
  
module.exports = {register, login, userProfile, logout};

