const express = require('express');
const router = express.Router();

const {
    createItem,
    dec,
    add,
    deleteItem,
    getUserIDMiddleware,
    showAllMatch,
    getItemIDMiddleware
} = require('../controllers/ItemController');

// Routes using different HTTP methods and corresponding controller functions
router.post('/:userID/create', getUserIDMiddleware, createItem);// Create an item for a specific user
router.post('/:userID/dec/:itemID', getUserIDMiddleware, getItemIDMiddleware, dec);// Decrease quantity of a specific item for a user
router.post('/:userID/inc/:itemID', getUserIDMiddleware, getItemIDMiddleware, add);// Increase quantity of a specific item for a user
router.get('/:userID/all', getUserIDMiddleware, showAllMatch);// Retrieve all items for a user
router.post('/:userID/delete/:itemID', getUserIDMiddleware, getItemIDMiddleware, deleteItem);// Delete a specific item for a user

// Exporting the router containing the item-related routes
module.exports = router;