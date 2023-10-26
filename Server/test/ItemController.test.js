const chai = require('chai');
const expect = chai.expect;
const supertest = require('supertest');
const app = require('../server');
const User = require('../models/UserModel');
const Item = require('../models/InventoryModel');
const { createItem } = require('../controllers/ItemController');
const ItemRoute = require('../routes/ItemRoute'); 

const request = supertest(app);

describe('Item Creation', () => {
    let userID;

    before(async () => {
        const user = new User({
            name: 'Test User',
            email: 'testuser@example.com',
            password: 'testpassword123',
        });
        const savedUser = await user.save();
        userID = savedUser._id;
    });

    it('should successfully create an item for a user', async () => {
        const newItem = {
            quantity: 10
        };

        const response = await request
            .post(`/api/${userID}/create`)
            .send(newItem)
            .expect(201);

        expect(response.body).to.be.an('object');
        expect(response.body.userID).to.equal(String(userID));
        expect(response.body.quantity).to.equal(newItem.quantity);
    });

    after(async () => {
        await User.findByIdAndDelete(userID);
    });
});

describe('Item Operations', () => {
    let userID;
    let itemID;

    before(async () => {
        const user = new User({
            name: 'Test User 2',
            email: 'testuser2@example.com',
            password: 'testpassword123',
        });
        const savedUser = await user.save();
        userID = savedUser._id;

        const newItem = {
            quantity: 5,
            userID: userID
        };

        const response = await request
            .post(`/api/${userID}/create`)
            .send(newItem)
            .expect(201);

        itemID = response.body._id;
    });

    it('should increase the item quantity by one', async () => {
        const response = await request
            .post(`/api/${userID}/inc/${itemID}`)
            .expect(200);

        expect(response.body).to.be.an('object');
        expect(response.body.item.quantity).to.equal(6);

        const updatedItem = await Item.findById(itemID);
        expect(updatedItem.quantity).to.equal(6);
    });

    it('should decrease the item quantity by one', async () => {
        const response = await request
            .post(`/api/${userID}/dec/${itemID}`)
            .expect(200);

        expect(response.body).to.be.an('object');
        expect(response.body.item.quantity).to.equal(5);

        const updatedItem = await Item.findById(itemID);
        expect(updatedItem.quantity).to.equal(5);
    });

    after(async () => {
        await User.findByIdAndDelete(userID);
        await Item.findByIdAndDelete(itemID);
    });
});
