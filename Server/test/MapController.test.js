const chai = require('chai');
const expect = chai.expect;
const supertest = require('supertest');
const app = require('../server');
const User = require('../models/UserModel');
const Map = require('../models/mapModel');

const { createMap } = require('../controllers/MapController');
const MapRoute = require('../routes/MapRoute'); 

const request = supertest(app);

// Test suite for Map Creation
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
        mapID = response.body.mapID;

        expect(response.body).to.be.an('object');
        expect(response.body.mapID).to.equal(mapID);
        // expect(response.body.mapData).to.equal(newMap.mapData);
    });

    after(async () => {
        await User.findByIdAndDelete(userID);
    });
});

// Test suite for Map Deletion
describe('Create Map Test', () => {
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

// Test suite for Saving a Map
describe('Save Map Test', () => {
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

    
    it('should save a map', async () => {
       

        console.log(`Attempting to save map with ID: ${mapID} for user: ${userID}`);
        const response = await request
            .post(`/api/${mapID}/savemap`)
            .expect(201);

        expect(response.body).to.be.an('object');
      
        expect(response.body.userID).to.equal(String(userID));
        expect(response.body._id).to.equal(String(mapID));
        expect(response.body.mapName).to.equal("testMapName2");
    });



    after(async () => {
        await User.findByIdAndDelete(userID);
    });
});

// Test suite for Retrieving Maps
describe('Retrieve Maps Test', () => {
    let userID;
    let maps = [];

    before(async () => {
        const user = new User({
            name: 'Test User 3',
            email: 'testuser3@example.com',
            password: 'testpassword123',
        });
        const savedUser = await user.save();
        userID = savedUser._id;
 
        const mapData = [
            {
                userID: userID,
                mapData: "testMapData1",
                mapName: "testMapName1"
            },
            {
                userID: userID,
                mapData: "testMapData2",
                mapName: "testMapName2"
            },
        ];
        for (const data of mapData) {
            const map = new Map(data);
            const savedMap = await map.save();
            maps.push(savedMap);
        }
    });

    it('should retrieve all maps for a user', async () => {
        const response = await request
            .get(`/api/${userID}/allmap`) 
            .expect(200); 

        expect(response.body).to.be.an('object');
        expect(response.body.maps).to.be.an('array').that.has.lengthOf(maps.length);


        response.body.maps.forEach((map, index) => {
            expect(map).to.include({
                _id: maps[index]._id.toString(),
                userID: userID.toString(),
                mapName: maps[index].mapName,
                
            });
        });
    });

    after(async () => {
        await User.findByIdAndDelete(userID);
        await Map.deleteMany({ userID });
    });
});

// Test suite for Loading a Map
describe('Load Map Test', () => {
    let userID;
    let mapID;
    let mapData;
    

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
        mapData = newMap.mapData;
    });

    
    it('should load a map', async () => {
        const response = await request
            .get(`/api/${mapID}/map`)
            .expect(200);

        Map.findById(mapID)
        expect(response.body).to.be.an('object');
      
        expect(response.body.mapData).to.equal(mapData);
    });



    after(async () => {
        await User.findByIdAndDelete(userID);
    });
});