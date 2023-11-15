const chai = require('chai');
const expect = chai.expect;
const supertest = require('supertest');
const app = require('../server');
const User = require('../models/UserModel');
const Map = require('../models/mapModel');

const { createMap } = require('../controllers/MapController');
const MapRoute = require('../routes/MapRoute'); 

const request = supertest(app);

describe('Map Creation', () => {
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

    it('should successfully create an Map for a user', async () => {
        const newMap = {
            mapData: "testMapData",
            mapName: "testMapName"
        };

        const response = await request
            .post(`/api/${userID}/createmap`)
            .send(newMap)
            .expect(201);

        expect(response.body).to.be.an('object');
        expect(response.body.mapID).to.be.a('string');
        // expect(response.body.mapData).to.equal(newMap.mapData);
    });

    after(async () => {
        await User.findByIdAndDelete(userID);
    });
});

describe('Map Operations', () => {
    let userID;
    let mapID;

    before(async () => {
        const user = new User({
            name: 'Test User 2',
            email: 'testuser2@example.com',
            password: 'testpassword123',
        });
        const savedUser = await user.save();
        userID = savedUser._id;

        const newMap = {
            
            userID: userID,
            mapData: "testMapData2",
            mapName: "testMapName2"
        };

        const response = await request
            .post(`/api/${userID}/createmap`)
            .send(newMap)
            .expect(201);

        mapID = response.body.mapID;
    });

    
    it('should delete a map', async () => {
       

        console.log(`Attempting to delete map with ID: ${mapID} for user: ${userID}`);
        const response = await request
            .post(`/api/${userID}/deletemap/${mapID}`)
            .expect(200);

        console.log('Delete response:', response.body);

        const findMap = await Map.findById(mapID);
        expect(findMap).to.be.null;
    });



    after(async () => {
        await User.findByIdAndDelete(userID);
    });
});


