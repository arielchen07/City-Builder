const express = require('express');
const router = express.Router();

const {
    createItem,
    decOne,
    addOne,
    deleteItem,
    getUserIDMiddleware,
    showAllMatch,
    getItemIDMiddleware
} = require('../controllers/ItemController');

router.post('/:userID/create', getUserIDMiddleware, createItem);
router.post('/:userID/dec/:itemID', getUserIDMiddleware, getItemIDMiddleware, decOne);
router.post('/:userID/inc/:itemID', getUserIDMiddleware, getItemIDMiddleware, addOne);
router.post('/:userID/all', getUserIDMiddleware, showAllMatch);
router.post('/:userID/delete/:itemID', getUserIDMiddleware, getItemIDMiddleware, deleteItem);

module.exports = router;