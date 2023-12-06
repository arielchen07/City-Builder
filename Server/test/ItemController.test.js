const chai = require('chai');
const expect = chai.expect;
const supertest = require('supertest');
const app = require('../server');
const User = require('../models/UserModel');
const Item = require('../models/InventoryModel');
const { createItem } = require('../controllers/ItemController');
const ItemRoute = require('../routes/ItemRoute'); 

const request = supertest(app);

// Test suite for Item Creation
describe('Item Creation Test', () => {
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

// Test suite for Item Retrieval
describe('Item retrival Test', () => {
    let userID;

    before(async () => {
        const user = new User({
            name: 'Test User 4',
            email: 'testuser@example.com',
            password: 'testpassword123',
        });
        const savedUser = await user.save();
        userID = savedUser._id;
    });

    after(async () => {
        await User.findByIdAndDelete(userID);
    });
    it('should return all item for the user', async () => {
        const newItem = {
            quantity: 10
        };

        const response = await request
            .get(`/api/${userID}/all`)
            .send(newItem)
            .expect(200);

         
        expect(response.body).to.be.an('object'); 
        expect(response.body.items).to.be.an('array');
        response.body.items.forEach(item => {
            expect(item).to.be.an('object');
            expect(item._id).to.be.a('string');
            expect(item.userID).to.equal(String(userID));
            expect(item.quantity).to.be.a('number');
            expect(item.category).to.be.a('string');
            expect(item.name).to.be.a('string');
            expect(item.createdAt).to.be.a('string');
            expect(item.updatedAt).to.be.a('string');
        });
    });

    after(async () => {
        await User.findByIdAndDelete(userID);
    });
});

// Test suite for Increase/Decrease Quantity
describe('Increase Decrease Quantity Test', () => {
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
            
            quantity: 5
            
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
        expect(response.body.quantity).to.equal(6);

        const updatedItem = await Item.findById(itemID);
        expect(updatedItem.quantity).to.equal(6);
    });

    it('should decrease the item quantity by one', async () => {
        const response = await request
            .post(`/api/${userID}/dec/${itemID}`)
            .expect(200);

        expect(response.body).to.be.an('object');
        expect(response.body.quantity).to.equal(5);

        const updatedItem = await Item.findById(itemID);
        expect(updatedItem.quantity).to.equal(5);
    });

    after(async () => {
        await User.findByIdAndDelete(userID);
        await Item.findByIdAndDelete(itemID);
    });
});

// Test suite for Item Deletion
describe('Item Deletion Test', () => {
    let userID;
    let itemID;

    before(async () => {
       
        const user = new User({
            name: 'Test User 3',
            email: 'testuser3@example.com',
            password: 'testpassword123',
        });
        const savedUser = await user.save();
        userID = savedUser._id;
        console.log("UserID (after creation):", userID); 
 
        const newItem = {
            quantity: 10,
            userID: userID
        };
        const createdItem = await request
            .post(`/api/${userID}/create`)
            .send(newItem)
            .expect(201);
        itemID = createdItem.body._id;
        console.log("ItemID (after creation):", itemID); 
    });

    it('should successfully delete an item for a user', async () => {
 
        const deleteResponse = await request
            .post(`/api/${userID}/delete/${itemID}`)
            .expect(200);

        expect(deleteResponse.body).to.be.an('object');
        expect(deleteResponse.body.message).to.equal('Item successfully deleted');
        expect(deleteResponse.body.item._id).to.equal(itemID);

        
        const deletedItem = await Item.findById(itemID);
        expect(deletedItem).to.be.null;
    });

    after(async () => {
        await User.findByIdAndDelete(userID);
    });
});